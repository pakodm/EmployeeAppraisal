using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Newtonsoft.Json.Linq;

using TaosPerformanceAPI.DAL;
using static TaosPerformanceAPI.Resources.Resources;

namespace TaosPerformanceAPI.Common
{
    public class Utils
    {
        public Utils() { }

        public static T CopyPropertiesTo<T>(object source, T dest)
        {
            var sourceProps = typeof(T).GetProperties().Where(x => x.CanRead).ToList();
            var destProps = typeof(T).GetProperties().Where(x => x.CanWrite).ToList();

            foreach (var sourceProp in sourceProps)
            {
                if (destProps.Any(x => x.Name == sourceProp.Name))
                {
                    var p = destProps.First(x => x.Name == sourceProp.Name);
                    p.SetValue(dest, sourceProp.GetValue(source, null), null);
                }
            }

            return dest;
        }

        public static ICollection<U> GetEntityList<T, U>(MySQLRepository repository, Func<T, object> orderByExpression = null,
            List<Expression<Func<T, bool>>> whereExpressions = null, bool asNoTrack = false, params Expression<Func<T, object>>[] includeProperties) where T : class, new()
        {
            // var ph = GetPaginationModel(pagination, repository.Count<T>());
            // response.AddPagination(ph.CurrentPage, ph.ItemsPerPage, ph.TotalItems, ph.TotalPages);
            var entityList = repository.GetAllWhere(whereExpressions, asNoTrack, includeProperties).ToList();
            if (orderByExpression != null)
            {
                entityList = entityList.OrderBy(orderByExpression).ToList();
            }

            return Mapper.Map<ICollection<T>, ICollection<U>>(entityList);
        }

        public static string DecodeFromBase64(string textInBase64)
        {
            var base64EncodedBytes = Convert.FromBase64String(textInBase64);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string EncodeToBase64(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string[] GetPropertiesFromJObject(JObject jsonData, string baseNodeName)
        {
            List<string> result = new List<string>();
            var values = jsonData.GetValue(baseNodeName);
            if (values != null && values.HasValues)
            {
                foreach (KeyValuePair<string, JToken> obj in (JObject)values)
                {
                    result.Add(obj.Key);
                }
            }
            else
            {
                return null;
            }

            return result.ToArray();
        }

        public static T EditPropertiesFromJObject<T>(object entity, JObject jsonData, string viewModelName)
        {
            var obj = jsonData[viewModelName];
            entity = new Utils().SetPropertiesToEntity(entity, obj);
            return (T)entity;
        }

        private object SetPropertiesToEntity(object entity, JToken obj)
        {
            if (obj.Type == JTokenType.Object)
            {
                foreach (JProperty child in obj.Children<JProperty>())
                {
                    PropertyInfo property = entity.GetType().GetProperty(child.Name);

                    if (property != null)
                    {
                        if (child.Value.HasValues)
                        {
                            var propertyValue = property.GetValue(entity);
                            if (propertyValue == null)
                            {
                                propertyValue = TryCreateInstance(property.PropertyType);
                            }
                            var value = SetPropertiesToEntity(propertyValue, child.Value);
                            SetGenericValue(property, entity, value);
                        }
                        else
                        {
                            SetGenericValue(property, entity, child.Value.ToObject(property.PropertyType));
                        }
                    }
                }
            }

            return entity;
        }

        private void SetGenericValue(PropertyInfo property, object entity, object value)
        {
            Type pType = property.PropertyType;
            if (pType.GetTypeInfo().IsGenericType && pType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                //if it's null, just set the value from the reserved word null, and return
                if (value == null)
                {
                    property.SetValue(entity, null, null);
                }
                else
                {
                    //Get the underlying type property instead of the nullable generic
                    pType = new NullableConverter(property.PropertyType).UnderlyingType;
                }
            }

            try
            {
                //use the converter to get the correct value
                property.SetValue(entity, Convert.ChangeType(value, pType), null);
            }
            catch (InvalidCastException ex)
            {
                var message = ex.Message;
                //TODO: Insertar excepcion en log
            }
        }

        public static object TryCreateInstance(Type type)
        {
            try
            {
                return Activator.CreateInstance(type);
            }
            catch
            {
                return null;
            }
        }

        public static U GetEntityById<T, U>(MySQLRepository repository, string viewModelName, Expression<Func<T, bool>> singlePredicate, string notFoundEntityMessage = null,
            params Expression<Func<T, object>>[] includeProperties) where T : class, new()
        {
            var entity = repository.GetSingle(singlePredicate, includeProperties);
            if (entity == null)
            {
                throw new Exception(string.IsNullOrEmpty(notFoundEntityMessage) ?
                    string.Format(ENTITY_NOT_FOUND, viewModelName)
                    : notFoundEntityMessage);
            }

            return Mapper.Map<T, U>(entity);
        }

        public static DateTime? ToUniversalTime(DateTime? date)
        {
            if (date.HasValue)
                return date.Value.ToUniversalTime();

            return null;
        }

        public static DateTime? ToCentralStandardTime(DateTime? utc)
        {
            if (utc.HasValue)
                return TimeZoneInfo.ConvertTimeFromUtc(utc.Value, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"));

            return null;
        }

        public static DateTime GetDate()
        {
            return DateTime.UtcNow;
        }
    }
}
