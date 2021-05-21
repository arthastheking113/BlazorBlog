using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Services
{
    public interface IImageService
    {
        Task<byte[]> EncodeFileAsync(IFormFile image);

        byte[] EncodeImage(string url);

        string DecodeFile(byte[] imageData, string contentType);

        string DecodeFileAvatar(byte[] imageData, string contentType);

        string DecodeFileAvatarAPI(byte[] imageData, string contentType);

        string RecordContentType(IFormFile image);
    }
}
