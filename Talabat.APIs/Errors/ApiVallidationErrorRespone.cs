namespace Talabat.APIs.Errors
{
    public class ApiVallidationErrorRespone:ApiRespones
    {
        public IEnumerable<string> Errors { get; set; }
        public ApiVallidationErrorRespone(IEnumerable<string>_Errors):base(400)
        {
            Errors = _Errors;
            
        }
    }
}
