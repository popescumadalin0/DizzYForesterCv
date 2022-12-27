using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public class Response<T>
    {
        public string? Message { get; set; }
        public int Status { get; set; }
        public T? Data { get; set; }
    }
}
