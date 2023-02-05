using AnyToPdf.Convert.Helpers;
using AnyToPdf.Convert.Models;
using AnyToPdf.Convert.Services.Abstracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web;

namespace AnyToPdf.Convert.Services.Concretes
{
    public class FileConverterService : IFileConverterService
    {
        private readonly IConfiguration configuration;
        private readonly IHostEnvironment hostEnvironment;

        public FileConverterService(IConfiguration configuration,
            IHostEnvironment hostEnvironment)
        {
            this.configuration = configuration;
            this.hostEnvironment = hostEnvironment;
        }
        public async Task<FileConvertResponseModel> ConvertFile(string fileUrl)
        {
            var requestUrl = configuration.GetSection("Api2Pdf:RequestUrl")?.Value;
            //By creating a membership from Api2Pdf, enter the AuthKey value of your membership into the configuration file.
            var authKey = configuration.GetSection("Api2Pdf:AuthKey")?.Value;
            if (String.IsNullOrEmpty(requestUrl) || String.IsNullOrEmpty(authKey)) return null; //Maybe throw exception
            var body = new FileConvertRequestModel()
            {
                url = HttpUtility.UrlDecode(fileUrl),
                fileName = "converted.pdf"
            };
            var stringBody = JsonConvert.SerializeObject(body);
            var resultOfRequest = await WebRequestHelper
                .Request(requestUrl, HttpRequestType.Post, stringBody, authKey);

            if (resultOfRequest != null)
            {
                var result = JsonConvert.DeserializeObject<FileConvertResponseModel>(resultOfRequest);
                if (result.Success)
                {
                    string downloadFolder = hostEnvironment.ContentRootPath + $@"\wwwroot\Download";
                    if (!Directory.Exists(fileUrl))
                        Directory.CreateDirectory(downloadFolder);
                    string fileDownloadPath = $@"{downloadFolder}\{Guid.NewGuid()}.pdf";

                    var downloadFileResult = DownloadFile(result.FileUrl, fileDownloadPath);
                    if (downloadFileResult)
                        return result;
                }


            }
            return null;
        }

        bool DownloadFile(string fileUrl, string savePath)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile(fileUrl, savePath);
                }
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
