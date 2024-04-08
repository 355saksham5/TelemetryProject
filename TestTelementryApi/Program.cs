using OpenTelemetry;
using OpenTelemetry.Extensions;
using Refit;
using TestTelementryApi.Service;
namespace TestTelementryApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddOpenTelementryService(builder.Configuration);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddRefitClient<IExternalApi1>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://localhost:7260");
            });
            builder.Services.AddRefitClient<IExternalApi2>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://localhost:7211");
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseOpenTelementry();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
