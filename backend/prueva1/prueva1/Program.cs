using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Habilitar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500")  // Permite solicitudes desde tu frontend
              .AllowAnyHeader()   // Permite cualquier encabezado
              .AllowAnyMethod();  // Permite cualquier método (GET, POST, PUT, DELETE)
    });
});

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Usa la política de CORS
app.UseCors("AllowLocalhost");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();



//var builder = WebApplication.CreateBuilder(args);



//builder.Services.AddDbContext<AppDBContext>(options =>
//options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//app.UseCors("AllowAll");

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}


//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
