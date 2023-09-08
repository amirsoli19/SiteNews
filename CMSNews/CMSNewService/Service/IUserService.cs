﻿using CMSNewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSNewService.Service
{
     public interface IUserService: IGenericService<User>
    {
        int GetUserId(string mobileNumber);
    }
}
