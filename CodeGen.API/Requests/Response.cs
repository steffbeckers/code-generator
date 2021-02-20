namespace CodeGen.API.Requests
{
    public class Response
    {
        public bool Success { get; set; } = true;
        public string Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
