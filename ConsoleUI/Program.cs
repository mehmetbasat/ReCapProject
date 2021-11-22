// See https://aka.ms/new-console-template for more information

using Business.Concrete;
using DataAccess.EntityFramework;

CarManager carManager = new CarManager(new EfCarDal());

foreach (var car in carManager.GetCarDetails())
{
    Console.WriteLine(car.CarName + " / " + car.BrandName + " / " + car.ColorName + " / " + car.DailyPrice);
}
