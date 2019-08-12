using AutoMapper;
using Permission.Library.Web.WebModel.SystemManager;
using Permission.Model.DbModel.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permission.Library.Web.AutoMapperManager.ModelMapConfig
{
    public class AdminUserProfile: Profile
    {
        public AdminUserProfile()
        {
            CreateMap<AdminUserWebM, AdminUserDb>().ForMember(dest => dest.pass_word, opt => opt.Condition(src => !string.IsNullOrEmpty(src.ConfirmPassword)));
        }
        
    }
}
