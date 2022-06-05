using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ServiceBase
    {
        public ServiceBase()
        {

        }
        public Guid UserId { get; set; }
        protected async Task<ServiceResponse<T>> LogError<T>(Exception ex, T data)
        {
            return new ServiceResponse<T> { Success = false, Data = data, Message = ex.Message };
        }

        public static string GetCurrentLanguage()
        {
            return System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
        }

    }
}
