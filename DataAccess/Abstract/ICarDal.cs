using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface ICarDal : IEntityRepository<Car>
    {
        List<CarDetailDto> GetCarDetails(Expression<Func<CarDetailDto, bool>> filter = null);




    }
}
