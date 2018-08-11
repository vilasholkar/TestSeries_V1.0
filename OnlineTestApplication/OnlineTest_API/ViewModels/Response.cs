using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
   public class Response<T>
    {
        public T Object { get; set; }
        public bool IsSuccessful { get; set; }
        public bool IsSessionExpired { get; set; }
        public string Message { get; set; }
    }
}
