using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSNewModels.Context;
using CMSNewModels.Models;


namespace CMSNewService.Service
{
    public class NewsService : GenericService<News>, INewsService
    {
        public NewsService(DbCMSNewsContext context) : base(context)
        {
        }
    }
}
