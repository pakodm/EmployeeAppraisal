using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TaosPerformanceAPI.Common
{

    public class ViewModelValidationResult<T> where T : class
    {
        public ObjectResult Result { get; set; } = null;
        public T ViewModel { get; set; } = null;
        public List<T> ViewModelList { get; set; } = null;
    }

    public class JsonParserResult<T>
    {
        public T Result { get; set; }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
    }

    public interface IRequestHandler
    {
        JsonParserResult<T> GetViewModel<T>(string objectName, JObject jObject) where T : class;
        JsonParserResult<dynamic> GetSingleDictionaryResult(JObject jsonData, string jsonKeyName);
        T GetNodeValue<T>(string nodeName, JObject jObject);
        ViewModelValidationResult<T> ViewModelValidation<T>(JObject jsonData, string viewModelName, AbstractValidator<T> validator, string[] properties = null) where T : class;
        ViewModelValidationResult<T> ViewModelValidation<T>(T viewModel, AbstractValidator<T> validator) where T : class;
        ViewModelValidationResult<T> ArrayViewModelValidator<T>(JObject jsonData, string viewModelName, AbstractValidator<T> validator) where T : class;
    }

    public class RequestHandler : IRequestHandler
    {
        private readonly ILogger _logger;
        private readonly IResponseFormatter _responseFormatter;

        public RequestHandler(ILogger<RequestHandler> logger, IResponseFormatter responseFormatter)
        {
            _logger = logger;
            _responseFormatter = responseFormatter;
        }

        public JsonParserResult<T> GetViewModel<T>(string objectName, JObject jObject) where T : class
        {
            JsonParserResult<T> response = new JsonParserResult<T>();
            try
            {
                var viewModel = jObject[objectName]?.ToObject<T>();
                response.IsError = viewModel == null ? true : false;
                response.ErrorMessage = viewModel == null ?
                    string.Format("Se esperaba un objeto con nombre {0}", objectName) : null;
                response.Result = viewModel;
            }
            catch (Newtonsoft.Json.JsonReaderException e)
            {

                response.Result = null;
                response.IsError = true;
                response.ErrorMessage = string.Format("Error en la propiedad {0}. {1}", e.Path, e.Message);
            }
            catch (InvalidCastException e)
            {
                _logger.LogError(e.Message);
                response.Result = null;
                response.IsError = true;
                response.ErrorMessage = string.Format("Una o mas propiedades no son compatibles. {0}", e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                response.Result = null;
                response.IsError = true;
                response.ErrorMessage = e.Message;
            }

            return response;
        }

        public T GetNodeValue<T>(string nodeName, JObject jObject)
        {
            T nodeValue;
            try
            {
                nodeValue = jObject[nodeName].ToObject<T>();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                nodeValue = default(T);
            }
            return nodeValue;
        }

        public JsonParserResult<dynamic> GetSingleDictionaryResult(JObject jsonData, string jsonKeyName)
        {
            JsonParserResult<dynamic> response = new JsonParserResult<dynamic>();
            try
            {
                var dynamicResult = jsonData[jsonKeyName]?.ToObject<Dictionary<string, object>>();
                response.IsError = dynamicResult == null ? true : false;
                response.Result = dynamicResult;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                response.Result = null;
                response.IsError = true;
                response.ErrorMessage = e.Message;
            }

            return response;
        }

        public ViewModelValidationResult<T> ViewModelValidation<T>(T viewModel, AbstractValidator<T> validator) where T : class
        {
            var results = new ViewModelValidationResult<T>();
            ValidationResult result = new ValidationResult();

            result = validator.Validate(viewModel);

            if (!result.IsValid)
            {
                return (new ViewModelValidationResult<T>()
                {
                    Result = new ObjectResult(result.Errors.ToList()),
                    ViewModel = viewModel
                });
            }

            return (new ViewModelValidationResult<T>()
            {
                ViewModel = viewModel
            });

        }

        public ViewModelValidationResult<T> ViewModelValidation<T>(JObject jsonData, string viewModelName, AbstractValidator<T> validator, string[] properties = null) where T : class
        {
            var parser = GetViewModel<T>(viewModelName, jsonData);
            if (parser.IsError)
            {
                _responseFormatter.SetError(parser.ErrorMessage);
                return new ViewModelValidationResult<T>()
                {
                    Result = new BadRequestObjectResult(_responseFormatter.GetResponse())
                };
            }

            ValidationResult result = new ValidationResult();

            if (properties != null && properties.Count() > 0)
            {
                var context = new ValidationContext<T>(parser.Result, new PropertyChain(), new MemberNameValidatorSelector(properties));

                result = validator.Validate(context);
            }
            else
                result = validator.Validate(parser.Result);

            if (!result.IsValid)
            {
                _responseFormatter.SetError(result.Errors.ToList());
                return new ViewModelValidationResult<T>()
                {
                    Result = new BadRequestObjectResult(_responseFormatter.GetResponse())
                };
            }

            return new ViewModelValidationResult<T>()
            {
                ViewModel = parser.Result
            };
        }

        public ViewModelValidationResult<T> ArrayViewModelValidator<T>(JObject jsonData, string viewModelName, AbstractValidator<T> validator) where T : class
        {
            var viewModelList = new List<T>();

            var parser = GetViewModel<List<T>>(viewModelName, jsonData);
            if (parser.IsError)
            {
                _responseFormatter.SetError(parser.ErrorMessage);

                return new ViewModelValidationResult<T>()
                {
                    Result = new BadRequestObjectResult(_responseFormatter.GetResponse())
                };
            }

            ValidationResult result = new ValidationResult();

            foreach (var v in parser.Result)
            {
                result = validator.Validate(v);

                if (!result.IsValid)
                {
                    _responseFormatter.SetError(result.Errors.ToList());
                    return new ViewModelValidationResult<T>()
                    {
                        Result = new BadRequestObjectResult(_responseFormatter.GetResponse())
                    };
                }
            }

            return new ViewModelValidationResult<T>()
            {
                ViewModelList = parser.Result
            };
        }
    }
}
