using EShop.Voucher.API.Entities;
using Microsoft.Extensions.Configuration;
using System;

namespace EShop.Voucher.API.Repositories
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly IConfiguration _configuration;


        public EVoucher CreateVoucher(EVoucher eVoucher)
        {
            // Call GRPC Service
            throw new NotImplementedException();
        }

        public bool DeleteVoucher(EVoucher eVoucher)
        {
            // Call GRPC Service
            throw new NotImplementedException();
        }

        public EVoucher GetVoucher(string voucherCode)
        {
            // Call GRPC Service
            throw new NotImplementedException();
        }

        public EVoucher UpdateVoucher(EVoucher eVoucher)
        {
            // Call GRPC Service
            throw new NotImplementedException();
        }
    }
}
