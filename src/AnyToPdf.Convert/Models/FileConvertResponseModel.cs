using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyToPdf.Convert.Models
{
    public class FileConvertResponseModel
    {
        public string ResponseId { get; set; }
        public decimal MbOut { get; set; }
        public decimal Cost { get; set; }
        public decimal Seconds { get; set; }
        public string Error { get; set; }
        public bool Success { get; set; }
        public string FileUrl { get; set; }
    }
}
