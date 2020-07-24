namespace VegeStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public interface IUploadService
    {
        public string UploadImageToCloudinary(Stream imageFileStream);
    }
}
