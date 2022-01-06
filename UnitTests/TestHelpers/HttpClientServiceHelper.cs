using System.Threading;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;

namespace UnitTests.TestHelpers
{
    /// <summary>
    /// The Mock Message Handeler to use for Unit Testing
    /// </summary>
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        /// <summary>
        /// Call for Send Async
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var responseMessage = new HttpResponseMessage(ResponseMessage.HttpStatusCode)
            {
                Content = ResponseMessage.ResponseMessageStringContent
            };

            return await Task.FromResult(responseMessage);
        }
    }

    /// <summary>
    /// The Response to return
    /// </summary>
    public static class ResponseMessage
    {
        /// <summary>
        /// Set the response string to what is passed in
        /// </summary>
        /// <param name="content"></param>
        public static void SetResponseMessageStringContent(StringContent content)
        {
            ResponseMessageStringContent = content;
        }

        /// <summary>
        /// Reset the response string to the default string
        /// </summary>
        public static void ResetResponseMessageStringContent()
        {
            ResponseMessageStringContent = DefaultStringContent;
        }

        // The Types of Status Codes
        public static HttpStatusCode HttpStatusCode = HttpStatusCode.OK;

        // Success
        public static HttpStatusCode HttpStatusCodeSuccess = HttpStatusCode.OK;

        // Bad Request for error generation
        public static HttpStatusCode HttpStatusCodeBadRequest = HttpStatusCode.BadRequest;

        // 404 
        public static HttpStatusCode HttpStatusCodeNotFound = HttpStatusCode.NotFound;

        /// <summary>
        /// Set the Http Status Code to return to be what is passed in
        /// </summary>
        /// <param name="code"></param>
        public static void SetHttpStatusCode(HttpStatusCode code)
        {
            HttpStatusCode = code;
        }

        /// <summary>
        /// When done, reset it to the default
        /// </summary>
        public static void ResetHttpStatusCode()
        {
            HttpStatusCode = HttpStatusCodeSuccess;
        }

        // Response String
        public static StringContent ResponseMessageStringContent = new("Content as string");

        // The default string to reset to
        public static StringContent DefaultStringContent = new("Content as string");

        // Return string is null
        public static StringContent NullStringContent;

        // Empty, trying to get byte
        public static StringContent EmptyStringContent = new(string.Empty);

        // Single Byte returned
        public static StringContent ByteMinimumStringContent = new(new byte[] { 1 }.ToString());

    }
}