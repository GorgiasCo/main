using System.Collections.Generic;
using System.Linq;

namespace Gorgias.Business.DataTransferObjects.Mobile.V2
{
    public class SettingMobileModel
    {
        public IQueryable<KeyValueMobileModel> SettingCollection { get; set; }
        public string SettingName { get; set; }
    }
}