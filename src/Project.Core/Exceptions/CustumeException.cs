using System;
using System.Net;

namespace Project.Core.Exceptions
{
    /// <summary>
    /// The bass exception class which can be handled by the global exception handeling middleware.
    /// All exceptions that should be handled automatically should inherit from this base class.
    /// </summary>
    public class CustumeException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public CustumeException(HttpStatusCode statusCode, string message): base(message ?? "N/A")
        {
            StatusCode = statusCode;
        }
    }
}
