using eCommerce.Infrastructure;
using eCommerce.Core;
using eCommerce.API.Middlewares;
using System.Text.Json.Serialization;
using eCommerce.Core.Mappers;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//Add Infrastructure services
builder.Services.AddInfrastructure();
builder.Services.AddCore();

// Add controllers to the service collection
//Opci�n recibir del json un string y convertirlo a enum
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

//El m�todo requiere el Assembly de d�nde se declar� la clase del perfil
//como todos los perfiles heredan de Profile solo hay que agregar uno porque
//en s� est�n declarados en el mismo assembly y eso es lo que se est� informando
builder.Services.AddAutoMapper(typeof(ApplicationUserMappingProfile).Assembly);

//FluentValidations
//Si se usa el m�todo sin AutoValidation se tiene que escribir c�digo para validar
//si la validaci�n pas�. Se escribir�a en el controlador
builder.Services.AddFluentValidationAutoValidation();

//Add API explorer services
builder.Services.AddEndpointsApiExplorer();

//Add swagger generation services to create swagger specification
builder.Services.AddSwaggerGen();

//Add cors services
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

//Build the web application
var app = builder.Build();

app.UseExceptionHandlingMiddleware();

//Routing
app.UseRouting();
app.UseSwagger(); //Adds endpoint that can serve the swagger.json
app.UseSwaggerUI(); //Adds swagger UI (interactive page to explore and test API endpoints)
app.UseCors();

//Auth
app.UseAuthentication();
app.UseAuthorization();

//Controller routes
app.MapControllers();

app.Run();