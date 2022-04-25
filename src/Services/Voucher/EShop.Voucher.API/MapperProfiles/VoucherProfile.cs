using AutoMapper;
using EShop.Voucher.API.Entities;
using EShop.Voucher.GRPC.Protos;

namespace EShop.Voucher.API.MapperProfiles
{
    public class VoucherProfile : Profile
    {
        public VoucherProfile()
        {
            CreateMap<EVoucher, VoucherModel>().ReverseMap();
        }
    }
}
