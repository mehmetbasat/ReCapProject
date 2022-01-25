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
    public class EfCustomerDal:EfEntityRepositoryBase<Customer, ReCapContext>, ICustomerDal
    {
        public List<CustomerDetailDto> GetCustomersDetail()
        {
            using (ReCapContext context = new ReCapContext())
            {
                var result = from c in context.Customers
                    join u in context.Users
                        on c.UserId equals u.Id
                    select new CustomerDetailDto
                    {
                        Id = c.CustomerId,
                        FirstName = u.Firstname,
                        LastName = u.Lastname,
                        Email = u.Email,
                        CompanyName = c.CompanyName
                    };
                return result.ToList();
            }
        }
    }
}
