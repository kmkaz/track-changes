using ChangeDetector;
using ChangeTracker;
using System.Diagnostics;

var configList = ConfigReader.ReadConfig();

var size = 10000;
var data = new Item[size];
var changedItems = new List<Item>();

for (int i = 0; i < size; i++)
{
    data[i] = new Item
    {
        Id = i,
        Name = $"Item {i}",
        Description = $"Item {i} - description v.{i} ",
        Category = new Category { Name = "Summer Dresses" },
        Price = 120,
        Make = new Make
        {
            Name = $"Manufacturer {i}",
            Supplier = new Supplier { Name = $"Suppelier {i}" }
        }
    };

    // every 10th item has price change
    // every 50th item has description change
    // every 100th item has category change

    changedItems.Add(new Item
    {
        Id = data[i].Id,
        Name = data[i].Name,
        Description = i % 50 == 0 ? $"{data[i].Description}.1" : data[i].Description,
        Category = i % 100 == 0 ? new Category { Name = $"{data[i].Category.Name}.v2" } : new Category { Name = data[i].Category.Name },
        Price = i % 10 == 0 ? data[i].Price + 1 : data[i].Price,
        Make = new Make
        {
            Name = data[i].Make.Name,
            Supplier = new Supplier { Name = data[i].Make.Supplier.Name }
        }
    });
}

var changes = new Dictionary<string, int>();
var timer1 = new Stopwatch();
timer1.Start();
for (int j = 0; j < size; j++)
{
    var item = data[j];
    var updatedItem = changedItems.FirstOrDefault(x => x.Id == item.Id);
    foreach (var config in configList)
    {
        if (DetectChangesV1.IsChanged(item, updatedItem, config.fields))
        {
            if (changes.ContainsKey(config.consumerId))
                changes[config.consumerId] += 1;
            else
                changes.Add(config.consumerId, 1);
        }
    }
}
timer1.Stop();

Console.WriteLine($"Time taken: {timer1.Elapsed.ToString(@"m\:ss\.fff")}");
foreach(var change in changes)
{
    Console.WriteLine($"{change.Value} changes for ${change.Key}");
}


Console.ReadLine();