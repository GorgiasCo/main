using Gorgias.Business.DataTransferObjects.Mobile.V2;
using Gorgias.BusinessLayer.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gorgias.Business.Facades
{
    public class MobileFacade
    {

        public ICollection<SettingMobileModel> getSettings(int ProfileID, string languageCode, int CategoryParentID)
        {
            ICollection<SettingMobileModel> result = new List<SettingMobileModel>();

            result.Add(new SettingMobileModel { SettingName = "Languages", SettingCollection = Facade.LanguageFacade().getLanguagesByKeyValue() });
            result.Add(new SettingMobileModel { SettingName = "Categories", SettingCollection = Facade.CategoryFacade().getCategories(CategoryParentID,languageCode)});
            result.Add(new SettingMobileModel { SettingName = "ContentRatings", SettingCollection = Facade.ContentRatingFacade().getContentRatingsByKeyValue(languageCode)});
            result.Add(new SettingMobileModel { SettingName = "Availabilities", SettingCollection = Facade.AlbumTypeFacade().getAvailabilities() });

            return result;
        }

    }
}