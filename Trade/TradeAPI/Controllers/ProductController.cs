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
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        [HttpDelete]
        public async Task<ActionResult<Product>> Delete(string article)
        {
            var product =  _context.Products.FirstOrDefault(p => p.ProductArticleNumber == article);
            if (product == null)
                return BadRequest();
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<Product>> Put(string article, [FromBody] Product model)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductArticleNumber == article);
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

        [HttpGet("Manufacturers")]
        public async Task<ActionResult<List<string>>> GetManufacturer()
        {
            var products = await _context.Products.ToListAsync();
            List<string> manufacturers = new List<string>();
            foreach (var product in products)
                manufacturers.Add(product.ProductManufacturer);
            manufacturers = manufacturers.Distinct().ToList();
            return Ok(manufacturers);
        }
    }
}
