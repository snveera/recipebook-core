using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace recipebook.functions.test.TestUtility.Extensions
{
    public static class HttpExtensions
    {
        public static HttpRequest GetRequest(this TestCompositionRoot root)
        {
            var request = new DefaultHttpRequest(new DefaultHttpContext()) { Method = "GET" };
            return request;
        }

        public static HttpRequest DeleteRequest(this TestCompositionRoot root)
        {
            var request = new DefaultHttpRequest(new DefaultHttpContext()) { Method = "DELETE" };
            return request;
        }

        public static HttpRequest PostRequest<T>(this TestCompositionRoot root, T body)
        {
            var request = new DefaultHttpRequest(new DefaultHttpContext()) { Method = "POST" };
            var bodyAsJson = JsonConvert.SerializeObject(body);
            request.Body = bodyAsJson.ToStream();

            return request;
        }

        public static HttpRequest PutRequest<T>(this TestCompositionRoot root, T body)
        {
            var request = new DefaultHttpRequest(new DefaultHttpContext()) { Method = "PUT" };
            var bodyAsJson = JsonConvert.SerializeObject(body);
            request.Body = bodyAsJson.ToStream();

            return request;
        }

        public static HttpRequest WithSearchCriteriaParameter(this HttpRequest request, string value)
        {
            request.WithQueryStringParameter("criteria", value);
            return request;
        }
        public static HttpRequest WithCategoryParameter(this HttpRequest request, string value)
        {
            request.WithQueryStringParameter("category", value);
            return request;
        }

        public static HttpRequest WithQueryStringParameter(this HttpRequest request, string name, string value)
        {
            request.QueryString = request.QueryString.Add(name, value);
            return request;
        }
    }
}
