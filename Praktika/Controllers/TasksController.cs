using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Praktika.Contracts;
using Praktika.Db;
using Praktika.Models;
using Praktika.Models.Entitys;
using Praktika.Services;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Praktika.Controllers
{
    [Route("[controller]")]
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
        [Route("/GetAllTasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            return Ok(await db.Tasks.ToListAsync());
        }

        [HttpGet]
        [Route("/GetTask/{Id}")]
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
        [Route("/AddTask")]
        public async Task<IActionResult> PostNewTask([FromBody]TasksEntity task)
        {

            task.Id = Guid.NewGuid();
            await db.Tasks.AddAsync(task);
            await db.SaveChangesAsync();

            return Ok(task);

        }

        [HttpPut]
        [Route("/ChangeTask/{id:Guid}")]
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

        [HttpPost]
        [Route("/ParsTask")]
         public IActionResult ParsRequest([FromBody]ParsRequestModel request )
        {

            List<string> response = parseService.Parse(request.SiteUrl, request.Selectors, request.SelectorsType);

            return Ok(response);
        }

        [HttpPost]
        [Route("/ExportCsv")]
        public async Task<IActionResult> ExportRequest(List<string> data)
        {



            return Ok(data);
        }

    }
}
