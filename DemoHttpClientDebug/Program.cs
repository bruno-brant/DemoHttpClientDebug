using DemoHttpClientDebug;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var httpClientName = "myServiceClient";
builder.Services.AddHttpClient(httpClientName)
	.ConfigureHttpClient(client =>
	{
		client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
	})
	.ConfigureAdditionalHttpMessageHandlers((handlers, services) =>
	{
		var logger = services.GetRequiredService<ILogger<MyLoggingMessageHandler>>();

		handlers.Add(new MyLoggingMessageHandler(logger));
	});

builder.Services.AddScoped(
	services => new JsonPlaceholderHttpClient(
		services.GetRequiredService<IHttpClientFactory>().CreateClient(httpClientName)));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
