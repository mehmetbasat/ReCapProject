﻿using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface ICarDal : IEntityRepository<Car>
    {
        List<CarDetailDto> GetCarDetails();
        List<CarDetailDto> GetCarsByBrandIdWithDetails(int brandId);
        List<CarDetailDto> GetCarsByColorIdWithDetails(int colorId);
        List<CarDetailDto> GetCarDetailsByCarId(int carId);


    }
}
