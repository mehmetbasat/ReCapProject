using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, ReCapContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (ReCapContext context = new ReCapContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.Id
                             join a in context.Colors on c.ColorId equals a.Id

                             select new CarDetailDto
                             {
                                 Id = c.Id,
                                 BrandName = b.Name,
                                 BrandId = b.Id,
                                 ColorName = a.Name,
                                 ColorId = a.Id,
                                 DailyPrice = c.DailyPrice,
                                 ModelYear = c.ModelYear,
                                 CarName = c.CarName,
                                 Description = c.Description,
                                 ImagePath = (from m in context.CarImages where m.CarId == c.Id select m.ImagePath).FirstOrDefault()

                             };

                return result.ToList();

            }

        }

        public List<CarDetailDto> GetCarsByBrandIdWithDetails(int brandId)
        {
            var result = GetCarDetails().Where(b => b.BrandId == brandId);
            return result.ToList();
        }

        public List<CarDetailDto> GetCarsByColorIdWithDetails(int colorId)
        {
            var result = GetCarDetails().Where(b => b.ColorId == colorId);
            return result.ToList();
        }

        public List<CarDetailDto> GetCarDetailsByCarId(int carId)
        {
            using (ReCapContext context = new ReCapContext())
            {
                var result = from c in context.Cars
                    join b in context.Brands
                        on c.BrandId equals b.Id
                    join co in context.Colors
                        on c.ColorId equals co.Id
                    where c.Id == carId
                    select new CarDetailDto
                    {
                        Id = c.Id,
                        BrandName = b.Name,
                        ColorName = co.Name,
                        ModelYear = c.ModelYear,
                        DailyPrice = c.DailyPrice,
                        BrandId = b.Id,
                        CarName = c.CarName,
                        ColorId = co.Id,
                        Description = c.Description,
                        ImagePath = (from m in context.CarImages where m.CarId == c.Id select m.ImagePath).FirstOrDefault()
                    };
                return result.ToList();
            }
        }


    }
}
