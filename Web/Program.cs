using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Services;
using Application.Interfaces;
using Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------
// 1. Add DbContext
// ---------------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ---------------------------
// 2. Add Identity
// ---------------------------
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// ---------------------------
// 3. Configure login and access denied paths
// ---------------------------

builder.Services.AddAuthentication()
.AddCookie("CustomerScheme", options =>
{
    options.LoginPath = "/Customer/Auth/CustomerLogin"; options.AccessDeniedPath = "/Customer/AccessDenied";
})
.AddCookie("AdminScheme", options =>
{
    options.LoginPath = "/Admin/Auth/AdminLogin.cshtml"; options.AccessDeniedPath = "/Admin/AccessDenied";
});

// ---------------------------
// 4. Add application services
// ---------------------------
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<ISupplierStatus, SupplierStatusService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IItemStatusService, ItemStatusService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICatalogService, CatalogService>();
builder.Services.AddScoped<IItemRelationService, ItemRelationService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IOrderStatusService, OrderStatusService>();
builder.Services.AddScoped<IWarehouseLogService, WarehouseLogService>();

// ---------------------------
// 5. Add authorization policies
// ---------------------------
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("isCustomer", policy =>
        policy.RequireRole("Customer"));

    options.AddPolicy("isAdmin", policy =>
        policy.RequireRole("Admin"));
});

// ---------------------------
// 6. Add Razor Pages
// ---------------------------
builder.Services.AddRazorPages();

// ---------------------------
// 7. Build app
// ---------------------------
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await IdentityRoleSeed.SeedRolesAsync(roleManager);
}

// ---------------------------
// 8. Middleware pipeline
// ---------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();