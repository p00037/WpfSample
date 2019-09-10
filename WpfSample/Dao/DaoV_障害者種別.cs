using System.Collections.Generic;
using System.Linq;
using WpfSample.Framework;
using WpfSample.Models.DBEntity;

namespace WpfSample.Dao
{
    public class DaoV_障害者種別 : DaoBase
    {
        public List<V_障害者種別Entity> GetV_障害者種別EntityList()
        {
            return this.context.V_障害者種別Entitys.ToList();
        }
    }
}
