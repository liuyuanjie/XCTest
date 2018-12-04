using System;
using Newtonsoft.Json;

namespace Xcelerator.Model.ErrorHandler
{
    public class CustomException : Exception
    {
        public ErrorCode Code { get; set; }

        public CustomException(ErrorCode code, string message)
            : base(message)
        {
            Code = code;
        }

        public CustomException(ErrorCode code, string message, Exception innerException)
            : base(message, innerException)
        {
            Code = code;
        }

        public string ResponeMessage => JsonConvert.SerializeObject(new { Code, Message });

        public override string ToString()
        {
            return $"code: {Code}, message: {Message}, exception: {InnerException}";
        }
    }
}
