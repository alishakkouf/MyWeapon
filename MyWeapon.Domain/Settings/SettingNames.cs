using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWeapon.Domain.Settings
{
    public static class SettingNames
    {
        public const string AppTimeZone = "App.TimeZone";
        public const string AppCountryPhoneCode = "App.CountryPhoneCode";
        public const string AppLanguage = "App.Language";
        public const string AppSmsLanguage = "App.SmsLanguage";
        /// <summary>
        /// Set whether the sms functionality enabled or not
        /// </summary>
        public const string AppSmsEnabled = "App.SmsEnabled";
        /// <summary>
        /// Time of day (HH:mm) to send sms to clients 
        /// </summary>
        public const string AppSmsSendTimeOfDay = "App.SmsSendTimeOfDay";


        public const string AppSendEmailNotificationEnabled = "App.SendEmailNotificationEnabled";

        public const string TenantName = "Tenant.Name";

        /// <summary>
        /// Used by user name to be 'user@domain-name'
        /// </summary>
        public const string TenantDomainName = "Tenant.DomainName";

        /// <summary>
        /// Serialized address as JsonAddress
        /// </summary>
        public const string TenantAddressInfo = "Tenant.AddressInfo";

        public const string TenantLogoBase64 = "Tenant.LogoBase64";
        public const string LogoUrl = "Tenant.LogoUrl";

        public const string TenantOwner = "Tenant.Owner";

        public const string Country = "Tenant.Country";
        public const string City = "Tenant.City";
        public const string IBAN = "Tenant.IBAN";
        public const string TaxNr = "Tenant.Tax_Nr";
        public const string TenantEmail = "Tenant.Email";
        public const string TenantPhone = "Tenant.Phone";
        public const string TenantCurrency = "Tenant.Currency";
        public const string TenantOpeningTime = "Tenant.OpeningTime";
        public const string TenantClosingTime = "Tenant.ClosingTime";

        public const string Is24Hour = "Tenant.Is24Hour";

        public const string WhatsAppEnabled = "WhatsApp.Enabled";
        public const string WhatsAppAccessToken = "WhatsApp.AccessToken";
        public const string WhatsAppChannelId = "WhatsApp.ChannelId";

        public const string TenantCoverImageUrl = "Tenant.CoverImageUrl";
        public const string TenantDescription = "Tenant.Description";
        public const string TenantIsOpen = "Tenant.IsOpen";


        // Engine settings
        public const string MsgEngChannels = "Tenant.ChannelsEnabled";
        public const string MsgEngWhatsAppAccessToken = "Tenant.WhatsApp.AccessToken";
        public const string MsgEngWhatsAppChannelId = "Tenant.WhatsApp.ChannelId";
        public const string MsgEngSmsConfiguration = "Tenant.Sms.Configuration";
        public const string MsgEngEmailConfiguration = "Tenant.Email.Configuration";
        public const string MsgEngWhatsAppConfiguration = "Tenant.WhatsApp.Configuration";
        // End Engine settings

        public const string TenantEnableExport = "Tenant.EnableExport";
    }

    public static class SettingDefaults
    {
        public static readonly Dictionary<string, string> Defaults =
            new Dictionary<string, string>
            {
                { SettingNames.AppTimeZone, "Europe/Berlin" },
                { SettingNames.AppCountryPhoneCode, "+49" },
                { SettingNames.AppLanguage, SupportedLanguages.English },
                { SettingNames.AppSmsLanguage, SupportedLanguages.English },
                { SettingNames.AppSmsEnabled, "true" },
                { SettingNames.AppSmsSendTimeOfDay, "08:00" },
                { SettingNames.AppSendEmailNotificationEnabled, "false" },

                { SettingNames.TenantName, "Ali" },
                { SettingNames.TenantDomainName, "Ali.Tenant" },
                { SettingNames.TenantAddressInfo, "" },
                { SettingNames.TenantLogoBase64, "" },
                { SettingNames.LogoUrl, "" },
                { SettingNames.Country, "" },
                { SettingNames.City, "" },
                { SettingNames.IBAN, "" },
                { SettingNames.TaxNr, "" },
                { SettingNames.TenantEmail, "" },
                { SettingNames.TenantPhone, "" },
                { SettingNames.TenantCurrency, "" },
                { SettingNames.TenantOpeningTime, "08:00" },
                { SettingNames.TenantClosingTime, "22:00" },

                { SettingNames.WhatsAppEnabled, "false" },
                { SettingNames.WhatsAppAccessToken, "" },
                { SettingNames.WhatsAppChannelId, "" },
                { SettingNames.TenantCoverImageUrl, "" },
                { SettingNames.TenantDescription, "" },
                { SettingNames.TenantIsOpen, "true" },
                { SettingNames.Is24Hour, "true" },


                { SettingNames.MsgEngChannels, "{'sms':0,'whatsapp':0,'email':0,'patientapp':1}" },
                { SettingNames.MsgEngWhatsAppAccessToken, "" },
                { SettingNames.MsgEngWhatsAppChannelId, "" },
                { SettingNames.MsgEngSmsConfiguration, "{'provider':'','configuration':null}" },
                { SettingNames.MsgEngEmailConfiguration, "{'provider':'','configuration':null}" },
                { SettingNames.MsgEngWhatsAppConfiguration, "{'provider':'','configuration':null}" },



               { SettingNames.TenantEnableExport, "true" }
            };
    }
}
