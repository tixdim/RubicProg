using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RubicProg.BusinessLogic.Core.Models;
using RubicProg.DataAccess.Core.Models;

namespace RubicProg.BusinessLogic.AutoMapperProfile
{
    public class BusinessLogicProfile: Profile
    {
        public BusinessLogicProfile()
        {
            CreateMap<UserRto, UserUpdateBlo>();
            CreateMap<UserRto, UserUpdateDobleBlo>();
        }
    }
}
