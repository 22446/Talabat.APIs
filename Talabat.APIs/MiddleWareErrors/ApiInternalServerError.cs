using System.Text.Json;
using Talabat.APIs.Errors;

namespace Talabat.APIs.MiddleWareErrors
{
    public class ApiInternalServerError
    {
        public RequestDelegate Next;
        public ILogger<ApiInternalServerError> Loggerr;
        public IHostEnvironment Env;
        public ApiInternalServerError(RequestDelegate _Next, ILogger<ApiInternalServerError> _Logger, IHostEnvironment _Env)
        {
            Next = _Next;
            Loggerr = _Logger;
            Env = _Env;
        }
        public async Task InvokeAsync(HttpContext content)
        {
            try
            {
                await Next.Invoke(content);
            }
            catch (Exception ex)
            {
                Loggerr.LogError(ex,ex.Message);
                content.Response.ContentType = "application/json";
                content.Response.StatusCode = 500;

                var apiResone = Env.IsDevelopment() ? new ApiResponeIntrernal(500, ex.Message, ex.StackTrace.ToString()) : new ApiResponeIntrernal(500);

                var option = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var SerializedApiRespone = JsonSerializer.Serialize(apiResone,option);

                await  content.Response.WriteAsync(SerializedApiRespone);
            }
        }
    }
}
