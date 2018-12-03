namespace Xcelerator.Model.ErrorHandler
{
    public interface IErrorHandler
    {
        string GetMessage(ErrorCode errorCode);

        CustomException GetCustomException(ErrorCode errorCode);

    }
}
