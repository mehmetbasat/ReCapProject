using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IRentalDal : IEntityRepository<Rental>
    {
        List<Rental> GetCarIdAndReturnDate(int carId, DateTime? returnDate);
        List<RentalDetailDto> GetRentalDetails();

    }
}
