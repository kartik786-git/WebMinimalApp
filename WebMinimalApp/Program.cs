using WebMinimalApp.Model;
using WebMinimalApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IBlogService, BlogService>();
// Add services to the container.
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


app.MapGet("/api/blog", (IBlogService blogSerive) =>
{
    return Results.Ok(blogSerive.GetAll());
});

app.MapGet("/api/blog/{id}", (IBlogService blogService, int id) =>
{
    var todoItem = blogService.GetById(id);
    if (todoItem == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(todoItem);
});

app.MapPost("/api/blog", async (IBlogService blogService, Blog blogItem) =>
{
    var createdTodoItem = blogService.Create(blogItem);
    return Results.Created($"/api/todo/{createdTodoItem.Id}", createdTodoItem);
});


app.MapPut("/api/blog/{id}", async (IBlogService blogService, int id, Blog updatedTodoItem) =>
{
    blogService.Update(id, updatedTodoItem);
    return Results.NoContent();
});

app.MapDelete("/api/blog/{id}", async (IBlogService blogService, int id) =>
{
    blogService.Delete(id);
    return Results.NoContent();
});

app.Run();
