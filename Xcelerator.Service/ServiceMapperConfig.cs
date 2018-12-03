using AutoMapper;
using Xcelerator.Entity;
using Xcelerator.Model;
using Xcelerator.Model.View;

namespace Xcelerator.Service
{
    public class ServiceMapperConfig
    {
        public static void Config()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<AuditDTO, Audit>().ReverseMap();
                cfg.CreateMap<RegisterViewModel, User>().ReverseMap();
                cfg.CreateMap<UserDTO, User>().ReverseMap();
                cfg.CreateMap<RoleDTO, Role>().ReverseMap();
            });
        }
    }
}
