using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2s18710.Models
{
    public class Track
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int IdTrack { get; set; }

        [MaxLength(20)]
        public string TrackName { get; set; }

        public float Duration { get; set; }

        [ForeignKey("Album")]
        public int? IdMusicAlbum { get; set; }


    }
}
