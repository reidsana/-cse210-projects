using System;

class Program
{
    static void Main(string[] args)
    {
        Address address1 = new Address("123 River Rd", "Bozeman", "MT", "USA");
        Customer customer1 = new Customer("Oksana Reid", address1);
        Order order1 = new Order(customer1);

        order1.AddProduct(new Product("Fly Rod", "FR123", 199.99, 1));
        order1.AddProduct(new Product("Fishing Line", "FL456", 24.99, 2));

        
        Address address2 = new Address("88 King Street", "Toronto", "ON", "Canada");
        Customer customer2 = new Customer("Emily Carter", address2);
        Order order2 = new Order(customer2);

        order2.AddProduct(new Product("Snow Goggles", "SG900", 149.99, 1));
        order2.AddProduct(new Product("Helmet", "HM300", 89.99, 1));

        
        Console.WriteLine("===== ORDER 1 =====");
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order1.GetTotalCost():0.00}\n");

        Console.WriteLine("===== ORDER 2 =====");
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order2.GetTotalCost():0.00}");
    }
}
