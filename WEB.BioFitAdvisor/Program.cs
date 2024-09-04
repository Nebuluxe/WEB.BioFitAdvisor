using System.Net.NetworkInformation;
using WEB.BioFitAdvisor.Core; // Asegúrate de que este using esté presente

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(13);
});

// Registro de Ping como un servicio que puede ser inyectado.
builder.Services.AddScoped<Ping>();

builder.Services.AddHttpContextAccessor();

// Registrar ApiConsumer y UserDataManipulator
builder.Services.AddScoped<ApiConsumer>();
builder.Services.AddScoped<UserDataManipulator>();

// Configurar CORS para permitir cualquier origen, método y encabezado
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowAnyOrigin"); // Aplicar la política CORS

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=WebSite}/{action=index}/{id?}");

app.Run();
