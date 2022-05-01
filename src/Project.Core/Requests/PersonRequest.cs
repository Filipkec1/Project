using Microsoft.AspNetCore.Http;
using System;

namespace Project.Core.Requests
{
    /// <summary>
    /// Defines person request.
    /// </summary>
    public class PersonRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }
        public string Email { get; set; }
        public string Oib { get; set; }
    }

    /// <summary>
    /// Defines person create request.
    /// </summary>
    public class PersonCreateRequest : PersonRequest
    { }

    /// <summary>
    /// Defines person update request.
    /// </summary>
    public class PersonUpdateRequest : PersonRequest
    {
        public Guid Id { get; set; }
    }

    /// <summary>
    /// Defines text file upload request.
    /// </summary>
    public class PersonTextFileUploadRequest
    {
        public IFormFile TextFile { get; set; }
    }
}
