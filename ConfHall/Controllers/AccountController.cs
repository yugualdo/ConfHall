namespace ConfHall.Controllers
{
    using ConfHall.Domain.Services;
    using ConfHall.Model;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;

    /// <summary>
    /// This class is used as an api for the search requests.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private IAccountService accountsService;
        private IUserService userService;

        public AccountController(IAccountService accountsService, IUserService userService)
        {
            this.accountsService = accountsService;
            this.userService = userService;
        }

        /// <summary>
        /// Login endpoint
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Login", Name ="Login")]
        public IActionResult Post([FromBody] AccountModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string remoteIpAddreess = this.Request.HttpContext.Connection.RemoteIpAddress.ToString();
                    var response = this.accountsService.PasswordSignInAsync(model, remoteIpAddreess);
                    if (response.Result != null)
                    {
                        return Ok(response.Result);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "User or Password invalid");
                        return BadRequest(ModelState);
                    }
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
        /// Register endpoint
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{UserModel}", Name ="create-user")]
        public IActionResult Post([FromBody] UserModel userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    userModel.Id = this.userService.Add(userModel);

                    if (!userModel.Id.Equals(Guid.Empty))
                        return Created(string.Empty, userModel);
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
        /// Update endpoint
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromBody] UserModel userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this.userService.Update(userModel);
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
        /// Get User endpoint
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("{Guid}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = this.userService.Get(id);

                    if (user != null)
                        return Ok(user);
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
        /// LogOut Endpoint
        /// </summary>
        /// <returns></returns>
        [HttpGet("Logout")]
        [Authorize]
        public IActionResult Logout()
        {
            try
            {
                this.accountsService.Logout();
            }
            catch (Exception) { }

            return Ok();
        }
    }
}
