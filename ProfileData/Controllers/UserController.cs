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

namespace ProfileData.Controlers
{
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
        public ActionResult<UserFullInfoDTO> Get(int id)
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
                var result = _service.Add(userDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

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
        public ActionResult<UserFullInfoDTO> Delete(int id)
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
