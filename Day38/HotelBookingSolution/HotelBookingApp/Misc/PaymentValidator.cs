using HotelBookingApp.Models;
using HotelBookingApp.Models.DTOs;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingApp.Misc
{
    public class PaymentValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Invalid payment details. Payment cannot be null hi");
            }

            var payment = value as PaymentDTO;
            if (payment == null)
            {
                return new ValidationResult("Invalid payment details. Payment cannot be null hello");
            }

            if (!Enum.IsDefined(typeof(Payment.PaymentType), payment.ModeOfPayment))
            {
                return new ValidationResult("Please select a mode of payment");
            }

            if (payment.ModeOfPayment == Payment.PaymentType.CreditCard || payment.ModeOfPayment == Payment.PaymentType.DebitCard)
            {
                if (string.IsNullOrEmpty(payment.CardNumber))
                {
                    return new ValidationResult("Card number cannot be empty");
                }
                if (payment.CardNumber.Length != 16)
                {
                    return new ValidationResult("Card number must be 16 digits long");
                }
            }

            if (payment.Amount <= 0)
            {
                return new ValidationResult("Amount must be greater than 0");
            }

            if (payment.BookingId <= 0)
            {
                return new ValidationResult("Invalid booking id");
            }

            if (payment.PaymentDate > DateTime.Now)
            {
                return new ValidationResult("Payment date cannot be in the future");
            }

            return ValidationResult.Success;

            //var payment = validationContext.ObjectInstance as Payment;
            //if (payment == null)
            //{
            //    return new ValidationResult("Invalid payment details. Payment cannot be null");
            //}

            //if (payment == null)
            //{
            //    return new ValidationResult("Invalid payment details. Payment cannot be null");
            //}

            //if (!Enum.IsDefined(typeof(Payment.PaymentType), payment.ModeOfPayment))
            //{
            //    return new ValidationResult("Please select a mode of payment");
            //}

            //if (payment.ModeOfPayment == Payment.PaymentType.CreditCard || payment.ModeOfPayment == Payment.PaymentType.DebitCard)
            //{
            //    if (string.IsNullOrEmpty(payment.CardNumber))
            //    {
            //        return new ValidationResult("Card number cannot be empty");
            //    }
            //    if (payment.CardNumber.Length != 16)
            //    {
            //        return new ValidationResult("Card number must be 16 digits long");
            //    }
            //}

            //if (payment.Amount <= 0)
            //{
            //    return new ValidationResult("Amount must be greater than 0");
            //}

            //if (payment.BookingId <= 0)
            //{
            //    return new ValidationResult("Invalid booking id");
            //}

            //if (payment.PaymentDate > DateTime.Now)
            //{
            //    return new ValidationResult("Payment date cannot be in the future");
            //}

            //return ValidationResult.Success;
        }

    }
}
