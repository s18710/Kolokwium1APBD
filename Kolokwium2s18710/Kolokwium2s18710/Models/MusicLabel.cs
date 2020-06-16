using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2s18710.Models
{
    public class MusicLabel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMusicLabel { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}
