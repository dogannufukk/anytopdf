using AnyToPdf.Convert.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AnyToPdf.Convert.Helpers
{
    public class WebRequestHelper
    {
        public static async Task<string> Request(string requestUrl, 
                HttpRequestType httpRequestType, 
                string param,
                string authKey,
                string contentType= "application/json")
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUrl);
                httpWebRequest.ContentType = contentType;
                httpWebRequest.Method = httpRequestType.ToString();
                httpWebRequest.PreAuthenticate = true;
                if (!String.IsNullOrEmpty(authKey))
                    httpWebRequest.Headers.Add("Authorization", authKey);
                httpWebRequest.Accept = contentType;
                if (!String.IsNullOrEmpty(param))
                {
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        streamWriter.Write(param);
                    }
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = await streamReader.ReadToEndAsync();
                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new HttpRequestException();

            }




        }


    }
}
