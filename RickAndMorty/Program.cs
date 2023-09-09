using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RickAndMorty.Interfaces;
using RickAndMorty.Models;
using RickAndMorty.Operations;
using RickAndMorty.Repository;
using NLog;
using NLog.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);
    //log
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Add services to the container.
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddHttpClient();
    string connection = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
    builder.Services.AddScoped<IDataOperation, DBclass>();
    builder.Services.AddScoped<ICharacterRequester, CharacteHttpRepository>();
    builder.Services.AddScoped<ICharacterDB, CharacterDbRepository>();
    builder.Services.AddScoped<ILocationRequester, LocationHttpRepository>();
    builder.Services.AddScoped<ILocationDB, LocationDbRepository>();
    builder.Services.AddScoped<IEpisodeRequester, EpisodeHttpRepository>();
    builder.Services.AddScoped<IEpisodeDB, EpisodeDbRepository>();

    var app = builder.Build();
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        //app.UseDeveloperExceptionPage();
        //app.UseSwagger();
        //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "test v1"));
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch(Exception ex)
{
    logger.Error(ex);
}
finally
{
    LogManager.Shutdown();
}

