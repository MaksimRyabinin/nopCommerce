using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.DiscountDelivery.Models;
using Nop.Services.Configuration;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.DiscountDelivery.Components
{
    [ViewComponent(Name = "WidgetsDiscountDelivery")]
    public class WidgetsDiscountDeliveryViewComponent : NopViewComponent
    {
        private readonly IStoreContext _storeContext;
        private readonly ISettingService _settingService;

        public WidgetsDiscountDeliveryViewComponent(IStoreContext storeContext,
            ISettingService settingService)
        {
            _storeContext = storeContext;
            _settingService = settingService;
        }

        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
            var discountDeliverySettings = _settingService.LoadSetting<DiscountDeliverySettings>(_storeContext.CurrentStore.Id);

            var model = new DiscountDeliveryModel()
            {
                Value = discountDeliverySettings.Value
            };

            return View("~/Plugins/Widgets.DiscountDelivery/Views/DiscountDeliveryInfo.cshtml", model);
        }
    }
}
