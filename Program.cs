// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");


using ChangeTracker;

var item1 = new Item
{
    Name = "Summer Dress",
    Category = new Category { Name = "Dresses" },
    Price = 120,
    Make = new Make
    {
        Name = "Dior",
        Supplier = new Supplier { Name = "Dior" }
    }
};

var item2 = new Item
{
    Name = "Summer Dress",
    Description = "Your go-to for all the latest trends, no matter who you are, where you’re from and what you’re up to.",
    Category = new Category { Name = "Summer Dresses" },
    Price = 120,
    Make = new Make
    {
        Name = "Dior",
        Supplier = new Supplier { Name = "Japan" }
    }
};

var configList = ConfigReader.ReadConfig();

foreach (var config in configList)
{
    if (TrackChanges.IsChanged(item1, item2, config.fields))
        Console.WriteLine($"Changes for {config.consumerId}!");
    else
        Console.WriteLine($"No changes for {config.consumerId}.");
}

