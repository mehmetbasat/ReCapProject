using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFramework
{
    public class EfRentalDal:EfEntityRepositoryBase<Rental, ReCapContext>, IRentalDal
    {
        public List<Rental> GetCarIdAndReturnDate(int carId,DateTime? returnDate)
        {
            using (ReCapContext context = new ReCapContext())
            {
                var result = from c in context.Rentals where c.CarId == carId && c.ReturnDate == returnDate select c;
                return result.ToList();

            }

        }
    }
}
