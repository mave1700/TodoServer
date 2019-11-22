using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace TodoServer.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public UserController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _repository.User.GetAllUsers();
                _logger.LogInfo($"Returned all users from database.");

                var usersResult = _mapper.Map<IEnumerable<UserDto>>(users);
                return Ok(usersResult);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllUsers action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "UserById")]
        public IActionResult GetUserById(Guid id)
        {
            try
            {
                var user = _repository.User.GetUserById(id);

                if (user == null)
                {
                    _logger.LogError($"User with id: {id} hasn't been found in database");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned user with id: {id}");

                    var userResult = _mapper.Map<UserDto>(user);
                    return Ok(userResult);
                }
            }

            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetUserById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }


        }

        [HttpGet("username/{userName}", Name = "UserByUsername")]
        public IActionResult GetUserByUsername(string userName)
        {
            try
            {
                var user = _repository.User.GetUserByUsername(userName);

                if (user == null)
                {
                    _logger.LogError($"User with user name: {userName} hasn't been found in database");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned user with user name: {userName}");

                    var userResult = _mapper.Map<UserDto>(user);
                    return Ok(userResult);
                }
            }

            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetUserByUsername action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }


        }

        [HttpGet("{id}/account")]
        public IActionResult GetUserWithDetails(Guid id)
        {
            try
            {
                var user = _repository.User.GetUserWithDetails(id);

                if (user == null)
                {
                    _logger.LogError($"User with id: {id} was not found in database");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned user with details for id: {id}");

                    var userResult = _mapper.Map<UserDto>(user);
                    return Ok(userResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetUserWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody]UserForCreationDto user)
        {
            try
            {
                if (user == null)
                {
                    _logger.LogError("User object sent from client is null");
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid user object sent from client");
                    return BadRequest("Invalid model object");
                }

                var userEntity = _mapper.Map<User>(user);

                _repository.User.CreateUser(userEntity);
                _repository.Save();

                var createdUser = _mapper.Map<UserDto>(userEntity);

                return CreatedAtRoute("UserById", new { id = createdUser.Id }, createdUser);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateUser action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(Guid id, [FromBody]UserForUpdateDto user)
        {
            try
            {
                if (user == null)
                {
                    _logger.LogError("User object sent from client is null.");
                    return BadRequest("User object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid user object sent from client");
                    return BadRequest("Invalid model object");
                }

                var userEntity = _repository.User.GetUserById(id);
                if(userEntity == null)
                {
                    _logger.LogError($"User with id: {id}, was not found in database.");
                    return NotFound();
                }

                _mapper.Map(user, userEntity);

                _repository.User.UpdateUser(userEntity);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateUser action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            try
            {
                var user = _repository.User.GetUserById(id);
                if (user == null)
                {
                    _logger.LogError($"User with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.User.DeleteUser(user);
                _repository.Save();

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteUser action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
