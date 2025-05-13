using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace r6_marketplace.Utils.Errors.API
{
    internal class ApiError
    {
        public List<ErrorDetail> errors { get; set; }
    }

    internal class ErrorDetail
    {
        public string message { get; set; }
    }
}
