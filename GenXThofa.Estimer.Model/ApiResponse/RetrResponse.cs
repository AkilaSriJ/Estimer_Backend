using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Model.ApiResponse
{
    public class RetrResponse<T>
    {
        public T Result { get; set; }
        public RetrError Error { get; set; }
        public RetrTrace Trace { get; set; }
        public RetrMeta Response { get; set; }
        public static RetrResponse<T> Success(T result, string message = "Operation successful")
        {
            return new RetrResponse<T>
            {
                Result = result,
                Error = null,
                Trace = new RetrTrace
                {
                    TraceId = Guid.NewGuid().ToString(),
                    Source = "Estimer.API"
                },
                Response = new RetrMeta
                {
                    Status = "SUCCESS",
                    Message = message,
                    Timestamp = DateTime.UtcNow
                }
            };
        }
        public static RetrResponse<T> Failure(string code, string message, List<string> details = null)
        {
            return new RetrResponse<T>
            {
                Result = default(T),
                Error = new RetrError
                {
                    Code = code,
                    Message = message,
                    Details = details ?? new List<string>()
                },
                Trace = new RetrTrace
                {
                    TraceId = Guid.NewGuid().ToString(),
                    Source = "Estimer.API"
                },
                Response = new RetrMeta
                {
                    Status = "FAILED",
                    Message = message,
                    Timestamp = DateTime.UtcNow
                }
            };
        }
    }
}
    public class RetrError
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public List<string> Details { get; set; }
    }
    public class RetrTrace
    {
        public string TraceId { get; set; }
        public string Source { get; set; }
    }
    public class RetrMeta
    {
        public string Status { get; set; }   
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
