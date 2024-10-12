using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Praktika.Contracts;
using Praktika.Db;
using Praktika.Models.Entitys;
using Praktika.Services;
using System.Runtime.CompilerServices;

namespace Praktika.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private ApiDbContext db;
        private IParseService parseService;
        public TasksController(ApiDbContext _context, IParseService parseservice)
        {
            db= _context;
            this.parseService= parseservice;
        }

        [HttpGet]
        [Route("/GetAll")]
        public async Task<IActionResult> GetAllTasks()
        {
            return Ok(await db.Tasks.ToListAsync());
        }

        [HttpGet]
        [Route("/Get/{Id}")]
        public async Task<IActionResult> GetTaskById([FromRoute]int Id)
        {
            var task = await db.Tasks.FindAsync(Id);
            if (task == null)
            {
                return BadRequest("Задача не найдена");
            }
            return Ok(task);
        }


        [HttpPost]
        [Route("/Add")]
        public async Task<IActionResult> PostNewTask([FromBody]TasksEntity task)
        {

            task.Id = Guid.NewGuid();
            await db.Tasks.AddAsync(task);
            await db.SaveChangesAsync();

            return Ok(task);

        }

        [HttpPut]
        [Route("/Change/{id:Guid}")]
        public async Task<IActionResult> UpdateTask([FromRoute] Guid id, [FromBody] TasksEntity Updatetask)
        {
            var existingTask = await db.Tasks.FindAsync(id);
            if (existingTask == null)
            {
                return BadRequest("Задача не найдена");
            }
           


            existingTask.NumberOfLines = Updatetask.NumberOfLines;

            await db.Tasks.AddAsync(existingTask);   
            await db.SaveChangesAsync();
            return Ok();



        }

    }
}
