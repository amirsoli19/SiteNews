using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSNewModels.Context;
using CMSNewModels.Models;
using CMSNewRespository.Repository;


namespace CMSNewService.Service
{
    public class GenericService<T>:GenericRepository<T>where T:BaseEntity
    {
        public GenericService(DbCMSNewsContext context) : base(context)
        {

        }
    }
}
