namespace Trinnk.Pinterest.Model.Result.PinDelete
{
    public class PinDelete
    {
        public Resource_Response resource_response { get; set; }
        public Client_Context client_context { get; set; }
        public Resource resource { get; set; }
        public string request_identifier { get; set; }
    }

    public class Resource_Response
    {
        public int code { get; set; }
        public object data { get; set; }
        public string message { get; set; }
        public string endpoint_name { get; set; }
        public string status { get; set; }
        public int http_status { get; set; }
    }

    public class Client_Context
    {
        public Active_Experiments active_experiments { get; set; }
        public string app_version { get; set; }
        public string browser_locale { get; set; }
        public string browser_name { get; set; }
        public int browser_type { get; set; }
        public string browser_version { get; set; }
        public string country { get; set; }
        public string country_from_hostname { get; set; }
        public string country_from_ip { get; set; }
        public string csp_nonce { get; set; }
        public string current_url { get; set; }
        public string deep_link { get; set; }
        public string[] enabled_advertiser_countries { get; set; }
        public Event_Log_Info event_log_info { get; set; }
        public object facebook_token { get; set; }
        public string http_referrer { get; set; }
        public string invite_code { get; set; }
        public string invite_sender_id { get; set; }
        public bool is_amp { get; set; }
        public bool is_authenticated { get; set; }
        public string is_bot { get; set; }
        public bool is_internal_ip { get; set; }
        public bool is_full_page { get; set; }
        public bool is_managed_advertiser { get; set; }
        public bool is_mobile_agent { get; set; }
        public bool is_shop_the_pin_campaign_whitelisted { get; set; }
        public bool is_sterling_on_steroids { get; set; }
        public bool is_tablet_agent { get; set; }
        public string language { get; set; }
        public string locale { get; set; }
        public bool nag_browser_unsupported { get; set; }
        public string origin { get; set; }
        public string path { get; set; }
        public object referrer { get; set; }
        public string region_from_ip { get; set; }
        public string request_host { get; set; }
        public string social_bot { get; set; }
        public int site_type { get; set; }
        public object sterling_on_steroids_ldap { get; set; }
        public string unauth_id { get; set; }
        public bool user_agent_can_use_native_app { get; set; }
        public bool no_fetch_context_on_resource { get; set; }
        public string user_agent_platform { get; set; }
        public object user_agent_platform_version { get; set; }
        public string user_agent { get; set; }
        public User user { get; set; }
        public object utm_campaign { get; set; }
        public object utm_medium { get; set; }
        public object utm_source { get; set; }
        public object utm_term { get; set; }
        public object utm_pai { get; set; }
        public string visible_url { get; set; }
        public Analysis_Ua analysis_ua { get; set; }
        public string request_identifier { get; set; }
        public object root_request_identifier { get; set; }
        public object parent_request_identifier { get; set; }
        public string full_path { get; set; }
        public string real_ip { get; set; }
        public object placed_experiences { get; set; }
        public bool batch_exp { get; set; }
    }

    public class Active_Experiments
    {
    }

    public class Event_Log_Info
    {
        public int event_type { get; set; }
        public string object_id_str { get; set; }
    }

    public class User
    {
        public string image_large_url { get; set; }
        public bool connected_to_microsoft { get; set; }
        public string last_name { get; set; }
        public int login_state { get; set; }
        public bool is_write_banned { get; set; }
        public object phone_country { get; set; }
        public bool is_high_risk { get; set; }
        public object phone_number { get; set; }
        public bool connected_to_youtube { get; set; }
        public bool connected_to_facebook { get; set; }
        public object domain_url { get; set; }
        public string full_name { get; set; }
        public string email { get; set; }
        public object custom_gender { get; set; }
        public bool connected_to_google { get; set; }
        public string gender { get; set; }
        public object allow_personalization_cookies { get; set; }
        public object listed_website_url { get; set; }
        public object[] verified_domains { get; set; }
        public object allow_analytic_cookies { get; set; }
        public bool is_matured_new_user { get; set; }
        public string ip_country { get; set; }
        public bool connected_to_dropbox { get; set; }
        public string image_medium_url { get; set; }
        public bool third_party_marketing_tracking_enabled { get; set; }
        public string unverified_phone_number_without_country { get; set; }
        public string id { get; set; }
        public Verified_Identity verified_identity { get; set; }
        public bool has_mfa_enabled { get; set; }
        public object[] verified_user_websites { get; set; }
        public bool profile_discovered_public { get; set; }
        public bool has_password { get; set; }
        public bool domain_verified { get; set; }
        public object twitter_url { get; set; }
        public bool connected_to_etsy { get; set; }
        public string created_at { get; set; }
        public string image_xlarge_url { get; set; }
        public object website_url { get; set; }
        public string push_package_user_id { get; set; }
        public bool personalize_from_offsite_browsing { get; set; }
        public bool is_partner { get; set; }
        public string type { get; set; }
        public string ip_region { get; set; }
        public string image_small_url { get; set; }
        public bool is_employee { get; set; }
        public object allow_marketing_cookies { get; set; }
        public bool twitter_publish_enabled { get; set; }
        public object unverified_phone_number { get; set; }
        public string facebook_id { get; set; }
        public string gplus_url { get; set; }
        public bool can_enable_mfa { get; set; }
        public string country { get; set; }
        public string username { get; set; }
        public bool connected_to_instagram { get; set; }
        public bool facebook_publish_stream_enabled { get; set; }
        public object[] nags { get; set; }
        public object unverified_phone_country { get; set; }
        public string first_name { get; set; }
        public Resurrection_Info resurrection_info { get; set; }
        public bool facebook_timeline_enabled { get; set; }
        public bool is_any_website_verified { get; set; }
        public string phone_number_end { get; set; }
    }

    public class Verified_Identity
    {
    }

    public class Resurrection_Info
    {
        public string id { get; set; }
        public string resurrection_dt { get; set; }
        public string type { get; set; }
    }

    public class Analysis_Ua
    {
        public int app_type { get; set; }
        public string browser_name { get; set; }
        public string browser_version { get; set; }
        public object device_type { get; set; }
        public string device { get; set; }
        public string os_name { get; set; }
        public string os_version { get; set; }
    }

    public class Resource
    {
        public string name { get; set; }
        public Options options { get; set; }
    }

    public class Options
    {
        public string[] bookmarks { get; set; }
        public string id { get; set; }
        public bool no_fetch_context_on_resource { get; set; }
    }

}
