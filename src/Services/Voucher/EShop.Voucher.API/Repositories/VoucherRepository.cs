using Dapper;
using Dapper.Contrib.Extensions;
using EShop.Voucher.API.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Threading.Tasks;

namespace EShop.Voucher.API.Repositories
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly IConfiguration _configuration;

        public VoucherRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public EVoucher CreateVoucher(EVoucher eVoucher)
        {
            using var connection = GetPostgresConnection();
            var result = connection.Insert(eVoucher);
            eVoucher.Id = result;
            return eVoucher;
        }

        public bool DeleteVoucher(EVoucher eVoucher)
        {
            using var connection = GetPostgresConnection();
            var isSuccessful = connection.Delete(eVoucher);
            return isSuccessful;
        }

        public EVoucher GetVoucher(string voucherCode)
        {
            using var connection = GetPostgresConnection();
            var voucher = connection.QueryFirstOrDefault<EVoucher>(
                sql: "SELECT * FROM Voucher WHERE Code=@VoucherCode", 
                param: new { VoucherCode = voucherCode});
            return voucher;
        }

        public EVoucher UpdateVoucher(EVoucher eVoucher)
        {
            using var connection = GetPostgresConnection();
            var isSuccessful = connection.Update(eVoucher);
            return isSuccessful ? eVoucher : null;
        }

        private NpgsqlConnection GetPostgresConnection()
        {
            var connection = new NpgsqlConnection(
                _configuration.GetValue<string>("DatabaseSettings:ConnectionString")
                );
            return connection;
        }
    }
}
