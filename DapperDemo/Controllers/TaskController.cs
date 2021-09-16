using DapperDemo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepo;

        public TaskController(ITaskRepository taskRepo)
        {
            _taskRepo = taskRepo;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetTasksAsync()
        {
            return Ok(await _taskRepo.GetTasksAsync());
        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetTaskByIdAsync(int Id)
        {
            return Ok(await _taskRepo.GetTaskByIdAsync(Id));
        }

        [HttpGet("TaskCounter")]
        public async Task<IActionResult> GetTaskAndCounter()
        {
            return Ok(await _taskRepo.GetTaskAndCounter());
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateTaskAsync(Data.Task task)
        {
            return Ok(await _taskRepo.SaveAsync(task));
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateTaskStatusAsync(Data.Task task)
        {
            return Ok(await _taskRepo.UpdateTaskStatusAsync(task));
        }

        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteTaskAsync(int Id)
        {
            return Ok(await _taskRepo.DeleteAsync(Id));
        }

    }
}
