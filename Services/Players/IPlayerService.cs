using TennisTournament.Data.Models;
using TennisTournament.Models.Player;
using TennisTournament.Services.Players.Models;

namespace TennisTournament.Services.Players
{
    public interface IPlayerService
    {
        Task<PlayerQueryServiceModel> All(string searchTerm, Gender gender);

        bool Edit(
            int id,
            string name,
            int age,
            Gender gender,
            StrongHand strongHand,
            BackHandStroke backHandStroke);

        PlayerDetailsServiceModel Details(int id);

        PlayerServiceModel Delete(int id);

        void AddPlayer(PlayerFormModel player, string userId, string profilePhoto);

        bool UserIsPlayer(string userId);
    }
}
