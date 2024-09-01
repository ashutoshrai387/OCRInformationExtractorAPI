# OCRInformationExtractorAPI

## Overview

The **OCR Information Extractor API** is a backend service built using ASP.NET Core that extracts information from Aadhaar image files using Optical Character Recognition (OCR). This API processes uploaded image files to extract personal details such as Name and Aadhaar Number and returns them as JSON.

## Features

- **File Upload Handling**: Accepts image files uploaded via HTTP POST requests.
- **OCR Integration**: Uses Tesseract OCR to process images and extract text.
- **Information Extraction**: Extracts Name and Aadhaar Number from the OCR output.
- **Flexible Aadhaar Number Formats**: Supports Aadhaar numbers with and without spaces.

## Tech Stack

- **Framework**: ASP.NET Core
- **OCR Library**: Tesseract
- **Logging**: ASP.NET Core built-in logging
- **Dependency Injection**: Used for service management

## Setup

### Prerequisites

- .NET 6.0 or later
- Tesseract OCR library

### Installation

1. **Clone the Repository**

   ```bash
   git clone https://github.com/yourusername/OCRInformationExtractorAPI.git
   cd OCRInformationExtractorAPI

2. **Install Dependencies**

- Run the following command to restore the required packages:

   ```bash
   dotnet restore
   ```
3. **Configure Tesseract Data Path**

- Ensure that the Tesseract data files are located in the tessdata directory. You can set the path in OcrService class in Services/OcrService.cs.

4. **Build and Run**

   ```bash
   dotnet build
   dotnet run

## API Endpoints

### POST /api/ocr/extract.

- Uploads an image file and extracts Name and Aadhaar Number.

#### Request

- **Content-Type**: multipart/form-data
- **Form Data**: file (The image file containing Aadhaar details)
  
#### Response

- **Status Code**: 200 OK
- **Content-Type**: application/json

#### Example Response

   ```json
{
  "name": "John Doe",
  "aadhaarNumber": "1234 5678 9012"
}
  ```

#### Possible Errors

- **400 Bad Request**: If no file is uploaded or the file is not an image.
- **500 Internal Server Error***: If there is an issue processing the image.

## Logging
- The application uses ASP.NET Core’s built-in logging mechanism. Logs are written to the console by default. You can configure logging in Program.cs.

## Aadhar Format
- This app will work for only a specific format of Aadhar Card Image. I have uploaded the sample image in the repositiory.
