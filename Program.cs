using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

public class Recipe
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public double Rating { get; set; }
    public string Ingredients { get; set; }

    public Recipe() { }

    public Recipe(string title, double rating, string ingredients)
    {
        this.Id = Guid.NewGuid();
        this.Title = title;
        this.Rating = rating;
        this.Ingredients = ingredients;
    }
}

public class AppContext : DbContext
{
    public DbSet<Recipe> Recipes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql("Host=localhost;Database=lektion5;Username=postgres;Password=password");
    }
}

class Program
{
    static void Main(string[] args)
    {
        using AppContext context = new AppContext();

        Recipe recipe = new Recipe("Hamburgare", 9.0, "köttfärs,bröd");
        context.Recipes.Add(recipe); // INSERT INTO
        context.SaveChanges();

        // Hämta alla recept (SELECT)
        List<Recipe> recipes = context.Recipes.ToList();
        foreach (Recipe all in recipes)
        {
            Console.WriteLine(all.Title);
        }

        Console.WriteLine("----");

        // Hämta alla receipt med rating över 9
        recipes = context.Recipes.Where(all => all.Rating > 9.0).ToList();
        foreach (Recipe all in recipes)
        {
            Console.WriteLine(all.Title);
        }

        // Radera (DELETE)
        context.Recipes.Remove(recipe);
        context.SaveChanges();

        // Uppdatera (UPDATE)
        recipe = context.Recipes.Where(all => all.Title.Equals("Pannkakor")).First();
        recipe.Rating -= 0.5;
        context.SaveChanges();
    }
}
