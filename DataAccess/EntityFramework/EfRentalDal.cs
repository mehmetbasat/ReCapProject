using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DTOs;

namespace DataAccess.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, ReCapContext>, IRentalDal
    {
        public List<Rental> GetCarIdAndReturnDate(int carId, DateTime? returnDate)
        {
            using (ReCapContext context = new ReCapContext())
            {
                var result = from c in context.Rentals where c.CarId == carId && c.ReturnDate == returnDate select c;
                return result.ToList();

            }

        }

        public List<RentalDetailDto> GetRentalDetails()
        {
            using (var context = new ReCapContext())
            {
                var result = from r in context.Rentals
                    join cr in context.Cars
                        on r.CarId equals cr.Id
                    join cs in context.Customers
                        on r.CustomerId equals cs.CustomerId
                    join br in context.Brands
                        on cr.BrandId equals br.Id
                    join u in context.Users
                        on cs.UserId equals u.Id

                    select new RentalDetailDto
                    {
                        RentalId = r.Id,
                        BrandName = br.Name,
                        CustomerName = u.Firstname + " " + u.Lastname,
                        RentDate = r.RentDate,
                        ReturnDate = r.ReturnDate
                    };
                return result.ToList();
            }
        }
    }
}
