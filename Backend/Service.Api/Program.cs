using Service.Api.Service.Authentication.Configurations;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddOpenApi();
builder.Services.AddIdentityConfiguration(builder.Configuration);
builder.Services.AddApiConfiguration();

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.AllowAnyOrigin() // fix to allow only correct origin                                                  
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                          });
});

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseApiConfiguration(app.Environment);

app.Run();
