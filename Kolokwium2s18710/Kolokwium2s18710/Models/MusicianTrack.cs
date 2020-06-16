using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2s18710.Models
{
    public class MusicianTrack
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMusicianTrack { get; set; }

        [ForeignKey("Track")]
        public int IdTrack { get; set; }

        [ForeignKey("Musician")]
        public int IdMusician { get; set; }


        public virtual Track track { get; set; }

        public virtual Musician musician { get; set; }
    }
}
