namespace ConfHall.Domain.Mapper
{
    using ConfHall.Domain.Entities;
    using ConfHall.Model;
    using AutoMapper;
    using ConfHall.Models;

    public class ConfigureAutoMapper
    {
        #region Methods

        public static void Now()
        {
            Mapper.Initialize(cfg =>
            {
                // From Entitty to Model
                cfg.CreateMap<User, UserModel>();
                cfg.CreateMap<Role, RoleModel>();
                cfg.CreateMap<Hall, HallModel>();
                // From Model to Entity
                cfg.CreateMap<UserModel, User>();
                cfg.CreateMap<RoleModel, Role>();
                cfg.CreateMap<HallModel, Hall>();
                // From Entity to Entity

            });
        }

        #endregion Methods
    }
}
