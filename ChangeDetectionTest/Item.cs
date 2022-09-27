namespace ChangeTracker
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Make Make { get; set; }
        public Category Category { get; set; }
    }

    public class Make
    {
        public string Name { get; set; }
        public Supplier Supplier { get; set; }
    }

    public class Supplier
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class Category
    {
        public string Name { get; set; }
    }
}
