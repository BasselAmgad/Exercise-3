var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/recipes", () =>
{
    Data data = new();
    return data.GetRecipes();
});

app.MapGet("/recipes/{id}", (Guid id) =>
{
    Data data = new();
    Recipe recipe = data.getRecipe(id);
    return Results.Ok(recipe);
});

app.MapPost("/recipes", (Recipe recipe) =>
{
    Data data = new();
    recipe.Id = Guid.NewGuid();
    data.AddRecipe(recipe);
    data.SaveData();
    return Results.Created($"/recipes/{recipe.Id}",recipe);
});

app.MapPut("/recipes/{id}", (Guid id, Recipe newRecipe) =>
{
    Data data = new();
    var updatedRecipe = data.EditRecipe(id, newRecipe);
    data.SaveData();
    return Results.Ok(updatedRecipe);
});

app.MapDelete("/recipes/{id}", (Guid id) =>
{
    Data data = new();
    data.RemoveRecipe(id);
    data.SaveData();
    return Results.Ok();
});

app.MapPost("recipes/category", (Guid id ,string category) =>
{
    Data data = new();
    data.AddCategoryToRecipe(id,category);
    data.SaveData();
    return Results.Created($"recipes/category/{category}",category);
});

app.MapPut("recipes/category", (Guid id, string category, string newCategory) => 
{
    Data data = new();
    data.EditCategory(id,category,newCategory);
    data.SaveData();
    return Results.Ok(data.getRecipe(id)); 
});

app.MapDelete("recipes/category", (Guid id, string category) =>
{
    Data data = new();
    data.RemoveCategoryFromRecipe(id,category);
    data.SaveData();
    return Results.Ok();
});

app.Run();