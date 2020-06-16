using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Kolokwium2s18710.Models;

namespace Kolokwium2s18710.Services
{
    public class MusiciansService : IMusiciansDbService
    {
        private readonly MusicDistributionDbContext _context;
        public MusiciansService(MusicDistributionDbContext context)
        {
            _context = context;
        }
        public GetMusicianAndTracksModel GetMusicianAndTracks(int musicianId)
        {
            var musicianFind = _context.Musicians.FirstOrDefault(m => m.IdMusician == musicianId);
            if(musicianFind != null)
            {
                GetMusicianAndTracksModel getMusicianAndTracks = new GetMusicianAndTracksModel();
                getMusicianAndTracks.muscian = musicianFind;

                var musicianTracksIds = _context.MusicianTracks.Where(m => m.IdMusician == musicianId).ToList();
                if(musicianTracksIds != null)
                {
                    List<Track> musiciansTracks= new List<Track>();
                    musicianTracksIds.ForEach(tId =>
                    {
                        var getTrackFromTracks = _context.Tracks.FirstOrDefault(t => t.IdTrack == tId.IdTrack);
                        musiciansTracks.Add(getTrackFromTracks);
                    });
                    getMusicianAndTracks.tracks = musiciansTracks;
                }
                else
                {
                    getMusicianAndTracks.tracks = null;
                }

                return getMusicianAndTracks;
            }
            else
            {
                return null;
            }
        }

        public Musician InsertMusician(string firstname, string lastName, string nickname, TrackInsertModel track)
        {
            // if lastname or firstname are empty return an empty musician to signal a bad request
            if (firstname == "" || lastName == "" || firstname == null || lastName == null)
            {
                return new Musician();
            }
                

            using (TransactionScope tranScope = new TransactionScope())
            {
                try
                {
                    var getNewestId = _context.Musicians.Max(m => m.IdMusician);
                    Musician newMusician = new Musician();
                    newMusician.FirstName = firstname;
                    newMusician.LastName = lastName;
                    newMusician.IdMusician = getNewestId + 1;

                    if(nickname != null)
                    {
                        newMusician.NickName = nickname;
                    }

                    //if track variables are not null proceed to add musician to track
                    if(track.trackName != null && track.trackName != "" && track.duration != 0)
                    {
                        var CheckIfTrackExists = _context.Tracks.Where(t => (t.TrackName == track.trackName) && (t.Duration == track.duration)).FirstOrDefault();

                        // if track doesnt exist create a new track
                        if(CheckIfTrackExists == null)
                        {
                            var getHighestTrackId = _context.MusicianTracks.Max(t => t.IdTrack);
                            Track newTrack = new Track();
                            newTrack.IdTrack = getHighestTrackId + 1;
                            newTrack.TrackName = track.trackName;
                            newTrack.Duration = track.duration;

                            _context.Tracks.Add(newTrack);

                            CheckIfTrackExists = newTrack;
                        }

                        //add track to musiciantrack table
                        var getHighiestMusicianTrackId = _context.MusicianTracks.Max(mt => mt.IdMusicianTrack);
                        getHighiestMusicianTrackId++;

                        MusicianTrack newMuscianTrack = new MusicianTrack();
                        newMuscianTrack.IdMusicianTrack = getHighiestMusicianTrackId;
                        newMuscianTrack.IdTrack = CheckIfTrackExists.IdTrack;
                        newMuscianTrack.IdMusician = newMusician.IdMusician;

                        _context.MusicianTracks.Add(newMuscianTrack);
                    }
                    //add musciian to database and complete the transaction
                    _context.Musicians.Add(newMusician);
                    tranScope.Complete();
                    return newMusician;
                }
                catch (Exception ex) {
                    return null;
                }
            }
        }
    }
}
