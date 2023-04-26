using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Abstractions.Services;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ProfileData.Controlers
{

    [ApiController]
    [Route("[controller]")]
    public class RoleController : Controller
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService roleService)
        {
            _service = roleService;
        }

        [HttpGet]
        public ActionResult<List<RoleDTO>> GetAll()
        {
            var result = _service.GetAll();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public ActionResult<RoleDTO> Get(int id)
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

        [HttpPost]
        public ActionResult<RoleDTO> Insert(RoleDTO roleDTO)
        {
            try
            {
                var result = _service.Add(roleDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public ActionResult Update(RoleDTO roleDTO)
        {
            try
            {
                var result = _service.Update(roleDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        public ActionResult<RoleDTO> Delete(int id)
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