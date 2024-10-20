// See https://aka.ms/new-console-template for more information
using DataLayer;


var ds = new DataService();



var od = ds.GetOrderDetailsByProductId(11);

Console.WriteLine(od.Count);
Console.WriteLine(od.First().OrderId);
Console.WriteLine(od.First().Order?.Date.ToString("yyyy-MM-dd"));
Console.WriteLine(od.First().UnitPrice);
Console.WriteLine(od.First().Quantity);

