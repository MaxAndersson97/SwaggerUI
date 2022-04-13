using Swagger.Models;
using Swagger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System;

namespace Swagger.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class UserController
    {
        private UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }
        //[Authorize]
        [HttpGet]
        public JsonResult GetUsers() => _userService.GetUsers();

        //[Authorize]
        [HttpGet("{id:length(24)}")]
        public JsonResult GetUser(string id) => _userService.GetUser(id);

        [HttpPost]
        [Route("Login")]
        public async Task<JsonResult> LoginAsync([FromBody] JObject users)
        {
            return await _userService.LoginAsync(users["email"].ToString(), users["password"].ToString(), Convert.ToInt32(users["role_id"]));
        }

        [HttpPost]
        [Route("Register")]
        public JsonResult PostUser(Users user) => _userService.PostUser(user);

        [HttpPatch("forgetPassword")]

        public JsonResult ForgetPassword(JObject json) => _userService.ForgetPassword(json);

        //[Authorize]
        [HttpPut("{id:length(24)}")]
        public JsonResult PutUser(string id, Users user) => _userService.PutUser(id, user);

        //[Authorize]
        [HttpDelete("{id:length(24)}")]
        public JsonResult DeleteUser(string id) => _userService.DeleteUser(id);


    }
}