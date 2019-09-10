using ExtensionsLibrary;
using Newtonsoft.Json;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using WpfSample.Common;
using WpfSample.Dao;
using WpfSample.Framework;
using WpfSample.Models.DBEntity;
using WpfSample.Models.SearchEntity;

namespace WpfSample.ViewModel
{
    public class MstAccountViewModel : MasterViewModelBase<M_AccountEntity>
    {
        public MstAccountViewModel()
        {
        }

        public ReactiveProperty<List<M_AccountEntity>> SearchResultEntitys { get; set; } = new ReactiveProperty<List<M_AccountEntity>>(new List<M_AccountEntity>());

        public ReactiveProperty<M_AccountEntity> EditData { get; set; } = new ReactiveProperty<M_AccountEntity>(new M_AccountEntity());

        public ReactiveProperty<MstAccountSearchEntity> SearchOptionEntity { get; set; } = new ReactiveProperty<MstAccountSearchEntity>(new MstAccountSearchEntity());

        protected override void Load()
        {
        }

        protected override void SetSearchResultEntitys()
        {
            var dao = new DaoM_Account();
            this.SearchResultEntitys.Value = dao.GetM_AccountList(SearchOptionEntity.Value);
        }

        protected override void Save()
        {
            var errorMessage = new List<string>();
            var dao = new DaoM_Account();
            errorMessage.AddRange(dao.GetErrorMessage(EditMode, this.EditData.Value));

            if (errorMessage.Count() > 0) throw new SaveErrorMessageExcenption(errorMessage.ConcatWith(Environment.NewLine));

            dao.Save(EditMode, this.EditData.Value);
        }

        protected override void Delete()
        {
            var dao = new DaoM_Account();
            dao.Save(ComEnum.EnmEditMode.Delete, this.EditData.Value);
        }

        protected override void SetEditDataToInsert()
        {
            EditData.Value = new M_AccountEntity();
        }

        protected override void SetEditDataToUpdate(M_AccountEntity selectEntity)
        {
            var dao = new DaoM_Account();
            EditData.Value = dao.GetM_Account(selectEntity.LoginId);
        }

        protected override string GetJsonEditData()
        {
            return JsonConvert.SerializeObject(this.EditData.Value);
        }
    }
}
