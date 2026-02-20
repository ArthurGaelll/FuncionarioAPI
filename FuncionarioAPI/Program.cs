using Microsoft.EntityFrameworkCore;
using FuncionarioAPI.Data;
using SqlKata.Execution;
using Microsoft.Data.SqlClient;
using SqlKata.Compilers;

var builder = WebApplication.CreateBuilder(args);

// 1. Pegar a String de Conexão do appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. CONFIGURAÇÃO DO ENTITY FRAMEWORK (Para suas Migrations e CRUD atual)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// 3. CONFIGURAÇÃO DO SQLKATA (Para Query Building dinâmico)
builder.Services.AddScoped(factory =>
{
    var connection = new SqlConnection(connectionString);
    var compiler = new SqlServerCompiler();
    return new QueryFactory(connection, compiler);
});

// 4. CONFIGURAÇÃO DO PETAPOCO (Para consultas ultra-rápidas)
// O PetaPoco geralmente é usado criando a instância na hora, 
// mas podemos registrar a string de conexão para facilitar.
builder.Services.AddScoped<PetaPoco.IDatabase>(sp =>
    new PetaPoco.Database(connectionString, "Microsoft.Data.SqlClient"));

// Configurações padrão da API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => {
    options.AddPolicy("PermitirTudo", builder => {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Configurar o Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("PermitirTudo");
app.UseAuthorization();
app.MapControllers();

app.Run();