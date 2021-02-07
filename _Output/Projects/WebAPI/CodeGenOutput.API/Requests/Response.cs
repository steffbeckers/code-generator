namespace CodeGenOutput.API.Requests
{
    public class Response<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
    }
}
