namespace WebApi.Models;

public class CreateProductModel
{
    public string Name { get; set; }
    public string ProductName { get; set; }
    public float UnitPrice { get; set; }
    public string QuantityPerUnit { get; set; }
    public int UnitsInStock { get; set; }
    public string CategoryName { get; set; }

}

