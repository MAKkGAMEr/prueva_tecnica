using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
    private readonly AppDBContext _dbContext;

    public ProductController(AppDBContext dbContext)
    {
        _dbContext = dbContext;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducto()
    {
        return await _dbContext.Producto.ToListAsync();

    }

        [HttpGet("{id}") ]
    public async Task<ActionResult <Product>> GetProducto(int id)
    {
        var Product = await _dbContext.Producto.FindAsync(id);
        if (Product == null)
        {
            return NotFound();
        }
        return Product;
    }

    [HttpPost]
    public async Task<IActionResult> ProductID([FromBody] Product product)
    {
        var ProductoExis = await _dbContext.Producto
            .Where(p => p.ProductoId == product.ProductoId)
            .FirstOrDefaultAsync();

        if (ProductoExis != null)
        {
            return BadRequest("El producto ya existe con ese ID.");
        }

        if (product.FechaCreacion == default(DateTime))
        {
            product.FechaCreacion = DateTime.Now;
        }

        await _dbContext.Producto.AddAsync(product);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProducto), new { id = product.ProductoId }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult>Actualizarproducto(int id, Product product)
    {
        if (id != product.ProductoId)
        {
            return BadRequest();
            
        }

        _dbContext.Entry(product).State = EntityState.Modified;
        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch
        {
            return BadRequest();
        } 
        return NoContent();

    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> Eliminarproductos(int id)
    {
        var Product = await _dbContext.Producto.FindAsync(id);
        if (Product == null)
        {
            return NotFound();
        }
        _dbContext.Producto.Remove(Product);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}