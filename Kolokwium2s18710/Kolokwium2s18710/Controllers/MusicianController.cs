using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kolokwium2s18710.Models;
using Kolokwium2s18710.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium2s18710.Controllers
{
    [Route("api")]
    [ApiController]
    public class MusicianController : ControllerBase
    {
        private readonly IMusiciansDbService _context;

        public MusicianController(IMusiciansDbService context)
        {
            _context = context;
        }

        [HttpGet("/musicians/{id}")]
        public IActionResult GetMusicianInfoAndTracks(int musicianId)
        {
            if (_context.GetMusicianAndTracks(musicianId) != null) {
                return Ok(_context.GetMusicianAndTracks(musicianId));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("/musicians")]
        public IActionResult InsertMusician(String firstname, string lastName, string nickname, TrackInsertModel track)
        {
            Musician musician = _context.InsertMusician(firstname, lastName, nickname, track);
            if(musician != null)
            {
                return Ok(musician);
            }else if(musician.FirstName == null || musician.LastName == null)
            {
                return BadRequest();
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}