﻿using Business.Abstract;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transactional;
using Core.Aspects.Autofac.Validation;
using Core.DataAccess;
using Core.Utilities.Result;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }
        //[SecuredOperation("admin")]
        [ValidationAspect(typeof(CarValidator))]
        [TransactionScopeAspect]
        public IResult Add(Car car)
        {
            _carDal.Add(car);

            return new SuccessResult(Messages.AddedCar);

        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.DeletedCar);

        }

        [CacheAspect()]
        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour==7)
            {
                return new ErrorDataResult<List<Car>>(Messages.MaintenanceTime);

            }

            return new SuccessDataResult<List<Car>>(_carDal.GetAll(),Messages.ListedCar);

        }
        [PerformanceAspect(5)]
        public IDataResult<Car> GetById(int carId)
        {
            Thread.Sleep(5000);
            return new SuccessDataResult<Car>(_carDal.GetById(c => c.Id == carId));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }

        public IDataResult< List<CarDetailDto>> GetCarsByBrandIdWithDetails(int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarsByBrandIdWithDetails(brandId));

        }

        public IDataResult<List<CarDetailDto>> GetCarsByColorIdWithDetails(int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarsByColorIdWithDetails(colorId));

        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByCarId(int carId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetailsByCarId(carId));
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(b => b.BrandId == brandId));
        }

        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c=>c.ColorId == colorId));
        }


        public IResult Update(Car car)
        {
            if (car.Id != null)
            {
                Car updatedCar = _carDal.GetById(c => c.Id == car.Id);
                if(car.ColorId != 0)
                    updatedCar.ColorId = car.ColorId;
                if (car.ModelYear != 0)
                    updatedCar.ModelYear = car.ModelYear;
                if (car.CarName != "string")
                    updatedCar.CarName = car.CarName;
                if (car.BrandId != 0)
                    updatedCar.BrandId = car.BrandId;
                if (car.DailyPrice != 0)
                    updatedCar.DailyPrice = car.DailyPrice;
                if (car.Description != "string")
                    updatedCar.Description = car.Description;
                _carDal.Update(updatedCar);
            }
            return new SuccessResult(Messages.UpdatedCar);
        }
    }
}
