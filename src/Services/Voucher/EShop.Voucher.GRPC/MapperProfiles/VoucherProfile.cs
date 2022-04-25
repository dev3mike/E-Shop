using AutoMapper;
using EShop.Voucher.GRPC.Entities;
using EShop.Voucher.GRPC.Protos;

namespace EShop.Voucher.GRPC.MapperProfiles
{
    public class VoucherProfile : Profile
    {
        public VoucherProfile()
        {
            CreateMap<EVoucher, VoucherModel>().ReverseMap();
        }
    }
}
