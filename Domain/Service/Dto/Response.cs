namespace Interview.Domain.Service.Dto
{
    using System.Collections.Generic;

    public class Response
    {
        public bool Success { get; set; }
        public List<ValidationError> Errors { get; set; }
    }
}
