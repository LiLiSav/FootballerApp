using FootballerApp.Models;
using FootballerApp.Models.DTOs.Incoming;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FootballerApp.Models.DTOs.Outcoming;

namespace FootballerApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayersController : ControllerBase
    {

        private readonly ILogger<PlayersController> _logger;
        private static List<Player> players = new List<Player>();
        private readonly IMapper _mapper;

        public PlayersController(ILogger<PlayersController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        //Get all Football players
        [HttpGet]
        public IActionResult GetPlayers()
        {
            var allPlayers = players.Where(x => x.Status == 1).ToList();

            // transform from entire list to new list with map
            var _allPlayers = _mapper.Map<IEnumerable<PlayerDto>>(allPlayers);
            return Ok(_allPlayers);
        }

        [HttpPost]
        public IActionResult CreatePlayer(PlayerCreationDto player)
        {
            if (ModelState.IsValid)
            {
                // in mapper, create an object of type Player and map the values from the data option
                var _player = _mapper.Map<Player>(player);

                players.Add(_player);

                var newDriver = _mapper.Map<PlayerDto>(_player);
                return CreatedAtAction("GetPlayer", new { _player.Id }, newDriver);
            }
            return BadRequest("Something went wrong");
        }

        [HttpGet("{id}")]
        public IActionResult GetPlayer(Guid id)
        {
            var player = players.FirstOrDefault(x => x.Id == id);

            if (player == null)
            {
                return NotFound();
            }
            var _player = _mapper.Map<PlayerDto>(player);
            return Ok(_player);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePlayer(Guid id, Player player)
        {
            if(id != player.Id)
            {
                return BadRequest();
            }
            var existingPlayer = players.FirstOrDefault(x => x.Id == player.Id );

            if(existingPlayer == null)
            {
                return NotFound();
            }

            existingPlayer.FirstName = player.FirstName;
            existingPlayer.LastName = player.LastName;
            existingPlayer.PlayerNumber = player.PlayerNumber;
            existingPlayer.Status = player.Status;
            existingPlayer.AllIrelands = player.AllIrelands;
            existingPlayer.DateUpdated = new DateTime();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePlayer(Guid id)
        {
            var existingPlayer = players.FirstOrDefault(y => y.Id == id);
            if (existingPlayer == null)
            {
                return NotFound();
            }

            existingPlayer.Status = 0;

            return NoContent();
        }
    }
}
