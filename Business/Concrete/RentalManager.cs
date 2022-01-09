using Business.Abstract;
using Business.Constans;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Entities.DTOs;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }
        
        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
            if (_rentalDal.GetCarIdAndReturnDate(rental.CarId,null).Count >0)
            {
                return new ErrorResult("Ekleme Başarısız Araba Kullanımda");
               
            }
            else
            {
                rental.ReturnDate = null;
                _rentalDal.Add(rental);
                return new SuccessResult(Messages.AddedRental);
            }  
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.DeletedRental);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.ListedRental);
        }


        public IDataResult<Rental> GetById(int rentalId)
        {
            return new SuccessDataResult<Rental>(_rentalDal.GetById(c => c.Id == rentalId), Messages.GettedRental);
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails());
        }

        public IResult SetReturnedById(int id)
        {
            DateTime now = DateTime.Now;
            Rental updatedRental = _rentalDal.GetById(c => c.Id == id);
            updatedRental.ReturnDate = now;
            _rentalDal.Update(updatedRental);
            return new SuccessResult(Messages.ReturnOk);
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.UpdatedRental);
        }
    }

}
