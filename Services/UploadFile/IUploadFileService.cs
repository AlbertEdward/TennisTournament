namespace TennisTournament.Services
{
    public interface IUploadFileService
    {
        string ProfilePhoto(
           IFormFile profilePhoto);
        string CoverPhoto(
           IFormFile coverPhoto);

    }
}
