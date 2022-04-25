using AutoMapper;
using EShop.Voucher.GRPC.Entities;
using EShop.Voucher.GRPC.Protos;
using EShop.Voucher.GRPC.Repositories;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace EShop.Voucher.GRPC.Services
{
    public class EVoucherService : VoucherProtoService.VoucherProtoServiceBase
    {
        private readonly IVoucherRepository _voucherRepository;
        private readonly IMapper _mapper;

        public EVoucherService(
            IVoucherRepository voucherRepository,
            IMapper mapper
            )
        {
            _voucherRepository = voucherRepository ?? throw new ArgumentNullException(nameof(voucherRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<VoucherModel> GetVoucher(GetVoucherRequest request, ServerCallContext context)
        {
            var voucher = _voucherRepository.GetVoucher(request.VoucherCode);

            if (voucher == null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Voucher not found. VoucherCode={request.VoucherCode}"));

            var standardModel = _mapper.Map<VoucherModel>(voucher);
            return standardModel;
        }

        public override async Task<VoucherModel> CreateVoucher(CUDVoucherRequest request, ServerCallContext context)
        {
            var eVoucher = _mapper.Map<EVoucher>(request.VoucherModel);
            var voucher = _voucherRepository.CreateVoucher(eVoucher);

            if (voucher == null)
                throw new RpcException(new Status(StatusCode.Internal, $"Could not create a new voucher"));

            var standardModel = _mapper.Map<VoucherModel>(voucher);
            return standardModel;
        }

        public override async Task<DeleteVoucherResponse> DeleteVoucher(CUDVoucherRequest request, ServerCallContext context)
        {
            var eVoucher = _mapper.Map<EVoucher>(request.VoucherModel);
            var isSuccess = _voucherRepository.DeleteVoucher(eVoucher);

            return new DeleteVoucherResponse
            {
                Success = isSuccess
            };
        }

        public override async Task<VoucherModel> UpdateVoucher(CUDVoucherRequest request, ServerCallContext context)
        {
            var eVoucher = _mapper.Map<EVoucher>(request.VoucherModel);
            var voucher = _voucherRepository.UpdateVoucher(eVoucher);

            if (voucher == null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Could not update the voucher. VoucherCode={request.VoucherModel.Code}"));

            var standardModel = _mapper.Map<VoucherModel>(voucher);
            return standardModel;
        }
    }
}
