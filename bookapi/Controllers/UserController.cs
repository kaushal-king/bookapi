

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
    public class UserController : ControllerBase
    {

        UserRepository _repository=new UserRepository();

        [HttpGet]
        [Route("Getusers")]
        public IActionResult Getuser()
        {
            return Ok(_repository.GetUsers());
        }

      



        [HttpGet]
        [Route("list")]
        public IActionResult GetUsers(int pageIndex = 1, int pageSize = 10, string keyword = "")
        {
            try
            {
                var users = _repository.GetUsersPaging(pageIndex, pageSize, keyword);
                if (users == null)
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please provide correct information");

                return StatusCode(HttpStatusCode.OK.GetHashCode(), users);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }



        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUser(int id)
        {
            try
            {
                var user = _repository.GetUser(id);
                if (user == null)
                    return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please provide correct information");

                return StatusCode(HttpStatusCode.OK.GetHashCode(), user);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }



        [HttpPut]
        [Route("update")]
        public IActionResult UpdateUser(UserModel model)
        {
            try
            {
                if (model != null)
                {
                    var user = _repository.GetUser(model.Id);
                    if (user == null)
                        return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "User not found");

                    user.Firstname = model.Firstname;
                    user.Lastname = model.Lastname;
                    user.Email = model.Email;

                    var isSaved = _repository.UpdateUser(user);
                    if (isSaved)
                    {
                        return StatusCode(HttpStatusCode.OK.GetHashCode(), "User detail updated successfully");
                    }
                }

                return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please provide correct information");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                if (id > 0)
                {
                    var user = _repository.GetUser(id);
                    if (user == null)
                        return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "User not found");

                    var isDeleted = _repository.DeleteUser(user);
                    if (isDeleted)
                    {
                        return StatusCode(HttpStatusCode.OK.GetHashCode(), "User detail deleted successfully");
                    }
                }

                return StatusCode(HttpStatusCode.NotFound.GetHashCode(), "Please provide correct information");
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), ex.Message);
            }
        }









        /*[HttpPost]
        [Route("login")]
        public IActionResult Login(LoginModel loginModel)
        {


            User user = _repository.Login(loginModel);
            if (user == null)
                return NotFound();

            return Ok(user);
        }*/


        /*[HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterModel registerModel)
        {


            User user = _repository.Register(registerModel);
            if (user == null)
                return BadRequest();

            return Ok(user);
        }*/


    }
}
