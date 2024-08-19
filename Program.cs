using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseAutofac();
builder.Services.ReplaceConfiguration(builder.Configuration);
builder.Services.AddApplication<MinimalModule>();

var app = builder.Build();

app.MapGet("/book", async ([FromServices] IRepository<Book, Guid> repository) =>
{
    return await repository.GetListAsync();
});

app.MapPost("/book", async (string name, [FromServices] IRepository<Book, Guid> repository) =>
{
    var newBook = await repository.InsertAsync(new Book(Guid.NewGuid(), name));
    return Results.Created($"/book/{newBook.Id}", newBook);
});

app.MapPut("/book/{id}", async (Guid id, string name, [FromServices] IRepository<Book, Guid> repository) =>
{
    var book = await repository.GetAsync(id);
    book.Name = name;
    return await repository.UpdateAsync(book);
});

app.MapDelete("/book/{id}", async (Guid id, [FromServices] IRepository<Book, Guid> repository) =>
{
    var book = await repository.GetAsync(id);
    await repository.DeleteAsync(id);
});

app.InitializeApplication();
app.Run();
