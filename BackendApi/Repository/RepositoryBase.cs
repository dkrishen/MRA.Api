using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BackendApi.Repository
{
    public class RepositoryBase
    {
        private string apiUrl;
        //private HttpWebRequest getRequest;
        //private HttpWebRequest postRequest;

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
                if(stream != null)
                {
                    result = new StreamReader(stream).ReadToEnd();
                }
            }
            catch (Exception) { }
            return result;
        }

        protected string GetRequest(string requestUrl, object data = null)
        {
            string content = data != null ? JsonConvert.SerializeObject(data) : string.Empty;
            var getRequest = (HttpWebRequest)WebRequest.Create(apiUrl + requestUrl + content);
            getRequest.Method = "GET";

            return makeRequest(getRequest);
        }

        //protected string GetRequest(string requestUrl, object content)
        //{
        //    var getRequest = (HttpWebRequest)WebRequest.Create(apiUrl + requestUrl);
        //    getRequest.Method = "GET";

        //    return makeRequest(getRequest);
        //}

        protected string PostRequest(string requestUrl)
        {
            var postRequest = (HttpWebRequest)WebRequest.Create(apiUrl + requestUrl);
            postRequest.Method = "POST";

            return makeRequest(postRequest);
        }

    }
}
