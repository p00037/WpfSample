using System.ComponentModel.DataAnnotations;

namespace WpfSample.Models.DBEntity
{
    public class M_サービス種類Entity
    {
        [Required(ErrorMessage = "障害者種別は必須です。")]
        public string 障害者種別 { get; set; }

        [Required(ErrorMessage = "サービス種類CDは必須です。")]
        [StringLength(4, ErrorMessage = "サービス種類CDの最大文字数は4です。")]
        public string サービス種類CD { get; set; }

        [StringLength(50, ErrorMessage = "サービス種類名の最大文字数は50です。")]
        public string サービス種類名 { get; set; }

        [StringLength(2, ErrorMessage = "種類区分の最大文字数は2です。")]
        public string 種類区分 { get; set; }

        public int? 様式3用 { get; set; }

        public int? 最長期間 { get; set; }

        public int? 入通 { get; set; }

        public int? 順序 { get; set; }

        [StringLength(4, ErrorMessage = "様式種別番号の最大文字数は4です。")]
        public string 様式種別番号 { get; set; }

        [StringLength(1, ErrorMessage = "利用者負担定率定額区分の最大文字数は1です。")]
        public string 利用者負担定率定額区分 { get; set; }

        [StringLength(3, ErrorMessage = "給付率の最大文字数は3です。")]
        public string 給付率 { get; set; }
    }
}
