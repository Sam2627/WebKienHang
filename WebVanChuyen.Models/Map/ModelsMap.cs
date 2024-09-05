using AutoMapper;
using WebVanChuyen.Models.Models;

namespace WebVanChuyen.App.Models
{
    public class ModelsMap : Profile
    {
        // Instruct CreateMap<Soure, Destination>();
        public ModelsMap()
        {
            CreateMap<Category, AddCategory>();

            CreateMap<ShipmentTrip, AddShipmentTrip>();

            CreateMap<AddShipmentTrip, ShipmentTrip>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)src.Status));

            CreateMap<AddPackage, Package>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (int)src.Status))
                .ForMember(dest => dest.PayStatus, opt => opt.MapFrom(src => (int)src.PayStatus));

            CreateMap<Employee, EmployeeInfo>();

            CreateMap<AddWarehouse, Warehouse>();
        }
    }
}
