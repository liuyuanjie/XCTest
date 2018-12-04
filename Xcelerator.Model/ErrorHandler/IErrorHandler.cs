namespace Xcelerator.Model.ErrorHandler
{
    public interface IErrorHandler
    {
        CustomException GetCustomException(ErrorCode errorCode);

    }
}
