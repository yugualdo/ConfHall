namespace ConfHall.Domain.Mapper
{
    using ConfHall.Domain.Entities;
    using ConfHall.Model;
    using AutoMapper;
    using ConfHall.Models;

    /// <summary>
    /// 
    /// </summary>
    public class ConfigureAutoMapper
    {
        #region Methods
        /// <summary>
        /// 
        /// </summary>
        public static void Now()
        {
            Mapper.Initialize(cfg =>
            {
                // From Entitty to Model
                cfg.CreateMap<User, UserModel>();
                cfg.CreateMap<Role, RoleModel>();
                cfg.CreateMap<Hall, HallModel>();
                cfg.CreateMap<Customer, CustomerModel>();
                cfg.CreateMap<Reservation, ReservationModel>();
                cfg.CreateMap<Feature, FeatureModel>();
                // From Model to Entity
                cfg.CreateMap<UserModel, User>();
                cfg.CreateMap<RoleModel, Role>();
                cfg.CreateMap<HallModel, Hall>();
                cfg.CreateMap<CustomerModel, Customer>();
                cfg.CreateMap<ReservationModel, Reservation>();
                cfg.CreateMap<FeatureModel, Feature>();
                // From Entity to Entity

            });
        }

        #endregion Methods
    }
}
