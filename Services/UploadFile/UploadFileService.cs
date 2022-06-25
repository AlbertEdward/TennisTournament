namespace TennisTournament.Services
{
    public class UploadFileService : IUploadFileService
    {
        public string CoverPhoto(IFormFile coverPhoto)
        {
            var uploadsFolder = Path.Combine("wwwroot/UploadedPhotos/CoverPhotos/");
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + coverPhoto.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                coverPhoto.CopyTo(fileStream);
            }

            return uniqueFileName;
        }

        public string ProfilePhoto(IFormFile profilePhoto)
        {
            var uploadsFolder = Path.Combine("wwwroot/UploadedPhotos/ProfilePhotos/");
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + profilePhoto.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                profilePhoto.CopyTo(fileStream);
            }

            return uniqueFileName;
        }
    }
}
