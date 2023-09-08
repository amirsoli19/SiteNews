using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSNewModels.Context;
using CMSNewModels.Models;


namespace CMSNewService.Service
{
     public interface INewsGroupService:IGenericService<NewGroup>
    {
        int NextNewsGroupId();
    }
}
