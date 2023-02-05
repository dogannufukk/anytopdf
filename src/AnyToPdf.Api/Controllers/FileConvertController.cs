using AnyToPdf.Convert.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace AnyToPdf.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileConvertController : ControllerBase
    {
        private readonly IFileConverterService fileConverterService;

        public FileConvertController(IFileConverterService fileConverterService)
        {
            this.fileConverterService = fileConverterService;
        }


        /// <summary>
        /// You must give the FilePath value a file url that can be accessed from the open world.
        /// </summary>
        /// <param name="filePath">Sample Url: https://www.api2pdf.com/wp-content/themes/api2pdf/assets/samples/sample-word-doc.docx</param>
        /// <returns></returns>
        [HttpPost("convert/{filePath}")]
        public async Task<IActionResult> Convert(string filePath)
        {
            var result = await fileConverterService.ConvertFile(filePath);
            return Ok(result);
        }

    }
}