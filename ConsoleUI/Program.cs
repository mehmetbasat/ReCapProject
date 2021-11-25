// See https://aka.ms/new-console-template for more information

using Business.Concrete;
using DataAccess.EntityFramework;

CarManager carManager = new CarManager(new EfCarDal());

var result = carManager.GetCarDetails();

if (result.Success == true)
{
    foreach (var car in result.Data)
    {

        Console.WriteLine(car.CarName + " / " + car.BrandName + " / " + car.ColorName + " / " + car.DailyPrice);
    }
}

else
{
    Console.WriteLine(result.Message);
}


