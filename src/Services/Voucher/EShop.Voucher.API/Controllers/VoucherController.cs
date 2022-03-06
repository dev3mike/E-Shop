using EShop.Voucher.API.Entities;
using EShop.Voucher.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace EShop.Voucher.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherRepository _voucherRepository;

        public VoucherController(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        [HttpPut("{voucherCode}", Name = "GetVoucher")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<EVoucher> GetVoucher(string voucherCode)
        {
            var voucher = _voucherRepository.GetVoucher(voucherCode);

            if (voucher == null)
                return NotFound();

            return Ok(voucher);
        }

        [HttpPost(Name = "CreateVoucher")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public ActionResult<EVoucher> CreateVoucher(EVoucher eVoucher)
        {
            ValidateEVoucher(eVoucher);

            var voucher = _voucherRepository.CreateVoucher(eVoucher);
            if (voucher == null)
                throw new Exception("Could not save the voucher");

            return Ok(voucher);
        }

        [HttpPut(Name = "UpdateVoucher")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public ActionResult<EVoucher> UpdateVoucher(EVoucher eVoucher)
        {
            ValidateEVoucher(eVoucher);

            var voucher = _voucherRepository.UpdateVoucher(eVoucher);
            return Ok(voucher);
        }

        [HttpPost(Name = "DeleteVoucher")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes(MediaTypeNames.Application.Json)]
        public ActionResult DeleteVoucher(string voucherCode)
        {
            var voucher = _voucherRepository.GetVoucher(voucherCode);
            if (voucher == null) return NotFound();

            var isDeleted = _voucherRepository.DeleteVoucher(voucher);
            return isDeleted ? NoContent() : StatusCode(500);
        }

        private void ValidateEVoucher(EVoucher eVoucher)
        {
            if (
                eVoucher?.Id == null ||
                eVoucher?.Code == null ||
                eVoucher?.Amount == null
                )
                throw new BadHttpRequestException("eVoucher is not valid", 400);
        }
    }
}
