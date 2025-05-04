
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


public class Product
    {


    [Key]  
    public int ProductoIdd { get; set; }
    public  string nombre { get; set; } 
    public decimal precio { get; set; }
    public DateTime FechaCreacion { get; set; }

}

    