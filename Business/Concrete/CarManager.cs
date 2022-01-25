using Business.Abstract;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transactional;
using Core.Aspects.Autofac.Validation;
using Core.Business;
using Core.DataAccess;
using Core.Utilities.Result;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {

        private readonly ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            _carDal.Add(car);
            return new SuccessResult(Messages.AddedCar);
        }

        public IResult Delete(int carId)
        {
            var rulesResult = BusinessRules.Run(CheckIfCarIdExist(carId));
            if (rulesResult != null)
            {
                return rulesResult;
            }

            var deletedCar = _carDal.GetById(c => c.Id == carId);
            _carDal.Delete(deletedCar);
            return new SuccessResult(Messages.DeletedCar);
        }

        
        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.ListedCar);

        }

        [CacheAspect(10)]
        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.GetById(c => c.Id == id), Messages.ListedCar);

        }

        public IDataResult<CarDetailDto> GetCarDetails(int carId)
        {
            return new SuccessDataResult<CarDetailDto>(_carDal.GetCarDetails(c => c.Id == carId).SingleOrDefault(), Messages.ListedCar);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == id), Messages.ListedCar);
        }

        public IDataResult<List<CarDetailDto>> GetCarsByBrandIdWithDetails(int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c => c.BrandId == brandId), Messages.ListedCar);
        }

        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == id), Messages.ListedCar);
        }

        public IDataResult<List<CarDetailDto>> GetCarsByColorIdWithDetails(int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c => c.ColorId == colorId), Messages.ListedCar);
        }

        public IDataResult<List<CarDetailDto>> GetCarsByFilterWithDetails(int brandId, int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c => c.BrandId == brandId && c.ColorId == colorId), Messages.ListedCar);
        }

        public IDataResult<List<CarDetailDto>> GetCarsWithDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(), Messages.ListedCar);
        }

        public IResult Update(Car car)
        {
            var rulesResult = BusinessRules.Run(CheckIfCarIdExist(car.Id));
            if (rulesResult != null)
            {
                return rulesResult;
            }
            _carDal.Update(car);
            return new SuccessResult(Messages.UpdatedCar);
        }

        //Business Rules

        private IResult CheckIfCarIdExist(int carId)
        {
            var result = _carDal.GetAll(c => c.Id == carId).Any();
            if (!result)
            {
                return new ErrorResult(Messages.CarNotExist);
            }
            return new SuccessResult();
        }
    }
}
