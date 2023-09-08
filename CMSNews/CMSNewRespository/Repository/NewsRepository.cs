using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSNewModels.Models;
using CMSNewModels.Context;

namespace CMSNewRespository.Repository
{
    public class NewsRepository : GenericRepository<News>,INewsRepository
    {
        public NewsRepository(DbCMSNewsContext context) : base(context)
        {

        }
    }
}
