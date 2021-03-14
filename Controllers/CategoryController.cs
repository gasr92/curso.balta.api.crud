using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    [Route("v1/categories")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("")] // ficando em branco, basta chamar: https://localhost:5001/categories
        [AllowAnonymous]
        [ResponseCache(VaryByHeader="User-Agent", Location=ResponseCacheLocation.Any, Duration=30)]
        public async Task<ActionResult<List<Category>>> Get([FromServices] DataContext context)
        {
            // o AsNoTracking desliga o proxy que o entity faz, trazendo somente as propriedades escritas no modelo
            var categories = await context.Categories.AsNoTracking().ToListAsync();
            return categories;
        }

        [HttpGet]
        [Route("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Category>> GetById(int id, [FromServices] DataContext context)
        {
            var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));
            return new Category();
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles="employee")]
        public async Task<ActionResult<Category>> Post([FromBody] Category model, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Categories.Add(model);
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Não foi possível criar a categoria:\r\n{ex.InnerException}" });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles="employee")]
        public async Task<ActionResult<Category>> Put(int id, [FromBody] Category model, [FromServices] DataContext context)
        {
            if (model.Id != id)
                return NotFound(new { mensagem = "Categoria não encontrada" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry<Category>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = $"Este objeto ja foi atualizado" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Não foi possível atualizar a categoria:\r\n{ex.InnerException}" });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles="employee")]
        public async Task<ActionResult<Category>> Delete(int id, [FromServices] DataContext context)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id.Equals(id));

                if (category == null)
                    return NotFound(new { message = "Categoria não encontrada" });

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return Ok(new {message = "Categoria excluída com sucesso"});
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Não foi possível excluir a categoria:\r\n{ex.InnerException}" });
            }
        }
    }
}