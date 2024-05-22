using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace WebQuanLyCongTy.Validators
{
    public class PhoneNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var phoneNumber = value as string;
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return new ValidationResult("Bạn chưa nhập số điện thoại");
            }

            if(phoneNumber.Length != 10)
            {
                return new ValidationResult("Số điện thoại phải có 10 số");
            }

            string[] validPrefixes = { "090", "098", "091", "031", "035", "038", "033" };
            bool isValidPrefix = false;
            foreach(var prefix in validPrefixes)
            {
                if (phoneNumber.StartsWith(prefix))
                {
                    isValidPrefix = true;
                    break;
                }
            }

            if (isValidPrefix == false)
            {
                return new ValidationResult("Số điện thoại phải bắt đầu bằng một trong các chuỗi số: 090, 098, 091, 031, 035 hoặc 038.");
            }

            if (!Regex.IsMatch(phoneNumber, @"^\d{10}$"))
            {
                return new ValidationResult("Số điện thoại phải là chuỗi số.");
            }
            return ValidationResult.Success;
        }
    }
}