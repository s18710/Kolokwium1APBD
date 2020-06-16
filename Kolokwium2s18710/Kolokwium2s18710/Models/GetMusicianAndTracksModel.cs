using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2s18710.Models
{
    public class GetMusicianAndTracksModel
    {
        public Musician muscian { get; set; }

        public IEnumerable<Track> tracks { get; set; }
    }
}
