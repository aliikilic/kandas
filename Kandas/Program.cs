using Kandas.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureRegisterationManager();
builder.Services.ConfigureIdentity();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.ConfigureSwaggerJwt();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
