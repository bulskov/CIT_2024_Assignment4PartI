// See https://aka.ms/new-console-template for more information
using DataLayer;


var ds = new DataService();


//foreach(var item in )
//{
//    Console.WriteLine(item.Name);
//}

var od = ds.GetOrderDetailsByProductId(11);

Console.WriteLine(od.Count);
Console.WriteLine(od.First().OrderId);
Console.WriteLine(od.First().Order?.Date.ToString("yyyy-MM-dd"));
Console.WriteLine(od.First().UnitPrice);
Console.WriteLine(od.First().Quantity);

/*
foreach (var item in od)
{
    Console.WriteLine(item.Name);
}
10528	1997-05-06	21	3
Assert.Equal(38, orderDetails.Count);
Assert.Equal(10248, orderDetails.First().OrderId);
Assert.Equal("1996-07-04", orderDetails.First().Order?.Date.ToString("yyyy-MM-dd"));
Assert.Equal(14, orderDetails.First().UnitPrice);
Assert.Equal(12, orderDetails.First().Quantity);*/