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
        private IAccountService _accountsService;
        private IUserService _userService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountsService"></param>
        /// <param name="userService"></param>
        public AccountController(IAccountService accountsService, IUserService userService)
        {
            _accountsService = accountsService;
            _userService = userService;
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
                    var response = _accountsService.PasswordSignInAsync(model, remoteIpAddreess);
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
        public IActionResult Post([FromBody] UserModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Id = _userService.Add(model);

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
        /// Update endpoint
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromBody] UserModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userService.Update(model);
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
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{Guid}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _userService.Get(id);

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
                _accountsService.Logout();
            }
            catch (Exception) { }

            return Ok();
        }
    }
}
