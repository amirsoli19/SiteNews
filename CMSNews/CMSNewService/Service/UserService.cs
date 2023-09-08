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
    public class UserService : GenericService<User>, IUserService
    {
        private IUserRepository _userRepository;
        public UserService(DbCMSNewsContext context) : base(context)
        {
            _userRepository = new UserRepository(context);
        }

        public int GetUserId(string mobileNumber)
        {
            return _userRepository.GetAll().FirstOrDefault(t=>t.MobileNumber== mobileNumber).UserId;
        }
    }
}
