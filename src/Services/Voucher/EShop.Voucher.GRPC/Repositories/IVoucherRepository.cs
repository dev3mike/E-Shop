using EShop.Voucher.GRPC.Entities;

namespace EShop.Voucher.GRPC.Repositories
{
    public interface IVoucherRepository
    {
        EVoucher GetVoucher(string voucherCode);
        EVoucher CreateVoucher(EVoucher eVoucher);
        EVoucher UpdateVoucher(EVoucher eVoucher);
        bool DeleteVoucher(EVoucher eVoucher);
    }
}
