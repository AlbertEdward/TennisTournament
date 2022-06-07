﻿using System.ComponentModel.DataAnnotations;

namespace TennisTournament.Data.Models
{
    public class Player
    {
        public int Id { get; init; }

        [Required]
        public string Name { get; init; }

        [Required]
        public string Gender { get; init; }

        [Required]
        public string StrongHand { get; init; }

        [Required]
        public string BackHandStroke { get; init; }

        public string Rank { get; init; }

        public int Wins { get; init; }

        public int Losts { get; init; }

        public int TotalMatches { get; init; }

        public int TournamentId { get; init; }

        public Tournament Tournament { get; init; }
    }
}
