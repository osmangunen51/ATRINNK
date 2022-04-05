using System;
using System.Web.Configuration;

namespace NeoSistem.MakinaTurkiye.Core
{
    public class CoreSettings
    {
        private static CoreSettings coreSettingsSingleton = null;

        private CoreSettings()
        {
            Load();
        }

        public static CoreSettings Instance
        {
            get
            {
                return coreSettingsSingleton ?? new CoreSettings();
            }
        }

        private void Load()
        {
            try
            {
                var collection = WebConfigurationManager.AppSettings;
                this.ImageFolder = collection["ImageFolder"];
                this.ScriptFolder = collection["ScriptFolder"];
                this.ContentFolder = collection["ContentFolder"];
                this.UserImageFolder = collection["UserImageFolder"];
                this.CssFolder = collection["CssFolder"];
                this.TurkiyeCountryId = Convert.ToInt16(collection["TurkiyeCountryId"]);
                this.MinPasswordLength = Convert.ToInt32(collection["MinPasswordLength"]);
            }
            catch
            {
            }
        }

        public string ImageFolder { get; private set; }

        public string UserImageFolder { get; private set; }

        public string CssFolder { get; private set; }

        public string ScriptFolder { get; private set; }

        public string ContentFolder { get; private set; }

        public short TurkiyeCountryId { get; set; }

        public int MinPasswordLength { get; set; }
    }
}
