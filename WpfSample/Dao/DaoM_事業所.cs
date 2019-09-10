
using ExtensionsLibrary;
using System.Collections.Generic;
using System.Linq;
using WpfSample.Common;
using WpfSample.Framework;
using WpfSample.Models.DBEntity;
using WpfSample.Models.SearchEntity;
using static WpfSample.Common.ComEnum;
using static WpfSample.Common.DB;

namespace WpfSample.Dao
{
    public class DaoM_事業所 : DaoBase 
    {
        public List<M_事業所Entity> GetM_事業所List()
        {
            return GetM_事業所List(new MstOfficeSearchEntity());
        }

        public List<M_事業所Entity> GetM_事業所List(MstOfficeSearchEntity serchData)
        {
            return this.context.M_事業所Entitys
                .Where(e => e.事業所番号.Contains(serchData.事業所番号.NullToValue("")))
                .Where(e => e.事業所名.Contains(serchData.事業所名.NullToValue("")))
                .Where(e => e.事業所名カナ.Contains(serchData.事業所名カナ.NullToValue("")))
                .WhereIf(serchData.定員規模開始 != null || serchData.定員規模開始.ToString() != "", e => e.定員規模 >= serchData.定員規模開始)
                .WhereIf(serchData.定員規模終了 != null || serchData.定員規模終了.ToString() != "", e => e.定員規模 <= serchData.定員規模終了)
                .ToList();
        }

        public M_事業所Entity GetM_事業所(string 事業所番号)
        {
            return this.context.M_事業所Entitys.Find(事業所番号);
        }

        public void Save(EnmEditMode editMode, M_事業所Entity data)
        {
            var contexForSave = CreateAppDbContextForSave();
            contexForSave.Entry(data).State = ConvertEnmEditModeToEntityState(editMode);
        }

        public List<string> GetErrorMessage(EnmEditMode editMode, M_事業所Entity data)
        {
            if (editMode == EnmEditMode.Delete) return new List<string>();

            var errorMessages = GetErrorMessageEntityValidation(data);
            if (editMode == EnmEditMode.Insert)
            {
                if (this.GetM_事業所(data.事業所番号) != null)
                {
                    errorMessages.Add("事業所番号は既に登録されています。");
                }
            }

            return errorMessages;
        }
    }
}
