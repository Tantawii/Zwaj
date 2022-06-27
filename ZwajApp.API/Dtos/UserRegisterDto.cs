using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZwajApp.API.Dtos
{
    public class UserRegisterDto
    {
        [Required]
        public string username { get; set; }
        [StringLength(8,MinimumLength = 4, ErrorMessage = "يجب ألا تقل كلمةالسر عن 4 مقاطع ولاتزيد عن 8")]
        public string password { get; set; }
    }
}