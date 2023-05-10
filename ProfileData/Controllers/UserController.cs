using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Domain.Abstractions.Services;
using Domain.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;


namespace ProfileData.Controlers
{
    //[Authorize(Policy = "ControllerAccessPolicy")]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService userService)
        {
            _service = userService;
        }

        [HttpGet]
        public ActionResult<List<UserShortInfoDTO>> GetAll()
        {
            return Ok(_service.GetAllShortInfo());
        }

        [HttpGet("{id}")]
        public ActionResult<UserFullInfoDTO> Get(Guid id)
        {
            try
            {
                var result = _service.Get(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [EnableCors("_myAllowSpecificOrigins")]
        [HttpPost]
        public ActionResult<UserFullInfoDTO> Insert([FromBody] UserFullInfoDTO userDTO)
        {
            try
            {
                // hash the user's password
                string hashedPassword = HashPassword(userDTO.HashedPassword);

                var result = _service.Add(userDTO, hashedPassword);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string HashPassword(string password)
        {
            // TODO: implement password hashing
            return password;
        }


        //[EnableCors("_myAllowSpecificOrigins")]
        //[HttpPost]
        //public ActionResult<UserFullInfoDTO> Insert([FromBody] UserFullInfoDTO userDTO)
        //{
        //    try
        //    {
        //        // hash the user's password
        //        string hashedPassword = HashPassword(userDTO.Password);

        //        var result = _service.Add(userDTO, hashedPassword);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //private string HashPassword(string password)
        //{
        //    // TODO: implement password hashing
        //    return password;
        //}



        //[AllowAnonymous]
        //[HttpPost("register")]
        //public IActionResult Register([FromBody] UserRegistrationDTO userRegistrationDTO)
        //{
        //    try
        //    {
        //        if (_service.UsernameExists(userRegistrationDTO.Username))
        //        {
        //            return BadRequest("Username already exists");
        //        }

        //        // Hash the password using SHA256
        //        var sha256 = SHA256.Create();
        //        var hashedPassword = sha256.ComputeHash(Encoding.UTF8.GetBytes(userRegistrationDTO.Password));
        //        userRegistrationDTO.HashedPassword = hashedPassword;

        //        var userDTO = _service.Add(_mapper.Map<UserFullInfoDTO>(userRegistrationDTO));
        //        return Ok(userDTO);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPut]
        public IActionResult Update(UserFullInfoDTO userDTO)
        {
            try
            {
                var result = _service.Update(userDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        public ActionResult<UserFullInfoDTO> Delete(Guid id)
        {

            try
            {
                _service.Remove(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }

        }

    }
}
