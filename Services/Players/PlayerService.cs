﻿using Microsoft.AspNetCore.Mvc;
using TennisTournament.Data;
using TennisTournament.Data.Models;
using TennisTournament.Models.Player;

namespace TennisTournament.Services.Players
{
    public class PlayerService : IPlayerService
    {
        private readonly TennisDbContext data;

        public PlayerService(TennisDbContext data)
        {
            this.data = data;
        }

        public PlayerQueryServiceModel All(
            string searchTerm,
            Gender gender)
        {
            var playersQuery = this.data.Players.AsQueryable();

            if (gender != Gender.Select)
            {
                playersQuery = playersQuery.Where(c => c.Gender == gender);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                playersQuery = playersQuery.Where(p =>
                    p.Name.ToLower().Contains(searchTerm.ToLower()));
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
    }
}
