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

        //general endpoints
        public const string CUSTOMERS = "/customers";
        public const string DEPARTMENT = "/departments";
        public const string DISCOUNT = "/discounts";
        public const string PAYMENT_CATEGORY = "/paymentcategories";
        public const string GROUP_LIST = "/groups";
        public const string POS_STATION = "/posstations";
        public const string PRODUCT = "/products";
        public const string PRODUCT_BARCODE = "/productbarcodes";
        public const string PROMOTIONS = "/promotions";
        public const string REASON = "/reasons";
        public const string RECEIPT = "/receipttemplates";
        public const string STOCKING_ATTRIBUTE_TYPE = "/stockingattributetypes";
        public const string STOCK_LOCATION = "/stocklocations";
        public const string USER_LIST = "/users";


        public static HttpClient BaseEirpointHttpClient(string milestoneEndPoint = null, KeyValuePair<string, string> requestHeader = new KeyValuePair<string, string>())
        {
            var authData = string.Format("{0}:{1}", BaseEirpointUser, BaseEirpointPass);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

            var _httpClient = new HttpClient();
            _httpClient.BaseAddress = string.IsNullOrEmpty(milestoneEndPoint) ? new Uri(BaseEirpointUrl) : new Uri(BaseEirpointUrl + milestoneEndPoint);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            return _httpClient;
        }
    }
}
