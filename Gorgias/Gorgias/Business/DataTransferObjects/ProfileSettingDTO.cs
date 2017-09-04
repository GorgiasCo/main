using System;
using FluentValidation.Attributes;
using Gorgias.Business.Validators;
namespace Gorgias.Business.DataTransferObjects
{

    [Validator(typeof(ProfileSettingValidator))]
    public class ProfileSettingDTO
    {
        public int ProfileID { get; set; }
        public String ProfileLanguageApp { get; set; }
        public int? ProfileCityID { get; set; }
        public DateTime? ProfileBirthday { get; set; }
    }
}

