using System.ComponentModel.DataAnnotations;

namespace WpfSample.Framework
{
    public class V_区分EntityBase
    {
        [Key]
        public string コード { get; set; }

        public string 名称 { get; set; }
    }
}
