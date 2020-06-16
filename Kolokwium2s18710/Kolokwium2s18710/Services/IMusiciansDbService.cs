using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kolokwium2s18710.Models;

namespace Kolokwium2s18710.Services
{
    public interface IMusiciansDbService
    {
        public GetMusicianAndTracksModel GetMusicianAndTracks(int musicianId);

        public Musician InsertMusician(String firstname, string lastName, string nickname, TrackInsertModel track);


    }
}
