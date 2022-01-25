using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.EntityFramework;
using Entities.Concrete;

namespace DataAccess.EntityFramework
{
    public class EfPaymentDal : EfEntityRepositoryBase<Payment, ReCapContext>, IPaymentDal
    {
    }
}
