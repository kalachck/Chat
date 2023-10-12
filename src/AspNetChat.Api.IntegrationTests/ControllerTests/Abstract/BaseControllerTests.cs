using AspNetChat.Api.IntegrationTests.ApplicationConfiguration;
using AspNetChat.DataAccess.Context;
using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using AspNetChat.Api.IntegrationTests.Constants;

namespace AspNetChat.Api.IntegrationTests.ControllerTests.Abstract
{
    public abstract class BaseControllerTests
    {
        protected TestWebApplicationFactory _factory;
        protected HttpClient _httpClient;
        protected TestDataManager _dataManager;
        protected Fixture _fixture;

        protected BaseControllerTests()
        {
            _factory = new TestWebApplicationFactory();
            _httpClient = _factory.CreateClient();
            _fixture = new Fixture();
            _dataManager = new TestDataManager(_factory.Services.CreateScope().ServiceProvider
                .GetRequiredService<DatabaseContext>(), _fixture);
        }

        protected async Task<HttpResponseMessage> ExecuteWithStatusCodeAsync(string requestUri,
            HttpMethod httpMethod, StringContent requestBody = null)
        {
            var requestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(requestUri, UriKind.Relative),
                Method = httpMethod,
                Content = requestBody,
            };

            var response = await _httpClient.SendAsync(requestMessage);

            return response;
        }

        protected async Task<(TReturn, HttpResponseMessage)> ExecuteWithFullResponseAsync<TReturn>(string requestUri,
            HttpMethod httpMethod, StringContent requestBody = null)
        {
            var requestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(requestUri, UriKind.Relative),
                Method = httpMethod,
                Content = requestBody,
            };

            var response = await _httpClient.SendAsync(requestMessage);

            var dtoResult = JsonConvert.DeserializeObject<TReturn>(await response.Content.ReadAsStringAsync());

            return (dtoResult, response);
        }

        protected static StringContent BuildRequestBody<TRequestModel>(TRequestModel requestModel)
        {
            var requestBody = new StringContent(JsonConvert.SerializeObject(requestModel));

            requestBody.Headers.ContentType = new MediaTypeHeaderValue(ApiConstants.ContentType);

            return requestBody;
        }
    }
}
