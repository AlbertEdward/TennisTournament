﻿using Microsoft.AspNetCore.Mvc;
using System.Web.Mvc;
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

        public PlayerServiceModel Delete(int id)
        {
            var player = this.data.Players.FirstOrDefault(c => c.Id == id);

            data.Players.Remove(player);

            data.SaveChanges();

            return new PlayerServiceModel();
        }

        public PlayerDetailsServiceModel Details(int id)
        => this.data
            .Players
            .Where(p => p.Id == id)
            .Select(player => new PlayerDetailsServiceModel
            {
                Id = player.Id,
                Name = player.Name,
                Age = player.Age,
                Gender = player.Gender,
                StrongHand = player.StrongHand,
                BackHandStroke = player.BackHandStroke
            })
            .FirstOrDefault();

        public bool Edit(
            int id,
            string name,
            int age,
            Gender gender,
            StrongHand strongHand,
            BackHandStroke backHandStroke)
        {
            var playerData = this.data.Players.Find(id);

            if (playerData == null)
            {
                return false;
            }

            //TODO make pictures editable
            playerData.Name = name;
            playerData.Age = age;
            playerData.Gender = gender;
            playerData.StrongHand = strongHand;
            playerData.BackHandStroke = backHandStroke;

            this.data.SaveChanges();

            return true;
        }


        public string UploadProfilePhoto(IFormFile profilePhoto)
        {
            var uploadsFolder = Path.Combine("wwwroot/UploadedPhotos/ProfilePhotos/");
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + profilePhoto.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                profilePhoto.CopyTo(fileStream);
            }

            return uniqueFileName;
        }
    }
}
