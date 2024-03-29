﻿using TennisTournament.Data.Models;

namespace TennisTournament.Services.Players
{
    public class PlayerQueryServiceModel
    {
        public int TotalPlayers { get; init; }

        public Gender Gender { get; init; }

        public ICollection<PlayerServiceModel> Players { get; init; }
    }
}
