using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProductType
{
    Vegetables,
    Toys,
    Bakery,
    None = 999
    /*Cake,
    Donuts,
    Hamburger,
    HamEgg,
    IceCream,
    Milk,
    Waffle,
    Bear,
    Monkey,
    Penguin,
    Pig,
    Rabbit,
    Sheep,
    Apple,
    Banana,
    Coconut,
    Orange,
    Pear,
    Pepper,
    Pineapple,
    Strawberry,
    Tomato,
    Watermelon*/
}

public class Product
{
    public readonly ProductType Type;
    public readonly int Cost;

    private Product(ProductType type, int cost)
    {
        Type = type;
        Cost = cost;
    }
    public static readonly Product Vegetable = new Product(ProductType.Vegetables,10);
    public static readonly Product Toy = new Product(ProductType.Toys, 20);
    public static readonly Product Bakery = new Product(ProductType.Bakery,30);
    public static readonly Product Empty = new Product(ProductType.None, 0);
    
    /*public static readonly Product Cake = new Product(ProductType.Cake, 30);
    public static readonly Product Donuts = new Product(ProductType.Donuts, 30);
    public static readonly Product Hamburger = new Product(ProductType.Hamburger, 30);
    public static readonly Product HamEgg = new Product(ProductType.HamEgg, 30);
    public static readonly Product IceCream = new Product(ProductType.IceCream, 30);
    public static readonly Product Milk = new Product(ProductType.Milk, 30);*/
}

