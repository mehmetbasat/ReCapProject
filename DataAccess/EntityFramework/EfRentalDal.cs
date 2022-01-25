using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Entities.DTOs;

namespace DataAccess.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, ReCapContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalsDetails(Expression<Func<RentalDetailDto, bool>> filter = null)
        {
            using (ReCapContext context = new ReCapContext())
            {
                var result = from r in context.Rentals
                    join c in context.Cars
                        on r.CarId equals c.Id
                    join cu in context.Customers
                        on r.CustomerId equals cu.CustomerId
                    join u in context.Users
                        on cu.UserId equals u.Id
                    join b in context.Brands
                        on c.BrandId equals b.Id
                    join p in context.Payments
                        on r.PaymentId equals p.Id
                    select new RentalDetailDto
                    {
                        Id = r.Id,
                        CarId = c.Id,
                        ModelFullName = $"{b.Name} {c.CarName}",
                        CustomerId = cu.CustomerId,
                        CustomerFullName = $"{u.Firstname} {u.Lastname}",
                        ReturnDate = r.ReturnDate,
                        DailyPrice = c.DailyPrice,
                        RentDate = r.RentDate,
                        PaymentId = r.PaymentId,
                        PaymentDate = p.PaymentDate,
                        DeliveryStatus = r.DeliveryStatus
                    };
                return filter == null
                    ? result.ToList()
                    : result.Where(filter).ToList();
            }
        }
    }
}
