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
    public class EfBrandDal:EfEntityRepositoryBase<Brand, ReCapContext>, IBrandDal
    {
        
    }
}
