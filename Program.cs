using MyStockAppConfiguration.Services;
using MyStockAppConfiguration.ServiceContracts;
using MyStockAppConfiguration.Services;
using MyStockAppConfiguration;

var builder = WebApplication.CreateBuilder(args);


//Services
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddScoped<FinnhubService>();
builder.Services.Add(new ServiceDescriptor(
  typeof(IFinnhubService),
  typeof(FinnhubService),
  ServiceLifetime.Scoped
));

builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection(nameof(TradingOptions))); //add IOptions<TradingOptions> as a service
var app = builder.Build();


app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
