namespace Core.Tests.Util {
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _fakeResponse;

        public MockHttpMessageHandler(HttpResponseMessage response)
        {
            _fakeResponse = response;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_fakeResponse);
        }
    }
}