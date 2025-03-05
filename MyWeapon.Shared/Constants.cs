namespace MyWeapon.Shared
{
    public static class Constants
    {
        public const string WebClientSecret = "lila-employee-secret";
        public const string WebScopeName = "lilaWebApi";
        public const string WebClientId = "lila-employee";
        public const string WebPatientClientId = "karisma-client";
        public const string WebPatientSecret = "karisma-client-secret";

        //public const string EmailRegularExpression = @"^([\w\.\-]+)@([\w\-]+\.)+(\w){2,}$";
        public const string EmailRegularExpression = @"^([\w\.\-]+)@([\w\-]+\.?)+(\w){2,}$";
        public const string PhoneCountryCodeExpression = @"^(\+)[1-9]\d{0,3}$";
        public const string PhoneRegularExpression = @"^(0|00|\+)[1-9]\d{5,14}$";
        public const string UserNameRegularExpression = @"^([\w\.\-]+)@([\w\-]+\.)*[\w\-]{2,}$";
        public const string DomainNameRegularExpression = @"^([\w\-]+\.)*[\w\-]{2,}$";
        public const string HexColorRegularExpression = @"^#([a-fA-F0-9]{3}){1,2}$";
        public const string EnglishCharactersRegex = @"^[a-zA-Z]*$";
        public const string CharactersRegularExpression = @"^[a-zA-Z \u00E4\u00F6\u00FC\u00C4\u00D6\u00DC\u00df]*$";
        public const string ArabicOrEnglishOrGermanCharactersRegex = @"^[a-zA-Z \u00E4\u00F6\u00FC\u00C4\u00D6\u00DC\u00df\u0621-\u065f]*$";
        public const string NumbersRegularExpression = @"^[0-9]*$";
        public const string InternationalPhoneRegularExpression = @"^(\+)[1-9]\d{5,14}$";
        public const string UrlRegularExpression = @"^(https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]+\.[^\s]{2,}|www\.[a-zA-Z0-9]+\.[^\s]{2,})";
        public const string SplitPascalCaseRegularExpression = @"(?<=[A-Z])(?=[A-Z][a-z])|(?<=[^A-Z])(?=[A-Z])|(?<=[A-Za-z])(?=[^A-Za-z])";
        /// <summary>
        /// Regular Expression Pattern for Time in format HH:mm
        /// </summary>
        public const string HoursMinutesRegularExpression = @"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$";

        public const string AdministratorUserName = "Administrator";
        public const string DefaultPassword = "P@ssw0rd";
        public const string DefaultPhoneNumber = "0";

        public const string SuperAdminRoleName = "SuperAdmin";
        public const string SuperAdminUserName = "SuperAdmin";
        public const string SuperAdminEmail = "superadmin@yolo.clinic";

        /// <summary>
        /// Id of the first seeded tenant
        /// </summary>
        public const int DefaultTenantId = 1;
        public const string DefaultTenantAdmin = "admin@yolo.clinic";
        public const string DefaultTenantDomain = "yolo.clinic";

        public const string UploadsFolderName = "Uploads";
        public const string ImagesFolderName = "Images";
        public const string AttachmentsFolderName = "Attachments";
        public const string ThumbnailsFolderName = "Thumbnails";
        public const string TemplatesFolderName = "Templates";
        public const string FontsFolderName = "Fonts";

        public const string GermanyCountryName = "Germany";
        public const string UAECountryName = "United Arab Emirates";
        public const string SyriaCountryName = "Syria";
        public const string EgyptCountryName = "Egypt";
        public const string JordanCountryName = "Jordan";
        public const string YemenCountryName = "Yemen";
        public const string LibyaCountryName = "Libya";
        public const string PalestineCountryName = "Palestine";
        public const string IraqCountryName = "Iraq";
        public const string TurkeyCountryName = "Turkey";

        public const string DefaultCategoryName = "Default";

        public const int DefaultPageIndex = 1;
        public const int DefaultPageSize = 10;
        public const int DropdownPageSize = 100;

        public const decimal DefaultTaxRate = 0.19m;

        // channels
        public const int OtherChannelId = 1;
        public const string OtherChannel = "Other";
        public const string DefaultChannel = "Direct";
        public const string FacebookChannel = "Facebook";
        public const string YoutubeChannel = "Youtube";
        public const string InstagramChannel = "Instagram";
        public const string TwitterChannel = "Twitter";
        public const string GoogleChannel = "Google";
        public const string TiktokChannel = "Tiktok";
        public const string RecommendedChannel = "Recommended";

        //PaymentMethods
        public const int Cash = 1;
        public const int CreditCard = 2;
        public const int PaymentLink = 3;
        public const int Voucher = 4;
        public const int PatientWallet = 5;
        public const int MasterCard = 6;
        public const int PayPal = 7;
        public const int AmericanExpress = 8;
        public const int Stripe = 9;
        public const int Visa = 10;
        public const int VodafoneCash = 11;
        public const int Tabby = 12;
        public const int Toothpay = 13;
        public const int BankTransfer = 14;
        public const int BundleVoucher = 15;
        public const int TransferToBundle = 16;
        public const int TransferToAppointment = 17;
        public const int TransferToWallet = 18;
        public const int Marketing = 19;
        public const int TransferToVoucher = 20;

        /// <summary>
        /// The limit count to be considered as getting all items
        /// </summary>
        public const int AllItemsPageSize = 1000;

        /// <summary>
        /// Configuration Key for Server Address in application config file
        /// </summary>
        public const string AppServerRootAddressKey = "App:ServerRootAddress";

        /// <summary>
        /// 
        /// </summary>
        public const string AwsS3PublicBaseUrlKey = "AWS:S3PublicBaseUrlKey";

        /// <summary>
        /// Configuration Key for Web Address in application config file
        /// </summary>
        public const string AppWebRootAddress = "App:WebRootAddress";

        /// <summary>
        /// Web address for the third party (ID reader).
        /// </summary>
        public const string EmirateIdReaderAddress = "EmirateIdReader:ThirdPartyAddress";

        /// <summary>
        /// Configuration Key for online reservation Address in application config file
        /// </summary>
        public const string AppOnlineReservationAddress = "App:OnlineReservationAddress";

        /// <summary>
        /// Configuration Key for Server Address in application config file
        /// </summary>
        public const string AppYoloPatientRootAddressKey = "App:YoloPatientRootAddress";

        public const string AppYoloPatientEnabledKey = "App:YoloPatientEnabled";

        /// <summary>
        /// Default start of week (Monday)
        /// </summary>
        public const DayOfWeek StartOfWeek = DayOfWeek.Monday;

        /// <summary>
        /// The custom claim type for the role permissions
        /// </summary>
        public const string PermissionsClaimType = "Permissions";

        /// <summary>
        /// The custom claim type to insure active user
        /// </summary>
        public const string ActiveUserClaimType = "UserIsActive";

        /// <summary>
        /// The custom claim type for tenant id
        /// </summary>
        public const string TenantIdClaimType = "TenantId";

        /// <summary>
        /// Maximum size of profile image, default = 1 mb
        /// </summary>
        public const int MaxProfileImageSize = 1;

        /// <summary>
        /// Maximum size of attachment file, default = 5 mb
        /// </summary>
        public const int MaxAttachmentSize = 20;

        /// <summary>
        /// Default duration of service is 15 minutes
        /// </summary>
        public const int DefaultServiceDuration = 15;

        /// <summary>
        /// Maximum number of appointments per day for all doctors if there is full occupancy.
        /// </summary>
        public const int FullOccupancyPerDay = 120;

        /// <summary>
        /// Number of days to send the reminder Sms before the appointment date.
        /// </summary>
        public const int AppointmentReminderDaysCount = 3;

        public const double MinimumAcceptableQuantity = 0.01d;

        /// <summary>
        /// Configuration Keys SendGrid
        /// </summary>
        public const string AzureSendGridApiKey = "Azure:SendGridApiKey";

        public const string AppAuditedArrayKey = "App:NewAuditedArray";

        public const string AppIgnoreTenantIdKey = "App:IgnoreTenantId";

        public const string AwsUseS3StorageKey = "AWS:UseS3Storage";
        public const string AwsS3BucketNameKey = "AWS:S3BucketName";
        public const string AwsS3PublicBucketNameKey = "AWS:S3PublicBucketName";

        public const string DevEnvironment = "Dev";
        public const string TestEnvironment = "Test";
        public const string StageEnvironment = "Stage";
        public const string CertEnvironment = "Cert";

        public const string ExcelFileMimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        public const string DocxFileMimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        public const string DocFileMimeType = "application/msword";
        public const string PdfFileMimeType = "application/pdf";
        public const string HtmlFileMimeType = "text/html";
        public const string JpgFileMimeType = "image/jpeg";
        public const string PngFileMimeType = "image/png";
        public const string BmpFileMimeType = "image/bmp";
        public const string Mp4FileMimeType = "video/mp4";
        public const string MkvFileMimeType = "video/x-matroska";
        public const string AviFileMimeType = "video/x-msvideo";
        public const string MovFileMimeType = "video/quicktime";
        public const string FlvFileMimeType = "video/x-flv";
        public const string WmvFileMimeType = "video/x-ms-wmv";
        public const string AvchdFileMimeType = "video/avchd-stream";
        public const string WebmFileMimeType = "video/webm";
        public const string H264FileMimeType = "video/h264";

        /// <summary>
        /// Nps token (secret) is valid for 7 days after issued
        /// </summary>
        public const int NpsValidForDays = 7;

        /// <summary>
        /// Url at front would be https://host:port/Nps?secretKey=...
        /// </summary>
        public const string NpsWebPath = "Nps";

        /// <summary>
        /// Nps email template html file
        /// </summary>
        public const string NpsEmailTemplate = "NpsEmailTemplate.html";

        public const string SystemEmail = "Azure:SystemEmail";
        public const string ServiceCenterEmail = "Azure:ServiceCenterEmail";

        public const int KarismaTenantId = 1;

        public const string OnlineAppointmentColor = "#FB36A0";

        public const int WhatsappAttemptsCount = 10;

        public const int NumOfDaysForRecurrentService = 15;


        public const string WhatsAppTemplateVerification = "verification_code";
        public const string WhatsAppTemplateMsgAppReminder = "reminder_final";
        public const string WhatsAppTemplateMsgAppCancel = "cancel_final";
        public const string WhatsAppTemplateMsgAppRecurrent = "appointment_recurrent_tmplt";

        public const string WhatsAppTemplateMsgAppReminderNoLink = "appointment_reminder_nolink";
        public const string WhatsAppTemplateMsgAppCancelNoLink = "appointment_cancel_nolink";
        public const string WhatsAppTemplateMsgAppRecurrentNoLink = "appointment_recurrent_nolink";
        public const string WhatsAppTemplateMsgAppConfirmation = "appointment_confirmation_final_template_x";
        public const string WhatsAppTemplateMsgAppReschedule = "reschedule_final";
        public const string WhatsAppTemplateMsgAppRescheduleNoLink = "appointment_reschedule_nolink";
        public const string WhatsAppTemplateMsgFollowUp = "follow_up_tmplt";
        public const string StripePaymentSuccessTemplate = "stripe_payment_success_tmplt";
        public const string StripePaymentFailedTemplate = "stripe_payment_failed_tmplt";
        public const string ApprovedRequestTemplateMsg = "approved_request_tmplt";
        public const string RejectedRequestTemplateMsg = "rejected_request_tmplt";

        public const string SyrianCountryPhoneCode = "+963";
        public const string UAECountryPhoneCode = "+971";

        public const string OphthalmologyClinicType = "Ophthalmology";

        public const string Sms = "sms";

        public const string WhatsApp = "whatsapp";

        public const string Email = "email";

        public const string SyriatelSmsProvider = "syriatel";

        public const string Shreesty = "shreesty";

        public const string WozTell = "woztell";

        public const string Dataslices = "Dataslices";

        public const string TwilioSmsProvider = "twilio";

        public const string DefaultProvider = "default"; // to use main provider where keys in Key Vault

        public const string SendGridEmailProvider = "sendgrid";

        public const double MaxValue = 9999999999999999.99d;

        public const string LegalDocPatientSignature = "Patient signature";

        public const string LegalDocDoctorSignature = "Doctor signature";

        public const string LegalDocWitnessSignature = "Witness signature";

        public const string VerificationCode = "VerificationCode";

        public const string Waba = "waba";

        public const int CodeExpiryDuration = 1; //Hours

        public const int ReSendCodeDuration = 1; //Hours

        public const int MaxNumCodeAttempts = 3;

        public const string PatientAppVerificationCode = "PatientAppVerificationCode";


        public const string CreateEvent = "create";

        public const string UpdateEvent = "update";

        public const string DeleteEvent = "delete";

        public const string CancelEvent = "cancel";

        public const string FinishEvent = "finish";

        public const string RegisterEvent = "register";

        public const string UpdatePatientEvent = "updatepatient";

        /// <summary>
        /// accept two words with just one space
        /// </summary>
        public const string PatientNameRegex = "^[a-zA-Z]([a-zA-Z]+)(\\s[a-zA-Z]+)?$";

        /// <summary>
        /// accept many words with spaces
        /// </summary>
        public const string ExtendedPatientNameRegex = "^[A-Za-z]{1}[A-Za-z ]+$";

        public const string UAE = "United Arab Emirates";

        public const string UaeIdMask = "000-0000-0000000-0";

        public const string OutIdMask = "-----------";

        public const string MedicalRecord = "MedicalRecord";

        public const string PatientApp = "patientapp";
        public const string ApiKey = "x-api-key";
        public const string ApiKeyValue = "App:ApiKey";


        public const string PatientsCardsData = "PatientsCardsData";

        public const string HeaderStyle = "HeaderStyle";

        public const string GroupStyle = "GroupStyle";

        public const string OnlineCollectionName = "OnlineBooking";
        public const string SubCollectionName = "Requests";

        public const int DefaultOnlineMaxSlotPerHour = 1;

        public const int DefaultPatientMaxOnlineRequestsPerDay = 1;
        public const int DefaultPatientMaxOnlineAppointmentPerDay = 1;
        public const int DefaultClinicMaxOnlineRequestsPerDay = 1000;
        public const int DefaultClinicMaxOnlineAppointmentPerDay = 1000;

        public const string ApiSocial = "apisocial";

        public const string NabihQueueEnabled = "App:NabihQueueEnabled";

        public const string Wazzup = "wazzup";

        public const string WebhookKey = "Stripe:WebhookKey";

    }
}
