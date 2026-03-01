using Microsoft.AspNetCore.Authentication.Cookies;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.CargoServices.CargoCompanyServices;
using MultiShop.WebUI.Services.CargoServices.CargoCustomerServices;
using MultiShop.WebUI.Services.CatalogServices.AboutServices;
using MultiShop.WebUI.Services.CatalogServices.BrandServices;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using MultiShop.WebUI.Services.CatalogServices.ContactServices;
using MultiShop.WebUI.Services.CatalogServices.FeatureServices;
using MultiShop.WebUI.Services.CatalogServices.OfferDiscountServices;
using MultiShop.WebUI.Services.CatalogServices.ProductDetailServices;
using MultiShop.WebUI.Services.CatalogServices.ProductImageServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.CatalogServices.SliderServices;
using MultiShop.WebUI.Services.CatalogServices.SpecialOfferServices;
using MultiShop.WebUI.Services.CommentServices;
using MultiShop.WebUI.Services.Concrete;
using MultiShop.WebUI.Services.DiscountServices;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Services.MessageServices;
using MultiShop.WebUI.Services.OrderServices.OrderAdressServices;
using MultiShop.WebUI.Services.OrderServices.OrderOrderingServices;
using MultiShop.WebUI.Services.StatisticServices.CatalogStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.DiscountStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.MessageStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.UserStatisticServices;
using MultiShop.WebUI.Services.UserIdentityServices;
using MultiShop.WebUI.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection("ClientSettings"));
builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection("ServiceApiSettings"));
var serviceApiSettings = builder.Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
    {
        opt.LoginPath = "/Login/Index";
        opt.ExpireTimeSpan = TimeSpan.FromDays(5);
        opt.Cookie.Name = "MultiShopCookie";
        opt.SlidingExpiration = true;
    });

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();

builder.Services.AddScoped<ClientCredentialTokenHandler>();
builder.Services.AddScoped<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddHttpClient<IIdentityService, IdentityService>();
builder.Services.AddHttpClient<IClientCredentialService, ClientCredentialService>();

builder.Services.AddHttpClient<IUserService, UserService>(opt =>
{
    opt.BaseAddress = new Uri(serviceApiSettings.IdentityServerUrl);
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<ICatalogStatisticServices, CatalogStatisticServices>(opt =>
{
    opt.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Catalog.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IMessageStatisticServices, MessageStatisticServices>(opt =>
{
    opt.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Message.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IDiscountStatisticService, DiscountStatisticService>(opt =>
{
    opt.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Discount.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IUserStatisticServices, UserStatisticServices>(opt =>
{
    opt.BaseAddress = new Uri(serviceApiSettings.IdentityServerUrl);
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IUserIdentityService, UserIdentityService>(opt =>
{
    opt.BaseAddress = new Uri(serviceApiSettings.IdentityServerUrl);
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IBasketService, BasketService>(opt =>
{
    opt.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Basket.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IOrderOrderingService, OrderOrderingService>(opt =>
{
    opt.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Order.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IOrderAdressService, OrderAdressService>(opt =>
{
    opt.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Order.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IDiscountService, DiscountService>(opt =>
{
    opt.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Discount.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<ICargoCompanyService, CargoCompanyService>(opt =>
{
    opt.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Cargo.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<ICargoCustomerService, CargoCustomerService>(opt =>
{
    opt.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Cargo.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();


builder.Services.AddHttpClient<IMessageService, MessageService>(opt =>
{
    opt.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Message.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

void AddCatalogClient<TInterface, TService>(string path) where TInterface : class where TService : class, TInterface
{
    builder.Services.AddHttpClient<TInterface, TService>(opt =>
    {
        opt.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{path}");
    }).AddHttpMessageHandler<ClientCredentialTokenHandler>();
}

AddCatalogClient<ICategoryService, CategoryService>(serviceApiSettings.Catalog.Path);
AddCatalogClient<IProductService, ProductService>(serviceApiSettings.Catalog.Path);
AddCatalogClient<ISpecialOfferService, SpecialOfferService>(serviceApiSettings.Catalog.Path);
AddCatalogClient<IFeatureSliderService, FeatureSliderService>(serviceApiSettings.Catalog.Path);
AddCatalogClient<IFeatureService, FeatureService>(serviceApiSettings.Catalog.Path);
AddCatalogClient<IOfferDiscountService, OfferDiscountService>(serviceApiSettings.Catalog.Path);
AddCatalogClient<IBrandService, BrandService>(serviceApiSettings.Catalog.Path);
AddCatalogClient<IAboutService, AboutService>(serviceApiSettings.Catalog.Path);
AddCatalogClient<IProductImageService, ProductImageService>(serviceApiSettings.Catalog.Path);
AddCatalogClient<IProductDetailService, ProductDetailService>(serviceApiSettings.Catalog.Path);
AddCatalogClient<IContactService, ContactService>(serviceApiSettings.Catalog.Path);

builder.Services.AddHttpClient<ICommentService, CommentService>(opt =>
{
    opt.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Comment.Path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

// --- CORS ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// --- Middleware (Ara Yazýlým) ---
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAll");

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();