using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Domain.DTO;
using ProfileData.Domain.Abstractions.Services;

namespace ProfileData.Controlers
{
    [ApiController]
    [Route("[controller]")]
    public class AvatarController : Controller
    {
        private readonly IAvatarService _service;

        public AvatarController(IAvatarService avatarService)
        {
            _service = avatarService;
        }

        [HttpGet]
        public ActionResult<List<AvatarDTO>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<AvatarDTO> Get(Guid id)
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

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Upload(IFormFile file)
        {
            try
            {
                return Ok(_service.Upload(file));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error. {ex.Message}");
            }
        }



        [HttpPut]
        public ActionResult Update(AvatarDTO avatarDTO)
        {
            try
            {
                return Ok(_service.Update(avatarDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        public ActionResult<AvatarDTO> Delete(Guid id)
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