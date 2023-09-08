using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSNewModels.Models;
using CMSNewModels.Context;
using CMSNewRespository.Repository;

namespace CMSNewRespository.Repository
{
    public class NewsGroupRepository:GenericRepository<NewGroup>,INewsGroupRepository
    {
        public NewsGroupRepository(DbCMSNewsContext context) : base(context)
        {

        }
    }
}
