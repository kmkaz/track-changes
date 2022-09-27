using ChangeDetector;
using ChangeTracker;
using System.Diagnostics;

var size = 50000;
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

StaticDetection(size, data, changedItems);

DynamicDetection(size, data, changedItems);

Console.ReadLine();

static void DynamicDetection(int size, Item[] data, List<Item> changedItems)
{
    var configList = ConfigReader.ReadConfig();
    var changes1 = new Dictionary<string, int>();
    var timer1 = new Stopwatch();
    Console.WriteLine("Starting dynamic change detection...");
    timer1.Start();
    for (int j = 0; j < size; j++)
    {
        var item = data[j];
        var updatedItem = changedItems.FirstOrDefault(x => x.Id == item.Id);
        if (updatedItem == null)
            continue;
        foreach (var config in configList)
        {
            if (DetectChangesV1.IsChanged(item, updatedItem, config.fields))
            {
                if (changes1.ContainsKey(config.consumerId))
                    changes1[config.consumerId] += 1;
                else
                    changes1.Add(config.consumerId, 1);
            }
        }
    }
    timer1.Stop();
    Console.WriteLine($"Done! Time taken: {timer1.Elapsed.ToString(@"m\:ss\.fff")}");
    foreach (var change in changes1)
    {
        Console.WriteLine($"{change.Value} changes for {change.Key}");
    }
    Console.WriteLine("=========================================================");
    Console.WriteLine();
}

static void StaticDetection(int size, Item[] data, List<Item> changedItems)
{
    var changes2 = new Dictionary<string, int>();
    var timer2 = new Stopwatch();
    timer2.Start();
    Console.WriteLine("Starting static change detection");
    var priceKey = "PriceMonitor";
    var descKey = "DescMonitor";
    var catKey = "CatMonitor";
    for (int k = 0; k < size; k++)
    {
        var item = data[k];
        var updatedItem = changedItems.FirstOrDefault(x => x.Id == item.Id);
        if (updatedItem == null)
            continue;
        if (item.Price != updatedItem.Price)
        {
            if (!changes2.ContainsKey(priceKey))
                changes2.Add(priceKey, 0);
            changes2[priceKey] += 1;
        }
        if (item.Description != updatedItem.Description)
        {
            if (!changes2.ContainsKey(descKey))
                changes2.Add(descKey, 0);
            changes2[descKey] += 1;
        }
        if (item.Category.Name != updatedItem.Category.Name)
        {
            if (!changes2.ContainsKey(catKey))
                changes2.Add(catKey, 0);
            changes2[catKey] += 1;
        }
    }
    timer2.Stop();
    Console.WriteLine($"Done! Time taken: {timer2.Elapsed.ToString(@"m\:ss\.fff")}");
    foreach (var change in changes2)
    {
        Console.WriteLine($"{change.Value} changes for {change.Key}");
    }
    Console.WriteLine("=========================================================");
    Console.WriteLine();
}