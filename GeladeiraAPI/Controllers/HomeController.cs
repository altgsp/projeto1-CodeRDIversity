using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuaAplicacao.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeladeiraController : ControllerBase
    {
        private readonly GeladeiraDbContext _context;

        public GeladeiraController(GeladeiraDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarItem(Item item)
        {
            _context.Itens.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetItem", new { id = item.Id }, item);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItens()
        {
            return await _context.Itens.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var item = await _context.Itens.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.Itens.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Itens.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

    public class Item
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Andar { get; set; }
        public int Container { get; set; }
        public int Posicao { get; set; }
    }

    public class GeladeiraDbContext : DbContext
    {
        public GeladeiraDbContext(DbContextOptions<GeladeiraDbContext> options)
            : base(options)
        {
        }

        public DbSet<Item> Itens { get; set; }
    }
}