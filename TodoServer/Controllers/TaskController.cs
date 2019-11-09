using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace TodoServer.Controllers
{
    [Route("api/task")]
    [ApiController]
    public class TaskController : ControllerBase
    {

        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public TaskController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{userId}/{taskId}", Name = "TaskDetails")]
        public IActionResult GetTaskDetails(Guid userId, int taskId)
        {
            try
            {
                var task = _repository.Task.GetTaskById(taskId);

                if (task == null)
                {
                    _logger.LogError($"User with id: {taskId} hasn't been found in database");
                    return NotFound();
                }
                else if (task.UserId.Equals(userId))
                {
                    _logger.LogInfo($"Returned task with id: {taskId}");

                    var taskResult = _mapper.Map<TaskDto>(task);
                    return Ok(taskResult);
                }
                else
                {
                    _logger.LogError($"There is no task with id {taskId} that matches {userId} in database");
                    return NotFound();
                }

            }

            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetTaskById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }


        }

        [HttpPost]
        public IActionResult CreateTask ([FromBody]TaskForCreationDto task)
        {
            try
            {

                if (task == null)
                {
                    _logger.LogError("Task object sent from client is null");
                    return BadRequest("Task object is null");
                }

                var user = _repository.User.GetUserById(task.UserId);

                if(user == null)
                {
                    _logger.LogError($"User with id {task.UserId} was not found in database");
                    return BadRequest("Invalid user id");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid user object sent from client");
                    return BadRequest("Invalid model object");
                }

                var taskEntity = _mapper.Map<Task>(task);

                _repository.Task.CreateTask(taskEntity);
                _repository.Save();

                var createdTask = _mapper.Map<TaskDto>(taskEntity);

                return CreatedAtRoute("TaskDetails", new { userId=user.Id,taskId = createdTask.Id }, createdTask);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateTask action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{userId}/{taskId}")]
        public IActionResult UpdateTask(Guid userId, int taskId, [FromBody]TaskForUpdateDto task)
        {
            try
            {
                if (task == null)
                {
                    _logger.LogError("Task object sent from client is null");
                    return BadRequest("Task object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid user object sent from client");
                    return BadRequest("Invalid model object");
                }

                var taskEntity = _repository.Task.GetTaskById(taskId);

                if (taskEntity == null)
                {
                    _logger.LogError($"Task with id: {taskId}, was not found in database.");
                    return NotFound();
                }

                if (!taskEntity.UserId.Equals(userId))
                {
                    _logger.LogError($"There is no task with id {taskId} that matches {userId} in database");
                    return NotFound();
                }

                _mapper.Map(task, taskEntity);

                _repository.Task.UpdateTask(taskEntity);
                _repository.Save();

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateTask action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }




    }
}