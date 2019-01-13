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
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {

        private IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;

        }


        // GET: api/Hall
        /// <summary>
        /// Get all vehicle records.
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet(Name = "Hall")]
        public IActionResult Get()
        {
            try
            {
                var ReservationList = _reservationService.Get();
                if (ReservationList != null)
                {
                    return Ok(ReservationList);
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
        /// Return Reservation by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(Guid id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ReservationModel ReservationModel = _reservationService.Get(id);
                    return Ok(ReservationModel);
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
        /// Create Reservation
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost(Name ="create-reservation")]
        public IActionResult Post([FromBody] ReservationModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Id = _reservationService.Add(model);

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
        /// Update Reservation.
        /// </summary>
        /// <param name="model">Reservation model</param>
        /// <returns>IActionResult</returns>
        [HttpPut("{id}",Name = "update-reservation")]
        public IActionResult Put([FromBody] ReservationModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _reservationService.Update(model);
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
        /// Delete a Reservation by Id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}",Name ="delete-reservation")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _reservationService.Delete(id);
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
