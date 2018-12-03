namespace Xcelerator.Model.ErrorHandler
{
    public enum ErrorCode : short
    {
        FailedToCreateAudit = 10001,
        FailedToUpdateAudit = 10002,
        FailedToDeleteAudit = 10003,


        FailedToLogin = 20001,
        InvalidEmail = 20002,
    }
}
