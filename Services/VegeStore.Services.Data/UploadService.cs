namespace VegeStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.Extensions.Configuration;

    public class UploadService : IUploadService
    {
        private readonly IConfiguration configuration;

        public UploadService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string UploadImageToCloudinary(Stream imageFileStream)
        {
            Account account = new Account
            {
                Cloud = this.configuration.GetSection("Cloudinary").GetSection("cloudName").Value,
                ApiKey = this.configuration.GetSection("Cloudinary").GetSection("apiKey").Value,
                ApiSecret = this.configuration.GetSection("Cloudinary").GetSection("apiSecret").Value,
            };

            Cloudinary cloudinary = new Cloudinary(account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription("thumbnail", imageFileStream),
            };
            var uploadResult = cloudinary.Upload(uploadParams);
            return uploadResult.SecureUri.AbsoluteUri;
        }
    }
}
