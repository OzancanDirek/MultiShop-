using Microsoft.AspNetCore.Authentication.Cookies;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.BasketServices;
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
using MultiShop.WebUI.Settings;

var builder = WebApplication.CreateBuilder(args);

// --- Ayarlarý Okuma ---
builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection("ClientSettings"));
builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection("ServiceApiSettings"));
var serviceApiSettings = builder.Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

// --- Authentication (Kimlik Doðrulama) Yapýlandýrmasý ---
// Sadece tek bir Cookie þemasý kullanýyoruz, JwtBearer ile Cookie çakýþmasýný sildik.
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

// --- Handler (Aracý) Tanýmlamalarý ---
builder.Services.AddScoped<ClientCredentialTokenHandler>();
builder.Services.AddScoped<ResourceOwnerPasswordTokenHandler>();

// --- Servis Kayýtlarý ---
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddHttpClient<IIdentityService, IdentityService>();
builder.Services.AddHttpClient<IClientCredentialService, ClientCredentialService>();

// --- Microservice HttpClient Kayýtlarý (Resource Owner Token Gerektirenler) ---
builder.Services.AddHttpClient<IUserService, UserService>(opt =>
{
    opt.BaseAddress = new Uri(serviceApiSettings.IdentityServerUrl);
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IBasketService, BasketService>(opt =>
{
    opt.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Basket.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddHttpClient<IDiscountService, DiscountService>(opt =>
{
    opt.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Discount.Path}");
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

// --- Microservice HttpClient Kayýtlarý (Client Credential Token Gerektirenler) ---
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