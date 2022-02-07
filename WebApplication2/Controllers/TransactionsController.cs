#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Web.Models;
using WebApplication2.Data;
using WebApplication2.ErrorHandling;
using WebApplication2.Utils;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly DataContext _context;

        public TransactionsController(DataContext context)
        {
            _context = context;
        }
        // GET: api/ModelTransactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModelTransaction>>> GetTransacciones()
        {
            Helpers.InsertDbTransactions(_context);
            return await _context.Transactions.ToListAsync();
        }

        [HttpGet("getBySKU/{SKU}")]
        public async  Task<string> GetTransactionsBySKU(string SKU)
        {

             List<ModelTransaction> skuTransactions= _context
                .Transactions
                .Where(transaction => transaction.SKU == SKU)
                .ToList();
            
            List <ModelTransaction> transformatedTransactions = GetTransactionList(skuTransactions);
            
            string json = JsonConvert.SerializeObject(new
            {
                results = transformatedTransactions,
                totalAmount= Helpers.CalculateTotalAmount(transformatedTransactions)

            });
            return json;
        }

        private List<ModelTransaction> GetTransactionList(List<ModelTransaction> skuTransactions) 
        {
            double result = 0;
            string actualCurrency;
            ModelCurrency currency = new ModelCurrency();
            List<ModelTransaction> transactionList = new List<ModelTransaction>();
            foreach (var transaction in skuTransactions)
            {
                actualCurrency = transaction.Currency;
                while (actualCurrency != "EUR")
                {
                    switch (actualCurrency)
                    {
                        case "CAD":
                            currency = _context.Currencies.Where(x => x.To == "EUR" && x.From == actualCurrency).SingleOrDefault();
                            if (currency == null)
                            {
                                currency = _context.Currencies.Where(x => x.From == actualCurrency).SingleOrDefault();
                            }
                            result = transaction.Amount * currency.Rate;
                            actualCurrency = currency.To;
                            break;
                        case "USD":
                            currency = _context.Currencies.Where(x => x.To == "EUR" && x.From == actualCurrency).SingleOrDefault();
                            if (currency == null)
                            {
                                currency = _context.Currencies.Where(x => x.From == actualCurrency).SingleOrDefault();
                            }
                            result = transaction.Amount * currency.Rate;
                            actualCurrency = currency.To;
                            break;
                        case "AUD":
                            currency = _context.Currencies.Where(x => x.To == "EUR" && x.From == actualCurrency).SingleOrDefault();
                            if (currency == null)
                            {
                                currency = _context.Currencies.Where(x => x.From == actualCurrency).SingleOrDefault();
                            }
                            result = transaction.Amount * currency.Rate;
                            actualCurrency = currency.To;
                            break;
                        case "EUR":
                            result = transaction.Amount;
                            actualCurrency = transaction.Currency;
                            break;
                    }
                }
                transaction.Amount = Math.Round(result, 2);
                transaction.Currency = actualCurrency;
                transactionList.Add(transaction);

            }
            return transactionList;
        }

        // GET: api/ModelTransactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ModelTransaction>> GetModelTransaction(int id)
        {
            var modelTransaction = await _context.Transactions.FindAsync(id);

            if (modelTransaction == null)
            {
                return NotFound();
            }

            return modelTransaction;
        }
        // PUT: api/ModelTransactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModelTransaction(int id, ModelTransaction modelTransaction)
        {
            if (id != modelTransaction.Id)
            {
                return BadRequest();
            }

            _context.Entry(modelTransaction).State = EntityState.Modified;

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

        // POST: api/ModelTransactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ModelTransaction>> PostModelTransaction(ModelTransaction modelTransaction)
        {
            _context.Transactions.Add(modelTransaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetModelTransaction", new { id = modelTransaction.Id }, modelTransaction);
        }

        // DELETE: api/ModelTransactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModelTransaction(int id)
        {
            var modelTransaction = await _context.Transactions.FindAsync(id);
            if (modelTransaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(modelTransaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModelTransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }

    }
}
