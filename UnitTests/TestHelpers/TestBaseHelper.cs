using System.Net.Http;

using Game;
using Game.Services;

namespace UnitTests.TestHelpers
{
    /// <summary>
    /// Test Helper for All test to use
    /// </summary>
    public static class TestBaseHelper
    {
        /// <summary>
        /// The global app
        /// </summary>
        static public App app;

        #region HttpService
        
        /// <summary>
        /// Http service
        /// </summary>
        static public HttpClientService HttpService;

        /// <summary>
        /// Http client for real calls
        /// </summary>
        static public HttpClient RealHttpClient;

        /// <summary>
        /// Http mock handler
        /// </summary>
        static public HttpClient MockHttpClient;
        
        #endregion HttpService

        static TestBaseHelper()
        {
            HttpService = HttpClientService.Instance;
            RealHttpClient = HttpClientService.Instance.GetHttpClient();
            MockHttpClient = new HttpClient(new MockHttpMessageHandler());
        }

        /// <summary>
        /// Set to use the Moc Http Client
        /// </summary>
        /// <returns></returns>
        public static bool SetHttpClientToMock()
        {
            _ = HttpClientService.Instance.SetHttpClient(MockHttpClient);
            return true;
        }

        /// <summary>
        /// Set to use the Moc Http Client
        /// </summary>
        /// <returns></returns>
        public static bool SetHttpClientToReal()
        {
            _ = HttpClientService.Instance.SetHttpClient(RealHttpClient);
            ResponseMessage.ResetResponseMessageStringContent();
            ResponseMessage.ResetHttpStatusCode();
            return true;
        }
    }
}