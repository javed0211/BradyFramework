using BradyFramework.PageObjects;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BradyFramework.APITesting.UsingRestSharp
{
    public class RestSharpAPISupport
    {
        #region Post API

        public RestResponse PostAPI(string serviceURI, string accesstoken, string urlSuffix, object data)
        {
            using (WebClient wc = new WebClient())
            {
                var restClient = new RestClient(serviceURI);
                var restRequest = new RestRequest(urlSuffix, Method.Post);
                restRequest.AddHeader(HttpRequestHeader.Authorization.ToString(), "Bearer " + accesstoken);
                restRequest.AddParameter("application/json", data, ParameterType.RequestBody);
                var response = restClient.Execute(restRequest);
                return response;
            }
        }
        public RestResponse PostAPI(string serviceURI, string urlSuffix, object data)
        {
            using (WebClient wc = new WebClient())
            {
                var restClient = new RestClient(serviceURI);
                var restRequest = new RestRequest(urlSuffix, Method.Post);
                restRequest.AddParameter("application/json", data, ParameterType.RequestBody);
                restRequest.AddHeader("Content-Type", "application/json");
                var response = restClient.Execute(restRequest);
                return response;
            }
        }

        #endregion

        #region Get API

        public RestResponse GetAPI(string serviceURI, string urlSuffix)
        {
            using (WebClient wc = new WebClient())
            {
                var restClient = new RestClient(serviceURI);
                var restRequest = new RestRequest(urlSuffix);
                restRequest.Method = Method.Get;
                var response = restClient.Execute(restRequest);
                return response;
            }
        }

        public RestResponse GetAPI(string serviceURI, string accesstoken, string urlSuffix)
        {
            using (WebClient wc = new WebClient())
            {
                var restClient = new RestClient(serviceURI);
                var restRequest = new RestRequest(urlSuffix, Method.Get);
                restRequest.AddHeader(HttpRequestHeader.Authorization.ToString(), "Bearer " + accesstoken);
                //restRequest.AddParameter("application/json", ParameterType.RequestBody);
                var response = restClient.Execute(restRequest);
                return response;
            }
        }
        #endregion
    }
}
