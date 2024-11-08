using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TemplateApiDb.Data.Contexts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TemplateApiDb.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersDbContext _context;
        private readonly IMapper _mapper;

        public UsersController(UsersDbContext context, IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(nameof(context));
            ArgumentNullException.ThrowIfNull(nameof(mapper));

            _context = context;
            _mapper = mapper;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            List<DTO.User>? users = await _context.Users?.ProjectTo<DTO.User>(_mapper.ConfigurationProvider).ToListAsync();
            // List<Domain.Entities.User>? users = await _context.Users.ToListAsync();
            return users?.Count == 0 ? NotFound() : Ok(users);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            DTO.User? user = await _context?.Users?.ProjectTo<DTO.User>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(u => u.Id == id);
            return user is null ? NotFound() : Ok(user);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] DTO.User newUser)
        {
            Domain.Entities.User user = _mapper.Map<Domain.Entities.User>(newUser);
            _ = await _context.Users.AddAsync(user);
            _ = _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAsync), new { id = user.Id }, user);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] DTO.User updatedUser)
        {
            if (id != updatedUser?.Id)
            {
                return BadRequest();
            }

            Domain.Entities.User user = _mapper.Map<Domain.Entities.User>(updatedUser);
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                _ = await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Users.AnyAsync(u => u.Id == user.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Domain.Entities.User? user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _ = _context.Users.Remove(user);
            _ = await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
