using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TradeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly TradeContext _context;

        public ProductController(TradeContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Post(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var books = await _context.Products.ToListAsync();
            return Ok(books);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Product>> Delete([FromRoute] int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return BadRequest();
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Product>> Put([FromRoute] int id, [FromBody] Product model)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return BadRequest();
            product.ProductManufacturer = model.ProductManufacturer;
            product.ProductPhoto = model.ProductPhoto;
            product.ProductProvider = model.ProductProvider;
            product.ProductName = model.ProductName;
            product.ProductCategory = model.ProductCategory;
            product.ProductCost = model.ProductCost;
            product.ProductDescription = model.ProductDescription;
            product.ProductDiscountAmount = model.ProductDiscountAmount;
            product.ProductMaxDiscountAmount = model.ProductMaxDiscountAmount;
            product.ProductQuantityInStock = model.ProductQuantityInStock;
            product.ProductUnitOfMeasurement = model.ProductUnitOfMeasurement;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct([FromRoute] int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
    }
}
