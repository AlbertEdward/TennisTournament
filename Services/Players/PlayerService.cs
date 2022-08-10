using Microsoft.EntityFrameworkCore;
using TennisTournament.Data;
using TennisTournament.Data.Models;
using TennisTournament.Models.Player;
using TennisTournament.Services.Players.Models;

namespace TennisTournament.Services.Players
{
    public class PlayerService : IPlayerService
    {
        private readonly TennisDbContext data;

        public PlayerService(TennisDbContext data)
        {
            this.data = data;
        }

        public async Task<PlayerQueryServiceModel> AllAsync(string searchTerm, Gender gender)
        {
            var playersQuery = await this.data.Players.ToListAsync();

            if (gender != Gender.Select)
            {
                playersQuery = playersQuery.Where(c => c.Gender == gender).ToList();
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                playersQuery = playersQuery.Where(p =>
                    p.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
            }

            var totalPlayers = playersQuery.Count();

            var players = playersQuery
                .OrderByDescending(p => p.Id)
                .Select(p => new PlayerServiceModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Gender = p.Gender,
                    Rank = p.Rank,
                    ProfilePhoto = p.ProfilePhoto
                })
                .ToList();

            return new PlayerQueryServiceModel
            {
                Players = players,
                TotalPlayers = totalPlayers,
                Gender = gender
            };
        }

        public async Task<PlayerServiceModel> DeleteAsync(int id)
        {
            var player = await this.data.Players.FirstOrDefaultAsync(c => c.Id == id);

            data.Players.Remove(player);
            await data.SaveChangesAsync();

            return new PlayerServiceModel();
        }

        public async Task<bool> EditAsync(
            int id,
            string name,
            int age,
            Gender gender,
            StrongHand strongHand,
            BackHandStroke backHandStroke)
        {
            var playerData = await this.data.Players.FindAsync(id);

            if (playerData == null)
            {
                return false;
            }

            playerData.Name = name;
            playerData.Age = age;
            playerData.Gender = gender;
            playerData.StrongHand = strongHand;
            playerData.BackHandStroke = backHandStroke;

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<PlayerDetailsServiceModel> DetailsAsync(int id)
        => await this.data
            .Players
            .Where(p => p.Id == id)
            .Select(player => new PlayerDetailsServiceModel
            {
                Id = player.Id,
                Name = player.Name,
                Rank = player.Rank,
                Age = player.Age,
                Gender = player.Gender,
                StrongHand = player.StrongHand,
                BackHandStroke = player.BackHandStroke,
                ProfilePhoto = player.ProfilePhoto,
                Tournaments = player.Tournaments,
                Challenges = player.Challenges,
                UserId = player.UserId
            })
            .FirstOrDefaultAsync();

        public void AddPlayer(PlayerFormModel player, string userId, string profilePhoto)
        {
            var userIsAlreadyPlayer = this.data
                .Players
                .Any(p => p.UserId == userId);

            var playerData = new Player
            {
                Name = player.Name,
                Age = player.Age,
                Gender = player.Gender,
                StrongHand = player.StrongHand,
                BackHandStroke = player.BackHandStroke,
                ProfilePhoto = profilePhoto,
                UserId = userId
            };

            this.data.Players.Add(playerData);
            this.data.SaveChanges();
        }

        public bool UserIsPlayer(string userId)
        {
            var user = this.data.Players.Any(p => p.UserId == userId);

            return user;
        }
    }
}
