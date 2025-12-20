using GenXThofa.Technologies.Estimer.Model.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Model.ApiResponse
{
    public class ApiResponseDto<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
        public List? Errors { get; set; }

        public static ApiResponseDto<T> SuccessResponse(T data, string message)
        {
            return new ApiResponseDto<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ApiResponseDto<T> ErrorResponse(string message, List? errors = null)
        {
            return new ApiResponseDto<T>
            {
                Success=false,  
                Message = message,
                Errors = errors
            };
        }

        public object SuccessResponse(IEnumerable<ClientDto> clients)
        {
            throw new NotImplementedException();
        }
    }
}
