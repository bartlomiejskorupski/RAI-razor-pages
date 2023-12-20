using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using RazorPagesLibrary.DTO;
using RazorPagesLibrary.Model;
using RazorPagesWeb;
using RazorPagesWeb.Data;
using RazorPagesWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();

    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapGet("/api/water", [AllowAnonymous](ApplicationDbContext db) =>
{
    var waters = db.Waters
        .Include(w => w.Type)
        .Include(w => w.Manufacturer)
        .Include(w => w.Packaging)
        .Include(w => w.Ions);

    return waters.Select(w => GetWaterResponse.FromWater(w));

}).WithName("Get Products");

app.MapGet("/api/company", [AllowAnonymous] (ApplicationDbContext db) =>
{
    var companies = db.Companies;

    return companies.ToList();

}).WithName("Get Companies");

app.MapGet("/api/user", [AllowAnonymous] (ApplicationDbContext db) =>
{
    return db.Users
        .Select(u => u.UserName)
        .ToList();

}).WithName("Get Users");

app.MapPost("/api/sale", [AllowAnonymous] (CreateSaleRequest req, ApplicationDbContext db) =>
{
    int createdId = 0;
    try
    {
        var newSale = new Sale()
        {
            SaleDate = DateTime.Now,
            EmployeeName = req.ClientName
        };

        newSale.SaleUnits = req.SaleUnits.Select(dto =>
        {
            if (dto.Count <= 0) throw new Exception("Count must be positive");
            return new SaleUnit()
            {
                Count = dto.Count,
                WaterId = dto.WaterId,
            };
        }).ToList();

        db.Add(newSale);
        db.SaveChanges();
        createdId = newSale.Id;
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.StackTrace, title: ex.Message);
    }

    return Results.Ok(new
    {
        Id = createdId
    });

}).WithName("Create Sale");

app.MapPut("/api/sale/{id:int}", [AllowAnonymous] (CreateSaleRequest req, int id, ApplicationDbContext db) =>
{
    using var transaction = db.Database.BeginTransaction();

    try
    {
        var sale = db.Sales
            .Include(s => s.SaleUnits)    
            .Single(s => s.Id == id);

        sale.EmployeeName = req.ClientName;

        foreach (var unit in sale.SaleUnits)
        {
            db.Remove(unit);
        }

        var newUnits = req.SaleUnits.Select(dto =>
        {
            if (dto.Count <= 0) throw new Exception("");
            return new SaleUnit()
            {
                Count = dto.Count,
                WaterId = dto.WaterId,
            };
        }).ToList();

        sale.SaleUnits = newUnits;

        db.SaveChanges();
        transaction.Commit();
    }
    catch (Exception ex)
    {
        transaction.Rollback();
        return Results.Problem(ex.StackTrace, title: ex.Message);
    }

    return Results.Ok();

}).WithName("Update Sale");

app.Run();
