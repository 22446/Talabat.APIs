namespace Talabat.APIs.Errors
{
    public class ApiResponeIntrernal:ApiRespones
    {
        public string? Details{ get; set; }
        public ApiResponeIntrernal(int statusCode,string? ErrorMessage=null,string? _Details=null):base(statusCode, ErrorMessage)
        {
            Details = _Details;
        }
    }
}
