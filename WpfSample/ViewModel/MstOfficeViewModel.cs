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
    public class MstOfficeViewModel : MasterViewModelBase<M_事業所Entity>
    {
        public MstOfficeViewModel()
        {
            BtnAddGridDetailRowCommand.Subscribe(_ => BtnAddGridDetailRowAction());
            BtnDeleteGridDetailRowCommand.Subscribe(x => BtnDeleteGridDetailRowAction(x));
        }

        public ReactiveProperty<List<M_事業所Entity>> SearchResultEntitys { get; set; } = new ReactiveProperty<List<M_事業所Entity>>(new List<M_事業所Entity>());

        public ReactiveProperty<M_事業所Entity> EditData { get; set; } = new ReactiveProperty<M_事業所Entity>(new M_事業所Entity());

        public ReactiveProperty<MstOfficeSearchEntity> SearchOptionEntity { get; set; } = new ReactiveProperty<MstOfficeSearchEntity>(new MstOfficeSearchEntity());

        public ReactiveProperty<List<M_事業所明細Entity>> EditGridDetailData { get; set; } = new ReactiveProperty<List<M_事業所明細Entity>>(new List<M_事業所明細Entity>());

        public ReactiveProperty<List<M_サービス種類Entity>> ComboItemsサービス種類 { get; set; } = new ReactiveProperty<List<M_サービス種類Entity>>(new List<M_サービス種類Entity>());

        public ReactiveProperty<List<V_障害者種別Entity>> ComboItems障害者種別 { get; set; } = new ReactiveProperty<List<V_障害者種別Entity>>(new List<V_障害者種別Entity>());

        public ReactiveCommand BtnAddGridDetailRowCommand { get; } = new ReactiveCommand();

        public ReactiveCommand BtnDeleteGridDetailRowCommand { get; } = new ReactiveCommand();

        protected override void Load()
        {
            var daoM_サービス種類 = new DaoM_サービス種類();
            ComboItemsサービス種類.Value = daoM_サービス種類.GetM_サービス種類EntityList();

            var daoV_障害者種別 = new DaoV_障害者種別();
            ComboItems障害者種別.Value = daoV_障害者種別.GetV_障害者種別EntityList();
        }

        protected override void SetSearchResultEntitys()
        {
            var dao = new DaoM_事業所();
            this.SearchResultEntitys.Value = dao.GetM_事業所List(SearchOptionEntity.Value);
        }

        protected override void Save()
        {
            foreach (var item in EditGridDetailData.Value.Select((v, i) => new { v, i }))
            {
                item.v.事業所番号 = EditData.Value.事業所番号;
                item.v.連番 = item.i;
            }

            var errorMessage = new List<string>();

            var daoM_事業所 = new DaoM_事業所();
            errorMessage.AddRange(daoM_事業所.GetErrorMessage(EditMode, this.EditData.Value));

            var daoM_事業所明細 = new DaoM_事業所明細();
            errorMessage.AddRange(daoM_事業所明細.GetErrorMessage(EditGridDetailData.Value.ToList()));
            if (errorMessage.Count() > 0) throw new SaveErrorMessageExcenption(errorMessage.ConcatWith(Environment.NewLine));

            daoM_事業所.Save(EditMode, this.EditData.Value);

            daoM_事業所明細.Save(EditData.Value.事業所番号, EditGridDetailData.Value);
        }

        protected override void Delete()
        {
            var daoM_事業所 = new DaoM_事業所();
            daoM_事業所.Save(ComEnum.EnmEditMode.Delete, this.EditData.Value);

            var daoM_事業所明細 = new DaoM_事業所明細();
            daoM_事業所明細.Save(EditData.Value.事業所番号, new List<M_事業所明細Entity>());
        }

        protected override void SetEditDataToInsert()
        {
            EditData.Value = new M_事業所Entity();

            EditGridDetailData.Value = new List<M_事業所明細Entity>();
        }

        protected override void SetEditDataToUpdate(M_事業所Entity selectEntity)
        {
            var daoM_事業所 = new DaoM_事業所();
            EditData.Value = daoM_事業所.GetM_事業所(selectEntity.事業所番号);

            var daoM_事業所明細 = new DaoM_事業所明細();
            EditGridDetailData.Value = daoM_事業所明細.GetM_事業所明細List(selectEntity.事業所番号);
        }

        protected override string GetJsonEditData()
        {
            var jsonEditdata = "";
            jsonEditdata += JsonConvert.SerializeObject(this.EditData.Value);

            jsonEditdata += Environment.NewLine;
            jsonEditdata += JsonConvert.SerializeObject(this.EditGridDetailData.Value);

            return jsonEditdata;
        }

        private void BtnAddGridDetailRowAction()
        {
            TryCatch.ShowMassage(() =>
            {
                AddGridDetailRow();
            });
        }

        private void AddGridDetailRow()
        {
            EditGridDetailData.Value.Add(new M_事業所明細Entity());
            EditGridDetailData.Value = EditGridDetailData.Value.DeepCopy();
        }

        private void BtnDeleteGridDetailRowAction(object entity)
        {
            TryCatch.ShowMassage(() =>
            {
                DeleteGridDetailRow(entity);
            });
        }

        private void DeleteGridDetailRow(object entity)
        {
            EditGridDetailData.Value.Remove((M_事業所明細Entity)entity);
            EditGridDetailData.Value = EditGridDetailData.Value.DeepCopy();
        }
    }
}
