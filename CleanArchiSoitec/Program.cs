
using CleanArchiSoitec.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.PlatformAbstractions;
using NSwag.AspNetCore;

namespace CleanArchiSoitec
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddScoped<IScheduleWriter, ScheduleJSONWriter>();
            // Génère un document OpenAPI "v1"
            builder.Services.AddOpenApiDocument(settings =>
            {
                settings.Title = "API";
                settings.Version = "v1";
            });

            var app = builder.Build();
   
              app.MapOpenApi();

            // 1) Votre sous-chemin d’hébergement
            app.UsePathBase("/CleanArchiSoitec");

            // 2) Servez le JSON OpenAPI (par défaut à /swagger/v1/swagger.json)
            app.UseOpenApi(options =>
            {
                // optionnel: garde le chemin par défaut
                // options.Path = "/swagger/{documentName}/swagger.json";

                // (recommandé derrière un reverse proxy / PathBase)
                options.PostProcess = (document, request) =>
                {
                    var basePath = request.HttpContext.Request.PathBase.ToString(); // ex: /CleanArchiSoitec
                    var scheme = request.Scheme;
                    var host = request.Host.Value;
                };
            });

            // 3) UI Swagger (NSwag)
            app.UseSwaggerUi3(ui =>
            {
                // L’UI sera servie à /CleanArchiSoitec/swagger
                ui.Path = "/swagger";

                // *** IMPORTANT *** : utiliser un chemin RELATIF vers le JSON
                // (pas de slash initial → respecte PathBase)
                ui.SwaggerRoutes.Clear();
                ui.SwaggerRoutes.Add(new SwaggerUi3Route("v1", "swagger/v1/swagger.json"));
                // Au final, le navigateur demandera: /CleanArchiSoitec/swagger/v1/swagger.json
            });
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
