using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spa.Models
{
    public class ErrorModel
    {
        public enum errorType
        {         
            error = 500,
            warning = 501
        }

        public errorType type { get; set; }

        public string message { get; set; }

        public ErrorModel(string message)
        {
            this.message = message;
            this.type = errorType.error;
        }

        public ErrorModel(string message, errorType type)
        {
            this.message = message;
            this.type = type;
        }
    }
}
