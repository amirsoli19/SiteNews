using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSNewModels.Models;
using CMSNewRespository.Repository;

namespace CMSNewService.Service
{
    public interface IGenericService<T> where T:BaseEntity
    {

    }
}
