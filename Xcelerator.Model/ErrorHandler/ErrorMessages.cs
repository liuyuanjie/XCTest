using System.Collections.Generic;

namespace Xcelerator.Model.ErrorHandler
{
    public class ErrorMessages : IErrorHandler
    {
        private readonly Dictionary<ErrorCode, string> _message =
            new Dictionary<ErrorCode, string>
            {
                { ErrorCode.FailedToCreateAudit, "Failed to create an audit" },
                { ErrorCode.FailedToUpdateAudit, "Failed to update an audit" },
                { ErrorCode.FailedToLogin, "Failed to login" },
                { ErrorCode.InvalidEmail, "Invalid email" },
            };

        public CustomException GetCustomException(ErrorCode errorCode)
        {
            return new CustomException(errorCode, _message[errorCode]);
        }

        public string GetMessage(ErrorCode errorCode)
        {
            return _message[errorCode];
        }
    }
}
