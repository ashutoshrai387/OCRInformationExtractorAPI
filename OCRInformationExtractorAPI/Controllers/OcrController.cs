using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OCRInformationExtractorAPI.Models;
using OCRInformationExtractorAPI.Services;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OcrInformationExtractorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OcrController : ControllerBase
    {
        private readonly OcrService _ocrService;
        private readonly ILogger<OcrController> _logger;

        public OcrController(OcrService ocrService, ILogger<OcrController> logger)
        {
            // Adjust the path to where your Tesseract data is located
            _ocrService = ocrService;
            _logger = logger;
        }

        [HttpPost("extract")]
        public async Task<IActionResult> ExtractInfo(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            if (!file.ContentType.StartsWith("image/"))
            {
                return BadRequest("Uploaded file is not an image.");
            }

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;
                var ocrText = _ocrService.ExtractTextFromImage(stream);

                _logger.LogInformation($"OCR Text: {ocrText}");

                var name = ExtractName(ocrText);
                var aadhaarNumber = ExtractAadhaarNumber(ocrText);

                var result = new OcrResult
                {
                    Name = name,
                    AadhaarNumber = aadhaarNumber
                };

                return Ok(result);
            }
        }

        private string ExtractName(string text)
        {
            // Pattern to match three words before "DOB" or "Date of Birth"
            var namePattern = @"(?:\b\w+\b\s+){1,3}(?=DOB|Date of Birth)";
            var nameMatch = Regex.Match(text, namePattern, RegexOptions.IgnoreCase);

            // Capture the name part from the match
            if (nameMatch.Success)
            {
                var namePart = nameMatch.Value.Trim();
                // Remove the trailing space (if any)
                return namePart.Trim();
            }

            return "Not Found";
        }

        private string ExtractAadhaarNumber(string text)
        {
            // Pattern to match 12 digit number after "Your Aadhaar No. :"
            var aadhaarPattern = @"Your Aadhaar No\. :\s*(\d{4}\s\d{4}\s\d{4}|\d{12})";
            var aadhaarMatch = Regex.Match(text, aadhaarPattern);
            return aadhaarMatch.Success ? aadhaarMatch.Groups[1].Value.Trim() : "Not Found";
        }
    }
}
