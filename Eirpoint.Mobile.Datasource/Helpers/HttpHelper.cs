using Eirpoint.Mobile.Datasource.Api;
using Eirpoint.Mobile.Datasource.Models;
using Newtonsoft.Json;
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
        public async Task<List<T>> Synchronize<T>(HttpClient httpClient, Action<int> onProgressCallback = null) where T : class
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

            //add new range
            string headerValue = "items=" + start + "-" + end;
            headers.Add("Range", headerValue);
        }


    }
}
