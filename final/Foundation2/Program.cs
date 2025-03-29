using System;
using System.Collections.Generic;

class Product
{
    private string name;
    private int productId;
    private decimal pricePerUnit;
    private int quantity;

    public Product(string name, int productId, decimal pricePerUnit, int quantity)
    {
        this.name = name;
        this.productId = productId;
        this.pricePerUnit = pricePerUnit;
        this.quantity = quantity;
    }

    public decimal GetTotalCost()
    {
        return pricePerUnit * quantity;
    }

    public string GetName() => name;
    public int GetProductId() => productId;
}

class Address
{
    private string street;
    private string city;
    private string stateProvince;
    private string country;

    public Address(string street, string city, string stateProvince, string country)
    {
        this.street = street;
        this.city = city;
        this.stateProvince = stateProvince;
        this.country = country;
    }

    public bool IsInUSA()
    {
        return country.ToUpper() == "USA";
    }

    public string GetFullAddress()
    {
        return $"{street}\n{city}, {stateProvince}\n{country}";
    }
}

class Customer
{
    private string name;
    private Address address;

    public Customer(string name, Address address)
    {
        this.name = name;
        this.address = address;
    }

    public bool LivesInUSA()
    {
        return address.IsInUSA();
    }

    public string GetName() => name;
    public Address GetAddress() => address;
}

class Order
{
    private List<Product> products = new List<Product>();
    private Customer customer;

    public Order(Customer customer)
    {
        this.customer = customer;
    }

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public decimal GetTotalCost()
    {
        decimal total = 0;
        foreach (Product p in products)
        {
            total += p.GetTotalCost();
        }

        total += customer.LivesInUSA() ? 5 : 35;
        return total;
    }

    public string GetPackingLabel()
    {
        string label = "Packing Label:\n";
        foreach (Product p in products)
        {
            label += $"{p.GetName()} (ID: {p.GetProductId()})\n";
        }
        return label;
    }

    public string GetShippingLabel()
    {
        return $"Shipping Label:\n{customer.GetName()}\n{customer.GetAddress().GetFullAddress()}";
    }
}

// Example usage
class Program
{
    static void Main()
    {
        Address addr1 = new Address("123 Maple St", "Rexburg", "ID", "USA");
        Customer cust1 = new Customer("Luke Skywalker", addr1);
        Order order1 = new Order(cust1);
        order1.AddProduct(new Product("Lightsaber", 101, 250.00m, 1));
        order1.AddProduct(new Product("Jedi Robes", 102, 75.00m, 2));

        Address addr2 = new Address("456 Tatooine Rd", "Mos Eisley", "Outer Rim", "Tattooine");
        Customer cust2 = new Customer("Anakin Skywalker", addr2);
        Order order2 = new Order(cust2);
        order2.AddProduct(new Product("Podracer Engine", 201, 400.00m, 1));
        order2.AddProduct(new Product("Helmet", 202, 150.00m, 1));

        PrintOrder(order1);
        Console.WriteLine();
        PrintOrder(order2);
    }

    static void PrintOrder(Order order)
    {
        Console.WriteLine(order.GetPackingLabel());
        Console.WriteLine(order.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order.GetTotalCost():F2}");
    }
}
