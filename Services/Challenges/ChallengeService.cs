﻿using Microsoft.EntityFrameworkCore;
using TennisTournament.Data;
using TennisTournament.Data.Models;
using TennisTournament.Models.Challenge;
using TennisTournament.Infrastructure;
using System.Security.Claims;
using TennisTournament.Services.Challenges.Models;

namespace TennisTournament.Services.Challenges
{
    public class ChallengeService : IChallengeService
    {
        private readonly TennisDbContext data;

        public ChallengeService(TennisDbContext data)
        {
            this.data = data;
        }

        public void AddPlayerToChallenge(string hostUserId, int guestId, int challengeId)
        {
            var playerHost = data.Players.FirstOrDefault(p => p.UserId == hostUserId);
            var playerGuest = data.Players.FirstOrDefault(p => p.Id == guestId);

            var challenge = data.Challenges.FirstOrDefault(c => c.Id == challengeId);

            if (challenge.Players.Count == 0)
            {
                challenge.Players.Add(playerHost);
                challenge.Players.Add(playerGuest);
            }

            this.data.SaveChanges();
        }

        public void DeleteChallenge(int id)
        {
            var challenge = data.Challenges
                .Include(c => c.Players)
                .FirstOrDefault(c => c.Id == id);

            challenge.Players.Clear();

            this.data.Remove(challenge);

            this.data.SaveChanges();
        }

        public ChallengeServiceModel Details(int Id)
        => this.data
            .Challenges
            .Where(c => c.Id == Id)
            .Select(challenges => new ChallengeServiceModel
            {
                Id = challenges.Id,
                Name = challenges.Name,
                CourtType = challenges.CourtType,
                Set = challenges.Sets,
                Game = challenges.Games,
                Rule = challenges.Rules,
                LastSet = challenges.LastSets,
                Description = challenges.Description,
                Players = challenges.Players
            })
            .FirstOrDefault();

        public void CreateChallenge(ChallengeFormModel challenge, int guestId, string hostUserId)
        {
            var challengeData = new Challenge
            {
                Name = challenge.Name,
                CourtType = challenge.CourtTypes,
                Sets = challenge.Sets,
                Games = challenge.Games,
                Rules = challenge.Rules,
                LastSets = challenge.LastSets,
                Description = challenge.Description,
                PlayerGuestId = guestId,
                PlayerHostUserId = hostUserId
            };

            this.data.Challenges.Add(challengeData);
            this.data.SaveChanges();

            var challengeId = challengeData.Id;

            AddPlayerToChallenge(hostUserId, guestId, challengeId);
        }

        public void ChallengeResult(int challengeId, int winnerId)
        {
            var challenge = this.data.Challenges.Include(p => p.Players).FirstOrDefault(c => c.Id == challengeId);

            var winner = challenge.Players.FirstOrDefault(p => p.Id == winnerId);
            var loser = challenge.Players.FirstOrDefault(p => p.Id != winnerId);

            challenge.Winner = winner.Id;
            challenge.Loser = loser.Id;

            winner.Wins += 1;
            loser.Losses += 1;

            winner.TotalMatches += 1;
            loser.TotalMatches += 1;

            winner.Rank = 10.00 - (winner.Wins + winner.Losses) / winner.TotalMatches;
            loser.Rank = 10.00 + (loser.Wins + loser.Losses) / loser.TotalMatches;

            if (winner.Rank > 10) 
            {
                winner.Rank = 10.00;
            }
            else if (loser.Rank > 10)
            {
                loser.Rank = 10.00;
            }

            this.data.SaveChanges();
        }
    }
}
