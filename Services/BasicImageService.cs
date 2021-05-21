using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Services
{
    public class BasicImageService : IImageService
    {
        public async Task<byte[]> EncodeFileAsync(IFormFile image)
        {
            if (image == null)
            {
                return null;
            }
            using var stream = new MemoryStream();
            await image.CopyToAsync(stream);
            return stream.ToArray();
        }

        public string RecordContentType(IFormFile image)
        {
            if (image == null)
            {
                return null;
            }
            return image.ContentType;
        }
        public string DecodeFile(byte[] imageData, string contentType)
        {
            if (imageData == null)
            {
                return "/assets/img/750x300.png";
            }
            var imageArray = Convert.ToBase64String(imageData);
            return $"data:{contentType};base64,{imageArray}";
        }
        public string DecodeFileAvatar(byte[] imageData, string contentType)
        {
            if (imageData == null)
            {
                return "/assets/img/avatar.png";
            }
            var imageArray = Convert.ToBase64String(imageData);
            return $"data:{contentType};base64,{imageArray}";
        }
        public string DecodeFileAvatarAPI(byte[] imageData, string contentType)
        {
            if (imageData == null)
            {
                return "assets/img/avatar.png";
            }
            var imageArray = Convert.ToBase64String(imageData);
            return $"data:{contentType};base64,{imageArray}";
        }

        public byte[] EncodeImage(string url)
        {
            var image = url.IndexOf(',');
            if (image >= 0)
            {
                var base64 = url.Substring(image + 1);
                return Convert.FromBase64String(base64);
            }
            return null;

        }
    }
}
