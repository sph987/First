using application.Interfaces;
using Microsoft.AspNetCore.Http;
using CloudinaryDotNet;
using application.Photos;
using Microsoft.Extensions.Options;
using CloudinaryDotNet.Actions;

namespace Infrastructure.Photos
{
    public class PhotoAccessor : IPhotoAccessor
    {
        private readonly Cloudinary _cloudinary;
        public PhotoAccessor(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecrets
            );
            _cloudinary = new Cloudinary(acc);
        }



        PhotoUploadResult IPhotoAccessor.AddPhoto(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream),
                        Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                    };
                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            if (uploadResult.Error != null)
                throw new System.Exception("problem saving changes");

            return new PhotoUploadResult
            {
                PublicId = uploadResult.PublicId,
                Url = uploadResult.SecureUri.AbsoluteUri
            };
        }
        public string DeletePhoto(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = _cloudinary.Destroy(deleteParams);
            return result.Result == "ok" ? result.Result : null;
        }
    }
}