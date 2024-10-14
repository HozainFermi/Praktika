using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using Praktika.Db;
using Praktika.Models.Entitys;

namespace Praktika.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private int MAX_USERNAME_LEN = 100;
        private ApiDbContext context { get; set; }
        public UsersController(ApiDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        [Route("/GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await context.Users.ToListAsync());

        }

        [HttpGet]
        [Route("/GetUser/{id:Guid}")]
        [ActionName("GetUserById")]
        public async Task<IActionResult> GetUserById([FromRoute]Guid id) 
        {
            var user = await context.Users.FindAsync(id);
            if (user == null) { return BadRequest("Пользователь не найден"); }
            return Ok(user);
        }


        [HttpPost]
        [Route("/AddUser")]
        public async Task<IActionResult> AddUser([FromBody]UserEntity user)
        {
            if (user == null || user.Email==null || user.Password==null || user.Name==null)
            {
                return BadRequest("Не был передан user или одно из его полей");
            }
            else if(user.Name.Length>MAX_USERNAME_LEN){
                return BadRequest($"Имя пользователя не должно превышать {MAX_USERNAME_LEN}"); 
            }
            user.Id = Guid.NewGuid();
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return Ok(user);
        }


        [HttpPut]
        [Route("/PutUser")]
        public async Task<IActionResult> PutUser(UserEntity user)
        {
            var olduser = await context.Users.FindAsync(user.Id);
            if (olduser == null) { return BadRequest("Пользователь не найден"); }
            else
            {
                UserEntity newuser = new UserEntity();

                newuser.Email = user.Email;
                newuser.Password = user.Password;
                newuser.Name = user.Name;
                await context.Users.AddAsync(newuser);
                await context.SaveChangesAsync();
                return Ok(user);
            }
        }

        [HttpDelete]
        [Route("/DeleteUser/{Id:Guid}")]
        public async Task< IActionResult> DeleteUser([FromRoute]Guid Id) 
        {
            var user = await context.Users.FindAsync(Id);
            if (user == null) { return BadRequest("Пользователь не найден"); }
            else 
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
                return Ok(user);
            }
        
        }


    }
}
