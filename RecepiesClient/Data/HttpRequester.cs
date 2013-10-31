namespace RecepiesClient.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public class HttpRequester
    {
        public async static Task<T> GetAsync<T>(string resourceUrl, IDictionary<string, string> headers = null)
        {
            var request = WebRequest.Create(resourceUrl) as HttpWebRequest;
            request.ContentType = "application/json";
            request.Method = "GET";

            AddHeadersIfExists(headers, request);

            var response = await request.GetResponseAsync();
            string responseString;

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                responseString = reader.ReadToEnd();
            }

            var responseData = JsonConvert.DeserializeObject<T>(responseString);

            return responseData;
        }

        public static void Get(string resourceUrl)
        {
            var request = WebRequest.Create(resourceUrl) as HttpWebRequest;
            request.ContentType = "application/json";
            request.Method = "GET";
            request.GetResponseAsync();
        }

        public async static Task<string> PostAsync(string resourceUrl, object data,
            IDictionary<string, string> headers = null)
        {
            var request = WebRequest.Create(resourceUrl) as HttpWebRequest;
            request.ContentType = "application/json";
            request.Method = "POST";

            AddHeadersIfExists(headers, request);

            using (StreamWriter writer = new StreamWriter(await request.GetRequestStreamAsync()))
            {
                writer.Write(data);
            }

            var response = await request.GetResponseAsync();
            string responseString;

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                responseString = await reader.ReadToEndAsync();
            }

            return responseString;
        }

        public async static Task<T> PostAsync<T>(string resourceUrl, object data, IDictionary<string, string> headers = null)
        {
            var request = WebRequest.Create(resourceUrl) as HttpWebRequest;
            request.ContentType = "application/json";
            request.Method = "POST";

            AddHeadersIfExists(headers, request);

            var jsonData = JsonConvert.SerializeObject(data);

            using (StreamWriter writer = new StreamWriter(await request.GetRequestStreamAsync()))
            {
                writer.Write(jsonData);
            }

            var response = await request.GetResponseAsync();
            string responseString;

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                responseString = reader.ReadToEnd();
            }

            var responseData = JsonConvert.DeserializeObject<T>(responseString);

            return responseData;
        }

        public async static void Delete(string resourceUrl)
        {
            var request = WebRequest.Create(resourceUrl) as HttpWebRequest;
            request.ContentType = "application/json";
            request.Method = "DELETE";
            request.GetResponseAsync();
        }

        public async static Task<bool> Put(string resourceUrl,
            IDictionary<string, string> headers = null)
        {
            var request = WebRequest.Create(resourceUrl) as HttpWebRequest;
            request.ContentType = "application/json";
            request.Method = "PUT";

            AddHeadersIfExists(headers, request);

            //var jsonData = JsonConvert.SerializeObject(data);

            using (StreamWriter writer =
                new StreamWriter(await request.GetRequestStreamAsync()))
            {
                writer.Write("");
            }

            try
            {
                var response = await request.GetResponseAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private static void AddHeadersIfExists(IDictionary<string, string> headers, HttpWebRequest request)
        {
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers[header.Key] = header.Value;//.Add(header.Key, header.Value);
                }
            }
        }
    }
}