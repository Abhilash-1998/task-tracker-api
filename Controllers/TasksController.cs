using Microsoft.AspNetCore.Mvc;
using TaskPlannerAPI.Models;

namespace TaskPlannerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private static List<TaskItem> tasks = new();
        private static int idCounter = 1;

        [HttpGet]
        public ActionResult<IEnumerable<TaskItem>> GetAll() => Ok(tasks);

        [HttpPost]
        public ActionResult<TaskItem> Create(TaskItem task)
        {
            task.Id = idCounter++;
            tasks.Add(task);
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        [HttpGet("{id}")]
        public ActionResult<TaskItem> GetById(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            return task == null ? NotFound() : Ok(task);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return NotFound();
            tasks.Remove(task);
            return NoContent();
        }
    }
}
