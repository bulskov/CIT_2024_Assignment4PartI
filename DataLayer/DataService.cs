using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataLayer;
public class DataService : IDataService
{
    public IList<Category> GetCategories()
    {
        var db = new NorthwindContext();
        return db.Categories.ToList();
    }
    
    public Category ? GetCategory(int id)
    {
        var db = new NorthwindContext();
        return db.Categories.FirstOrDefault(x=>x.Id == id);
    }
    
    public Category ? CreateCategory(string name, string description)
    {
        var db = new NorthwindContext();

        int id = db.Categories.Max(x => x.Id) + 1;
        var category = new Category
        {
            Id = id,
            Name = name,
            Description = description
        };

        db.Categories.Add(category);

        db.SaveChanges();

        return db.Categories.FirstOrDefault(x => x.Id == category.Id);

    }

    public bool DeleteCategory(int id)
    {
        var db = new NorthwindContext();

        var category = db.Categories.Find(id);

        if (category == null)
        {
            return false;
        }

        db.Categories.Remove(category);

        return db.SaveChanges() > 0;

    }

    public bool UpdateCategory(int id, string name, string description)
    {
        var db = new NorthwindContext();

        var category = db.Categories.FirstOrDefault(x => x.Id == id);

        if (category == null)
        {
            return false;
        }
        else
        {
            category.Name=name;
            category.Description=description;
            return db.SaveChanges() > 0; ;
        }
    }

    public ProductWithCategoryName ? GetProduct(int id)
    {
        var db = new NorthwindContext();
        var product = db.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == id);

        if (product == null)
        {
            return null;
        }

        return new ProductWithCategoryName
        {
            Id = product.Id,
            Name = product.Name,
            ProductName = product.Name,
            QuantityPerUnit = product.QuantityPerUnit,
            UnitPrice = product.UnitPrice,
            UnitsInStock = product.UnitsInStock,
            CategoryName = product.Category.Name
        };
    }
    
    public IList<ProductWithCategoryName> ? GetProductByCategory(int id)
    {
        var db = new NorthwindContext();
        var product= db.Products.Include(x => x.Category)
        .Where(x => x.Category.Id == id)
        .ToList();
        /*if (product.Count == 0)
        {
            return null;
        }*/
        var ProWithCatNamList = new List<ProductWithCategoryName>();
        foreach (var item in product)
        {
            ProWithCatNamList.Add(new ProductWithCategoryName
            {
                Id = item.Id,
                Name = item.Name,
                ProductName = item.Name,
                QuantityPerUnit = item.QuantityPerUnit,
                UnitPrice = item.UnitPrice,
                UnitsInStock = item.UnitsInStock,
                CategoryName = item.Category.Name
            });
        }
        return ProWithCatNamList;
    }
    public IList<ProductWithCategoryName> ? GetProductByName(string name)
    {
        var db = new NorthwindContext();
        var product= db.Products.Include(x => x.Category)
        .Where(x => x.Name.ToLower().Contains(name.ToLower()))
        .ToList();
        var ProWithCatNamList = new List<ProductWithCategoryName>();
        foreach (var item in product)
        {
            ProWithCatNamList.Add(new ProductWithCategoryName
            {
                Id = item.Id,
                Name = item.Name,
                ProductName = item.Name,
                QuantityPerUnit = item.QuantityPerUnit,
                UnitPrice = item.UnitPrice,
                UnitsInStock = item.UnitsInStock,
                CategoryName = item.Category.Name
            });
        }
        return ProWithCatNamList;
    }

    public Order? GetOrder(int id)
    {
        var db = new NorthwindContext();
        return db.Orders
            .Include(x => x.OrderDetails)
            .ThenInclude(x => x.Product)
            .ThenInclude(x => x.Category)
            .FirstOrDefault(x => x.Id == id);
    }

    public IList<Order> GetOrders()
    {
        var db = new NorthwindContext();
        return db.Orders
            .Include(x => x.OrderDetails)
            .ThenInclude(x => x.Product)
            .ThenInclude(x => x.Category)
            .ToList();
    }

    public IList<OrderDetails> ? GetOrderDetailsByOrderId(int id)
    {
        var db = new NorthwindContext();
        var orderDetails = db.OrderDetails
            .Include(x => x.Product)
            .ThenInclude(x => x.Category)
            .Where(x => x.OrderId == id)
            .ToList();
        if (orderDetails == null)
        {
            return null;
        }
        return orderDetails;
    }

    public IList<OrderDetails>? GetOrderDetailsByProductId(int id)
    {
        var db = new NorthwindContext();
        var orderDetails = db.OrderDetails
            .Include(x => x.Order)
            .Include(x => x.Product)
            .ThenInclude(x => x.Category)
            .Where(x => x.Product.Id == id)
            .OrderBy (x => x.OrderId)
            .ToList();
        if (orderDetails == null)
        {
            return null;
        }
        return orderDetails;
    }
}
