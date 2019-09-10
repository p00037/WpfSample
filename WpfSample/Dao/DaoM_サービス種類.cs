using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSample.Framework;
using WpfSample.Models.DBEntity;

namespace WpfSample.Dao
{
    public class DaoM_サービス種類 : DaoBase
    {
        public List<M_サービス種類Entity> GetM_サービス種類EntityList()
        {
            return this.context.M_サービス種類Entitys.ToList();
        }
    }
}
