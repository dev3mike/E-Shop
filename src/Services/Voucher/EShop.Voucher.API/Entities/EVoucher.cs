using System;

namespace EShop.Voucher.API.Entities
{
    public class EVoucher
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public int Amount { get; set; }
        public bool IsUsed { get; set; }
        public DateTime UsedDate { get; set; }
    }
}
