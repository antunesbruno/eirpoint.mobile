using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Eirpoint.Mobile.Datasource.Api
{
    public static class Endpoints
    {
        //Eirpoint FrankFahy - Development
        public static string BaseEirpointUser => "FrankFahy";
        public static string BaseEirpointPass => "fRa$kf!hy";
        public static string BaseEirpointUrl => "http://109.169.66.175:8080/apiFrankFahy/clients/FrankFahy";

        public static HttpClient BaseEirpointHttpClient(string milestoneEndPoint = null, KeyValuePair<string,string> requestHeader = new KeyValuePair<string,string>())
        {
            var authData = string.Format("{0}:{1}", BaseEirpointUser, BaseEirpointPass);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
        
            var httpClient = new HttpClient();
            httpClient.BaseAddress = string.IsNullOrEmpty(milestoneEndPoint) ? new Uri(BaseEirpointUrl) : new Uri(BaseEirpointUrl + milestoneEndPoint);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            return httpClient;
        }
    }
}
