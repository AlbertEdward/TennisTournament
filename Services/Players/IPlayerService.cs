using TennisTournament.Data.Models;
using TennisTournament.Models.Player;
using TennisTournament.Services.Players.Models;

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

        bool Edit(
            int id,
            string name,
            int age,
            Gender gender,
            StrongHand strongHand,
            BackHandStroke backHandStroke);

        PlayerDetailsServiceModel Details(int id);
    }
}
