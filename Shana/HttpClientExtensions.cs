using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace System.Net.Http
{
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> PostAsFormEncoded<T>(this HttpClient client, Uri uri, T value)
        {
            return await client.PostAsync(uri, value.ToFormUrlEncodedContent());
        }

        public static async Task<HttpResponseMessage> PostAsFormEncoded<T>(this HttpClient client, string url, T value)
        {
            return await client.PostAsync(url, value.ToFormUrlEncodedContent());
        }

        public static FormUrlEncodedContent ToFormUrlEncodedContent<T>(this T value)
        {
            return new FormUrlEncodedContent(value.ToFormEncoded());
        }

        private static IEnumerable<KeyValuePair<string, string>> ToFormEncoded<T>(this T value)
        {
            foreach (var property in value.GetType().GetProperties())
            {
                var propertyValue = property.GetValue(value)?.ToString();
                var propertyName = property.Name;
                yield return new KeyValuePair<string, string>(propertyName, propertyValue);
            }
        }
    }
}