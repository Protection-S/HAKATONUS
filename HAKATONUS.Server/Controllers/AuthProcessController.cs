using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using HAKATONUS.Server.Models;
using HAKATONUS.Server.Data;

namespace HAKATONUS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthProcessController : ControllerBase
    {
        private readonly DBcontext _context;

        public AuthProcessController(DBcontext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Users user)
        {
            if (await _context.User.AnyAsync(u => u.Email == user.Email))
            {
                return BadRequest("Пользователь с таким email уже существует");
            }

            user.Password = HashPassword(user.Password);

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Пользователь успешно зарегистрирован");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Users user)
        {
            var existingUser = await _context.User.FirstOrDefaultAsync(u => u.Email == user.Email);

            if (existingUser == null || existingUser.Password != HashPassword(user.Password))
            {
                return BadRequest("Неверный email или пароль");
            }


            return Ok("Авторизация успешна");
        }

        private string HashPassword(string password)
        {
            return password; 
        }

        [HttpGet("getUserData/{email}")]
        public async Task<IActionResult> GetUserData(string email)
        {
            var existingUser = await _context.User.FirstOrDefaultAsync(u => u.Email == email);

            if (existingUser == null)
            {
                return NotFound("Пользователь не найден");
            }

            return Ok(existingUser);
        }

        [HttpPut("updatePassword/{email}")]
        public async Task<IActionResult> UpdatePassword(string email, UpdatePasswordRequest request)
        {
            var existingUser = await _context.User.FirstOrDefaultAsync(u => u.Email == email);

            if (existingUser == null)
            {
                return NotFound("Пользователь не найден");
            }

            existingUser.Password = HashPassword(request.NewPassword);

            _context.Entry(existingUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok("Пароль успешно обновлен");
        }

        public class UpdatePasswordRequest
        {
            public string NewPassword { get; set; }
        }



        [HttpGet("takeData")]
        public async Task<IActionResult> TakeData(int page = 1, int pageSize = 4)
        {
            var events = await _context.Event
                .OrderBy(e => e.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(events);
        }


        [HttpPost("addCampaign")]
        public async Task<IActionResult> AddCampaign(ProjectInfo campaign)
        {
            _context.Event.Add(campaign);
            await _context.SaveChangesAsync();

            return Ok("Кампания успешно добавлена");
        }



        [HttpPost("addPerson")]
        public async Task<IActionResult> AddPerson([FromBody] Person newPerson)
        {
            _context.Persons.Add(newPerson);
            await _context.SaveChangesAsync();

            return Ok("Person successfully added");
        }

        [HttpGet("getAllPersons")]
        public async Task<IActionResult> GetAllPersons()
        {
            var persons = await _context.Persons.ToListAsync();
            return Ok(persons);
        }

        [HttpPost("addTeam")]
        public async Task<IActionResult> AddTeam([FromBody] Team newTeam)
        {
            _context.Teams.Add(newTeam);
            await _context.SaveChangesAsync();

            return Ok("Team successfully added");
        }

        [HttpGet("getAllTeams")]
        public async Task<IActionResult> GetAllTeams()
        {
            var teams = await _context.Teams.ToListAsync();
            return Ok(teams);
        }

        [HttpGet("getAllStartups")]
        public async Task<IActionResult> GetAllStartups()
        {
            var startups = await _context.Startups.ToListAsync();
            return Ok(startups);
        }

        [HttpPost("addStartup")]
        public async Task<IActionResult> AddStartup([FromBody] Startup newStartup)
        {
            _context.Startups.Add(newStartup);
            await _context.SaveChangesAsync();

            return Ok("Startup successfully added");
        }


        [HttpPost("addInvestor")]
        public async Task<IActionResult> AddInvestor([FromBody] Investors investor)
        {
            _context.Investors.Add(investor);
            await _context.SaveChangesAsync();
            return Ok("Инвестор успешно добавлен");
        }

        [HttpGet("getAllInvestors")]
        public async Task<IActionResult> GetAllInvestors()
        {
            var investors = await _context.Investors.ToListAsync();
            return Ok(investors);
        }


    }
}
