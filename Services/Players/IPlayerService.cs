using TennisTournament.Data.Models;

namespace TennisTournament.Services.Players
{
    public interface IPlayerService
    {
        PlayerQueryServiceModel All(
            string searchTerm,
            Gender gender);

        PlayerServiceModel Delete(
            int id);

        string UploadProfilePhoto(
            IFormFile profilePhoto);
    }
}
