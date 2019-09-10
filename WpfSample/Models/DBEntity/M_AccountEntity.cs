using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfSample.Models.DBEntity
{
    public class M_AccountEntity
    {
        [Key]
        [Required(ErrorMessage = "ログインIDは必須です。")]
        [StringLength(20, ErrorMessage = "ログインIDの最大文字数は20です。")]
        public string LoginId { get; set; }

        [Required(ErrorMessage = "パスワードは必須です。")]
        [StringLength(20, ErrorMessage = "パスワードの最大文字数は20です。")]
        public string Password { get; set; }
    }
}
