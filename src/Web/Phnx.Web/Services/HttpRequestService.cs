﻿using Phnx.Web.Fluent;
using System.Net.Http;
using System.Threading.Tasks;

namespace Phnx.Web.Services
{
    /// <summary>
    /// The api request engine that manages sending web requests
    /// </summary>
    public class HttpRequestService : IHttpRequestService
    {
        /// <summary>
        /// Create a new <see cref="HttpRequestService"/> with a new, default <see cref="System.Net.Http.HttpClient"/>
        /// </summary>
        public HttpRequestService()
        {
            HttpClient = new HttpClient();
        }

        /// <summary>
        /// Create a new <see cref="HttpRequestService"/> with a given <see cref="System.Net.Http.HttpClient"/>
        /// </summary>
        /// <param name="httpClient"></param>
        public HttpRequestService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        /// <summary>
        /// The <see cref="System.Net.Http.HttpClient"/> responsible for sending requests
        /// </summary>
        /// <remarks>
        /// One instance of HttpClient per application (<see href="https://docs.microsoft.com/en-us/azure/architecture/antipatterns/improper-instantiation/#problem-description"/>)
        /// </remarks>
        public HttpClient HttpClient { get; }

        /// <summary>
        /// Create a new fluent HTTP request
        /// </summary>
        /// <returns>A new fluent HTTP request</returns>
        public FluentRequest CreateRequest()
        {
            return new FluentRequest(this);
        }

        /// <summary>
        /// Send the <see cref="HttpRequestMessage"/> over HTTP
        /// </summary>
        /// <param name="request">The request to send</param>
        /// <returns>The response from the foreign URL</returns>
        /// <remarks>This method will complete when only the response headers have been completely downloaded. Using Content.GetBodyAsStringAsync() will wait until the content body is completely loaded</remarks>
        public virtual Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return HttpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
        }

        /// <summary>
        /// Send the <see cref="HttpRequestMessage"/> over HTTP, with a non-default <see cref="HttpClient"/>
        /// </summary>
        /// <param name="request">The request to send</param>
        /// <param name="httpClient">The HTTP Client to use for the request</param>
        /// <returns>The response from the foreign URL</returns>
        /// <remarks>This method will complete when only the response headers have been completely downloaded. Using Content.GetBodyAsStringAsync() will wait until the content body is completely loaded</remarks>
        public virtual Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpClient httpClient)
        {
            return httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
        }
    }
}
