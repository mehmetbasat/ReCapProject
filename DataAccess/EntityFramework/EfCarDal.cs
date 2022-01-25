using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System.Linq.Expressions;

namespace DataAccess.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, ReCapContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails(Expression<Func<CarDetailDto, bool>> filter = null)
        {
            using (ReCapContext context = new ReCapContext())
            {

                var result = from c in context.Cars
                             join b in context.Brands
                                 on c.BrandId equals b.Id
                             join co in context.Colors
                                 on c.ColorId equals co.Id
                             select new CarDetailDto
                             {
                                 Id = c.Id,
                                 BrandId = c.BrandId,
                                 BrandName = b.Name,
                                 ColorId = c.ColorId,
                                 ColorName = co.Name,
                                 CarName = c.CarName,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description,
                                 ModelYear = c.ModelYear,
                                 CarImages = ((from ci in context.CarImages
                                     where (c.Id == ci.CarId)
                                     select new CarImage
                                     {
                                         Id = ci.Id,
                                         CarId = ci.CarId,
                                         Date = ci.Date,
                                         ImagePath = ci.ImagePath
                                     }).ToList()).Count == 0
                                     ? new List<CarImage> { new CarImage { Id = -1, CarId = c.Id, Date = DateTime.Now, ImagePath = "/images/default.jpg" } }
                                     : (from ci in context.CarImages
                                         where (c.Id == ci.CarId)
                                         select new CarImage
                                         {
                                             Id = ci.Id,
                                             CarId = ci.CarId,
                                             Date = ci.Date,
                                             ImagePath = ci.ImagePath
                                         }).ToList()
                             };
                return filter == null
                ? result.ToList()
                : result.Where(filter).ToList();
            }
        }
    }
}
