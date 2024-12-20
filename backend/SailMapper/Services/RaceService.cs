﻿using Microsoft.EntityFrameworkCore;
using SailMapper.Classes;
using SailMapper.Data;

namespace SailMapper.Services
{

    // TODO: try catch all the things
    public class RaceService
    {
        private static readonly string[] FinishTypes = new[]
        {
             "FIN","DNF", "DNS", "RET", "DSQ", "OCS", "BFD", "DNC", "DGM", "RDG", "SCP", "ZFP", "UFD", "TLE", "NSC"
        };



        private readonly SailDBContext _dbContext;
        public RaceService(SailDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddRace(Race race)
        {
            try
            {
                await _dbContext.Races.AddAsync(race);
                await _dbContext.SaveChangesAsync();
                return race.Id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<List<Race>> GetRaces()
        {
            List<Race> races = await _dbContext.Races.ToListAsync();

            return races;
        }

        public async Task<Race> GetRace(int id)
        {
            return await _dbContext.Races
                .Include(r => r.Participants)
                .Include(r => r.Course)
                .Include(r => r.Results)
                .Include(r => r.Tracks)
                .FirstOrDefaultAsync(r => r.Id == id);
        }


        public async Task<bool> DeleteRace(int id)
        {
            Race race = await GetRaceEntity(id);
            if (race != null)
            {
                _dbContext.Races.Remove(race);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateRace(Race raceUpdate)
        {
            var race = _dbContext.Races.Update(raceUpdate);
            if (race != null)
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Boat>> GetParticipants(int id)
        {
            Race race = await GetRaceEntity(id);
            if (race != null && race.Participants != null)
            {
                return race.Participants.ToList();
            }

            return null;
        }

        public async Task<bool> AddParticipant(int id, int boatId)
        {
            Race race = await GetRaceEntity(id);
            Boat boat = await _dbContext.Boats.FindAsync(boatId);

            if (race != null && boat != null)
            {
                race.Participants.Add(boat);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<bool> RemoveParticipant(int id, int boatId)
        {
            Race race = await GetRaceEntity(id);
            Boat boat = await _dbContext.Boats.FindAsync(boatId);

            if (race != null && boat != null)
            {
                race.Participants.Remove(boat);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<List<Result>> GetResults(int id)
        {
            Race race = await GetRaceEntity(id);
            return await GetResults(race);
        }

        public async Task<List<Result>> GetResults(Race race)
        {
            List<Result> results = _dbContext.Results.Where(c => c.Race == race).ToList();

            if (race.Participants != null && results.Count == race.Participants.Count)
            {
                return results;
            }
            else
            {
                return await CalculateResults(race.Id);
            }
        }



        public async Task<List<Result>> CalculateResults(int id, int averageBoat = -1, int B = 550)
        {
            List<Track> tracks = _dbContext.Tracks.Where(c => c.Race != null && c.Race.Id == id).ToList();

            // write more efficinet query
            foreach (Track track in tracks)
            {
                track.CurrentRating = _dbContext.Boats.Find(track.BoatId).Rating.CurrentRating;
            }


            if (tracks.Count == 0)
            {
                return new List<Result>();
            }

            if (averageBoat == -1 || averageBoat >= tracks.Count)
            {
                averageBoat = FindAverageBoat(tracks, B);
            }

            List<Result> results = CalculateResults(tracks, averageBoat, B);

            _dbContext.Results.AddRange(results);
            await _dbContext.SaveChangesAsync();
            return results;
        }


        // calculate results for a race using PHRF Time on Time
        // takes a list of tracks from a race and returns the results for those boats

        private static List<Result> CalculateResults(List<Track> tracks, int averageBoat, int B = 550)
        {
            List<Result> results = new List<Result>();

            if (tracks != null && tracks.Count > averageBoat && tracks[averageBoat].CurrentRating != null)
            {
                int A = (int)tracks[averageBoat].CurrentRating;
                if (A == 0)
                {
                    return results;
                }

                for (int i = 0; i < tracks.Count; i++)
                {
                    Track track = tracks[i];
                    results.Add(new Result());
                    results[i].BoatId = track.BoatId;
                    results[i].RaceId = track.RaceId;

                    if (track.Boat != null && track.Boat.Rating != null)
                    {

                        double TCF = A / (B + (double)track.CurrentRating);

                        results[i].ElapsedTime = TimeSpan.FromSeconds((track.Finished.Subtract(track.Started)).TotalSeconds);
                        results[i].CorrectedTime = TimeSpan.FromSeconds(results[i].ElapsedTime.TotalSeconds * TCF);
                    }
                    else //boat could not be retrived or it did not have a rating 
                    {
                        results[i].ElapsedTime = track.Started - track.Finished;
                        results[i].CorrectedTime = new TimeSpan(0);
                    }
                }

                results.Sort((r1, r2) => r1.CorrectedTime.CompareTo(r2.CorrectedTime));

                for (int i = 0; i < results.Count; i++)
                {

                    if (results[i].CorrectedTime.TotalSeconds > 1)
                    {
                        results[i].FinishPosition = i + 1;
                        results[i].Points = i + 1;
                        results[i].FinishType = FinishTypes[0];
                    }
                    else
                    {
                        results[i].FinishPosition = results.Count;
                        results[i].Points = results.Count + 1;
                        results[i].FinishType = FinishTypes[1];
                    }


                }
            }
            return results;
        }

        // find the boat that has a midpack rating with a respectible finish time
        //TODO: Implement
        private static int FindAverageBoat(List<Track> tracks, int B)
        {
            return 0;
        }

        private async Task<Race> GetRaceEntity(int id)
        {
            return await _dbContext.Races.FindAsync(id);
        }

    }
}