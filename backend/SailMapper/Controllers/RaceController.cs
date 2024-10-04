﻿using Microsoft.AspNetCore.Mvc;
using SailMapper.Classes;
using SailMapper.Services;
using System.Text;
using System.Text.Json;

namespace SailMapper.Controllers
{
    [ApiController]
    [Route("/race")]
    public class RaceController
    {
        private static readonly string[] FinishTypes = new[]
        {
             "DNS", "DNF", "RET", "DSQ", "OCS", "BFD", "DNC", "DGM", "RDG", "SCP", "ZFP", "UFD", "TLE", "NSC"
        };

        private readonly RaceService raceService;

        public RaceController()
        {
            raceService = new RaceService();
        }

        /// <summary>
        /// Add a Race by sending a race object
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// Post /race
        /// {
        ///     "Course Id": string
        ///     "Start Time": DateTime
        ///     "End Time": DateTime
        ///     "Name": string
        /// }
        /// </remarks>
        /// <param name="request"></param>
        /// <returns>Id of created race</returns>
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> CreateRace(HttpRequestMessage request)
        {
            if (request.Content != null)
            {
                var race = await JsonSerializer.DeserializeAsync<Race>(new MemoryStream(Encoding.UTF8.GetBytes(request.Content.ToString())));
                if (race == null)
                {
                    return Results.BadRequest();
                }
                var id = await raceService.AddRace(race);
                if (id != null)
                {
                    return Results.Created(id, race);
                }
            }
            return Results.Problem();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of all races</returns>
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> GetRaces()
        {
            var races = await raceService.GetRaces();
            return Results.Ok(races);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Data for specified race</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        public async Task<IResult> GetRace(string id)
        {
            var race = await raceService.GetRace(id);
            return Results.Ok(race);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Http codes</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        public async Task<IResult> DeleteRace(string id)
        {
            bool success = await raceService.DeleteRace(id);
            return Results.Ok(success);
        }

        /// <summary>
        /// 
        /// </summary>
        ///<remarks>
        /// send a partially populated Race object, all populated fields will be updated to the given data
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns>Http codes</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        public async Task<IResult> UpdateRace(string id, HttpRequestMessage request)
        {
            if (request != null && request.Content != null)
            {
                var race = await JsonSerializer.DeserializeAsync<Race>(new MemoryStream(Encoding.UTF8.GetBytes(request.Content.ToString())));
                if (race == null)
                {
                    return Results.BadRequest();
                }
                bool success = await raceService.UpdateRace(id, race);
                if (id != null)
                {
                    return Results.Ok(id);
                }
            }
            return Results.Problem();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of all boat objects in the specified race</returns>
        [HttpGet("{id}/participants")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        public async Task<IResult> GetParticipants(string id)
        {
            Boat[] boats = await raceService.GetParticipants(id);
            if (boats == null)
            {
                return Results.NotFound();
            }
            return Results.Ok<Boat[]>(boats);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="boat"></param>
        /// <returns>Http codes</returns>
        [HttpPut("{id}/participant/{boat}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        public async Task<IResult> AddParticipant(string id, string boat)
        {
           bool success = await raceService.AddParticipant(id, boat);
            if (success)
            {
                return Results.Ok(id);
            }
            return Results.NotFound();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="boat"></param>
        /// <returns>Http codes</returns>
        [HttpDelete("{id}/participant/{boat}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        public async Task<IResult> RemoveParticipant(string id, string boat)
        {
           bool success = await raceService.RemoveParticipant(id, boat);
            if (success)
            {
                return Results.Ok(id);
            }
            return Results.NotFound();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Result object for each boat in the race</returns>
        [HttpGet("{id}/results")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IResult> GetResults(string id)
        {
            Result[] result = await raceService.GetResults(id);
            if(result == null)
                return Results.NotFound();
            return Results.Ok(result);
        }


    }
}