﻿using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Safqah.Payment.Data;
using Safqah.Payment.Entites;
using Safqah.Payment.Models;
using Safqah.Shared.BaseClases;
using Safqah.Shared.BaseRepository;
using Safqah.Shared.Enums;
using Safqah.Shared.MessagesContracts;
using System;
using System.Threading.Tasks;

namespace Safqah.Payment.Controllers
{
    [Authorize]
    public class PaymentController : BaseController
    {
        private readonly IRepository<PaymentTransaction, Guid, PaymentDbContext> _paymentTransactionRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public PaymentController(
            IRepository<PaymentTransaction, Guid, PaymentDbContext> paymentTransactionRepository,
            IPublishEndpoint publishEndpoint)
        {
            _paymentTransactionRepository = paymentTransactionRepository;
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost("ApplePay")]
        public async Task<IActionResult> ProcessApplePay([FromBody] PaymentTransactionModel paymentTransactionModel)
        {
            var paymentTransaction = new PaymentTransaction
            {
                PaymentSource = PaymentSource.ApplePay,
                PaymentStatus = paymentTransactionModel.PaymentStatus,
                PaymentCardType = paymentTransactionModel.PaymentCardType,
                Amount = paymentTransactionModel.Amount,
                CreatedAt = DateTime.Now,
                CreatorId = _userId,
            };


            await _paymentTransactionRepository.AddAsync(paymentTransaction);


            if (paymentTransaction.PaymentStatus == PaymentStatus.Success)
            {
                // TODO: Add to the queue to Pop up the wallet If success
                await _publishEndpoint.Publish(new PaymentSucceed { Amount = paymentTransaction.Amount, UserId = _userId });
            }

            return Ok(paymentTransaction);
        }

        [HttpPost("Pay")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentTransactionModel paymentTransactionModel)
        {
            var paymentTransaction = new PaymentTransaction
            {
                PaymentSource = PaymentSource.Card,
                PaymentStatus = paymentTransactionModel.PaymentStatus,
                PaymentCardType = paymentTransactionModel.PaymentCardType,
                Amount = paymentTransactionModel.Amount
            };


            await _paymentTransactionRepository.AddAsync(paymentTransaction);


            if (paymentTransaction.PaymentStatus == PaymentStatus.Success)
            {
                // TODO: Add to the queue to Pop up the wallet If success
                await _publishEndpoint.Publish(new PaymentSucceed { Amount = paymentTransaction.Amount, UserId = _userId });
            }

            return Ok(paymentTransaction);
        }

        [AllowAnonymous]
        [HttpPost("Webhook")]
        public async Task<IActionResult> Webhook([FromBody] PaymentTransactionModel paymentTransactionModel)
        {
            var paymentTransaction = await _paymentTransactionRepository.GetByAsync(x => x.Id == paymentTransactionModel.Id);
            paymentTransaction.PaymentStatus = paymentTransactionModel.PaymentStatus;

            await _paymentTransactionRepository.UpdateAsync(paymentTransaction);

            if (paymentTransaction.PaymentStatus == PaymentStatus.Success)
            {
                // TODO: Add to the queue to Pop up the wallet If success
                await _publishEndpoint.Publish(new PaymentSucceed { Amount = paymentTransaction.Amount, UserId = _userId });
            }

            return Ok();
        }
    }
}
