using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;

namespace FinalProject_PRN.Models
{
    public class CloudinarySettings
    {   
        public string CloudName { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public CloudinarySettings() { }
        public static CloudinarySettings GetCloudinarySettings()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var cloudinarySettings = new CloudinarySettings();
            configuration.GetSection("CloudinarySettings").Bind(cloudinarySettings);
            return cloudinarySettings;
        }
        public ImageUploadResult CloudinaryUpload(IFormFile f)
        {
            CloudinarySettings cs = GetCloudinarySettings();
            Account account = new Account(cs.CloudName,cs.ApiKey,cs.ApiSecret);
            Cloudinary cloudinary = new Cloudinary(account);

            var uploadParams = new ImageUploadParams() {
                File = new FileDescription(f.FileName, f.OpenReadStream())
                
            };
            return cloudinary.Upload(uploadParams);
        }
    }
}
