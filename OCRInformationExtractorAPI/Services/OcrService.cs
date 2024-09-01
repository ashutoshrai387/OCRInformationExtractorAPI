using System;
using System.IO;
using Tesseract;

namespace OCRInformationExtractorAPI.Services
{
    public class OcrService
    {
        private readonly string _tessdataPath;

        public OcrService(IConfiguration configuration)
        {
            _tessdataPath = configuration["Tesseract:TessdataPath"];
        }

        public string ExtractTextFromImage(Stream imageStream)
        {
            try
            {
                // Rewind the stream to the beginning
                imageStream.Position = 0;
                using (var engine = new TesseractEngine(_tessdataPath, "eng", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromMemory(ReadFully(imageStream)))
                    {
                        using (var page = engine.Process(img))
                        {
                            return page.GetText();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                // You may use any logging mechanism here
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        private static byte[] ReadFully(Stream input)
        {
            if (input == null || input.Length == 0)
            {
                throw new ArgumentException("Stream is empty or null.");
            }

            using (var ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }


    }
}
