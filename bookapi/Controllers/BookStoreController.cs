using System;
using System.Net;
using bookapi.Models.Models;
using bookapi.Models.ViewModels;
using bookapi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bookapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookStoreController : ControllerBase
    {

        UserRepository _repository = new UserRepository();

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginModel model)
        {
            try
            {
                User user = _repository.Login(model);
                if (user == null)
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "User not found");

                return StatusCode(HttpStatusCode.OK.GetHashCode(), user);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterModel model)
        {
            try
            {
                User user = _repository.Register(model);
                if (user == null)
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), "Bad request");

                return StatusCode(HttpStatusCode.OK.GetHashCode(), user);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }
    }
}
