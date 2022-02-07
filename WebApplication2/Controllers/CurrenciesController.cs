#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using WebApplication2.Data;
using WebApplication2.ErrorHandling;
using WebApplication2.Utils;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly DataContext _context;

        public CurrenciesController(DataContext context)
        {
            _context = context;
        }
       
        // GET: api/ModelCurrencies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModelCurrency>>> GetCurrencies()
        {
            Helpers.InsertDbCurrencies( _context);
            return await _context.Currencies.ToListAsync();
        }

        // GET: api/ModelCurrencies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ModelCurrency>> GetModelCurrency(int id)
        {
            var modelCurrency = await _context.Currencies.FindAsync(id);

            if (modelCurrency == null)
            {
                return NotFound();
            }

            return modelCurrency;
        }

        // PUT: api/ModelCurrencies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModelCurrency(int id, ModelCurrency modelCurrency)
        {
            if (id != modelCurrency.Id)
            {
                return BadRequest();
            }

            _context.Entry(modelCurrency).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                StreamWriter w = new StreamWriter("Log/error.txt");
                ErrorLog.save(ex.Message, w); 
            }

            return NoContent();
        }

        // POST: api/ModelCurrencies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ModelCurrency>> PostModelCurrency(ModelCurrency modelCurrency)
        {
            _context.Currencies.Add(modelCurrency);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetModelCurrency", new { id = modelCurrency.Id }, modelCurrency);
        }

        // DELETE: api/ModelCurrencies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModelCurrency(int id)
        {
            var modelCurrency = await _context.Currencies.FindAsync(id);
            if (modelCurrency == null)
            {
                return NotFound();
            }

            _context.Currencies.Remove(modelCurrency);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModelCurrencyExists(int id)
        {
            return _context.Currencies.Any(e => e.Id == id);
        }

    }
}
