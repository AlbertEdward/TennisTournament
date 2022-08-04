using TennisTournament.Data.Models;
using TennisTournament.Models.Player;
using TennisTournament.Services.Players.Models;

namespace TennisTournament.Services.Players
{
    public interface IPlayerService
    {
        Task<PlayerQueryServiceModel> AllAsync(string searchTerm, Gender gender);

        Task<bool> EditAsync(
            int id,
            string name,
            int age,
            Gender gender,
            StrongHand strongHand,
            BackHandStroke backHandStroke);

        Task<PlayerDetailsServiceModel> DetailsAsync(int id);

        Task<PlayerServiceModel> DeleteAsync(int id);

        void AddPlayer(PlayerFormModel player, string userId, string profilePhoto);

        bool UserIsPlayer(string userId);
    }
}
