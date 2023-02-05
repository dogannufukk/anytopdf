using AnyToPdf.Convert.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyToPdf.Convert.Services.Abstracts
{
    public interface IFileConverterService
    {
        Task<FileConvertResponseModel> ConvertFile(string fileUrl);
    }
}
