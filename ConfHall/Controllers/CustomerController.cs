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
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerService"></param>
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;

        }


        // GET: api/Hall
        /// <summary>
        /// Get all Customer records.
        /// </summary>
        /// <returns>IActionResult</returns>

        [HttpGet(Name = "Customer")]
        public IActionResult Get()
        {
            try
            {
                var CustomerList = _customerService.Get();
                if (CustomerList != null)
                {
                    return Ok(CustomerList);
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
        /// Return Customer by Id
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
                    CustomerModel HallModel = _customerService.Get(id);
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
        /// Create Customer
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost(Name = "create-customer")]
        public IActionResult Post([FromBody] CustomerModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Id = _customerService.Add(model);

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
        /// Update Customer.
        /// </summary>
        /// <param name="model">Customer model</param>
        /// <returns>IActionResult</returns>
        [HttpPut("{id}", Name = "update-customer")]
        public IActionResult Put([FromBody] CustomerModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _customerService.Update(model);
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
        /// Delete a Customer by Id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}", Name = "delete-customer")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _customerService.Delete(id);
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
