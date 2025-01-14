using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class AppDBContext : DbContext
    {

        public AppDBContext(DbContextOptions <AppDBContext> options) : base(options) { }

    public DbSet<Product> Producto { get; set; }
    public Task<ActionResult<IEnumerable<Product>>> Nombre { get; internal set; }
}

