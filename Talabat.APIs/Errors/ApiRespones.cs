namespace Talabat.APIs.Errors
{
    public class ApiRespones
    {
        public int StatusCode { get; set; }
        public string? ErrorMessage  { get; set; }

        public ApiRespones(int _StatusCode,string? _ErrorMessage=null)
        {
            StatusCode = _StatusCode;
            ErrorMessage = _ErrorMessage??DefaultErrorRespone(StatusCode);
        }

        private string? DefaultErrorRespone(int StatusCode)
        {
            return StatusCode switch
            {
                500 => "Internal Server Error",
                400 => "Bad Request",
                401 => "Unauthirized",
                404 => "Not Found",
                _   => "Error Happend"
            };
        }
    }
}
