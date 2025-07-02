using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StorageApi.Data;
using StorageApi.DTOs;
using StorageApi.Models;

namespace StorageApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StorageContext _context;

        public ProductsController(StorageContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProduct()
        {
            var res = _context.Product.Select((p) => new ProductDto
            { 
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Count = p.Count,
            });

            return Ok(await res.ToListAsync());
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = _context.Product
                .Where(p => p.Id == id)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Count = p.Count,
                })
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            return Ok(await product);
        }

        // GET: api/products/stats
        [HttpGet("stats")]
        public async Task<ActionResult<ProductStatsDto>> GetStats()
        {
            var products = await _context.Product.ToListAsync();

            if (products == null || !products.Any())
            {
                return NotFound("No products found.");
            }

            var productsStats = new ProductStatsDto
            {
                TotalProductsCount = products.Sum(p => p.Count),
                TotalInventoryValue = products.Sum(p => p.Price * p.Count),
                AveragePrice = (int)products.Average(p => p.Price)
            };

            return Ok(productsStats);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, CreateProductDto productDto)
        {
            var product = new Product 
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Category = productDto.Category,
                Shelf = productDto.Shelf,
                Count = productDto.Count,
                Description = productDto.Description
            };

            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductDto>> PostProduct(CreateProductDto productDto)
        {
            var product = new Product 
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Category = productDto.Category,
                Shelf = productDto.Shelf,
                Count = productDto.Count,
                Description = productDto.Description
            };
              
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, productDto);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
