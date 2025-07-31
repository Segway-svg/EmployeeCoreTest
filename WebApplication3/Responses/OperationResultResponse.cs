namespace WebApplication3.Responses
{
    public class OperationResultResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T Body { get; set; }
        public List<ResponseError> Errors { get; set; } = new List<ResponseError>();
    }

    public class ResponseError
    {
        public int Code { get; set; }
        public string Messages { get; set; }
    }
}