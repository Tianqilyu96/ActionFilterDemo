var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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


// Requirements
// 1. Database Transactions: Either all successful or all failed.
// 2. Automation: Automatically start, commit, and rollback transactions.
// 3. When using EF Core for database operations within the scope declared by TransactionScope, the code is automatically marked as "supporting transactions".
// 4. TransactionScope implements the IDisposable interface. If an object of TransactionScope is disposed without calling Complete(), the transaction will be rolled back; otherwise, the transaction will be committed.
// 5. TransactionScope also supports nested transactions.
// 6. TransactionScope in .NET Core does not have the distributed transaction escalation issue like in .NET Framework's MSDTC. Please use eventual consistency transactions.