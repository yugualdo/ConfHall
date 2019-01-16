namespace ConfHall.Controllers
{
    using System;
    using ConfHall.Models;
    using ConfHall.Services;
    using Microsoft.AspNetCore.Mvc;


    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize] 
    public class ReservationController : ControllerBase
    {

        private IReservationService _reservationService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reservationService"></param>
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;

        }

        /// <summary>
        /// Get all Reservation records.
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet(Name = "Reservation")]
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
                    return BadRequest("There are no Reservations.");
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
        [HttpGet("{id}")]
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
        [HttpPost(Name = "create-reservation")]
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
        [HttpPut("{id}", Name = "update-reservation")]
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
        [HttpDelete("{id}", Name = "delete-reservation")]
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

        /// <summary>
        /// Get last 10 Reservation records from given Customer Id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("top", Name = "top-ten")]
        public IActionResult Top(Guid customerId)
        {
            try
            {
                var ReservationList = _reservationService.Top(customerId);
                if (ReservationList != null)
                {
                    return Ok(ReservationList);
                }
                else
                {
                    return BadRequest("There are no Reservations for this Customer.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Get all unconfirmed reservations
        /// </summary>
        /// <returns></returns>
        [HttpGet("unconfirmed", Name = "unconfirmed")]
        public IActionResult GetUnconfirmed()
        {
            try
            {
                var ReservationList = _reservationService.GetUnconfirmed();
                if (ReservationList != null)
                {
                    return Ok(ReservationList);
                }
                else
                {
                    return BadRequest("There are no Unconfirmed Reservations.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Get all pending payment reservations
        /// </summary>
        /// <returns></returns>
        [HttpGet("pendingpayment", Name = "pendingpayment")]
        public IActionResult GetPendingPayment()
        {
            try
            {
                var ReservationList = _reservationService.GetPendingPayment();
                if (ReservationList != null)
                {
                    return Ok(ReservationList);
                }
                else
                {
                    return BadRequest("There are no pending payment Reservations.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Confirm a reservation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("confirm/{id}")]
        public IActionResult Confirm(Guid id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _reservationService.Confirm(id);
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
        /// Confirm a reservation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("pay/{id}")]
        public IActionResult Pay(Guid id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _reservationService.Pay(id);
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
