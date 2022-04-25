using AutoMapper;
using EShop.Voucher.API.Entities;
using EShop.Voucher.GRPC.Protos;
using System;
using static EShop.Voucher.GRPC.Protos.VoucherProtoService;

namespace EShop.Voucher.API.Repositories
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly VoucherProtoServiceClient _voucherProtoServiceClient;
        private readonly IMapper _mapper;

        public VoucherRepository(
            VoucherProtoServiceClient voucherProtoServiceClient,
            IMapper mapper)
        {
            _voucherProtoServiceClient = voucherProtoServiceClient ?? throw new ArgumentNullException(nameof(voucherProtoServiceClient));
            _mapper = mapper;
        }

        public EVoucher CreateVoucher(EVoucher eVoucher)
        {
            var request = new CUDVoucherRequest
            {
                VoucherModel = _mapper.Map<VoucherModel>(eVoucher),
            };
            var voucher = _voucherProtoServiceClient.CreateVoucher(request);

            var standardModel = _mapper.Map<EVoucher>(voucher);
            return standardModel;
        }

        public bool DeleteVoucher(EVoucher eVoucher)
        {
            // Call GRPC Service
            throw new NotImplementedException();
        }

        public EVoucher GetVoucher(string voucherCode)
        {
            var request = new GetVoucherRequest
            {
                VoucherCode = voucherCode
            };
            var voucher = _voucherProtoServiceClient.GetVoucher(request);

            var standardModel = _mapper.Map<EVoucher>(voucher);
            return standardModel;
        }

        public EVoucher UpdateVoucher(EVoucher eVoucher)
        {
            // Call GRPC Service
            throw new NotImplementedException();
        }
    }
}
