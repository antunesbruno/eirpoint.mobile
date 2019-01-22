using Eirpoint.Mobile.Datasource.Api;
using Eirpoint.Mobile.Datasource.Models;
using Eirpoint.Mobile.Datasource.Repository.Base;
using Eirpoint.Mobile.Datasource.Repository.Entity;
using Eirpoint.Mobile.Shared.NativeInterfaces;
using Newtonsoft.Json;
using Platform.Ioc.Injection;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Eirpoint.Mobile.Datasource.Helpers
{
    public class HttpHelper
    {
        // Ranging parameters.
        int totalItems = 0;
        int maxItems = 0;
        int percentageComplete = 0;
        int headerStart = 0;
        int headerEnd = 0;
        int connectionTimes = 0;
        bool isCompleted = false;

        private bool IsConnected { get { return Injector.Resolver<IConnectivity>().IsConnected(); } }

        public async Task Synchronize<T>(HttpClient httpClient, Action<int> onProgressCallback = null) where T : class
        {
            // Request the resource.
            try
            {
                //headers
                Dictionary<string, string> headers = new Dictionary<string, string>();

                //response http
                HttpResponseMessage httpResponse;

                //is completed all requests
                isCompleted = false;

                //redirect
                bool redirected = false;

                //if has internet connection
                if (IsConnected)
                {
                    // Loop until the whole query is fulfilled.
                    while (!isCompleted)
                    {
                        //add headers
                        AddDefaultRequestHeader(headers, httpClient);

                        //set the request
                        var httpRequest = RestService.For<IGenericApi<T, string>>(httpClient);

                        //get response
                        httpResponse = await httpRequest.GetAll();

                        // Examine the response to determine success or failure, and if 
                        // further requests are required.                
                        if (httpResponse.StatusCode == HttpStatusCode.OK)
                        {
                            // 200 OK suggests that all the data we requested was returned.
                            DeserializeAndInsertResponse<T>(httpResponse);

                            //set completed
                            isCompleted = true;
                        }
                        else if (httpResponse.StatusCode == HttpStatusCode.NoContent)
                        {
                            // GET requests on collections should actually return an empty array if no items
                            // match the query, but we will handle a 204 No Content in the same way.
                            var deserializedResponse = new List<T>();

                            //set completed
                            isCompleted = true;
                        }
                        else if (httpResponse.StatusCode == HttpStatusCode.RequestEntityTooLarge)
                        {
                            // There is too much data to return at once, and the server is demanding a ranged request.
                            // The total is contained in the entity body, which is an error object of type "RangeTooLargeError".              
                            DeserializeRequestTooLarge(headers, httpResponse);

                            //set completed
                            isCompleted = false;

                        }
                        else if (httpResponse.StatusCode == HttpStatusCode.PartialContent)
                        {
                            // Some items were returned, but not all.
                            // We need to process the items that were returned, and add/update a 
                            // Range header to retrieve more items.                  

                            isCompleted = await DeserializeAndInsertPartialResponse<T>(headers, httpClient, httpResponse);

                            //execute progress callback
                            onProgressCallback?.Invoke(percentageComplete);
                        }
                        else if (httpResponse.StatusCode == HttpStatusCode.MovedPermanently ||
                              httpResponse.StatusCode == HttpStatusCode.Moved |
                             httpResponse.StatusCode == HttpStatusCode.SeeOther |
                             httpResponse.StatusCode == HttpStatusCode.TemporaryRedirect)
                        {
                            // We've been redirected to somewhere else. Update the URL.
                            // Unless we've already been redirected, in which case something is
                            // probably wrong.
                            if (redirected)
                                throw new Exception("Multiple redirects occurred.");

                            string collectionUrl = httpResponse.Content.Headers.GetValues("Location").FirstOrDefault();
                            redirected = true;
                        }
                        else
                        {
                            // Report other HTTP responses back to the caller. 
                            var errorBody = await httpResponse.Content.ReadAsStringAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //if (connectionTimes < 3)
                //{
                //    //add log of partial response inserted                
                //    InsertRangeHeaderLog(typeof(T).ToString());

                //    //wait 30 secondes to get connection again
                //    await Task.Delay(30000);

                //    //increment connection times
                //    connectionTimes++;
                //}
                //else
                //{
                //    //exit cicle
                //    isCompleted = true;

                //    //TODO: treat the error in the view
                //    throw ex;
                //}
            }
        }


        /// <summary>
        /// Get Data and return a list of T elements
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="onProgressCallback"></param>
        /// <returns></returns>
        public async Task<List<T>> SynchronizeAndGetList<T>(HttpClient httpClient, Action<int> onProgressCallback = null) where T : class
        {
            //response of request
            List<T> deserializedResponse = new List<T>();

            //headers
            Dictionary<string, string> headers = new Dictionary<string, string>();

            //response http
            HttpResponseMessage httpResponse;

            // Ranging parameters.
            int totalItems = 0;
            int maxItems = 0;

            //is completed all requests
            bool isCompleted = false;

            //redirect
            bool redirected = false;

            // Loop until the whole query is fulfilled.
            while (!isCompleted)
            {
                //add headers
                foreach (var keyValue in headers)
                    httpClient.DefaultRequestHeaders.Add(keyValue.Key, keyValue.Value);

                //set the request
                var httpRequest = RestService.For<IGenericApi<T, string>>(httpClient);

                // Request the resource.
                try
                {
                    httpResponse = await httpRequest.GetAll();
                }
                catch (Exception e)
                {
                    throw e;
                }

                // Examine the response to determine success or failure, and if 
                // further requests are required.                
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    // 200 OK suggests that all the data we requested was returned.
                    var response = await httpResponse.Content.ReadAsStringAsync();
                    deserializedResponse = await Task.Run(() => JsonConvert.DeserializeObject<List<T>>(response));

                    //set completed
                    isCompleted = true;
                }
                else if (httpResponse.StatusCode == HttpStatusCode.NoContent)
                {
                    // GET requests on collections should actually return an empty array if no items
                    // match the query, but we will handle a 204 No Content in the same way.
                    deserializedResponse = new List<T>();

                    //set completed
                    isCompleted = true;
                }
                else if (httpResponse.StatusCode == HttpStatusCode.RequestEntityTooLarge)
                {
                    // There is too much data to return at once, and the server is demanding a
                    // ranged request.

                    // The total is contained in the entity body, which is an error object
                    // of type "RangeTooLargeError".               

                    try
                    {
                        var response = await httpResponse.Content.ReadAsStringAsync();
                        var errorBody = await Task.Run(() => JsonConvert.DeserializeObject<HttpTooLargeModel>(response));

                        totalItems = errorBody.TotalCount;
                        maxItems = errorBody.MaximumRange;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }

                    SetRangeHeader(headers, 0, totalItems, maxItems);
                }
                else if (httpResponse.StatusCode == HttpStatusCode.PartialContent)
                {
                    // Some items were returned, but not all.
                    // We need to process the items that were returned, and add/update a 
                    // Range header to retrieve more items.                  
                    try
                    {
                        var response = await httpResponse.Content.ReadAsStringAsync();
                        var partialResponse = await Task.Run(() => JsonConvert.DeserializeObject<List<T>>(response));

                        //get range header
                        string rangeHeader = httpResponse.Content.Headers.GetValues("Content-Range").FirstOrDefault();

                        ContentRangeHeaderHelper parsedRangeHeader = new ContentRangeHeaderHelper(rangeHeader);

                        if (parsedRangeHeader.isFinal())
                            isCompleted = true;
                        else
                        {
                            //clean httpclient header range
                            httpClient.DefaultRequestHeaders.Remove("Range");

                            SetRangeHeader(
                                    headers,
                                    parsedRangeHeader.getEnd(),
                                    parsedRangeHeader.getTotal(),
                                    maxItems
                                );
                        }

                        //add partial response
                        deserializedResponse.AddRange(partialResponse);

                        // Record the totalItems if not already set, and report progress.
                        if (totalItems == 0)
                            totalItems = parsedRangeHeader.getTotal();
                        if (totalItems > 0)
                        {
                            //set percentage
                            int percentageComplete = parsedRangeHeader.getEnd() * 100 / totalItems;

                            //execute progress callback
                            onProgressCallback?.Invoke(percentageComplete);
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }

                }
                else if (httpResponse.StatusCode == HttpStatusCode.MovedPermanently ||
                      httpResponse.StatusCode == HttpStatusCode.Moved |
                     httpResponse.StatusCode == HttpStatusCode.SeeOther |
                     httpResponse.StatusCode == HttpStatusCode.TemporaryRedirect)
                {
                    // We've been redirected to somewhere else. Update the URL.
                    // Unless we've already been redirected, in which case something is
                    // probably wrong.
                    if (redirected)
                        throw new Exception("Multiple redirects occurred.");

                    string collectionUrl = httpResponse.Content.Headers.GetValues("Location").FirstOrDefault();
                    redirected = true;
                }
                else
                {
                    // Report other HTTP responses back to the caller.                   
                    try
                    {
                        var errorBody = await httpResponse.Content.ReadAsStringAsync();
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }

            // If we haven't bailed with an error response by now, things must have
            // gone well.
            return deserializedResponse;
        }


        /// <summary>
        /// Configure range header
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="lastRequested"></param>
        /// <param name="contentSize"></param>
        /// <param name="maxItems"></param>
        private void SetRangeHeader(Dictionary<string, string> headers, int lastRequested = 0, int contentSize = 0, int maxItems = 0)
        {
            int start = 0;
            int end;

            if (lastRequested > 0)
            {
                start = lastRequested + 1;
            }
            if (contentSize == 0)
            {
                end = start + maxItems - 1;
            }
            else
            {
                if (start + (maxItems - 1) < contentSize)
                    end = start + maxItems - 1;
                else
                    end = contentSize - 1;
            }

            //clean de last range
            headers.Remove("Range");

            //update values
            headerStart = start;
            headerEnd = end;

            //add new range
            string headerValue = "items=" + start + "-" + end;
            headers.Add("Range", headerValue);
        }


        private void AddDefaultRequestHeader(Dictionary<string, string> headers, HttpClient httpClient)
        {
            foreach (var keyValue in headers)
                httpClient.DefaultRequestHeaders.Add(keyValue.Key, keyValue.Value);
        }

        private async void DeserializeAndInsertResponse<T>(HttpResponseMessage httpResponse) where T : class
        {
            var response = await httpResponse.Content.ReadAsStringAsync();
            var deserializedResponse = await Task.Run(() => JsonConvert.DeserializeObject<List<T>>(response));

            if (deserializedResponse != null)
                await Injector.Resolver<IPersistenceBase<T>>().InsertAll(deserializedResponse);
        }

        private async void DeserializeRequestTooLarge(Dictionary<string, string> headers, HttpResponseMessage httpResponse)
        {
            var response = await httpResponse.Content.ReadAsStringAsync();
            var errorBody = await Task.Run(() => JsonConvert.DeserializeObject<HttpTooLargeModel>(response));

            totalItems = errorBody.TotalCount;
            maxItems = errorBody.MaximumRange;

            SetRangeHeader(headers, 0, totalItems, maxItems);
        }

        private async Task<bool> DeserializeAndInsertPartialResponse<T>(Dictionary<string, string> headers, HttpClient httpClient, HttpResponseMessage httpResponse) where T : class
        {
            var response = await httpResponse.Content.ReadAsStringAsync();
            var partialResponse = await Task.Run(() => JsonConvert.DeserializeObject<List<T>>(response));

            //get range header
            string rangeHeader = httpResponse.Content.Headers.GetValues("Content-Range").FirstOrDefault();
            ContentRangeHeaderHelper parsedRangeHeader = new ContentRangeHeaderHelper(rangeHeader);

            if (parsedRangeHeader.isFinal())
            {
                //iscompleted process
                return true;
            }
            else
            {
                //clean httpclient header range
                httpClient.DefaultRequestHeaders.Remove("Range");

                SetRangeHeader(
                        headers,
                        parsedRangeHeader.getEnd(),
                        parsedRangeHeader.getTotal(),
                        maxItems
                    );
            }

            //add partial response                  
            if (partialResponse != null)
                await Injector.Resolver<IPersistenceBase<T>>().InsertAll(partialResponse);

            // Record the totalItems if not already set, and report progress.
            if (totalItems == 0)
                totalItems = parsedRangeHeader.getTotal();

            //set percentage
            if (totalItems > 0)              
                percentageComplete = parsedRangeHeader.getEnd() * 100 / totalItems;

            return false;
        }

        private async void InsertRangeHeaderLog(string dataItemName)
        {
            var rangeHeader = new RangeHeaderLog() { DataItem = dataItemName, StartRange = headerStart, EndRange = headerEnd, LogTime = DateTime.Now };
            await Injector.Resolver<IPersistenceBase<RangeHeaderLog>>().Insert(rangeHeader);
        }
    }
}
