using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.DiscountDelivery.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.DiscountDelivery.Controllers
{
    [Area(AreaNames.Admin)]
    [AutoValidateAntiforgeryToken]
    public class WidgetsDiscountDeliveryController : BasePluginController
    {
        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;

        public WidgetsDiscountDeliveryController(ILocalizationService localizationService,
            INotificationService notificationService,
            IPermissionService permissionService,
            ISettingService settingService,
            IStoreContext storeContext)
        {
            _localizationService = localizationService;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _settingService = settingService;
            _storeContext = storeContext;
        }

        public IActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var discountDeliverySettings = _settingService.LoadSetting<DiscountDeliverySettings>(storeScope);

            var model = new ConfigurationModel
            {
                Value = discountDeliverySettings.Value
            };

            if (storeScope > 0)
            {               
            }

            return View("~/Plugins/Widgets.DiscountDelivery/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public IActionResult Configure(ConfigurationModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //load settings for a chosen store scope
            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var discountDeliverySettings = _settingService.LoadSetting<DiscountDeliverySettings>(storeScope);

            discountDeliverySettings.Value = model.Value;            

            /* We do not clear cache after each setting update.
             * This behavior can increase performance because cached settings will not be cleared 
             * and loaded from database after each update */
            _settingService.SaveSettingOverridablePerStore(discountDeliverySettings, x => x.Value, false, storeScope, false);

            //now clear settings cache
            _settingService.ClearCache();
          
            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));
            return Configure();
        }
    }
}
