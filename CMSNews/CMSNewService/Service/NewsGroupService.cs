using CMSNewModels.Context;
using CMSNewModels.Models;
using CMSNewRespository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CMSNewService.Service
{
    public class NewsGroupService : GenericService<NewGroup>, INewsGroupService
    {
        private INewsGroupRepository _newsGroupRepository;
        public NewsGroupService(DbCMSNewsContext context) : base(context)
        {
            _newsGroupRepository = new NewsGroupRepository(context);

             
        }

        public int NextNewsGroupId()
        {
            int max = 1;
            var newsGroups = _newsGroupRepository.GetAll().ToList();
            if(newsGroups.Count>0)
            {
                max=newsGroups.Max(t=>t.NewsGroupId)+1;
            }
            return max;
        }
    }
}
