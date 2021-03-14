using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using Shop.Services;

namespace Shop.Controllers
{
    [Route("users")]
    public class UserController : Controller
    {
        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Post([FromServices]DataContext context, [FromBody]User model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                model.Role = "employee"; // ao criar um novo usuario o mesmo comeca sempre como empregado
                
                context.Users.Add(model);
                await context.SaveChangesAsync();

                model.Password = string.Empty;

                return model;
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Erro ao salvar o usuário:\r\n{ex.Message}" });
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromServices] DataContext context, [FromBody]User model)
        {
            var user = await context
                            .Users
                            .AsNoTracking()
                            .Where(x => x.Username.Equals(model.Username) && x.Password.Equals(model.Password))
                            .FirstOrDefaultAsync();

            if(user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);
            user.Password = string.Empty;
            return new 
            {
                user = user,
                token = token
            };
        }

        [HttpGet]
        [Route("")]
        [Authorize(Roles="manager")]
        public async Task<ActionResult<List<User>>> GetAction([FromServices]DataContext context)
        {
            var users = await context
                        .Users
                        .AsNoTracking()
                        .ToListAsync();

            return users;
        }
    }
}