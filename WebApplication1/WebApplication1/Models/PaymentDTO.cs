using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebApplication1.Models
{
    public class PaymentDTO
    {
        [Required]
        public int Id { get; set; }

        [CCNumberIsValid]
        public string CCNumber { get; set; }

        [Required]
        public string OwnerName { get; set; }


        [ExpiredDateIsValid]
        public DateTime ExpireDate { get; set; }

        [CVCNumberIsValid]
        public string CVC { get; set; }
    }

    public class ExpiredDateIsValid : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var paymentDTO = (PaymentDTO)validationContext.ObjectInstance;
            if (paymentDTO.ExpireDate <= DateTime.Today) 
            {
                return new ValidationResult("The Credit Card Has Expired");
            }
            else {
                return ValidationResult.Success;
            } 
        }
    }

    public class CVCNumberIsValid : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var paymentDTO = (PaymentDTO)validationContext.ObjectInstance;
            Regex regexMC = new Regex(@"^5[1-5][0-9]{14}$");
            Regex regexVisa = new Regex(@"^4[0-9]{12}(?:[0-9]{3})?$");
            Regex regAmEx = new Regex(@"^3[47][0-9]{13}$");
            Regex regCVCMV = new Regex("^[0-9]{3}$");
            Regex regCVCAmEx = new Regex("^[0-9]{4}$");

            if (regexMC.Match(paymentDTO.CCNumber).Success == true)
            {
                if (regCVCMV.Match(paymentDTO.CVC).Success == false)
                    return new ValidationResult("CVC is not valid for Master Card Type");
            }

            else if (regexVisa.Match(paymentDTO.CCNumber).Success == true)
            {
                if (regCVCMV.Match(paymentDTO.CVC).Success == false)
                    return new ValidationResult("CVC is not for Visa Type");
            }

            else if (regAmEx.Match(paymentDTO.CCNumber).Success == true)
            {
                if (regCVCAmEx.Match(paymentDTO.CVC).Success == false)
                    return new ValidationResult("CVC is not for American Express type");
            }

            return ValidationResult.Success;
        }
    }

    public class CCNumberIsValid : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var paymentDTO = (PaymentDTO)validationContext.ObjectInstance;
            Regex regexMC = new Regex(@"^5[1-5][0-9]{14}$");
            Regex regexVisa = new Regex(@"^4[0-9]{12}(?:[0-9]{3})?$");
            Regex regAmEx = new Regex(@"^3[47][0-9]{13}$");
            Regex regCVCMV = new Regex("^[0-9]{3}$");
            Regex regCVCAmEx = new Regex("^[0-9]{4}$");

            if (regexMC.Match(paymentDTO.CCNumber).Success == false && regexVisa.Match(paymentDTO.CCNumber).Success == false && regAmEx.Match(paymentDTO.CCNumber).Success == false)
            {
               return new ValidationResult("CC Number is not valid");
            }
            return ValidationResult.Success;
        }
    }


}
