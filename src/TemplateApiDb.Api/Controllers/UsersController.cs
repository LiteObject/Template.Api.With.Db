using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TemplateApiDb.Data.Contexts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TemplateApiDb.Api.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<IActionResult> GetAsync([FromQuery] IDictionary<string, string> searchCriteria)
        {
            bool predicate(Domain.Entities.User u)
            {
                return BuildPredicate(u, searchCriteria);
            }

            Domain.Entities.User[]? users = _context.Users?.Where(predicate).ToArray();

            if (users?.Length == 0)
            {
                return NotFound();
            }

            DTO.User[] userDtos = _mapper.Map<DTO.User[]>(users);

            return Ok(userDtos);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Domain.Entities.User? user = await _context?.Users?.FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
            {
                return NotFound();
            }

            DTO.User userDto = _mapper.Map<DTO.User>(user);

            return Ok(userDto);
        }

        // POST api/<UsersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private static bool BuildPredicate(Domain.Entities.User user, IDictionary<string, string> searchCriteria)
        {
            foreach (KeyValuePair<string, string> kvp in searchCriteria)
            {
                PropertyInfo? propertyInfo = user.GetType().GetProperty(kvp.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                string? propertyValue = propertyInfo?.GetValue(user)?.ToString();

                string value = kvp.Value;

                if (propertyValue != value)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
