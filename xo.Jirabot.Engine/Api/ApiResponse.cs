using System;
using System.Collections.Generic;
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

    /// <summary>
    /// All responses have a general template. In general API operations will not fail, but will return the proper HttpStatusCode embeeded in the return message.
    /// Some operations need to return a collection of elements.
    /// </summary>
    public class ApiResponseWithCollection<T> : ApiResponse
    {
        /// <summary>
        /// When successful, the response will include the collection of elements as an array.
        /// </summary>
        public ICollection<T> Content;

        public ApiResponseWithCollection(IEnumerable<T> data)
        {
            this.Content = new List<T>(data);
        }

        public ApiResponseWithCollection(HttpStatusCode code)
            : base(code)
        {
            if (code == HttpStatusCode.OK)
                throw new ArgumentException("This constructor should only be used when the Error Code is different from Successful.", "code");
        }

        public ApiResponseWithCollection(HttpStatusCode code, string details)
            : base(code, details)
        { }
    }

    /// <summary>
    /// All responses have a general template. In general API operations will not fail, but will return the proper HttpStatusCode embeeded in the return message.
    /// Some operations may need to return a tuples of elements.
    /// </summary>
    public class ApiResponse<T1> : ApiResponse
    {

        /// <summary>
		/// When successful, this item will not be null.
        /// </summary>
        public T1 Item1;


        public ApiResponse(T1 item1)
        {
            this.Item1 = item1;

        }

        public ApiResponse(Tuple<T1> content)
        {
            this.Item1 = content.Item1;

        }

        public ApiResponse(HttpStatusCode code)
            : base(code)
        {
            if (code == HttpStatusCode.OK)
                throw new ArgumentException("This constructor should only be used when the Error Code is different from Successful.", "code");
        }

        public ApiResponse(HttpStatusCode code, string details)
            : base(code, details)
        { }
    }
}
