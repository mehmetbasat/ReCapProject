using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.EntityFramework;
using Entities.Concrete;

namespace DataAccess.EntityFramework
{
    public class EfCreditCardDal : EfEntityRepositoryBase<CreditCard, ReCapContext>, ICreditCardDal
    {
    }
}
