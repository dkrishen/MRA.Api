using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace BackendApi.Repository
{
    public class RepositoryBase
    {
        private string apiUrl;

        public RepositoryBase(string apiUrl)
        {
            this.apiUrl = apiUrl;
        }

        private string makeRequest(HttpWebRequest request)
        {
            var result = "";
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var stream = response.GetResponseStream();
                if (stream != null)
                {
                    result = new StreamReader(stream).ReadToEnd();
                }
            }
            catch (Exception) { }
            return result;
        }

        protected string Request(string requestUrl, string requestMethod, object data = null)
        {
            string content = data != null ? JsonConvert.SerializeObject(data) : string.Empty;
            var getRequest = (HttpWebRequest)WebRequest.Create(apiUrl + requestUrl + content);
            getRequest.Method = requestMethod;

            return makeRequest(getRequest);
        }
    }
}
