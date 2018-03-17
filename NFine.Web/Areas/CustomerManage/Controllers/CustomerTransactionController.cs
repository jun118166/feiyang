using NFine.Application.CustomerManage;
using NFine.Code;
using NFine.Domain.Entity.CustomerManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.CustomerManage.Controllers
{
    public class CustomerTransactionController : ControllerBase
    {
        private CustomerTransactionApp customerTranApp = new CustomerTransactionApp();

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = customerTranApp.GetList(pagination, keyword),
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
    }
}
