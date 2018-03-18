using NFine.Application.CustomerManage;
using NFine.Code;
using NFine.Code.Excel;
using NFine.Domain.Entity;
using NFine.Domain.Entity.CustomerManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.CustomerManage.Controllers
{
    public class CustomerTransactionController : ControllerBase
    {
        private CustomerTransactionApp customerTranApp = new CustomerTransactionApp();
        private CustomerApp customerApp = new CustomerApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword, DateTime? beginTime, DateTime? endTime, bool? tranStaus)
        {
            var data = new
            {
                rows = customerTranApp.GetList(pagination, keyword, beginTime, endTime, tranStaus),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = customerTranApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(CustomerTransactionEntity customerEntity, string keyValue)
        {
            var loginInfo = OperatorProvider.Provider.GetCurrent();

            CustomerEntity entity = customerApp.GetForm(customerEntity.CustomerId);
            customerEntity.CustomerName = entity.Name;
            customerEntity.Telphone = entity.Telphone;
            customerEntity.Address = entity.Address;
            customerEntity.Salesman = loginInfo.UserName;
            customerEntity.SalesmanCode = loginInfo.UserCode;
            customerEntity.Source = entity.SourceName;
            customerTranApp.SubmitForm(customerEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            customerTranApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 将泛类型集合List类转换成DataTable
        /// </summary>
        /// <param name="list">泛类型集合</param>
        /// <returns></returns>
        private static DataTable ListToDataTable<T>(List<T> entitys)
        {
            //检查实体集合不能为空
            if (entitys == null || entitys.Count < 1)
            {
                throw new Exception("需转换的集合为空");
            }
            //取出第一个实体的所有Propertie
            Type entityType = entitys[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();

            //生成DataTable的structure
            //生产代码中，应将生成的DataTable结构Cache起来，此处略
            DataTable dt = new DataTable();
            for (int i = 0; i < entityProperties.Length; i++)
            {
                var first = entityProperties[i].GetCustomAttributes(typeof(DataFieldAttribute), false)[0];
                dt.Columns.Add(((DataFieldAttribute)first).DescName);
            }
            //将所有entity添加到DataTable中
            foreach (object entity in entitys)
            {
                //检查所有的的实体都为同一类型
                if (entity.GetType() != entityType)
                {
                    throw new Exception("要转换的集合元素类型不一致");
                }
                object[] entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    entityValues[i] = entityProperties[i].GetValue(entity, null);
                }
                dt.Rows.Add(entityValues);
            }
            return dt;
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult Excel(DateTime? starttime, DateTime? endtime, bool? status)
        {
            NPOIExcel ex = new NPOIExcel();
            var data = customerTranApp.GetList(starttime, endtime, status);

            List<CustomerTransactionSimple> lst = data.Select(t => new CustomerTransactionSimple() { BillDate = t.BillDate, SourceName = t.Source, CustomerName = t.CustomerName, CustomerTelphone = t.Telphone, CustomerAddress = t.Address, TranStatus = t.TradStatus == true ? "成交" : "未成交", Remark = t.Remark }).ToList();

            DataTable dt = ListToDataTable<CustomerTransactionSimple>(lst);

            string filePath = Server.MapPath("~/Temp/客户跟单统计.xls");
            ex.ToExcel(dt, "客户跟单统计", "", filePath);
            return Success("操作成功");
        }
        public ActionResult Download()
        {
            string path = Server.MapPath("~/Temp/" + "客户跟单统计.xls");
            return File(path, "application/octet-stream", Url.Encode("客户跟单统计.xls"));
        }
    }
}
