using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfHall.Models;
using ConfHall.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConfHall.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HallController : ControllerBase
    {

        private IHallService _hallService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hallService"></param>
        public HallController(IHallService hallService)
        {
            _hallService = hallService;

        }


        // GET: api/Hall
        /// <summary>
        /// Get all Hall records.
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet(Name = "Hall")]
        public IActionResult Get()
        {
            try
            {
                var HallList = _hallService.Get();
                if (HallList != null)
                {
                    return Ok(HallList);
                }
                else
                {
                    return BadRequest("There are no Halls.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Return Hall by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HallModel HallModel = _hallService.Get(id);
                    return Ok(HallModel);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return BadRequest(ModelState);
            }
        }

        // POST: api/Hall
        /// <summary>
        /// Create Hall
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost(Name ="create-hall")]
        public IActionResult Post([FromBody] HallModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Id = _hallService.Add(model);

                    if (!model.Id.Equals(Guid.Empty))
                        return Created(string.Empty, model);
                    else
                        return NoContent();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Update Hall.
        /// </summary>
        /// <param name="model">Hall model</param>
        /// <returns>IActionResult</returns>
        [HttpPut("{id}",Name = "update-hall")]
        public IActionResult Put([FromBody] HallModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _hallService.Update(model);
                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return BadRequest(ModelState);
            }
        }

        

        /// <summary>
        /// Delete a Hall by Id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}",Name ="delete-hall")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _hallService.Delete(id);
                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return BadRequest(ModelState);
            }
        }
    }
}
