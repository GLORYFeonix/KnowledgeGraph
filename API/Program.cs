using Api.BLL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IUserBll, UserBll>();
builder.Services.AddSingleton<ILoginBll, LoginBll>();
builder.Services.AddCors(options =>
        {
            options.AddPolicy("Local",
                policy => policy.WithOrigins("http://127.0.0.1:5500").AllowAnyHeader().AllowAnyMethod());
            // policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            options.AddPolicy("AnotherPolicy",
                policy =>
                {
                    policy.WithOrigins("http://www.contoso.com")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                });
        });
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

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
