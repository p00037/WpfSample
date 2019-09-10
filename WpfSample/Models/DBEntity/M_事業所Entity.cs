
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WpfSample.Framework;

namespace WpfSample.Models.DBEntity
{
    public class M_事業所Entity : DBEntityBase
    {
        [Required(ErrorMessage = "事業所番号は必須です。")]
        [RegularExpression(@"[a-zA-Z0-9]+", ErrorMessage = "事業所番号は半角英数字のみ入力できます")]
        [StringLength(14, ErrorMessage = "事業所番号の最大文字数は14です。")]
        public string 事業所番号 { get; set; }

        [Required(ErrorMessage = "事業所名は必須です。")]
        [StringLength(100, ErrorMessage = "事業所名の最大文字数は100です。")]
        public string 事業所名 { get; set; }

        [Required(ErrorMessage = "事業所名カナは必須です。")]
        [RegularExpression(@"[｡-ﾟ+]+", ErrorMessage = "事業所名カナは半角カタカナのみ入力できます")]
        [StringLength(100, ErrorMessage = "事業所名カナの最大文字数は100です。")]
        public string 事業所名カナ { get; set; }

        [RegularExpression(@"[a-zA-Z0-9]+", ErrorMessage = "級地コードは半角英数字のみ入力できます")]
        [StringLength(1, ErrorMessage = "級地コードの最大文字数は1です。")]
        public string 級地コード { get; set; }

        [RegularExpression(@"[a-zA-Z0-9]+", ErrorMessage = "郵便番号は半角英数字のみ入力できます")]
        [StringLength(7, ErrorMessage = "郵便番号の最大文字数は7です。")]
        public string 郵便番号 { get; set; }

        [Required(ErrorMessage = "住所1は必須です。")]
        [StringLength(100, ErrorMessage = "住所1の最大文字数は100です。")]
        public string 住所1 { get; set; }

        [StringLength(100, ErrorMessage = "住所2の最大文字数は100です。")]
        public string 住所2 { get; set; }

        [StringLength(100, ErrorMessage = "代表者の最大文字数は100です。")]
        public string 代表者 { get; set; }

        [RegularExpression(@"[a-zA-Z0-9- /:-@\[-~]+", ErrorMessage = "電話番号は半角英数字記号のみ入力できます")]
        [StringLength(15, ErrorMessage = "電話番号の最大文字数は15です。")]
        public string 電話番号 { get; set; }

        [RegularExpression(@"[a-zA-Z0-9]+", ErrorMessage = "指定は半角英数字のみ入力できます")]
        [StringLength(1, ErrorMessage = "指定の最大文字数は1です。")]
        public string 指定 { get; set; }

        [Range(0, 99999, ErrorMessage = "定員規模の範囲は0～99999です")]
        public decimal? 定員規模 { get; set; }

        [RegularExpression(@"[a-zA-Z0-9]+", ErrorMessage = "社福軽減実施有無は半角英数字のみ入力できます")]
        [StringLength(1, ErrorMessage = "社福軽減実施有無の最大文字数は1です。")]
        public string 社福軽減実施有無 { get; set; }

        [RegularExpression(@"[a-zA-Z0-9]+", ErrorMessage = "就労継続A型減免有無は半角英数字のみ入力できます")]
        [StringLength(1, ErrorMessage = "就労継続A型減免有無の最大文字数は1です。")]
        public string 就労継続A型減免有無 { get; set; }
    }
}
