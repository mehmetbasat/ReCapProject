﻿using Core.Utilities.Result;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IDataResult<List<Rental>> GetAll();
        IDataResult<Rental> GetRentalById(int rentalId);
        IDataResult<bool> CheckIfCanCarBeRentedNow(int carId);
        IDataResult<bool> CheckIfAnyRentalBetweenSelectedDates(int carId, DateTime rentDate, DateTime returnDate);
        IDataResult<List<RentalDetailDto>> GetRentalsDetails();
        IDataResult<List<RentalDetailDto>> GetRentalsByCustomerIdWithDetails(int customerId);
        IResult Add(Rental rental);
        IResult Delete(int rentalId);
        IResult Update(Rental rental);
        IDataResult<int> Rent(RentPaymentRequest rentPaymentRequest);
    }
}
