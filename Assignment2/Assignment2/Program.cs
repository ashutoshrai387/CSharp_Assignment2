class Program
{
    // Product class
    class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Type { get; set; }

        public Product(string name, double price, int quantity, string type)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            Type = type;
        }
    }

    // List to store products
    static List<Product> inventory = new List<Product>();

    static void Main(string[] args)
    {
        // Add given products
        InitializeInventory();
        Console.WriteLine($"Total number of products: {inventory.Count}");

        // Add a new product and print the updated list
        AddProduct("Potato", 10, 50, "Root");
        PrintInventory();

        // Print all the products of which have the type Leafy green
        Console.WriteLine("\nLeafy green products:");
        PrintProductsByType("Leafy green");

        // Remove garlic from list and print the no. of remaining products
        RemoveProduct("garlic");
        Console.WriteLine($"\nGarlic Sold Out!\nTotal number of products left: {inventory.Count}");

        // Add cabbages and print final quantity
        AddQuantity("cabbage", 50);
        Console.WriteLine($"\nCabbages Added.\nFinal quantity of cabbage: {GetProductQuantity("cabbage")}");

        // Calculate price to be paid by user
        double totalPrice = Purchase("lettuce", 1) + Purchase("zucchini", 2) + Purchase("broccoli", 1);
        Console.WriteLine($"\nTotal price for the purchase: {totalPrice} RS");
    }

    // Method to add given products
    static void InitializeInventory()
    {
        // Products given
        string[] productsInput = {
            "lettuce, 10.5 RS, 50, Leafy green",
            "cabbage, 20 RS, 100, Cruciferous",
            "pumpkin, 30 RS, 30, Marrow",
            "cauliflower, 10 RS, 25, Cruciferous",
            "zucchini, 20.5 RS, 50, Marrow",
            "yam, 30 RS, 50, Root",
            "spinach, 10 RS, 100, Leafy green",
            "broccoli, 20.2 RS, 75, Cruciferous",
            "garlic, 30 RS, 20, Leafy green",
            "silverbeet, 10 RS, 50, Marrow"
        };

        // Add given products
        foreach (var productInput in productsInput)
        {
            string[] details = productInput.Split(", ");
            string name = details[0];
            double price = double.Parse(details[1].Replace(" RS", ""));
            int quantity = int.Parse(details[2]);
            string type = details[3];
            inventory.Add(new Product(name, price, quantity, type));
        }
    }

    // Method to add new product
    static void AddProduct(string name, double price, int quantity, string type)
    {
        inventory.Add(new Product(name, price, quantity, type));
    }

    // Method to print the inventory
    static void PrintInventory()
    {
        Console.WriteLine("\nList of all Products:");
        foreach (var product in inventory)
        {
            Console.WriteLine($"{product.Name}, {product.Price} RS, {product.Quantity}, {product.Type}");
        }
        Console.WriteLine($"Total number of products: {inventory.Count}");
    }

    // Method to print products by type
    static void PrintProductsByType(string type)
    {
        foreach (var product in inventory.Where(p => p.Type.Equals(type, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine($"{product.Name}, {product.Price} RS, {product.Quantity}, {product.Type}");
        }
    }

    // Method to remove product from inventory
    static void RemoveProduct(string name)
    {
        inventory.RemoveAll(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    // Method to get the quantity of a product
    static int GetProductQuantity(string name)
    {
        var product = inventory.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        return product != null ? product.Quantity : 0;
    }

    // Method to add more to an existing product in the inventory
    static void AddQuantity(string name, int quantity)
    {
        var product = inventory.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (product != null)
            product.Quantity += quantity;
    }

    // Method to calculate the total price for a given purchase
    static double Purchase(string name, int kilograms)
    {
        var product = inventory.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (product != null && product.Quantity >= kilograms)
        {
            product.Quantity -= kilograms;
            return Math.Round(product.Price * kilograms, 2);
        }
        return 0;
    }
}