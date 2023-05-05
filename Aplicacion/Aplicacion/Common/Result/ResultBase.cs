namespace Aplicacion.Common.Result
{
    public class ResultBase
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }

        public ResultBase(string code, bool isSuccess, string message = null)
        {
            Code = code;
            Message = message;
            IsSuccess = isSuccess;
        }
    }

    public class ResultBase<T> : ResultBase
    {
        public T Data { get; set; }

        public ResultBase(string code, bool isSuccess, string message = null, T data = default) : base(code, isSuccess, message)
        {
            Data = data;
        }
    }
}
