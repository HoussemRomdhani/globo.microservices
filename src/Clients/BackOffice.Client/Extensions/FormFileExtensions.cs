using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace BackOffice.Client.Extensions
{
    public static class FormFileExtensions
    {
        public static string ToBase64(this IFormFile formFile)
        {
            byte[] bytes;

            using (var memoryStream = new MemoryStream())
            {
                formFile.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            return Convert.ToBase64String(bytes);
        }
    }
}
