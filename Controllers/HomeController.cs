using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    [Route("v1")]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<dynamic>> GetTask([FromServices] DataContext context)
        {
            var employee = new User { Id = 1, Username = "robin", Password = "102030", Role = "employee" };
            var manager = new User { Id = 2, Username = "batman", Password = "123456", Role = "manager" };
            var category = new Category {Id = 1, Title = "Informática"};
            var product = new Product{Id = 1, Category = category, Title = "Kit teclado e mouse", Description="Ótimo teclado e mouse", Price=180};

            context.Users.Add(employee);
            context.Users.Add(manager);
            context.Categories.Add(category);
            context.Products.Add(product);

            await context.SaveChangesAsync();

            return Ok(new {message = "Dados iniciais configurados"});
        }
    }
}