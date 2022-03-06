using EShop.Voucher.API.Entities;
using System.Threading.Tasks;

namespace EShop.Voucher.API.Repositories
{
    public interface IVoucherRepository
    {
        EVoucher GetVoucher(string voucherCode);
        EVoucher CreateVoucher(EVoucher eVoucher);
        EVoucher UpdateVoucher(EVoucher eVoucher);
        bool DeleteVoucher(EVoucher eVoucher);
    }
}
