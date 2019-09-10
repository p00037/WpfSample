
using System.Collections.Generic;
using System.Linq;
using WpfSample.Common;
using WpfSample.Framework;
using WpfSample.Models.DBEntity;
using static WpfSample.Common.ComEnum;
using static WpfSample.Common.DB;

namespace WpfSample.Dao
{
    public class DaoM_事業所明細 : DaoBase 
    {
        public List<M_事業所明細Entity> GetM_事業所明細List(string 事業所番号)
        {
            return this.context.M_事業所明細Entitys
                .Where(e => e.事業所番号 == 事業所番号)
                .ToList();
        }

        public void Save(EnmEditMode editMode, M_事業所明細Entity data)
        {
            var contexForSave = CreateAppDbContextForSave();
            contexForSave.Entry(data).State = ConvertEnmEditModeToEntityState(editMode);
        }

        public void Save(string 事業所番号, List<M_事業所明細Entity> list)
        {
            var saveBeforeList = GetM_事業所明細List(事業所番号);

            foreach (var data in list)
            {
                var saveBeforeData = saveBeforeList.Where(e => e.事業所番号== data.事業所番号 && e.連番== data.連番).FirstOrDefault();
                if (saveBeforeData == null)
                {
                    Save(EnmEditMode.Insert, data);
                }
                else
                {
                    Save(EnmEditMode.Update, data);
                }
            }

            foreach (var data in saveBeforeList)
            {
                if (list.Any(e => data.事業所番号== e.事業所番号 && data.連番== e.連番) == false)
                {
                    Save(EnmEditMode.Delete, data);
                }
            }
        }

        public List<string> GetErrorMessage(List<M_事業所明細Entity> list)
        {
            var errorMessages = new List<string>();

            foreach (var item in list.Select((v, i) => new { v, i }))
            {
                var tmpErrorMessages = GetErrorMessage(EnmEditMode.Insert, item.v);
                if (tmpErrorMessages.Count() > 0)
                {
                    errorMessages.Add((item.i+1).ToString() + "行目:");
                    errorMessages.AddRange(tmpErrorMessages);
                }
            }

            return errorMessages;
        }

        public List<string> GetErrorMessage(EnmEditMode editMode, M_事業所明細Entity data)
        {
            if (editMode == EnmEditMode.Delete) return new List<string>();

            var errorMessages = GetErrorMessageEntityValidation(data);

            return errorMessages;
        }
    }
}
