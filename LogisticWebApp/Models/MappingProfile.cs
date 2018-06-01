using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Logistic.Web.Models
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserVm>();
            CreateMap<ApplicationUserVm, ApplicationUser>();
        }
    }
}
