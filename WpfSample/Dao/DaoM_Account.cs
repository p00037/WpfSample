using ExtensionsLibrary;
using System.Collections.Generic;
using System.Linq;
using WpfSample.Framework;
using WpfSample.Models.DBEntity;
using WpfSample.Models.SearchEntity;
using static WpfSample.Common.ComEnum;
using static WpfSample.Common.DB;

namespace WpfSample.Dao
{
    public class DaoM_Account : DaoBase
    {
        public List<M_AccountEntity> GetM_AccountList()
        {
            return GetM_AccountList(new MstAccountSearchEntity());
        }

        public List<M_AccountEntity> GetM_AccountList(MstAccountSearchEntity serchData)
        {
            return this.context.M_AccountEntitys
                .Where(e => e.LoginId.Contains(serchData.LoginId.NullToValue("")))
                .OrderBy(e => e.LoginId).ToList();
        }

        public M_AccountEntity GetM_Account(string loginId)
        {
            return this.context.M_AccountEntitys.Find(loginId);
        }

        public bool CheckLogin(M_AccountEntity checkData)
        {
            if (this.context.M_AccountEntitys.Where(e => e.LoginId == checkData.LoginId &&
                e.Password == checkData.Password).FirstOrDefault() == null)
            {
                return false;
            }

            return true;
        }

        public void Save(EnmEditMode editMode, M_AccountEntity data)
        {
            var contexForSave = CreateAppDbContextForSave();
            contexForSave.Entry(data).State = ConvertEnmEditModeToEntityState(editMode);
        }

        public void Save(List<M_AccountEntity> list)
        {
            var saveBeforeList = GetM_AccountList();

            foreach (var data in list)
            {
                var saveBeforeData = saveBeforeList.Where(e => e.LoginId == data.LoginId).FirstOrDefault();
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
                if (list.Any(e => e.LoginId == data.LoginId) == false)
                {
                    Save(EnmEditMode.Delete, data);
                }
            }
        }

        public List<string> GetErrorMessage(EnmEditMode editMode, M_AccountEntity data)
        {
            if (editMode == EnmEditMode.Delete) return new List<string>();

            var errorMessages = GetErrorMessageEntityValidation(data);
            if (editMode == EnmEditMode.Insert)
            {
                if (this.GetM_Account(data.LoginId) != null)
                {
                    errorMessages.Add("ログインIDは既に登録されています。");
                }
            }

            return errorMessages;
        }
    }
}
