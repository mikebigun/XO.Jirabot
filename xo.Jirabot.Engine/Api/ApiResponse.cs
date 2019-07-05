using System;
using System.Net;

namespace xo.Jirabot.Engine.Api
{
    public class ApiResponse
    {
        /// <summary>
        /// The HTTP Status Code.
        /// </summary>
        public HttpStatusCode Result;

        /// <summary>
        /// When in error, the response will include developer friendly error information.
        /// </summary>
        public string Error;

        public ApiResponse(HttpStatusCode code)
        {
            this.Result = code;
        }

        public ApiResponse(HttpStatusCode code, string details)
        {
            if (code == HttpStatusCode.OK)
                throw new ArgumentException("This constructor should only be used when the Error Code is different from Successful.", "code");

            if (string.IsNullOrWhiteSpace(details))
                throw new ArgumentException("Details cannot be null if call from this method.", "details");

            this.Result = code;
            this.Error = details;
        }

        public ApiResponse()
            : this(HttpStatusCode.OK)
        {
            this.Error = string.Empty;
        }
    }
}
