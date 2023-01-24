using System.Text.Json.Serialization;
using Lib.AspNetCore.ServerSentEvents;
using WebApi.HostedServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();

builder.Services.AddControllersWithViews();

{
    var services = builder.Services;
    var env = builder.Environment;

    services.AddControllers().AddJsonOptions(x =>
    {
        // serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

        // ignore omitted parameters on models to enable optional params (e.g. User update)
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    services.AddDbContext<DataContext>();
    services.AddScoped<IQuizService, QuizService>();
    services.AddScoped<IWordleService, WordleService>();


    services.AddControllersWithViews()
        .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

    services.AddCors();
}

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
 {
     options.CustomSchemaIds(type => type.FullName);
 });


builder.Services.AddServerSentEvents(options =>
{
    options.OnClientConnected = (service, clientConnectedArgs) =>
    {
        service.AddToGroup("", clientConnectedArgs.Client);
    };
    options.OnClientDisconnected = (service, clientConnectedArgs) =>
    {
        service.AddToGroup("", clientConnectedArgs.Client);
    };
});

builder.Services.AddHostedService<ServerEventsWorker>();

var app = builder.Build();

app.MapServerSentEvents("sse/rn-updates");

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
