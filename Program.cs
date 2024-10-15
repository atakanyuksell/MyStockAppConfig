using MyStockAppConfiguration.Services;
using MyStockAppConfiguration.ServiceContracts;
using MyStockAppConfiguration.Services;

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


var app = builder.Build();


app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
