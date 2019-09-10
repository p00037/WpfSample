
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WpfSample.Framework;

namespace WpfSample.Models.DBEntity
{
    public class M_事業所明細Entity : DBEntityBase
    {
        [Required(ErrorMessage = "事業所番号は必須です。")]
        [RegularExpression(@"[a-zA-Z0-9]+", ErrorMessage = "事業所番号は半角英数字のみ入力できます")]
        [StringLength(14, ErrorMessage = "事業所番号の最大文字数は14です。")]
        public string 事業所番号 { get; set; }

        [Required(ErrorMessage = "連番は必須です。")]
        public int 連番 { get; set; }

        [StringLength(100, ErrorMessage = "施設名の最大文字数は100です。")]
        public string 施設名 { get; set; }

        [Required(ErrorMessage = "サービス種類コードは必須です。")]
        [RegularExpression(@"[a-zA-Z0-9]+", ErrorMessage = "サービス種類コードは半角英数字のみ入力できます")]
        public string サービス種類CD { get; set; }

        public bool? 種類コード { get; set; }

        [RegularExpression(@"[a-zA-Z0-9]+", ErrorMessage = "サービス提供単位番号は半角英数字のみ入力できます")]
        [StringLength(20, ErrorMessage = "サービス提供単位番号の最大文字数は20です。")]
        public string サービス提供単位番号 { get; set; }

        [Range(0, 9999, ErrorMessage = "定員の範囲は0～9999です")]
        public decimal? 定員 { get; set; }

        [RegularExpression(@"[a-zA-Z0-9]+", ErrorMessage = "多機能型用件は半角英数字のみ入力できます")]
        [StringLength(1, ErrorMessage = "多機能型用件の最大文字数は1です。")]
        public string 多機能型用件 { get; set; }

        [Range(0, 99.99, ErrorMessage = "単位数単価の範囲は0～99.99です")]
        public decimal? 単位数単価 { get; set; }

        [RegularExpression(@"[a-zA-Z0-9]+", ErrorMessage = "障害者種別は半角英数字のみ入力できます")]
        public string 障害者種別 { get; set; }

        [StringLength(4, ErrorMessage = "種類区分の最大文字数は4です。")]
        public string 種類区分 { get; set; }
    }
}
