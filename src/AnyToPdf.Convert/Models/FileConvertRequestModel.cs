using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyToPdf.Convert.Models
{
    public class FileConvertRequestModel
    {
        public string url { get; set; }
        public bool inline { get; set; }
        public string fileName { get; set; }
        public bool useCustomStorage { get; set; }
        public Storage? Storage { get; set; }

    }
    public class Storage
    {
        public string method { get; set; }
        public string url { get; set; }
        public object extraHTTPHeaders { get; set; }
    }
}
