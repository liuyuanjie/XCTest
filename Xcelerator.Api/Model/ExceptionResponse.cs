using Newtonsoft.Json;

namespace Xcelerator.Api.Model
{
    public class ExceptionResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
