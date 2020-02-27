using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaosPerformanceAPI.Common
{
    public interface IResponseFormatter
    {
        JObject GetResponse();
        void Add(string key, IEnumerable<IValidatableObject> viewModelList, string message = null);
        void Add(string key, IValidatableObject viewModel, string message = null);
        void Add(string key, object value, string message = null);
        void SetError(string message);
        void SetError(List<string> messages);
        void SetError(List<ValidationFailure> errorList);
        void SetError(Exception ex);
        void SetMessage(string message);
        void SetMessages(List<string> messages);
    }

    public class ResponseFormatter : IResponseFormatter
    {
        private dynamic _jObject;
        private dynamic _status;
        private List<string> _messages;
        private short _isError;

        public ResponseFormatter()
        {
            _jObject = new JObject();
            _status = new JObject();
            _messages = new List<string>();
            _isError = 0;
            SetResponse();
        }

        public void SetError(string message)
        {
            SetMessage(message);
            _isError = 1;
        }

        public void SetError(List<string> messages)
        {
            SetMessages(messages);
            _isError = 1;
        }

        public void SetError(List<ValidationFailure> errorList)
        {
            _messages.Clear();
            foreach (var error in errorList)
            {
                _messages.Add(error.ErrorMessage);
            }
            _isError = 1;
        }

        public void SetError(Exception ex)
        {
            _messages.Clear();
            if (ex.InnerException != null)
            {
                GetErrorFromException(ex.InnerException);
            }
            else
                _messages.Add(ex.Message);
            _isError = 1;
        }

        private void GetErrorFromException(Exception ex)
        {
            _messages.Add(ex.Message);
            if (ex.InnerException != null)
                GetErrorFromException(ex.InnerException);
        }

        public JObject GetResponse()
        {
            SetResponse();
            return _jObject;
        }

        public void Add(string key, IEnumerable<IValidatableObject> viewModelList, string message = null)
        {
            JArray jArray = new JArray();
            foreach (var viewModel in viewModelList)
            {
                jArray.Add(JObject.FromObject(viewModel));
            }
            _jObject[key] = jArray;

            if (!string.IsNullOrEmpty(message))
            {
                _messages.Clear();
                _messages.Add(message);
            }

        }

        public void Add(string key, IValidatableObject viewModel, string message = null)
        {
            _jObject[key] = JObject.FromObject(viewModel);

            if (!string.IsNullOrEmpty(message))
            {
                _messages.Clear();
                _messages.Add(message);
            }
        }

        public void Add(string key, object value, string message = null)
        {
            JToken jobj = null;
            if (value != null)
                jobj = JToken.FromObject(value);

            _jObject[key] = jobj;

            if (!string.IsNullOrEmpty(message))
            {
                _messages.Clear();
                _messages.Add(message);
            }
        }

        private void SetResponse()
        {
            JArray jArray = new JArray
            {
                _messages
            };
            _status.messages = jArray;
            _status.isError = _isError;
            _jObject.status = _status;
        }

        public void SetMessage(string message)
        {
            _messages.Clear();
            _messages.Add(message);
        }

        public void SetMessages(List<string> messages)
        {
            _messages.Clear();
            _messages.AddRange(messages);
        }

        public ObjectResult GetObjectResult(JObject result, Exception ex = null)
        {
            if (ex == null)
                return new OkObjectResult(result);
            else
            {
                switch (ex)
                {
                    default:
                        return new BadRequestObjectResult(result);
                }
            }
        }
    }
}
