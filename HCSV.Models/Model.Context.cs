﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HCSV.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TCSEntities : DbContext
    {
        public TCSEntities()
            : base("name=TCSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<jos__function_rights> jos__function_rights { get; set; }
        public virtual DbSet<jos_awb_number> jos_awb_number { get; set; }
        public virtual DbSet<jos_awb_user_type> jos_awb_user_type { get; set; }
        public virtual DbSet<jos_awb_users> jos_awb_users { get; set; }
        public virtual DbSet<jos_banner> jos_banner { get; set; }
        public virtual DbSet<jos_bannerclient> jos_bannerclient { get; set; }
        public virtual DbSet<jos_categories> jos_categories { get; set; }
        public virtual DbSet<jos_components> jos_components { get; set; }
        public virtual DbSet<jos_contact> jos_contact { get; set; }
        public virtual DbSet<jos_contact_details> jos_contact_details { get; set; }
        public virtual DbSet<jos_content> jos_content { get; set; }
        public virtual DbSet<jos_content_frontpage> jos_content_frontpage { get; set; }
        public virtual DbSet<jos_content_rating> jos_content_rating { get; set; }
        public virtual DbSet<jos_core_acl_aro> jos_core_acl_aro { get; set; }
        public virtual DbSet<jos_core_acl_aro_groups> jos_core_acl_aro_groups { get; set; }
        public virtual DbSet<jos_core_acl_aro_map> jos_core_acl_aro_map { get; set; }
        public virtual DbSet<jos_core_acl_aro_sections> jos_core_acl_aro_sections { get; set; }
        public virtual DbSet<jos_dbcache> jos_dbcache { get; set; }
        public virtual DbSet<jos_feedback> jos_feedback { get; set; }
        public virtual DbSet<jos_functions> jos_functions { get; set; }
        public virtual DbSet<jos_groups> jos_groups { get; set; }
        public virtual DbSet<jos_jf_content> jos_jf_content { get; set; }
        public virtual DbSet<jos_jf_languages_ext> jos_jf_languages_ext { get; set; }
        public virtual DbSet<jos_jf_tableinfo> jos_jf_tableinfo { get; set; }
        public virtual DbSet<jos_language_translation> jos_language_translation { get; set; }
        public virtual DbSet<jos_languages> jos_languages { get; set; }
        public virtual DbSet<jos_links> jos_links { get; set; }
        public virtual DbSet<jos_menu> jos_menu { get; set; }
        public virtual DbSet<jos_menu_link_types> jos_menu_link_types { get; set; }
        public virtual DbSet<jos_menu_rights> jos_menu_rights { get; set; }
        public virtual DbSet<jos_menu_types> jos_menu_types { get; set; }
        public virtual DbSet<jos_messages> jos_messages { get; set; }
        public virtual DbSet<jos_migration_backlinks> jos_migration_backlinks { get; set; }
        public virtual DbSet<jos_modules> jos_modules { get; set; }
        public virtual DbSet<jos_modules_menu> jos_modules_menu { get; set; }
        public virtual DbSet<jos_newsfeeds> jos_newsfeeds { get; set; }
        public virtual DbSet<jos_p_address> jos_p_address { get; set; }
        public virtual DbSet<jos_p_buy> jos_p_buy { get; set; }
        public virtual DbSet<jos_p_category> jos_p_category { get; set; }
        public virtual DbSet<jos_p_discount> jos_p_discount { get; set; }
        public virtual DbSet<jos_p_image> jos_p_image { get; set; }
        public virtual DbSet<jos_p_order> jos_p_order { get; set; }
        public virtual DbSet<jos_p_product> jos_p_product { get; set; }
        public virtual DbSet<jos_p_related> jos_p_related { get; set; }
        public virtual DbSet<jos_plugins> jos_plugins { get; set; }
        public virtual DbSet<jos_poll_data> jos_poll_data { get; set; }
        public virtual DbSet<jos_poll_date> jos_poll_date { get; set; }
        public virtual DbSet<jos_poll_menu> jos_poll_menu { get; set; }
        public virtual DbSet<jos_polls> jos_polls { get; set; }
        public virtual DbSet<jos_qcontacts_config> jos_qcontacts_config { get; set; }
        public virtual DbSet<jos_qcontacts_details> jos_qcontacts_details { get; set; }
        public virtual DbSet<jos_redirection> jos_redirection { get; set; }
        public virtual DbSet<jos_rights> jos_rights { get; set; }
        public virtual DbSet<jos_rights_users> jos_rights_users { get; set; }
        public virtual DbSet<jos_sections> jos_sections { get; set; }
        public virtual DbSet<jos_sef_sm_cache> jos_sef_sm_cache { get; set; }
        public virtual DbSet<jos_sef_sm_menus> jos_sef_sm_menus { get; set; }
        public virtual DbSet<jos_sef_sm_pingback> jos_sef_sm_pingback { get; set; }
        public virtual DbSet<jos_sef_sm_pingback_log> jos_sef_sm_pingback_log { get; set; }
        public virtual DbSet<jos_sef_sm_pingback_stack> jos_sef_sm_pingback_stack { get; set; }
        public virtual DbSet<jos_sef_sm_plugins> jos_sef_sm_plugins { get; set; }
        public virtual DbSet<jos_sef_sm_settings> jos_sef_sm_settings { get; set; }
        public virtual DbSet<jos_session> jos_session { get; set; }
        public virtual DbSet<jos_sh404sef_aliases> jos_sh404sef_aliases { get; set; }
        public virtual DbSet<jos_sh404sef_meta> jos_sh404sef_meta { get; set; }
        public virtual DbSet<jos_templates_menu> jos_templates_menu { get; set; }
        public virtual DbSet<jos_type_of_page> jos_type_of_page { get; set; }
        public virtual DbSet<jos_u_user> jos_u_user { get; set; }
        public virtual DbSet<jos_users> jos_users { get; set; }
        public virtual DbSet<jos_vvisitcounter> jos_vvisitcounter { get; set; }
        public virtual DbSet<jos_weblinks> jos_weblinks { get; set; }
        public virtual DbSet<jos_xmap> jos_xmap { get; set; }
        public virtual DbSet<jos_xmap_ext> jos_xmap_ext { get; set; }
        public virtual DbSet<jos_xmap_items> jos_xmap_items { get; set; }
        public virtual DbSet<jos_xmap_sitemap> jos_xmap_sitemap { get; set; }
        public virtual DbSet<jos_bannertrack> jos_bannertrack { get; set; }
        public virtual DbSet<jos_core_acl_groups_aro_map> jos_core_acl_groups_aro_map { get; set; }
        public virtual DbSet<jos_core_log_items> jos_core_log_items { get; set; }
        public virtual DbSet<jos_core_log_searches> jos_core_log_searches { get; set; }
        public virtual DbSet<jos_messages_cfg> jos_messages_cfg { get; set; }
        public virtual DbSet<jos_sef_sm_cache_items> jos_sef_sm_cache_items { get; set; }
        public virtual DbSet<jos_sef_sm_menu> jos_sef_sm_menu { get; set; }
        public virtual DbSet<jos_sef_sm_pingback_menu> jos_sef_sm_pingback_menu { get; set; }
        public virtual DbSet<jos_stats_agents> jos_stats_agents { get; set; }
        public virtual DbSet<Tmp_jos_users> Tmp_jos_users { get; set; }
    }
}
