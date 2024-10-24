// See https://aka.ms/new-console-template for more information
using DataLayer;


var ds = new DataService();



var od = ds.GetProductByCategory(100001);
if (od == null)
{
    Console.WriteLine("Null");
}
Console.WriteLine(od.First().Name);
//Console.WriteLine(od.First().Order?.Date.ToString("yyyy-MM-dd"));
//Console.WriteLine(od.First().UnitPrice);
//Console.WriteLine(od.First().Quantity);

