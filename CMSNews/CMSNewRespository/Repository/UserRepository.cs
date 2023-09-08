using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSNewModels.Models;
using CMSNewModels.Context;

namespace CMSNewRespository.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public  UserRepository(DbCMSNewsContext context):base(context)
        {

        }
    }
}
