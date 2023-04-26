using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<AvatarDTO> Get(int id)
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
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    AvatarDTO avatar;
                    using (MemoryStream stream = new MemoryStream())
                    {
                        file.CopyTo(stream);
                        byte[] imageByteArray = stream.ToArray();
                        Image image = Image.Load(imageByteArray);
                        image.Mutate(x => x.Resize(256, 256));
                        MemoryStream lightStream = new MemoryStream();
                        image.SaveAsJpeg(lightStream);
                        avatar = _service.Add(new AvatarDTO { Image = lightStream.ToArray() });
                    }
                    return Ok(new { avatar });
                }
                else
                {
                    return BadRequest();
                }
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


        [HttpDelete("/Avatar/delete/{id}")]
        public ActionResult<AvatarDTO> Delete(int id)
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