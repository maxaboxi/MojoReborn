using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MojoPortal.Legacy.Data;

public partial class MojoLegacyContext : DbContext
{
    public MojoLegacyContext()
    {
    }

    public MojoLegacyContext(DbContextOptions<MojoLegacyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<I7SflexiField> I7SflexiFields { get; set; }

    public virtual DbSet<I7SflexiItem> I7SflexiItems { get; set; }

    public virtual DbSet<I7SflexiSearchdef> I7SflexiSearchdefs { get; set; }

    public virtual DbSet<I7SflexiValue> I7SflexiValues { get; set; }

    public virtual DbSet<MpAuthorizeNetLog> MpAuthorizeNetLogs { get; set; }

    public virtual DbSet<MpBannedIpaddress> MpBannedIpaddresses { get; set; }

    public virtual DbSet<MpBlog> MpBlogs { get; set; }

    public virtual DbSet<MpBlogCategory> MpBlogCategories { get; set; }

    public virtual DbSet<MpBlogComment> MpBlogComments { get; set; }

    public virtual DbSet<MpBlogItemCategory> MpBlogItemCategories { get; set; }

    public virtual DbSet<MpBlogStat> MpBlogStats { get; set; }

    public virtual DbSet<MpCalendarEvent> MpCalendarEvents { get; set; }

    public virtual DbSet<MpCategory> MpCategories { get; set; }

    public virtual DbSet<MpCategoryItem> MpCategoryItems { get; set; }

    public virtual DbSet<MpComment> MpComments { get; set; }

    public virtual DbSet<MpCommerceReport> MpCommerceReports { get; set; }

    public virtual DbSet<MpCommerceReportOrder> MpCommerceReportOrders { get; set; }

    public virtual DbSet<MpContactFormMessage> MpContactFormMessages { get; set; }

    public virtual DbSet<MpContentHistory> MpContentHistories { get; set; }

    public virtual DbSet<MpContentMetaLink> MpContentMetaLinks { get; set; }

    public virtual DbSet<MpContentMetum> MpContentMeta { get; set; }

    public virtual DbSet<MpContentRating> MpContentRatings { get; set; }

    public virtual DbSet<MpContentStyle> MpContentStyles { get; set; }

    public virtual DbSet<MpContentTemplate> MpContentTemplates { get; set; }

    public virtual DbSet<MpContentWorkflow> MpContentWorkflows { get; set; }

    public virtual DbSet<MpContentWorkflowAuditHistory> MpContentWorkflowAuditHistories { get; set; }

    public virtual DbSet<MpCurrency> MpCurrencies { get; set; }

    public virtual DbSet<MpEmailSendLog> MpEmailSendLogs { get; set; }

    public virtual DbSet<MpEmailSendQueue> MpEmailSendQueues { get; set; }

    public virtual DbSet<MpEmailTemplate> MpEmailTemplates { get; set; }

    public virtual DbSet<MpFileAttachment> MpFileAttachments { get; set; }

    public virtual DbSet<MpForum> MpForums { get; set; }

    public virtual DbSet<MpForumPost> MpForumPosts { get; set; }

    public virtual DbSet<MpForumSubscription> MpForumSubscriptions { get; set; }

    public virtual DbSet<MpForumThread> MpForumThreads { get; set; }

    public virtual DbSet<MpForumThreadSubscription> MpForumThreadSubscriptions { get; set; }

    public virtual DbSet<MpFriendlyUrl> MpFriendlyUrls { get; set; }

    public virtual DbSet<MpGalleryImage> MpGalleryImages { get; set; }

    public virtual DbSet<MpGeoCountry> MpGeoCountries { get; set; }

    public virtual DbSet<MpGeoZone> MpGeoZones { get; set; }

    public virtual DbSet<MpGoogleCheckoutLog> MpGoogleCheckoutLogs { get; set; }

    public virtual DbSet<MpHtmlContent> MpHtmlContents { get; set; }

    public virtual DbSet<MpIndexingQueue> MpIndexingQueues { get; set; }

    public virtual DbSet<MpLanguage> MpLanguages { get; set; }

    public virtual DbSet<MpLetter> MpLetters { get; set; }

    public virtual DbSet<MpLetterHtmlTemplate> MpLetterHtmlTemplates { get; set; }

    public virtual DbSet<MpLetterInfo> MpLetterInfos { get; set; }

    public virtual DbSet<MpLetterSendLog> MpLetterSendLogs { get; set; }

    public virtual DbSet<MpLetterSubscribe> MpLetterSubscribes { get; set; }

    public virtual DbSet<MpLetterSubscribeHx> MpLetterSubscribeHxes { get; set; }

    public virtual DbSet<MpLink> MpLinks { get; set; }

    public virtual DbSet<MpMediaFile> MpMediaFiles { get; set; }

    public virtual DbSet<MpMediaPlayer> MpMediaPlayers { get; set; }

    public virtual DbSet<MpMediaTrack> MpMediaTracks { get; set; }

    public virtual DbSet<MpModule> MpModules { get; set; }

    public virtual DbSet<MpModuleDefinition> MpModuleDefinitions { get; set; }

    public virtual DbSet<MpModuleDefinitionSetting> MpModuleDefinitionSettings { get; set; }

    public virtual DbSet<MpModuleSetting> MpModuleSettings { get; set; }

    public virtual DbSet<MpPage> MpPages { get; set; }

    public virtual DbSet<MpPageModule> MpPageModules { get; set; }

    public virtual DbSet<MpPayPalLog> MpPayPalLogs { get; set; }

    public virtual DbSet<MpPaymentLog> MpPaymentLogs { get; set; }

    public virtual DbSet<MpPlugNpayLog> MpPlugNpayLogs { get; set; }

    public virtual DbSet<MpPoll> MpPolls { get; set; }

    public virtual DbSet<MpPollModule> MpPollModules { get; set; }

    public virtual DbSet<MpPollOption> MpPollOptions { get; set; }

    public virtual DbSet<MpPollUser> MpPollUsers { get; set; }

    public virtual DbSet<MpRedirectList> MpRedirectLists { get; set; }

    public virtual DbSet<MpRole> MpRoles { get; set; }

    public virtual DbSet<MpRssFeed> MpRssFeeds { get; set; }

    public virtual DbSet<MpRssFeedEntry> MpRssFeedEntries { get; set; }

    public virtual DbSet<MpSavedQuery> MpSavedQueries { get; set; }

    public virtual DbSet<MpSchemaScriptHistory> MpSchemaScriptHistories { get; set; }

    public virtual DbSet<MpSchemaVersion> MpSchemaVersions { get; set; }

    public virtual DbSet<MpSharedFile> MpSharedFiles { get; set; }

    public virtual DbSet<MpSharedFileFolder> MpSharedFileFolders { get; set; }

    public virtual DbSet<MpSharedFilesHistory> MpSharedFilesHistories { get; set; }

    public virtual DbSet<MpSite> MpSites { get; set; }

    public virtual DbSet<MpSiteFolder> MpSiteFolders { get; set; }

    public virtual DbSet<MpSiteHost> MpSiteHosts { get; set; }

    public virtual DbSet<MpSiteModuleDefinition> MpSiteModuleDefinitions { get; set; }

    public virtual DbSet<MpSiteSettingsEx> MpSiteSettingsexes { get; set; }

    public virtual DbSet<MpSiteSettingsExDef> MpSiteSettingsExDefs { get; set; }

    public virtual DbSet<MpSurvey> MpSurveys { get; set; }

    public virtual DbSet<MpSurveyModule> MpSurveyModules { get; set; }

    public virtual DbSet<MpSurveyPage> MpSurveyPages { get; set; }

    public virtual DbSet<MpSurveyQuestion> MpSurveyQuestions { get; set; }

    public virtual DbSet<MpSurveyQuestionAnswer> MpSurveyQuestionAnswers { get; set; }

    public virtual DbSet<MpSurveyQuestionOption> MpSurveyQuestionOptions { get; set; }

    public virtual DbSet<MpSurveyResponse> MpSurveyResponses { get; set; }

    public virtual DbSet<MpSystemLog> MpSystemLogs { get; set; }

    public virtual DbSet<MpTag> MpTags { get; set; }

    public virtual DbSet<MpTagItem> MpTagItems { get; set; }

    public virtual DbSet<MpTagVocabulary> MpTagVocabularies { get; set; }

    public virtual DbSet<MpTaskQueue> MpTaskQueues { get; set; }

    public virtual DbSet<MpTaxClass> MpTaxClasses { get; set; }

    public virtual DbSet<MpTaxRate> MpTaxRates { get; set; }

    public virtual DbSet<MpTaxRateHistory> MpTaxRateHistories { get; set; }

    public virtual DbSet<MpUser> MpUsers { get; set; }

    public virtual DbSet<MpUserClaim> MpUserClaims { get; set; }

    public virtual DbSet<MpUserLocation> MpUserLocations { get; set; }

    public virtual DbSet<MpUserLogin> MpUserLogins { get; set; }

    public virtual DbSet<MpUserPage> MpUserPages { get; set; }

    public virtual DbSet<MpUserProperty> MpUserProperties { get; set; }

    public virtual DbSet<MpUserRole> MpUserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=127.0.0.1,1433;Database=MojoLegacy;UID=sa;PWD=YourStrong!Passw0rd;TrustServerCertificate=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_CI_AS");

        modelBuilder.Entity<I7SflexiField>(entity =>
        {
            entity.HasKey(e => e.FieldGuid);

            entity.ToTable("i7_sflexi_fields");

            entity.HasIndex(e => new { e.DefinitionGuid, e.Name }, "IX_i7_sflexi_fields_DefGuid_Name");

            entity.HasIndex(e => new { e.DefinitionGuid, e.IsDeleted, e.SortOrder, e.Name }, "IX_i7_sflexi_fields_DefGuid_Name_Deleted_Sort");

            entity.HasIndex(e => new { e.SortOrder, e.Name, e.IsDeleted, e.DefinitionGuid }, "IX_i7_sflexi_fields_SortOrder_Name_IsDeleted_DefinitionGuid");

            entity.Property(e => e.FieldGuid).ValueGeneratedNever();
            entity.Property(e => e.ControlType).HasMaxLength(25);
            entity.Property(e => e.DataType).HasMaxLength(100);
            entity.Property(e => e.DatePickerYearRange).HasMaxLength(10);
            entity.Property(e => e.DefinitionName).HasMaxLength(50);
            entity.Property(e => e.EditPageControlCssClass).HasMaxLength(50);
            entity.Property(e => e.EditPageControlWrapperCssClass).HasMaxLength(50);
            entity.Property(e => e.EditPageLabelCssClass).HasMaxLength(50);
            entity.Property(e => e.EditRoles).HasDefaultValue("");
            entity.Property(e => e.HelpKey).HasMaxLength(255);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false, "DF_i7_sflexi_fields_IsDeleted");
            entity.Property(e => e.Label).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PostTokenStringWhenFalse).HasDefaultValue("");
            entity.Property(e => e.PostTokenStringWhenTrue).HasDefaultValue("");
            entity.Property(e => e.PreTokenStringWhenFalse).HasDefaultValue("");
            entity.Property(e => e.PreTokenStringWhenTrue).HasDefaultValue("");
            entity.Property(e => e.TextBoxMode).HasMaxLength(25);
            entity.Property(e => e.Token).HasMaxLength(50);
            entity.Property(e => e.ViewRoles).HasDefaultValue("All Users;");
        });

        modelBuilder.Entity<I7SflexiItem>(entity =>
        {
            entity.HasKey(e => e.ItemGuid);

            entity.ToTable("i7_sflexi_items");

            entity.HasIndex(e => new { e.ItemId, e.ItemGuid }, "IX_i7_sflexi_items_ItemID_ItemGuid");

            entity.Property(e => e.ItemGuid).ValueGeneratedNever();
            entity.Property(e => e.CreatedUtc).HasColumnType("datetime");
            entity.Property(e => e.EditRoles).HasDefaultValue("");
            entity.Property(e => e.ItemId)
                .ValueGeneratedOnAdd()
                .HasColumnName("ItemID");
            entity.Property(e => e.LastModUtc).HasColumnType("datetime");
            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
            entity.Property(e => e.ViewRoles).HasDefaultValue("All Users;");
        });

        modelBuilder.Entity<I7SflexiSearchdef>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("i7_sflexi_searchdefs");

            entity.Property(e => e.Guid).ValueGeneratedNever();
        });

        modelBuilder.Entity<I7SflexiValue>(entity =>
        {
            entity.HasKey(e => e.ValueGuid);

            entity.ToTable("i7_sflexi_values");

            entity.HasIndex(e => new { e.ModuleGuid, e.FieldGuid }, "IX_i7_sflexi_values_ModuleGuid_FieldGuid");

            entity.Property(e => e.ValueGuid).ValueGeneratedNever();
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<MpAuthorizeNetLog>(entity =>
        {
            entity.HasKey(e => e.RowGuid);

            entity.ToTable("mp_AuthorizeNetLog");

            entity.Property(e => e.RowGuid).HasDefaultValueSql("(newid())", "DF_mp_AuthorizeNetLog_RowGuid");
            entity.Property(e => e.Amount).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.AuthCode).HasMaxLength(50);
            entity.Property(e => e.AvsCode).HasMaxLength(50);
            entity.Property(e => e.CavCode)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.CcvCode)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.CreatedUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_AuthorizeNetLog_CreatedUtc")
                .HasColumnType("datetime");
            entity.Property(e => e.Duty).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.Freight).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.Method).HasMaxLength(20);
            entity.Property(e => e.ResponseCode)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.ResponseReasonCode).HasMaxLength(20);
            entity.Property(e => e.Tax).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.TransactionId).HasMaxLength(50);
            entity.Property(e => e.TransactionType).HasMaxLength(50);
        });

        modelBuilder.Entity<MpBannedIpaddress>(entity =>
        {
            entity.HasKey(e => e.RowId);

            entity.ToTable("mp_BannedIPAddresses");

            entity.Property(e => e.RowId).HasColumnName("RowID");
            entity.Property(e => e.BannedIp)
                .HasMaxLength(50)
                .HasColumnName("BannedIP");
            entity.Property(e => e.BannedReason).HasMaxLength(255);
            entity.Property(e => e.BannedUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_BannedIPAddresses_BannedUTC")
                .HasColumnType("datetime")
                .HasColumnName("BannedUTC");
        });

        modelBuilder.Entity<MpBlog>(entity =>
        {
            entity.HasKey(e => e.ItemId);

            entity.ToTable("mp_Blogs");

            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.Approved).HasDefaultValue(true);
            entity.Property(e => e.ApprovedBy).HasDefaultValue(new Guid("00000000-0000-0000-0000-000000000000"));
            entity.Property(e => e.ApprovedDate)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedByUser).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.Heading).HasMaxLength(255);
            entity.Property(e => e.HeadlineImageUrl).HasMaxLength(255);
            entity.Property(e => e.IncludeImageInExcerpt).HasDefaultValue(true);
            entity.Property(e => e.IncludeInFeed).HasDefaultValue(true, "DF_mp_Blogs_IncludeInFeed");
            entity.Property(e => e.IncludeInSearch).HasDefaultValue(true);
            entity.Property(e => e.IncludeInSiteMap).HasDefaultValue(true);
            entity.Property(e => e.ItemUrl).HasMaxLength(255);
            entity.Property(e => e.LastModUtc).HasColumnType("datetime");
            entity.Property(e => e.MapHeight)
                .HasMaxLength(10)
                .HasDefaultValue("300");
            entity.Property(e => e.MapType)
                .HasMaxLength(20)
                .HasDefaultValue("G_SATELLITE_MAP");
            entity.Property(e => e.MapWidth)
                .HasMaxLength(10)
                .HasDefaultValue("500");
            entity.Property(e => e.MapZoom).HasDefaultValue(13);
            entity.Property(e => e.MetaDescription).HasMaxLength(255);
            entity.Property(e => e.MetaKeywords).HasMaxLength(255);
            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
            entity.Property(e => e.PubAccess).HasMaxLength(20);
            entity.Property(e => e.PubGenres).HasMaxLength(255);
            entity.Property(e => e.PubGeoLocations).HasMaxLength(255);
            entity.Property(e => e.PubKeyWords).HasMaxLength(255);
            entity.Property(e => e.PubLanguage).HasMaxLength(7);
            entity.Property(e => e.PubName).HasMaxLength(255);
            entity.Property(e => e.PubStockTickers).HasMaxLength(255);
            entity.Property(e => e.ShowAuthorAvatar).HasDefaultValue(true);
            entity.Property(e => e.ShowAuthorBio).HasDefaultValue(true);
            entity.Property(e => e.ShowAuthorName).HasDefaultValue(true);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.SubTitle).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<MpBlogCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId);

            entity.ToTable("mp_BlogCategories");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Category).HasMaxLength(255);
            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
        });

        modelBuilder.Entity<MpBlogComment>(entity =>
        {
            entity.HasKey(e => e.BlogCommentId);

            entity.ToTable("mp_BlogComments");

            entity.Property(e => e.BlogCommentId).HasColumnName("BlogCommentID");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())", "DF_mp_BlogComments_DateCreated")
                .HasColumnType("datetime");
            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.Url)
                .HasMaxLength(200)
                .HasColumnName("URL");

            entity.HasOne(d => d.Item).WithMany(p => p.MpBlogComments)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_mp_BlogComments_mp_Blogs");
        });

        modelBuilder.Entity<MpBlogItemCategory>(entity =>
        {
            entity.ToTable("mp_BlogItemCategories");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.ItemId).HasColumnName("ItemID");

            entity.HasOne(d => d.Category).WithMany(p => p.MpBlogItemCategories)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_mp_BlogItemCategories_mp_BlogCategories");

            entity.HasOne(d => d.Item).WithMany(p => p.MpBlogItemCategories)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_mp_BlogItemCategories_mp_Blogs");
        });

        modelBuilder.Entity<MpBlogStat>(entity =>
        {
            entity.HasKey(e => e.ModuleId);

            entity.ToTable("mp_BlogStats");

            entity.Property(e => e.ModuleId)
                .ValueGeneratedNever()
                .HasColumnName("ModuleID");
        });

        modelBuilder.Entity<MpCalendarEvent>(entity =>
        {
            entity.HasKey(e => e.ItemId);

            entity.ToTable("mp_CalendarEvents");

            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())", "DF_mp_CalendarEvents_CreatedDate")
                .HasColumnType("datetime");
            entity.Property(e => e.EndTime).HasColumnType("smalldatetime");
            entity.Property(e => e.EventDate).HasColumnType("datetime");
            entity.Property(e => e.ImageName).HasMaxLength(100);
            entity.Property(e => e.LastModUtc).HasColumnType("datetime");
            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
            entity.Property(e => e.ShowMap).HasDefaultValue(true);
            entity.Property(e => e.StartTime).HasColumnType("smalldatetime");
            entity.Property(e => e.TicketPrice).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<MpCategory>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("mp_Category");

            entity.HasIndex(e => e.ModuleGuid, "IX_mp_Category");

            entity.HasIndex(e => e.Category, "IX_mp_Category_1");

            entity.HasIndex(e => e.ParentGuid, "IX_mp_Category_2");

            entity.Property(e => e.Guid).HasDefaultValueSql("(newid())", "DF_mp_Category_Guid");
            entity.Property(e => e.Category).HasMaxLength(255);
            entity.Property(e => e.CreatedUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_Category_CreatedUtc")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_Category_ModifiedUtc")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<MpCategoryItem>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("mp_CategoryItem");

            entity.HasIndex(e => e.ModuleGuid, "IX_mp_CategoryItem");

            entity.HasIndex(e => e.FeatureGuid, "IX_mp_CategoryItem_1");

            entity.HasIndex(e => e.ExtraGuid, "IX_mp_CategoryItem_2");

            entity.HasIndex(e => e.ItemGuid, "IX_mp_CategoryItem_3");

            entity.Property(e => e.Guid).HasDefaultValueSql("(newid())", "DF_mp_CategoryItem_Guid");
        });

        modelBuilder.Entity<MpComment>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("mp_Comments");

            entity.HasIndex(e => e.SiteGuid, "IX_mp_Comments");

            entity.HasIndex(e => e.ContentGuid, "IX_mp_Comments_1");

            entity.HasIndex(e => e.FeatureGuid, "IX_mp_Comments_2");

            entity.HasIndex(e => e.ModuleGuid, "IX_mp_Comments_3");

            entity.HasIndex(e => e.ParentGuid, "IX_mp_Comments_4");

            entity.Property(e => e.Guid).HasDefaultValueSql("(newid())", "DF_mp_Comments_Guid");
            entity.Property(e => e.CreatedUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_Comments_CreatedUtc")
                .HasColumnType("datetime");
            entity.Property(e => e.LastModUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_Comments_LastModUtc")
                .HasColumnType("datetime");
            entity.Property(e => e.ModerationReason).HasMaxLength(255);
            entity.Property(e => e.ModerationStatus).HasDefaultValue((byte)1, "DF_Table_1_Approved");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UserEmail).HasMaxLength(100);
            entity.Property(e => e.UserIp).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.UserUrl).HasMaxLength(255);
        });

        modelBuilder.Entity<MpCommerceReport>(entity =>
        {
            entity.HasKey(e => e.RowGuid);

            entity.ToTable("mp_CommerceReport");

            entity.HasIndex(e => e.SiteGuid, "IX_mp_CommerceReport");

            entity.HasIndex(e => e.UserGuid, "IX_mp_CommerceReport_1");

            entity.HasIndex(e => e.ModuleGuid, "IX_mp_CommerceReport_2");

            entity.HasIndex(e => e.ItemGuid, "IX_mp_CommerceReport_3");

            entity.Property(e => e.RowGuid).ValueGeneratedNever();
            entity.Property(e => e.AdminOrderLink).HasMaxLength(255);
            entity.Property(e => e.IncludeInAggregate).HasDefaultValue(true);
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(250)
                .HasColumnName("IPAddress");
            entity.Property(e => e.ItemName).HasMaxLength(255);
            entity.Property(e => e.ModuleTitle).HasMaxLength(255);
            entity.Property(e => e.OrderDateUtc).HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.RowCreatedUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_CommerceReport_RowCreatedUtc")
                .HasColumnType("datetime");
            entity.Property(e => e.SubTotal).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.UserOrderLink).HasMaxLength(255);
        });

        modelBuilder.Entity<MpCommerceReportOrder>(entity =>
        {
            entity.HasKey(e => e.RowGuid);

            entity.ToTable("mp_CommerceReportOrders");

            entity.HasIndex(e => e.SiteGuid, "IX_mp_CommerceReportOrders");

            entity.HasIndex(e => e.FeatureGuid, "IX_mp_CommerceReportOrders_1");

            entity.HasIndex(e => e.ModuleGuid, "IX_mp_CommerceReportOrders_2");

            entity.HasIndex(e => e.UserGuid, "IX_mp_CommerceReportOrders_3");

            entity.HasIndex(e => e.OrderGuid, "IX_mp_CommerceReportOrders_4");

            entity.HasIndex(e => e.BillingCountry, "IX_mp_CommerceReportOrders_5");

            entity.HasIndex(e => e.BillingState, "IX_mp_CommerceReportOrders_6");

            entity.HasIndex(e => e.BillingPostalCode, "IX_mp_CommerceReportOrders_7");

            entity.HasIndex(e => e.PaymentMethod, "IX_mp_CommerceReportOrders_8");

            entity.Property(e => e.RowGuid).ValueGeneratedNever();
            entity.Property(e => e.AdminOrderLink).HasMaxLength(255);
            entity.Property(e => e.BillingAddress1).HasMaxLength(255);
            entity.Property(e => e.BillingAddress2).HasMaxLength(255);
            entity.Property(e => e.BillingCity).HasMaxLength(255);
            entity.Property(e => e.BillingCompany).HasMaxLength(255);
            entity.Property(e => e.BillingCountry).HasMaxLength(255);
            entity.Property(e => e.BillingFirstName).HasMaxLength(100);
            entity.Property(e => e.BillingLastName).HasMaxLength(50);
            entity.Property(e => e.BillingPostalCode).HasMaxLength(20);
            entity.Property(e => e.BillingState).HasMaxLength(255);
            entity.Property(e => e.BillingSuburb).HasMaxLength(255);
            entity.Property(e => e.IncludeInAggregate).HasDefaultValue(true);
            entity.Property(e => e.OrderDateUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_CommerceReportOrders_OrderDateUtc")
                .HasColumnType("datetime");
            entity.Property(e => e.OrderTotal).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.RowCreatedUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_CommerceReportOrders_RowCreatedUtc")
                .HasColumnType("datetime");
            entity.Property(e => e.ShippingTotal).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.SubTotal).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.TaxTotal).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.UserOrderLink).HasMaxLength(255);
        });

        modelBuilder.Entity<MpContactFormMessage>(entity =>
        {
            entity.HasKey(e => e.RowGuid);

            entity.ToTable("mp_ContactFormMessage");

            entity.Property(e => e.RowGuid).HasDefaultValueSql("(newid())", "DF_mp_ContactFormMessage_RowGuid");
            entity.Property(e => e.CreatedFromIpAddress).HasMaxLength(255);
            entity.Property(e => e.CreatedUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_ContactFormMessage_CreatedUtc")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Subject).HasMaxLength(255);
            entity.Property(e => e.Url).HasMaxLength(255);
        });

        modelBuilder.Entity<MpContentHistory>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("mp_ContentHistory");

            entity.HasIndex(e => e.SiteGuid, "IX_mp_ContentHistory");

            entity.HasIndex(e => e.ContentGuid, "IX_mp_ContentHistory_1");

            entity.HasIndex(e => e.UserGuid, "IX_mp_ContentHistory_2");

            entity.Property(e => e.Guid).ValueGeneratedNever();
            entity.Property(e => e.CreatedUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_ContentHistory_CreatedUtc")
                .HasColumnType("datetime");
            entity.Property(e => e.HistoryUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_ContentHistory_HistoryUtc")
                .HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<MpContentMetaLink>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("mp_ContentMetaLink");

            entity.HasIndex(e => e.SiteGuid, "IX_mp_ContentMetaLink");

            entity.HasIndex(e => e.ModuleGuid, "IX_mp_ContentMetaLink_1");

            entity.HasIndex(e => e.ContentGuid, "IX_mp_ContentMetaLink_2");

            entity.Property(e => e.Guid).ValueGeneratedNever();
            entity.Property(e => e.CreatedUtc).HasColumnType("datetime");
            entity.Property(e => e.Href).HasMaxLength(255);
            entity.Property(e => e.HrefLang).HasMaxLength(10);
            entity.Property(e => e.LastModUtc).HasColumnType("datetime");
            entity.Property(e => e.Media).HasMaxLength(50);
            entity.Property(e => e.Rel).HasMaxLength(255);
            entity.Property(e => e.Rev).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<MpContentMetum>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("mp_ContentMeta");

            entity.HasIndex(e => e.SiteGuid, "IX_mp_ContentMeta");

            entity.HasIndex(e => e.ModuleGuid, "IX_mp_ContentMeta_1");

            entity.HasIndex(e => e.ContentGuid, "IX_mp_ContentMeta_2");

            entity.Property(e => e.Guid).ValueGeneratedNever();
            entity.Property(e => e.ContentProperty)
                .HasMaxLength(255)
                .HasDefaultValue("content", "ContentProperty_Default");
            entity.Property(e => e.CreatedUtc).HasColumnType("datetime");
            entity.Property(e => e.Dir).HasMaxLength(3);
            entity.Property(e => e.LangCode).HasMaxLength(10);
            entity.Property(e => e.LastModUtc).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.NameProperty)
                .HasMaxLength(255)
                .HasDefaultValue("name", "NameProperty_Default");
            entity.Property(e => e.Scheme).HasMaxLength(255);
        });

        modelBuilder.Entity<MpContentRating>(entity =>
        {
            entity.HasKey(e => e.RowGuid);

            entity.ToTable("mp_ContentRating");

            entity.HasIndex(e => e.ContentGuid, "IX_mp_ContentRatingContentGuid");

            entity.HasIndex(e => e.IpAddress, "IX_mp_ContentRatingIPAddr");

            entity.HasIndex(e => e.SiteGuid, "IX_mp_ContentRatingSiteGuid");

            entity.HasIndex(e => e.UserGuid, "IX_mp_ContentRatingUserGuid");

            entity.Property(e => e.RowGuid).ValueGeneratedNever();
            entity.Property(e => e.CreatedUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_ContentRating_CreatedUtc")
                .HasColumnType("datetime");
            entity.Property(e => e.EmailAddress).HasMaxLength(100);
            entity.Property(e => e.IpAddress).HasMaxLength(50);
            entity.Property(e => e.LastModUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_ContentRating_LastModUtc")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<MpContentStyle>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("mp_ContentStyle");

            entity.HasIndex(e => e.SiteGuid, "IX_mp_ContentStyle");

            entity.HasIndex(e => e.Name, "IX_mp_ContentStyle_1");

            entity.HasIndex(e => e.SkinName, "IX_mp_ContentStyle_2");

            entity.Property(e => e.Guid).ValueGeneratedNever();
            entity.Property(e => e.CreatedUtc).HasColumnType("datetime");
            entity.Property(e => e.CssClass).HasMaxLength(50);
            entity.Property(e => e.Element).HasMaxLength(50);
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_mp_ContentStyle_IsActive");
            entity.Property(e => e.LastModUtc).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.SkinName).HasMaxLength(100);
        });

        modelBuilder.Entity<MpContentTemplate>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("mp_ContentTemplate");

            entity.HasIndex(e => e.SiteGuid, "IX_mp_ContentTemplate");

            entity.HasIndex(e => e.Title, "IX_mp_ContentTemplate_1");

            entity.Property(e => e.Guid).ValueGeneratedNever();
            entity.Property(e => e.CreatedUtc).HasColumnType("datetime");
            entity.Property(e => e.ImageFileName).HasMaxLength(255);
            entity.Property(e => e.LastModUtc).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<MpContentWorkflow>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("mp_ContentWorkflow");

            entity.HasIndex(e => e.SiteGuid, "IX_mp_ContentWorkflow");

            entity.HasIndex(e => e.ModuleGuid, "IX_mp_ContentWorkflow_1");

            entity.Property(e => e.Guid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDateUtc).HasColumnType("datetime");
            entity.Property(e => e.LastModUtc).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(20);
        });

        modelBuilder.Entity<MpContentWorkflowAuditHistory>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("mp_ContentWorkflowAuditHistory");

            entity.HasIndex(e => e.ContentWorkflowGuid, "IX_mp_ContentWorkflowAuditHistory");

            entity.HasIndex(e => e.ModuleGuid, "IX_mp_ContentWorkflowAuditHistory_1");

            entity.HasIndex(e => e.UserGuid, "IX_mp_ContentWorkflowAuditHistory_2");

            entity.Property(e => e.Guid).HasDefaultValueSql("(newid())", "DF__mp_Content__Guid__186270A4");
            entity.Property(e => e.CreatedDateUtc).HasColumnType("datetime");
            entity.Property(e => e.NewStatus).HasMaxLength(20);

            entity.HasOne(d => d.ContentWorkflow).WithMany(p => p.MpContentWorkflowAuditHistories)
                .HasForeignKey(d => d.ContentWorkflowGuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_mp_ContentWorkflowAuditHistory_mp_ContentWorkflow");
        });

        modelBuilder.Entity<MpCurrency>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("mp_Currency");

            entity.Property(e => e.Guid).HasDefaultValueSql("(newid())", "DF_mp_Currency_Guid");
            entity.Property(e => e.Code)
                .HasMaxLength(3)
                .IsFixedLength();
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_Currency_Created")
                .HasColumnType("datetime");
            entity.Property(e => e.DecimalPlaces)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.DecimalPointChar)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.LastModified).HasColumnType("datetime");
            entity.Property(e => e.SymbolLeft).HasMaxLength(15);
            entity.Property(e => e.SymbolRight).HasMaxLength(15);
            entity.Property(e => e.ThousandsPointChar)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.Value).HasColumnType("decimal(13, 8)");
        });

        modelBuilder.Entity<MpEmailSendLog>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("mp_EmailSendLog");

            entity.HasIndex(e => e.SiteGuid, "IX_mp_EmailSendLog");

            entity.HasIndex(e => e.ModuleGuid, "IX_mp_EmailSendLog_1");

            entity.HasIndex(e => e.SpecialGuid1, "IX_mp_EmailSendLog_2");

            entity.HasIndex(e => e.SpecialGuid2, "IX_mp_EmailSendLog_3");

            entity.HasIndex(e => e.Type, "IX_mp_EmailSendLog_4");

            entity.Property(e => e.Guid).ValueGeneratedNever();
            entity.Property(e => e.BccAddress).HasMaxLength(255);
            entity.Property(e => e.CcAddress).HasMaxLength(255);
            entity.Property(e => e.FromAddress).HasMaxLength(100);
            entity.Property(e => e.ReplyTo).HasMaxLength(100);
            entity.Property(e => e.SentUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_EmailSendLog_SentUtc")
                .HasColumnType("datetime");
            entity.Property(e => e.Subject).HasMaxLength(255);
            entity.Property(e => e.ToAddress).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<MpEmailSendQueue>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("mp_EmailSendQueue");

            entity.HasIndex(e => e.SiteGuid, "IX_mp_EmailSendQueue");

            entity.HasIndex(e => e.ModuleGuid, "IX_mp_EmailSendQueue_1");

            entity.HasIndex(e => e.UserGuid, "IX_mp_EmailSendQueue_2");

            entity.HasIndex(e => e.SpecialGuid1, "IX_mp_EmailSendQueue_3");

            entity.HasIndex(e => e.SpecialGuid2, "IX_mp_EmailSendQueue_4");

            entity.HasIndex(e => e.ToAddress, "IX_mp_EmailSendQueue_5");

            entity.HasIndex(e => e.Type, "IX_mp_EmailSendQueue_6");

            entity.Property(e => e.Guid).ValueGeneratedNever();
            entity.Property(e => e.BccAddress).HasMaxLength(255);
            entity.Property(e => e.CcAddress).HasMaxLength(255);
            entity.Property(e => e.CreatedUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_EmailSendQueue_CreatedUtc")
                .HasColumnType("datetime");
            entity.Property(e => e.DateToSend).HasColumnType("datetime");
            entity.Property(e => e.FromAddress).HasMaxLength(100);
            entity.Property(e => e.ReplyTo).HasMaxLength(100);
            entity.Property(e => e.Subject).HasMaxLength(255);
            entity.Property(e => e.ToAddress).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<MpEmailTemplate>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("mp_EmailTemplate");

            entity.HasIndex(e => e.SiteGuid, "IX_mp_EmailTemplate");

            entity.HasIndex(e => e.ModuleGuid, "IX_mp_EmailTemplate_1");

            entity.HasIndex(e => e.SpecialGuid1, "IX_mp_EmailTemplate_2");

            entity.HasIndex(e => e.SpecialGuid2, "IX_mp_EmailTemplate_3");

            entity.HasIndex(e => e.FeatureGuid, "IX_mp_EmailTemplate_4");

            entity.HasIndex(e => e.Name, "IX_mp_EmailTemplate_5");

            entity.Property(e => e.Guid).ValueGeneratedNever();
            entity.Property(e => e.CreatedUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_EmailTemplate_CreatedUtc")
                .HasColumnType("datetime");
            entity.Property(e => e.IsEditable).HasDefaultValue(true, "DF_mp_EmailTemplate_IsEditable");
            entity.Property(e => e.LastModUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_EmailTemplate_LastModUtc")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Subject).HasMaxLength(255);
        });

        modelBuilder.Entity<MpFileAttachment>(entity =>
        {
            entity.HasKey(e => e.RowGuid);

            entity.ToTable("mp_FileAttachment");

            entity.HasIndex(e => e.SiteGuid, "IX_mp_FileAttachment");

            entity.HasIndex(e => e.ModuleGuid, "IX_mp_FileAttachment_1");

            entity.HasIndex(e => e.ItemGuid, "IX_mp_FileAttachment_2");

            entity.HasIndex(e => e.SpecialGuid1, "IX_mp_FileAttachment_3");

            entity.HasIndex(e => e.SpecialGuid2, "IX_mp_FileAttachment_4");

            entity.Property(e => e.RowGuid).HasDefaultValueSql("(newid())", "DF_mp_FileAttachment_RowGuid");
            entity.Property(e => e.ContentLength).HasDefaultValue(0L);
            entity.Property(e => e.ContentTitle).HasMaxLength(255);
            entity.Property(e => e.ContentType).HasMaxLength(50);
            entity.Property(e => e.CreatedUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_FileAttachment_CreatedUtc")
                .HasColumnType("datetime");
            entity.Property(e => e.FileName).HasMaxLength(255);
            entity.Property(e => e.ServerFileName).HasMaxLength(255);
        });

        modelBuilder.Entity<MpForum>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK_mp_ForumBoards");

            entity.ToTable("mp_Forums");

            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.AllowAnonymousPosts).HasDefaultValue(true, "DF_mp_Forums_AllowAnonymousPosts");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())", "DF_mp_ForumBoards_DateCreated")
                .HasColumnType("datetime");
            entity.Property(e => e.ForumGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.IncludeInGoogleMap).HasDefaultValue(true);
            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_mp_ForumBoards_Active");
            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
            entity.Property(e => e.MostRecentPostDate).HasColumnType("datetime");
            entity.Property(e => e.MostRecentPostUserId)
                .HasDefaultValue(-1, "DF_mp_ForumBoards_MostRecentPostUserID")
                .HasColumnName("MostRecentPostUserID");
            entity.Property(e => e.PostsPerPage).HasDefaultValue(10, "DF_mp_Forums_EntriesPerPage");
            entity.Property(e => e.RolesThatCanPost).HasDefaultValue("Authenticated Users");
            entity.Property(e => e.SortOrder).HasDefaultValue(100, "DF_mp_ForumBoards_SortOrder");
            entity.Property(e => e.ThreadsPerPage).HasDefaultValue(40, "DF_mp_Forums_ThreadsPerPage");
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.Visible).HasDefaultValue(true);
        });

        modelBuilder.Entity<MpForumPost>(entity =>
        {
            entity.HasKey(e => e.PostId);

            entity.ToTable("mp_ForumPosts");

            entity.HasIndex(e => e.UserId, "idxforumpostuid");

            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.ApprovedUtc).HasColumnType("datetime");
            entity.Property(e => e.ModStatus).HasDefaultValue(1);
            entity.Property(e => e.NotificationSent).HasDefaultValue(true);
            entity.Property(e => e.PostDate)
                .HasDefaultValueSql("(getdate())", "DF_mp_ForumPosts_PostDate")
                .HasColumnType("datetime");
            entity.Property(e => e.PostGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.SortOrder).HasDefaultValue(100, "DF_mp_ForumPosts_SortOrder");
            entity.Property(e => e.Subject).HasMaxLength(255);
            entity.Property(e => e.ThreadId).HasColumnName("ThreadID");
            entity.Property(e => e.ThreadSequence).HasDefaultValue(1, "DF_mp_ForumPosts_ThreadSequence");
            entity.Property(e => e.UserId)
                .HasDefaultValue(-1, "DF_mp_ForumPosts_UserID")
                .HasColumnName("UserID");
            entity.Property(e => e.UserIp).HasMaxLength(50);

            entity.HasOne(d => d.Thread).WithMany(p => p.MpForumPosts)
                .HasForeignKey(d => d.ThreadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_mp_ForumPosts_mp_ForumThreads");
        });

        modelBuilder.Entity<MpForumSubscription>(entity =>
        {
            entity.HasKey(e => e.SubscriptionId);

            entity.ToTable("mp_ForumSubscriptions");

            entity.Property(e => e.SubscriptionId).HasColumnName("SubscriptionID");
            entity.Property(e => e.ForumId).HasColumnName("ForumID");
            entity.Property(e => e.SubGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.SubscribeDate)
                .HasDefaultValueSql("(getdate())", "DF_mp_ForumSubscriptions_SubscribeDate")
                .HasColumnType("datetime");
            entity.Property(e => e.UnSubscribeDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<MpForumThread>(entity =>
        {
            entity.HasKey(e => e.ThreadId);

            entity.ToTable("mp_ForumThreads");

            entity.Property(e => e.ThreadId).HasColumnName("ThreadID");
            entity.Property(e => e.ForumId).HasColumnName("ForumID");
            entity.Property(e => e.ForumSequence).HasDefaultValue(1, "DF_mp_ForumThreads_ForumSequence");
            entity.Property(e => e.IncludeInSiteMap).HasDefaultValue(true);
            entity.Property(e => e.IsQuestion).HasDefaultValue(true);
            entity.Property(e => e.LockedReason).HasMaxLength(255);
            entity.Property(e => e.LockedUtc).HasColumnType("datetime");
            entity.Property(e => e.ModStatus).HasDefaultValue(1);
            entity.Property(e => e.MostRecentPostDate)
                .HasDefaultValueSql("(getdate())", "DF_mp_ForumThreads_MostRecentPostDate")
                .HasColumnType("datetime");
            entity.Property(e => e.MostRecentPostUserId).HasColumnName("MostRecentPostUserID");
            entity.Property(e => e.PtitleOverride)
                .HasMaxLength(255)
                .HasColumnName("PTitleOverride");
            entity.Property(e => e.SortOrder).HasDefaultValue(1000, "DF_mp_ForumThreads_SortOrder");
            entity.Property(e => e.StartedByUserId).HasColumnName("StartedByUserID");
            entity.Property(e => e.ThreadDate)
                .HasDefaultValueSql("(getdate())", "DF_mp_ForumThreads_ThreadDate")
                .HasColumnType("datetime");
            entity.Property(e => e.ThreadGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ThreadSubject).HasMaxLength(255);
            entity.Property(e => e.ThreadType).HasMaxLength(100);

            entity.HasOne(d => d.Forum).WithMany(p => p.MpForumThreads)
                .HasForeignKey(d => d.ForumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_mp_ForumThreads_mp_Forums");
        });

        modelBuilder.Entity<MpForumThreadSubscription>(entity =>
        {
            entity.HasKey(e => e.ThreadSubscriptionId);

            entity.ToTable("mp_ForumThreadSubscriptions");

            entity.Property(e => e.ThreadSubscriptionId).HasColumnName("ThreadSubscriptionID");
            entity.Property(e => e.SubGuid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.SubscribeDate)
                .HasDefaultValueSql("(getdate())", "DF_mp_ForumThreadSubscriptions_SubscribeDate")
                .HasColumnType("datetime");
            entity.Property(e => e.ThreadId).HasColumnName("ThreadID");
            entity.Property(e => e.UnSubscribeDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Thread).WithMany(p => p.MpForumThreadSubscriptions)
                .HasForeignKey(d => d.ThreadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_mp_ForumThreadSubscriptions_mp_ForumThreads");
        });

        modelBuilder.Entity<MpFriendlyUrl>(entity =>
        {
            entity.HasKey(e => e.UrlId);

            entity.ToTable("mp_FriendlyUrls");

            entity.HasIndex(e => e.FriendlyUrl, "IX_mp_FriendlyUrls");

            entity.Property(e => e.UrlId).HasColumnName("UrlID");
            entity.Property(e => e.FriendlyUrl).HasMaxLength(255);
            entity.Property(e => e.RealUrl).HasMaxLength(255);
            entity.Property(e => e.SiteId).HasColumnName("SiteID");
        });

        modelBuilder.Entity<MpGalleryImage>(entity =>
        {
            entity.HasKey(e => e.ItemId);

            entity.ToTable("mp_GalleryImages");

            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.Caption).HasMaxLength(255);
            entity.Property(e => e.DisplayOrder).HasDefaultValue(100, "DF_mp_GalleryImages_DisplayOrder");
            entity.Property(e => e.ImageFile).HasMaxLength(100);
            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
            entity.Property(e => e.ThumbnailFile).HasMaxLength(100);
            entity.Property(e => e.UploadDate)
                .HasDefaultValueSql("(getdate())", "DF_mp_GalleryImages_UploadDate")
                .HasColumnType("datetime");
            entity.Property(e => e.UploadUser).HasMaxLength(100);
            entity.Property(e => e.WebImageFile).HasMaxLength(100);
        });

        modelBuilder.Entity<MpGeoCountry>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("mp_GeoCountry");

            entity.Property(e => e.Guid).ValueGeneratedNever();
            entity.Property(e => e.Isocode2)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("ISOCode2");
            entity.Property(e => e.Isocode3)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("ISOCode3");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<MpGeoZone>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("mp_GeoZone");

            entity.Property(e => e.Guid).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.Country).WithMany(p => p.MpGeoZones)
                .HasForeignKey(d => d.CountryGuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_mp_GeoZone_mp_GeoCountry");
        });

        modelBuilder.Entity<MpGoogleCheckoutLog>(entity =>
        {
            entity.HasKey(e => e.RowGuid);

            entity.ToTable("mp_GoogleCheckoutLog");

            entity.Property(e => e.RowGuid).HasDefaultValueSql("(newid())", "DF_mp_GoogleCheckoutLog_RowGuid");
            entity.Property(e => e.AuthAmt).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.AuthExpDate).HasColumnType("datetime");
            entity.Property(e => e.AvsResponse).HasMaxLength(5);
            entity.Property(e => e.BuyerId).HasMaxLength(50);
            entity.Property(e => e.CreatedUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_GoogleCheckoutLog_CreatedUtc")
                .HasColumnType("datetime");
            entity.Property(e => e.CvnResponse).HasMaxLength(5);
            entity.Property(e => e.DiscountTotal).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.FinanceState).HasMaxLength(50);
            entity.Property(e => e.FullfillState).HasMaxLength(50);
            entity.Property(e => e.Gtimestamp)
                .HasColumnType("datetime")
                .HasColumnName("GTimestamp");
            entity.Property(e => e.LatestChargeback).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.LatestChgAmt).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.LatestRefundAmt).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.NotificationType).HasMaxLength(255);
            entity.Property(e => e.OrderNumber).HasMaxLength(50);
            entity.Property(e => e.OrderTotal).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.ProviderName).HasMaxLength(255);
            entity.Property(e => e.SerialNumber).HasMaxLength(50);
            entity.Property(e => e.ShippingTotal).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.TaxTotal).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.TotalChargeback).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.TotalChgAmt).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.TotalRefundAmt).HasColumnType("decimal(15, 4)");
        });

        modelBuilder.Entity<MpHtmlContent>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK_mp_HtmlText");

            entity.ToTable("mp_HtmlContent");

            entity.HasIndex(e => new { e.ModuleId, e.BeginDate, e.EndDate, e.LastModUserGuid, e.UserGuid }, "IX_mp_HtmlContent_ModuleID_BeginDate_EndDate_LastModUserGuid_UserGuid");

            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.BeginDate)
                .HasDefaultValueSql("(((1)/(1))/(1901))", "DF_mp_HtmlText_BeginDate")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())", "DF_mp_HtmlText_CreatedDate")
                .HasColumnType("datetime");
            entity.Property(e => e.EndDate)
                .HasDefaultValueSql("(((1)/(1))/(2200))", "DF_mp_HtmlText_EndDate")
                .HasColumnType("datetime");
            entity.Property(e => e.LastModUtc).HasColumnType("datetime");
            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
            entity.Property(e => e.MoreLink).HasMaxLength(255);
            entity.Property(e => e.SortOrder).HasDefaultValue(500, "DF_mp_HtmlText_SortOrder");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Module).WithMany(p => p.MpHtmlContents)
                .HasForeignKey(d => d.ModuleId)
                .HasConstraintName("FK_HtmlText_Modules");
        });

        modelBuilder.Entity<MpIndexingQueue>(entity =>
        {
            entity.HasKey(e => e.RowId);

            entity.ToTable("mp_IndexingQueue");

            entity.Property(e => e.IndexPath).HasMaxLength(255);
            entity.Property(e => e.ItemKey).HasMaxLength(255);
            entity.Property(e => e.SiteId)
                .HasDefaultValue(1)
                .HasColumnName("SiteID");
        });

        modelBuilder.Entity<MpLanguage>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("mp_Language");

            entity.Property(e => e.Guid).ValueGeneratedNever();
            entity.Property(e => e.Code)
                .HasMaxLength(2)
                .IsFixedLength();
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<MpLetter>(entity =>
        {
            entity.HasKey(e => e.LetterGuid);

            entity.ToTable("mp_Letter");

            entity.Property(e => e.LetterGuid).HasDefaultValueSql("(newid())", "DF_mp_Letter_LetterGuid");
            entity.Property(e => e.CreatedUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_Letter_CreatedUTC")
                .HasColumnType("datetime")
                .HasColumnName("CreatedUTC");
            entity.Property(e => e.LastModUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_Letter_LastModUTC")
                .HasColumnType("datetime")
                .HasColumnName("LastModUTC");
            entity.Property(e => e.SendClickedUtc)
                .HasColumnType("datetime")
                .HasColumnName("SendClickedUTC");
            entity.Property(e => e.SendCompleteUtc)
                .HasColumnType("datetime")
                .HasColumnName("SendCompleteUTC");
            entity.Property(e => e.SendStartedUtc)
                .HasColumnType("datetime")
                .HasColumnName("SendStartedUTC");
            entity.Property(e => e.Subject).HasMaxLength(255);

            entity.HasOne(d => d.LetterInfo).WithMany(p => p.MpLetters)
                .HasForeignKey(d => d.LetterInfoGuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_mp_Letter_mp_LetterInfo");
        });

        modelBuilder.Entity<MpLetterHtmlTemplate>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("mp_LetterHtmlTemplate");

            entity.Property(e => e.Guid).HasDefaultValueSql("(newid())", "DF_mp_LetterHtmlTemplate_Guid");
            entity.Property(e => e.LastModUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_LetterHtmlTemplate_LastModUTC")
                .HasColumnType("datetime")
                .HasColumnName("LastModUTC");
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<MpLetterInfo>(entity =>
        {
            entity.HasKey(e => e.LetterInfoGuid);

            entity.ToTable("mp_LetterInfo");

            entity.Property(e => e.LetterInfoGuid).HasDefaultValueSql("(newid())", "DF_Table_1_InfoGuid");
            entity.Property(e => e.AllowUserFeedback).HasDefaultValue(true, "DF_mp_LetterInfo_AllowUserFeedback");
            entity.Property(e => e.AvailableToRoles).HasDefaultValue("All Users");
            entity.Property(e => e.CreatedUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_LetterInfo_CreatedUTC")
                .HasColumnType("datetime")
                .HasColumnName("CreatedUTC");
            entity.Property(e => e.DisplayNameDefault).HasMaxLength(50);
            entity.Property(e => e.EnableViewAsWebPage).HasDefaultValue(true, "DF_mp_LetterInfo_EnableViewAsWebPage");
            entity.Property(e => e.Enabled).HasDefaultValue(true, "DF_mp_LetterInfo_Enabled");
            entity.Property(e => e.FirstNameDefault).HasMaxLength(50);
            entity.Property(e => e.FromAddress).HasMaxLength(255);
            entity.Property(e => e.FromName).HasMaxLength(255);
            entity.Property(e => e.LastModUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_LetterInfo_LastModUTC")
                .HasColumnType("datetime")
                .HasColumnName("LastModUTC");
            entity.Property(e => e.LastNameDefault).HasMaxLength(50);
            entity.Property(e => e.ReplyToAddress).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<MpLetterSendLog>(entity =>
        {
            entity.HasKey(e => e.RowId);

            entity.ToTable("mp_LetterSendLog");

            entity.HasIndex(e => e.SubscribeGuid, "IX_mp_SendLogSubscribe");

            entity.Property(e => e.RowId).HasColumnName("RowID");
            entity.Property(e => e.EmailAddress).HasMaxLength(100);
            entity.Property(e => e.Utc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_LetterSendLog_UTC")
                .HasColumnType("datetime")
                .HasColumnName("UTC");
        });

        modelBuilder.Entity<MpLetterSubscribe>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("mp_LetterSubscribe");

            entity.HasIndex(e => e.Email, "IX_mp_LetterSubscribe");

            entity.HasIndex(e => e.IpAddress, "IX_mp_LetterSubscribeIp");

            entity.HasIndex(e => e.LetterInfoGuid, "IX_mp_LetterSubscribe_1");

            entity.HasIndex(e => e.UserGuid, "IX_mp_LetterSubscribe_2");

            entity.HasIndex(e => e.SiteGuid, "IX_mp_LetterSubscribe_3");

            entity.Property(e => e.Guid).HasDefaultValueSql("(newid())", "DF_mp_LetterSubscribe_Guid");
            entity.Property(e => e.BeginUtc).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.IpAddress).HasMaxLength(100);
            entity.Property(e => e.UseHtml).HasDefaultValue(true, "DF_mp_LetterSubscribe_UseHtml");
        });

        modelBuilder.Entity<MpLetterSubscribeHx>(entity =>
        {
            entity.HasKey(e => e.RowGuid);

            entity.ToTable("mp_LetterSubscribeHx");

            entity.HasIndex(e => e.SiteGuid, "IX_mp_LetterSubscribeHx");

            entity.Property(e => e.RowGuid).ValueGeneratedNever();
            entity.Property(e => e.BeginUtc).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.EndUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_LetterSubscribeHx_EndUtc")
                .HasColumnType("datetime");
            entity.Property(e => e.IpAddress).HasMaxLength(100);
            entity.Property(e => e.UseHtml).HasDefaultValue(true, "DF_mp_LetterSubscribeHx_UseHtml");
        });

        modelBuilder.Entity<MpLink>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK_Links");

            entity.ToTable("mp_Links");

            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
            entity.Property(e => e.Target)
                .HasMaxLength(20)
                .HasDefaultValue("_blank", "DF_mp_Links_Target");
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Module).WithMany(p => p.MpLinks)
                .HasForeignKey(d => d.ModuleId)
                .HasConstraintName("FK_Links_Modules");
        });

        modelBuilder.Entity<MpMediaFile>(entity =>
        {
            entity.HasKey(e => e.FileId);

            entity.ToTable("mp_MediaFile");

            entity.Property(e => e.FileId).HasColumnName("FileID");
            entity.Property(e => e.AddedDate)
                .HasDefaultValueSql("(getdate())", "DF_mp_AudioFile_AddedDate")
                .HasColumnType("datetime");
            entity.Property(e => e.FilePath).HasMaxLength(255);
            entity.Property(e => e.TrackId).HasColumnName("TrackID");

            entity.HasOne(d => d.Track).WithMany(p => p.MpMediaFiles)
                .HasForeignKey(d => d.TrackId)
                .HasConstraintName("FK_mp_AudioFiles_AudioTracks1");
        });

        modelBuilder.Entity<MpMediaPlayer>(entity =>
        {
            entity.HasKey(e => e.PlayerId);

            entity.ToTable("mp_MediaPlayer");

            entity.Property(e => e.PlayerId).HasColumnName("PlayerID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())", "DF_mp_MediaPlayer_CreatedDate")
                .HasColumnType("datetime");
            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
            entity.Property(e => e.PlayerType).HasMaxLength(10);
            entity.Property(e => e.Skin).HasMaxLength(50);
        });

        modelBuilder.Entity<MpMediaTrack>(entity =>
        {
            entity.HasKey(e => e.TrackId);

            entity.ToTable("mp_MediaTrack");

            entity.Property(e => e.TrackId).HasColumnName("TrackID");
            entity.Property(e => e.Artist).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())", "DF_mp_AudioTrack_CreatedDate")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PlayerId).HasColumnName("PlayerID");
            entity.Property(e => e.TrackType).HasMaxLength(10);

            entity.HasOne(d => d.Player).WithMany(p => p.MpMediaTracks)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("FK_mp_MediaTrack_MediaPlayers");
        });

        modelBuilder.Entity<MpModule>(entity =>
        {
            entity.HasKey(e => e.ModuleId).HasName("PK_Modules");

            entity.ToTable("mp_Modules");

            entity.HasIndex(e => e.ModuleDefId, "IX_mp_ModulesDefId");

            entity.HasIndex(e => e.FeatureGuid, "IX_mp_ModulesFeatGuid");

            entity.HasIndex(e => e.SiteGuid, "IX_mp_ModulesSiteGuid");

            entity.HasIndex(e => e.SiteId, "IX_mp_ModulesSiteID");

            entity.HasIndex(e => e.FeatureGuid, "idxModulesFGuid");

            entity.HasIndex(e => e.Guid, "idxModulesGuid");

            entity.HasIndex(e => e.ModuleDefId, "idxModulesMDef");

            entity.HasIndex(e => e.SiteId, "idxModulesSID");

            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
            entity.Property(e => e.AllowMultipleInstancesOnMyPage).HasDefaultValue(true, "DF_mp_Modules_AllowMultipleInstancesOnMyPage");
            entity.Property(e => e.CreatedByUserId)
                .HasDefaultValue(-1, "DF_mp_Modules_CreatedByUserID")
                .HasColumnName("CreatedByUserID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())", "DF_mp_Modules_CreatedDate")
                .HasColumnType("datetime");
            entity.Property(e => e.EditUserId).HasColumnName("EditUserID");
            entity.Property(e => e.HeadElement).HasMaxLength(25);
            entity.Property(e => e.Icon).HasMaxLength(255);
            entity.Property(e => e.ModuleDefId).HasColumnName("ModuleDefID");
            entity.Property(e => e.ModuleTitle).HasMaxLength(255);
            entity.Property(e => e.ShowTitle).HasDefaultValue(true, "DF_mp_Modules_ShowTitle");
            entity.Property(e => e.SiteId).HasColumnName("SiteID");

            entity.HasOne(d => d.ModuleDef).WithMany(p => p.MpModules)
                .HasForeignKey(d => d.ModuleDefId)
                .HasConstraintName("FK_Modules_ModuleDefinitions");
        });

        modelBuilder.Entity<MpModuleDefinition>(entity =>
        {
            entity.HasKey(e => e.ModuleDefId).HasName("PK_ModuleDefinitions");

            entity.ToTable("mp_ModuleDefinitions");

            entity.HasIndex(e => e.Guid, "IX_mp_ModuleDefinitions");

            entity.HasIndex(e => e.Guid, "idxModuleDefGuid");

            entity.Property(e => e.ModuleDefId).HasColumnName("ModuleDefID");
            entity.Property(e => e.ControlSrc).HasMaxLength(255);
            entity.Property(e => e.DeleteProvider).HasMaxLength(255);
            entity.Property(e => e.FeatureName).HasMaxLength(255);
            entity.Property(e => e.Guid).HasDefaultValueSql("(newid())", "DF_mp_ModuleDefinitions_Guid");
            entity.Property(e => e.Icon).HasMaxLength(255);
            entity.Property(e => e.PartialView).HasMaxLength(255);
            entity.Property(e => e.ResourceFile).HasMaxLength(255);
            entity.Property(e => e.SearchListName).HasMaxLength(255);
            entity.Property(e => e.SkinFileName).HasMaxLength(255);
            entity.Property(e => e.SortOrder).HasDefaultValue(500, "DF_mp_ModuleDefinitions_SortOrder");
        });

        modelBuilder.Entity<MpModuleDefinitionSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_mp_ModuleDefinitionSettings_1");

            entity.ToTable("mp_ModuleDefinitionSettings");

            entity.HasIndex(e => e.GroupName, "IX_mp_ModuleDefGroup");

            entity.HasIndex(e => e.ModuleDefId, "IX_mp_ModuleDefSetDefId");

            entity.HasIndex(e => e.SettingName, "IX_mp_ModuleDefSetSetName");

            entity.HasIndex(e => new { e.ModuleDefId, e.SettingName }, "IX_mp_ModuleDefinitionSettings").IsUnique();

            entity.HasIndex(e => new { e.ModuleDefId, e.Id }, "IX_mp_ModuleDefinitionSettings_ModuleDefID_ID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Attributes)
                .HasDefaultValue("")
                .HasColumnType("ntext");
            entity.Property(e => e.ControlSrc).HasMaxLength(255);
            entity.Property(e => e.ControlType).HasMaxLength(50);
            entity.Property(e => e.GroupName).HasMaxLength(255);
            entity.Property(e => e.HelpKey).HasMaxLength(255);
            entity.Property(e => e.ModuleDefId).HasColumnName("ModuleDefID");
            entity.Property(e => e.Options)
                .HasDefaultValue("")
                .HasColumnType("ntext");
            entity.Property(e => e.ResourceFile).HasMaxLength(255);
            entity.Property(e => e.SettingName).HasMaxLength(50);
            entity.Property(e => e.SortOrder).HasDefaultValue(100);
        });

        modelBuilder.Entity<MpModuleSetting>(entity =>
        {
            entity.ToTable("mp_ModuleSettings");

            entity.HasIndex(e => e.ModuleId, "IX_mp_ModuleSetModId");

            entity.HasIndex(e => e.SettingName, "IX_mp_ModuleSetSetName");

            entity.HasIndex(e => new { e.ModuleId, e.SettingName }, "IX_mp_ModuleSettings_ModuleID_SettingName");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ControlSrc).HasMaxLength(255);
            entity.Property(e => e.ControlType).HasMaxLength(50);
            entity.Property(e => e.HelpKey).HasMaxLength(255);
            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
            entity.Property(e => e.SettingName).HasMaxLength(50);
            entity.Property(e => e.SortOrder).HasDefaultValue(100);

            entity.HasOne(d => d.Module).WithMany(p => p.MpModuleSettings)
                .HasForeignKey(d => d.ModuleId)
                .HasConstraintName("FK_ModuleSettings_Modules");
        });

        modelBuilder.Entity<MpPage>(entity =>
        {
            entity.HasKey(e => e.PageId).HasName("PK_Tabs");

            entity.ToTable("mp_Pages");

            entity.HasIndex(e => e.PageName, "IX_mp_page_name");

            entity.Property(e => e.PageId).HasColumnName("PageID");
            entity.Property(e => e.AdditionalMetaTags).HasMaxLength(255);
            entity.Property(e => e.AllowBrowserCache).HasDefaultValue(true, "DF_mp_Pages_AllowBrowserCache");
            entity.Property(e => e.BodyCssClass).HasMaxLength(50);
            entity.Property(e => e.CanonicalOverride).HasMaxLength(255);
            entity.Property(e => e.ChangeFrequency).HasMaxLength(20);
            entity.Property(e => e.CompiledMetaUtc).HasColumnType("datetime");
            entity.Property(e => e.IncludeInMenu).HasDefaultValue(true, "DF_mp_Pages_IncludeInMenu");
            entity.Property(e => e.LastModifiedUtc)
                .HasColumnType("datetime")
                .HasColumnName("LastModifiedUTC");
            entity.Property(e => e.LinkRel).HasMaxLength(20);
            entity.Property(e => e.MenuCssClass).HasMaxLength(50);
            entity.Property(e => e.PageDescription).HasMaxLength(255);
            entity.Property(e => e.PageEncoding).HasMaxLength(255);
            entity.Property(e => e.PageGuid).HasDefaultValueSql("(newid())", "DF_mp_Pages_PageGuid");
            entity.Property(e => e.PageHeading).HasMaxLength(255);
            entity.Property(e => e.PageKeyWords).HasMaxLength(1000);
            entity.Property(e => e.PageName).HasMaxLength(255);
            entity.Property(e => e.PageTitle).HasMaxLength(255);
            entity.Property(e => e.ParentId)
                .HasDefaultValue(-1, "DF_mp_Pages_ParentID")
                .HasColumnName("ParentID");
            entity.Property(e => e.PcreatedBy).HasColumnName("PCreatedBy");
            entity.Property(e => e.PcreatedFromIp)
                .HasMaxLength(36)
                .HasColumnName("PCreatedFromIp");
            entity.Property(e => e.PcreatedUtc)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime")
                .HasColumnName("PCreatedUtc");
            entity.Property(e => e.PlastModBy).HasColumnName("PLastModBy");
            entity.Property(e => e.PlastModFromIp)
                .HasMaxLength(36)
                .HasColumnName("PLastModFromIp");
            entity.Property(e => e.PlastModUtc)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime")
                .HasColumnName("PLastModUtc");
            entity.Property(e => e.PubDateUtc)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RequireSsl).HasColumnName("RequireSSL");
            entity.Property(e => e.ShowPageHeading).HasDefaultValue(true);
            entity.Property(e => e.SiteId).HasColumnName("SiteID");
            entity.Property(e => e.SiteMapPriority).HasMaxLength(10);
            entity.Property(e => e.Skin).HasMaxLength(100);
            entity.Property(e => e.Url).HasMaxLength(255);

            entity.HasOne(d => d.Site).WithMany(p => p.MpPages)
                .HasForeignKey(d => d.SiteId)
                .HasConstraintName("FK_Tabs_Portals");
        });

        modelBuilder.Entity<MpPageModule>(entity =>
        {
            entity.HasKey(e => new { e.PageId, e.ModuleId });

            entity.ToTable("mp_PageModules");

            entity.HasIndex(e => e.PaneName, "IX_mp_pm_pane");

            entity.Property(e => e.PageId).HasColumnName("PageID");
            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
            entity.Property(e => e.ModuleOrder).HasDefaultValue(3, "DF_mp_PageModules_ModuleOrder");
            entity.Property(e => e.PaneName).HasMaxLength(50);
            entity.Property(e => e.PublishBeginDate)
                .HasDefaultValueSql("(getdate())", "DF_mp_PageModules_PublishBeginDate")
                .HasColumnType("datetime");
            entity.Property(e => e.PublishEndDate).HasColumnType("datetime");

            entity.HasOne(d => d.Module).WithMany(p => p.MpPageModules)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_mp_PageModules_mp_Modules");

            entity.HasOne(d => d.Page).WithMany(p => p.MpPageModules)
                .HasForeignKey(d => d.PageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_mp_PageModules_mp_PageModules");
        });

        modelBuilder.Entity<MpPayPalLog>(entity =>
        {
            entity.HasKey(e => e.RowGuid);

            entity.ToTable("mp_PayPalLog");

            entity.Property(e => e.RowGuid).HasDefaultValueSql("(newid())", "DF_mp_PayPalLog_RowGuid");
            entity.Property(e => e.ApiVersion).HasMaxLength(50);
            entity.Property(e => e.CartTotal).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.CreatedUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_PayPalLog_CreatedUtc")
                .HasColumnType("datetime");
            entity.Property(e => e.CurrencyCode).HasMaxLength(50);
            entity.Property(e => e.ExchangeRate).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.FeeAmt).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.IpnproviderName)
                .HasMaxLength(255)
                .HasColumnName("IPNProviderName");
            entity.Property(e => e.PayPalAmt).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.PayerId).HasMaxLength(50);
            entity.Property(e => e.PaymentStatus).HasMaxLength(50);
            entity.Property(e => e.PaymentType).HasMaxLength(10);
            entity.Property(e => e.PdtproviderName)
                .HasMaxLength(255)
                .HasColumnName("PDTProviderName");
            entity.Property(e => e.PendingReason).HasMaxLength(255);
            entity.Property(e => e.ProviderName).HasMaxLength(255);
            entity.Property(e => e.ReasonCode).HasMaxLength(50);
            entity.Property(e => e.RequestType).HasMaxLength(255);
            entity.Property(e => e.Response).HasMaxLength(255);
            entity.Property(e => e.ReturnUrl).HasMaxLength(255);
            entity.Property(e => e.SettleAmt).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.TaxAmt).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.Token).HasMaxLength(50);
            entity.Property(e => e.TransactionId).HasMaxLength(50);
        });

        modelBuilder.Entity<MpPaymentLog>(entity =>
        {
            entity.HasKey(e => e.RowGuid);

            entity.ToTable("mp_PaymentLog");

            entity.Property(e => e.RowGuid).HasDefaultValueSql("(newid())", "DF_mp_PaymentLog_RowGuid");
            entity.Property(e => e.Amount).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.AuthCode).HasMaxLength(50);
            entity.Property(e => e.AvsCode).HasMaxLength(50);
            entity.Property(e => e.CavCode)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.CcvCode)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.CreatedUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_PaymentLog_CreatedUtc")
                .HasColumnType("datetime");
            entity.Property(e => e.Duty).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.Freight).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.Method).HasMaxLength(20);
            entity.Property(e => e.Provider).HasMaxLength(100);
            entity.Property(e => e.ResponseCode)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.ResponseReasonCode).HasMaxLength(20);
            entity.Property(e => e.Tax).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.TransactionId).HasMaxLength(50);
            entity.Property(e => e.TransactionType).HasMaxLength(50);
        });

        modelBuilder.Entity<MpPlugNpayLog>(entity =>
        {
            entity.HasKey(e => e.RowGuid);

            entity.ToTable("mp_PlugNPayLog");

            entity.Property(e => e.RowGuid).HasDefaultValueSql("(newid())", "DF_mp_PlugNPayLog_RowGuid");
            entity.Property(e => e.Amount).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.AuthCode).HasMaxLength(50);
            entity.Property(e => e.AvsCode).HasMaxLength(50);
            entity.Property(e => e.CavCode).HasMaxLength(10);
            entity.Property(e => e.CcvCode).HasMaxLength(10);
            entity.Property(e => e.CreatedUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_PlugNPayLog_CreatedUtc")
                .HasColumnType("datetime");
            entity.Property(e => e.Duty).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.Freight).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.Method).HasMaxLength(20);
            entity.Property(e => e.ResponseCode).HasMaxLength(10);
            entity.Property(e => e.ResponseReasonCode).HasMaxLength(20);
            entity.Property(e => e.Tax).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.TransactionId).HasMaxLength(50);
            entity.Property(e => e.TransactionType).HasMaxLength(50);
        });

        modelBuilder.Entity<MpPoll>(entity =>
        {
            entity.HasKey(e => e.PollGuid);

            entity.ToTable("mp_Polls");

            entity.Property(e => e.PollGuid).ValueGeneratedNever();
            entity.Property(e => e.Active).HasDefaultValue(true, "DF_mp_Poll_Active");
            entity.Property(e => e.ActiveFrom)
                .HasDefaultValueSql("(getdate())", "DF_mp_Polls_ActiveFrom")
                .HasColumnType("datetime");
            entity.Property(e => e.ActiveTo)
                .HasDefaultValueSql("(getdate())", "DF_mp_Polls_ActiveTo")
                .HasColumnType("datetime");
            entity.Property(e => e.Question).HasMaxLength(255);
        });

        modelBuilder.Entity<MpPollModule>(entity =>
        {
            entity.HasKey(e => new { e.PollGuid, e.ModuleId });

            entity.ToTable("mp_PollModules");

            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
        });

        modelBuilder.Entity<MpPollOption>(entity =>
        {
            entity.HasKey(e => e.OptionGuid);

            entity.ToTable("mp_PollOptions");

            entity.Property(e => e.OptionGuid).ValueGeneratedNever();
            entity.Property(e => e.Answer).HasMaxLength(255);
            entity.Property(e => e.Order).HasDefaultValue(1, "DF_mp_PollOptions_Order");
        });

        modelBuilder.Entity<MpPollUser>(entity =>
        {
            entity.HasKey(e => new { e.PollGuid, e.UserGuid });

            entity.ToTable("mp_PollUsers");
        });

        modelBuilder.Entity<MpRedirectList>(entity =>
        {
            entity.HasKey(e => e.RowGuid);

            entity.ToTable("mp_RedirectList");

            entity.HasIndex(e => e.OldUrl, "IX_mp_RedirectListOldUrl");

            entity.HasIndex(e => e.SiteGuid, "IX_mp_RedirectListSiteGuid");

            entity.HasIndex(e => e.SiteId, "IX_mp_RedirectListSiteID");

            entity.Property(e => e.RowGuid).HasDefaultValueSql("(newid())", "DF_mp_RedirectList_RowGuid");
            entity.Property(e => e.CreatedUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_RedirectList_CreatedUtc")
                .HasColumnType("datetime");
            entity.Property(e => e.ExpireUtc).HasColumnType("datetime");
            entity.Property(e => e.NewUrl).HasMaxLength(255);
            entity.Property(e => e.OldUrl).HasMaxLength(255);
            entity.Property(e => e.SiteId).HasColumnName("SiteID");
        });

        modelBuilder.Entity<MpRole>(entity =>
        {
            entity.HasKey(e => e.RoleId);

            entity.ToTable("mp_Roles");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.DisplayName).HasMaxLength(50);
            entity.Property(e => e.RoleName).HasMaxLength(50);
            entity.Property(e => e.SiteId).HasColumnName("SiteID");

            entity.HasOne(d => d.Site).WithMany(p => p.MpRoles)
                .HasForeignKey(d => d.SiteId)
                .HasConstraintName("FK_Roles_Portals");
        });

        modelBuilder.Entity<MpRssFeed>(entity =>
        {
            entity.HasKey(e => e.ItemId);

            entity.ToTable("mp_RssFeeds");

            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.Author).HasMaxLength(100);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())", "DF_mp_RssFeeds_CreatedDate")
                .HasColumnType("datetime");
            entity.Property(e => e.FeedType).HasMaxLength(20);
            entity.Property(e => e.ImageUrl).HasMaxLength(255);
            entity.Property(e => e.LastModUtc).HasColumnType("datetime");
            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
            entity.Property(e => e.RssUrl).HasMaxLength(255);
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<MpRssFeedEntry>(entity =>
        {
            entity.HasKey(e => e.RowGuid);

            entity.ToTable("mp_RssFeedEntries");

            entity.HasIndex(e => e.EntryHash, "idxEntryHash");

            entity.HasIndex(e => e.FeedId, "idxFeedId");

            entity.HasIndex(e => e.ModuleGuid, "idxModuleGuid");

            entity.Property(e => e.RowGuid).HasDefaultValueSql("(newid())", "DF_mp_RssFeedEntries_RowGuid");
            entity.Property(e => e.Author).HasMaxLength(100);
            entity.Property(e => e.BlogUrl).HasMaxLength(255);
            entity.Property(e => e.CachedTimeUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_RssFeedEntries_CachedTimeUtc")
                .HasColumnType("datetime");
            entity.Property(e => e.Link).HasMaxLength(255);
            entity.Property(e => e.PubDate).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<MpSavedQuery>(entity =>
        {
            entity.ToTable("mp_SavedQuery");

            entity.HasIndex(e => e.Name, "IX_mp_SavedQName");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedUtc).HasColumnType("datetime");
            entity.Property(e => e.LastModUtc).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<MpSchemaScriptHistory>(entity =>
        {
            entity.ToTable("mp_SchemaScriptHistory");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");
            entity.Property(e => e.RunTime)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_SchemaScriptHistory_RunCompletedTime")
                .HasColumnType("datetime");
            entity.Property(e => e.ScriptFile).HasMaxLength(255);

            entity.HasOne(d => d.Application).WithMany(p => p.MpSchemaScriptHistories)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_mp_SchemaScriptHistory_mp_SchemaVersion");
        });

        modelBuilder.Entity<MpSchemaVersion>(entity =>
        {
            entity.HasKey(e => e.ApplicationId);

            entity.ToTable("mp_SchemaVersion");

            entity.Property(e => e.ApplicationId)
                .ValueGeneratedNever()
                .HasColumnName("ApplicationID");
            entity.Property(e => e.ApplicationName).HasMaxLength(255);
        });

        modelBuilder.Entity<MpSharedFile>(entity =>
        {
            entity.HasKey(e => e.ItemId);

            entity.ToTable("mp_SharedFiles");

            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.FolderId).HasColumnName("FolderID");
            entity.Property(e => e.FriendlyName).HasMaxLength(255);
            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
            entity.Property(e => e.OriginalFileName).HasMaxLength(255);
            entity.Property(e => e.ServerFileName).HasMaxLength(255);
            entity.Property(e => e.SizeInKb).HasColumnName("SizeInKB");
            entity.Property(e => e.UploadDate)
                .HasDefaultValueSql("(getdate())", "DF_mp_SharedFiles_UploadDate")
                .HasColumnType("datetime");
            entity.Property(e => e.UploadUserId).HasColumnName("UploadUserID");
            entity.Property(e => e.ViewRoles).HasDefaultValue("All Users", "DF_mp_SharedFiles_ViewRoles");
        });

        modelBuilder.Entity<MpSharedFileFolder>(entity =>
        {
            entity.HasKey(e => e.FolderId);

            entity.ToTable("mp_SharedFileFolders");

            entity.Property(e => e.FolderId).HasColumnName("FolderID");
            entity.Property(e => e.FolderName).HasMaxLength(255);
            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
            entity.Property(e => e.ParentId).HasColumnName("ParentID");
            entity.Property(e => e.ViewRoles).HasDefaultValue("All Users", "DF_mp_SharedFileFolders_ViewRoles");
        });

        modelBuilder.Entity<MpSharedFilesHistory>(entity =>
        {
            entity.ToTable("mp_SharedFilesHistory");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ArchiveDate)
                .HasDefaultValueSql("(getdate())", "DF_mp_SharedFilesHistory_ArchiveDate")
                .HasColumnType("datetime");
            entity.Property(e => e.FriendlyName).HasMaxLength(255);
            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
            entity.Property(e => e.OriginalFileName).HasMaxLength(255);
            entity.Property(e => e.ServerFileName).HasMaxLength(50);
            entity.Property(e => e.SizeInKb).HasColumnName("SizeInKB");
            entity.Property(e => e.UploadDate).HasColumnType("datetime");
            entity.Property(e => e.UploadUserId).HasColumnName("UploadUserID");
            entity.Property(e => e.ViewRoles).HasDefaultValue("All Users", "DF_mp_SharedFilesHistory_ViewRoles");

            entity.HasOne(d => d.Item).WithMany(p => p.MpSharedFilesHistories)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_mp_SharedFilesHistory_mp_SharedFiles");
        });

        modelBuilder.Entity<MpSite>(entity =>
        {
            entity.HasKey(e => e.SiteId).HasName("PK_Portals");

            entity.ToTable("mp_Sites");

            entity.HasIndex(e => e.SiteGuid, "idxSitesGuid");

            entity.Property(e => e.SiteId).HasColumnName("SiteID");
            entity.Property(e => e.AllowHideMenuOnPages).HasDefaultValue(true, "DF_mp_Sites_AllowHideMenuOnPages");
            entity.Property(e => e.AllowNewRegistration).HasDefaultValue(true, "DF_mp_Sites_AllowNewRegistration");
            entity.Property(e => e.AllowOpenIdauth).HasColumnName("AllowOpenIDAuth");
            entity.Property(e => e.AllowPageSkins).HasDefaultValue(true, "DF_mp_Sites_AllowPageSkins");
            entity.Property(e => e.AllowPasswordReset).HasDefaultValue(true, "DF_mp_Sites_AllowPasswordReset");
            entity.Property(e => e.AllowPasswordRetrieval).HasDefaultValue(true, "DF_mp_Sites_AllowPasswordRetrieval");
            entity.Property(e => e.ApiKeyExtra1).HasMaxLength(255);
            entity.Property(e => e.ApiKeyExtra2).HasMaxLength(255);
            entity.Property(e => e.ApiKeyExtra3).HasMaxLength(255);
            entity.Property(e => e.ApiKeyExtra4).HasMaxLength(255);
            entity.Property(e => e.ApiKeyExtra5).HasMaxLength(255);
            entity.Property(e => e.AutoCreateLdapUserOnFirstLogin).HasDefaultValue(true, "DF_mp_Sites_AutoCreateLdapUserOnFirstLogin");
            entity.Property(e => e.CaptchaProvider).HasMaxLength(255);
            entity.Property(e => e.DatePickerProvider).HasMaxLength(255);
            entity.Property(e => e.DefaultAdditionalMetaTags).HasMaxLength(255);
            entity.Property(e => e.DefaultEmailFromAddress).HasMaxLength(100);
            entity.Property(e => e.DefaultFriendlyUrlPatternEnum)
                .HasMaxLength(50)
                .HasDefaultValue("PageNameWithDotASPX", "DF_mp_Sites_DefaultFriendlyUrlPatternEnum");
            entity.Property(e => e.DefaultPageDescription).HasMaxLength(255);
            entity.Property(e => e.DefaultPageEncoding).HasMaxLength(255);
            entity.Property(e => e.DefaultPageKeyWords).HasMaxLength(255);
            entity.Property(e => e.EditorProvider).HasMaxLength(255);
            entity.Property(e => e.EditorSkin)
                .HasMaxLength(50)
                .HasDefaultValue("normal", "DF_mp_Sites_EditorSkin");
            entity.Property(e => e.EnableMyPageFeature).HasDefaultValue(true, "DF_mp_Sites_EnableMyPageFeature");
            entity.Property(e => e.GmapApiKey).HasMaxLength(255);
            entity.Property(e => e.Icon).HasMaxLength(50);
            entity.Property(e => e.LdapDomain).HasMaxLength(255);
            entity.Property(e => e.LdapPort).HasDefaultValue(389, "DF_mp_Sites_LdapPort");
            entity.Property(e => e.LdapRootDn)
                .HasMaxLength(255)
                .HasColumnName("LdapRootDN");
            entity.Property(e => e.LdapServer).HasMaxLength(255);
            entity.Property(e => e.LdapUserDnkey)
                .HasMaxLength(10)
                .HasDefaultValue("uid", "DF_mp_Sites_LdapUserDNKey")
                .HasColumnName("LdapUserDNKey");
            entity.Property(e => e.Logo).HasMaxLength(50);
            entity.Property(e => e.MaxInvalidPasswordAttempts).HasDefaultValue(5, "DF_mp_Sites_MaxInvalidPasswordAttempts");
            entity.Property(e => e.MinRequiredPasswordLength).HasDefaultValue(4, "DF_mp_Sites_MinRequiredPasswordLength");
            entity.Property(e => e.PasswordAttemptWindowMinutes).HasDefaultValue(5, "DF_mp_Sites_PasswordAttemptWindowMinutes");
            entity.Property(e => e.ReallyDeleteUsers).HasDefaultValue(true, "DF_mp_Sites_ReallyDeleteUsers");
            entity.Property(e => e.RecaptchaPrivateKey).HasMaxLength(255);
            entity.Property(e => e.RecaptchaPublicKey).HasMaxLength(255);
            entity.Property(e => e.RequiresUniqueEmail).HasDefaultValue(true, "DF_mp_Sites_RequiresUniqueEmail");
            entity.Property(e => e.SiteAlias).HasMaxLength(50);
            entity.Property(e => e.SiteName).HasMaxLength(255);
            entity.Property(e => e.Skin).HasMaxLength(100);
            entity.Property(e => e.UseEmailForLogin).HasDefaultValue(true, "DF_mp_Sites_UseEmailForLogin");
            entity.Property(e => e.UseSslonAllPages).HasColumnName("UseSSLOnAllPages");
            entity.Property(e => e.WindowsLiveAppId)
                .HasMaxLength(255)
                .HasColumnName("WindowsLiveAppID");
            entity.Property(e => e.WindowsLiveKey).HasMaxLength(255);
            entity.Property(e => e.WordpressApikey)
                .HasMaxLength(255)
                .HasColumnName("WordpressAPIKey");
        });

        modelBuilder.Entity<MpSiteFolder>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("mp_SiteFolders");

            entity.HasIndex(e => e.FolderName, "IX_mp_SiteFolders");

            entity.Property(e => e.Guid).HasDefaultValueSql("(newid())", "DF_mp_SiteFolders_Guid");
            entity.Property(e => e.FolderName).HasMaxLength(255);
        });

        modelBuilder.Entity<MpSiteHost>(entity =>
        {
            entity.HasKey(e => e.HostId);

            entity.ToTable("mp_SiteHosts");

            entity.Property(e => e.HostId).HasColumnName("HostID");
            entity.Property(e => e.HostName).HasMaxLength(255);
            entity.Property(e => e.SiteId).HasColumnName("SiteID");
        });

        modelBuilder.Entity<MpSiteModuleDefinition>(entity =>
        {
            entity.HasKey(e => new { e.SiteId, e.ModuleDefId });

            entity.ToTable("mp_SiteModuleDefinitions");

            entity.Property(e => e.SiteId).HasColumnName("SiteID");
            entity.Property(e => e.ModuleDefId).HasColumnName("ModuleDefID");
        });

        modelBuilder.Entity<MpSiteSettingsEx>(entity =>
        {
            entity.HasKey(e => new { e.SiteId, e.KeyName });

            entity.ToTable("mp_SiteSettingsEx");

            entity.Property(e => e.SiteId).HasColumnName("SiteID");
            entity.Property(e => e.KeyName).HasMaxLength(128);
            entity.Property(e => e.GroupName).HasMaxLength(128);
        });

        modelBuilder.Entity<MpSiteSettingsExDef>(entity =>
        {
            entity.HasKey(e => e.KeyName);

            entity.ToTable("mp_SiteSettingsExDef");

            entity.Property(e => e.KeyName).HasMaxLength(128);
            entity.Property(e => e.GroupName).HasMaxLength(128);
        });

        modelBuilder.Entity<MpSurvey>(entity =>
        {
            entity.HasKey(e => e.SurveyGuid);

            entity.ToTable("mp_Surveys");

            entity.Property(e => e.SurveyGuid).ValueGeneratedNever();
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.SubmissionLimit).HasDefaultValue(1, "DF_mp_Surveys_SubmissionLimit");
            entity.Property(e => e.SurveyName).HasMaxLength(255);
        });

        modelBuilder.Entity<MpSurveyModule>(entity =>
        {
            entity.HasKey(e => new { e.SurveyGuid, e.ModuleId });

            entity.ToTable("mp_SurveyModules");

            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");
        });

        modelBuilder.Entity<MpSurveyPage>(entity =>
        {
            entity.HasKey(e => e.PageGuid);

            entity.ToTable("mp_SurveyPages");

            entity.Property(e => e.PageGuid).ValueGeneratedNever();
            entity.Property(e => e.PageTitle).HasMaxLength(255);
        });

        modelBuilder.Entity<MpSurveyQuestion>(entity =>
        {
            entity.HasKey(e => e.QuestionGuid);

            entity.ToTable("mp_SurveyQuestions");

            entity.Property(e => e.QuestionGuid).ValueGeneratedNever();
            entity.Property(e => e.QuestionName).HasMaxLength(255);
            entity.Property(e => e.ValidationMessage).HasMaxLength(255);
        });

        modelBuilder.Entity<MpSurveyQuestionAnswer>(entity =>
        {
            entity.HasKey(e => e.AnswerGuid).HasName("PK_mp_SurveyQuestionAnswers_1");

            entity.ToTable("mp_SurveyQuestionAnswers");

            entity.Property(e => e.AnswerGuid).ValueGeneratedNever();
            entity.Property(e => e.AnsweredDate)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_SurveyQuestionAnswers_AnsweredDate")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<MpSurveyQuestionOption>(entity =>
        {
            entity.HasKey(e => e.QuestionOptionGuid);

            entity.ToTable("mp_SurveyQuestionOptions");

            entity.Property(e => e.QuestionOptionGuid).ValueGeneratedNever();
            entity.Property(e => e.Answer).HasMaxLength(255);
        });

        modelBuilder.Entity<MpSurveyResponse>(entity =>
        {
            entity.HasKey(e => e.ResponseGuid);

            entity.ToTable("mp_SurveyResponses");

            entity.Property(e => e.ResponseGuid).ValueGeneratedNever();
            entity.Property(e => e.SubmissionDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<MpSystemLog>(entity =>
        {
            entity.ToTable("mp_SystemLog");

            entity.HasIndex(e => e.LogDate, "IX_mp_SystemLog");

            entity.HasIndex(e => e.LogLevel, "IX_mp_SystemLog_1");

            entity.HasIndex(e => e.ShortUrl, "IX_mp_SystemLog_2");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Culture).HasMaxLength(10);
            entity.Property(e => e.IpAddress).HasMaxLength(50);
            entity.Property(e => e.LogDate)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_SystemLog_LogDate")
                .HasColumnType("datetime");
            entity.Property(e => e.LogLevel).HasMaxLength(20);
            entity.Property(e => e.Logger).HasMaxLength(255);
            entity.Property(e => e.ShortUrl).HasMaxLength(255);
            entity.Property(e => e.Thread).HasMaxLength(255);
        });

        modelBuilder.Entity<MpTag>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("mp_Tag");

            entity.HasIndex(e => e.Tag, "IX_mp_Tag");

            entity.HasIndex(e => e.ModuleGuid, "IX_mp_Tag_1");

            entity.HasIndex(e => e.FeatureGuid, "IX_mp_Tag_2");

            entity.HasIndex(e => e.SiteGuid, "IX_mp_Tag_3");

            entity.HasIndex(e => e.VocabularyGuid, "IX_mp_Tag_VocG");

            entity.Property(e => e.Guid).HasDefaultValueSql("(newid())", "DF_mp_Tag_Guid");
            entity.Property(e => e.CreatedUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_Tag_CreatedUtc")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedUtc).HasColumnType("datetime");
            entity.Property(e => e.Tag).HasMaxLength(255);
        });

        modelBuilder.Entity<MpTagItem>(entity =>
        {
            entity.HasKey(e => e.TagItemGuid);

            entity.ToTable("mp_TagItem");

            entity.HasIndex(e => e.ModuleGuid, "IX_mp_TagItem");

            entity.HasIndex(e => e.TagGuid, "IX_mp_TagItem_1");

            entity.HasIndex(e => e.SiteGuid, "IX_mp_TagItem_2");

            entity.HasIndex(e => e.FeatureGuid, "IX_mp_TagItem_3");

            entity.HasIndex(e => e.RelatedItemGuid, "IX_mp_TagItem_4");

            entity.HasIndex(e => e.TaggedBy, "IX_mp_TagItem_5");

            entity.Property(e => e.TagItemGuid).HasDefaultValueSql("(newid())", "DF_mp_TagItem_Guid");

            entity.HasOne(d => d.Tag).WithMany(p => p.MpTagItems)
                .HasForeignKey(d => d.TagGuid)
                .HasConstraintName("FK_mp_TagItem_mp_Tag");
        });

        modelBuilder.Entity<MpTagVocabulary>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PK_mp_TagVocab");

            entity.ToTable("mp_TagVocabulary");

            entity.HasIndex(e => e.Name, "IX_mp_TagVocName");

            entity.HasIndex(e => e.ModuleGuid, "IX_mp_TagVoc_1");

            entity.HasIndex(e => e.FeatureGuid, "IX_mp_TagVoc_2");

            entity.HasIndex(e => e.SiteGuid, "IX_mp_TagVoc_3");

            entity.Property(e => e.Guid).HasDefaultValueSql("(newid())", "DF_mp_TagVoc_Guid");
            entity.Property(e => e.CreatedUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_TagVoc_CreatedUtc")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedUtc).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<MpTaskQueue>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("mp_TaskQueue");

            entity.Property(e => e.Guid).HasDefaultValueSql("(newid())", "DF_mp_TaskQueue_Guid");
            entity.Property(e => e.CompleteUtc)
                .HasColumnType("datetime")
                .HasColumnName("CompleteUTC");
            entity.Property(e => e.LastStatusUpdateUtc)
                .HasColumnType("datetime")
                .HasColumnName("LastStatusUpdateUTC");
            entity.Property(e => e.NotificationFromEmail).HasMaxLength(255);
            entity.Property(e => e.NotificationSentUtc)
                .HasColumnType("datetime")
                .HasColumnName("NotificationSentUTC");
            entity.Property(e => e.NotificationSubject).HasMaxLength(255);
            entity.Property(e => e.NotificationToEmail).HasMaxLength(255);
            entity.Property(e => e.QueuedUtc)
                .HasColumnType("datetime")
                .HasColumnName("QueuedUTC");
            entity.Property(e => e.SerializedTaskType).HasMaxLength(255);
            entity.Property(e => e.StartUtc)
                .HasColumnType("datetime")
                .HasColumnName("StartUTC");
            entity.Property(e => e.Status).HasMaxLength(255);
            entity.Property(e => e.TaskName).HasMaxLength(255);
            entity.Property(e => e.UpdateFrequency).HasDefaultValue(5, "DF_mp_TaskQueue_UpdateFrequency");
        });

        modelBuilder.Entity<MpTaxClass>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("mp_TaxClass");

            entity.Property(e => e.Guid).HasDefaultValueSql("(newid())", "DF_mp_TaxClass_Guid");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_TaxClass_Created")
                .HasColumnType("datetime");
            entity.Property(e => e.LastModified).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<MpTaxRate>(entity =>
        {
            entity.HasKey(e => e.Guid);

            entity.ToTable("mp_TaxRate");

            entity.Property(e => e.Guid).HasDefaultValueSql("(newid())", "DF_mp_TaxRate_Guid");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_TaxRate_Created")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.LastModified).HasColumnType("datetime");
            entity.Property(e => e.Priority).HasDefaultValue(1, "DF_mp_TaxRate_Priority");
            entity.Property(e => e.Rate).HasColumnType("decimal(18, 4)");
        });

        modelBuilder.Entity<MpTaxRateHistory>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PK_mp_TaxRateHx");

            entity.ToTable("mp_TaxRateHistory");

            entity.Property(e => e.Guid).HasDefaultValueSql("(newid())", "DF_mp_TaxRateHistory_Guid");
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.LastModified).HasColumnType("datetime");
            entity.Property(e => e.LogTime)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_TaxRateHistory_LogTime")
                .HasColumnType("datetime");
            entity.Property(e => e.Rate).HasColumnType("decimal(18, 4)");
        });

        modelBuilder.Entity<MpUser>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("mp_Users");

            entity.HasIndex(e => e.UserGuid, "IX_mp_Users");

            entity.HasIndex(e => e.OpenIduri, "IX_mp_Users_1");

            entity.HasIndex(e => e.WindowsLiveId, "IX_mp_Users_2");

            entity.HasIndex(e => e.RegisterConfirmGuid, "IX_mp_usersreguid");

            entity.HasIndex(e => e.SiteId, "IX_mp_userssiteid");

            entity.HasIndex(e => e.UserGuid, "idxUserUGuid");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Aim)
                .HasMaxLength(50)
                .HasColumnName("AIM");
            entity.Property(e => e.ApprovedForForums).HasDefaultValue(true, "DF_Users_Approved");
            entity.Property(e => e.AvatarUrl)
                .HasMaxLength(255)
                .HasDefaultValue("blank.gif", "DF_mp_Users_AvatarUrl");
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())", "DF_Users_DateCreated")
                .HasColumnType("datetime");
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.DisplayInMemberList).HasDefaultValue(true, "DF_mp_Users_DisplayInMemberList");
            entity.Property(e => e.EditorPreference).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.EmailConfirmed).HasDefaultValue(true);
            entity.Property(e => e.FailedPwdAnswerWindowStart).HasColumnType("datetime");
            entity.Property(e => e.FailedPwdAttemptWindowStart).HasColumnType("datetime");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Icq)
                .HasMaxLength(50)
                .HasColumnName("ICQ");
            entity.Property(e => e.Interests).HasMaxLength(100);
            entity.Property(e => e.LastActivityDate).HasColumnType("datetime");
            entity.Property(e => e.LastLockoutDate).HasColumnType("datetime");
            entity.Property(e => e.LastLoginDate).HasColumnType("datetime");
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.LastPasswordChangedDate).HasColumnType("datetime");
            entity.Property(e => e.LockoutEndDateUtc).HasColumnType("datetime");
            entity.Property(e => e.LoginName).HasMaxLength(50);
            entity.Property(e => e.LoweredEmail).HasMaxLength(100);
            entity.Property(e => e.MobilePin)
                .HasMaxLength(16)
                .HasColumnName("MobilePIN");
            entity.Property(e => e.Msn)
                .HasMaxLength(50)
                .HasColumnName("MSN");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.NewEmail).HasMaxLength(100);
            entity.Property(e => e.Occupation).HasMaxLength(100);
            entity.Property(e => e.OpenIduri)
                .HasMaxLength(255)
                .HasColumnName("OpenIDURI");
            entity.Property(e => e.PasswordAnswer).HasMaxLength(255);
            entity.Property(e => e.PasswordQuestion).HasMaxLength(255);
            entity.Property(e => e.PasswordSalt).HasMaxLength(128);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            entity.Property(e => e.ProfileApproved).HasDefaultValue(true, "DF_Users_ProfileApproved");
            entity.Property(e => e.Pwd).HasMaxLength(1000);
            entity.Property(e => e.SiteId).HasColumnName("SiteID");
            entity.Property(e => e.Skin).HasMaxLength(100);
            entity.Property(e => e.State).HasMaxLength(100);
            entity.Property(e => e.TimeZoneId).HasMaxLength(32);
            entity.Property(e => e.TotalRevenue).HasColumnType("decimal(15, 4)");
            entity.Property(e => e.UserGuid).HasDefaultValueSql("(newid())", "DF_mp_Users_UserGuid");
            entity.Property(e => e.WebSiteUrl)
                .HasMaxLength(100)
                .HasColumnName("WebSiteURL");
            entity.Property(e => e.WindowsLiveId)
                .HasMaxLength(36)
                .HasColumnName("WindowsLiveID");
            entity.Property(e => e.Yahoo).HasMaxLength(50);
        });

        modelBuilder.Entity<MpUserClaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.AspNetUserClaims");

            entity.ToTable("mp_UserClaims");

            entity.Property(e => e.UserId).HasMaxLength(128);
        });

        modelBuilder.Entity<MpUserLocation>(entity =>
        {
            entity.HasKey(e => e.RowId);

            entity.ToTable("mp_UserLocation");

            entity.HasIndex(e => e.Ipaddress, "idxULocateIP");

            entity.HasIndex(e => e.UserGuid, "idxULocateU");

            entity.Property(e => e.RowId)
                .HasDefaultValueSql("(newid())", "DF_mp_UserLocation_RowID")
                .HasColumnName("RowID");
            entity.Property(e => e.CaptureCount).HasDefaultValue(1, "DF_mp_UserLocation_CaptureCount");
            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.Continent).HasMaxLength(255);
            entity.Property(e => e.Country).HasMaxLength(255);
            entity.Property(e => e.FirstCaptureUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_UserLocation_FirstCaptureUTC")
                .HasColumnType("datetime")
                .HasColumnName("FirstCaptureUTC");
            entity.Property(e => e.Hostname).HasMaxLength(255);
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(50)
                .HasColumnName("IPAddress");
            entity.Property(e => e.IpaddressLong).HasColumnName("IPAddressLong");
            entity.Property(e => e.Isp)
                .HasMaxLength(255)
                .HasColumnName("ISP");
            entity.Property(e => e.LastCaptureUtc)
                .HasDefaultValueSql("(getutcdate())", "DF_mp_UserLocation_LastCaptureUTC")
                .HasColumnType("datetime")
                .HasColumnName("LastCaptureUTC");
            entity.Property(e => e.Region).HasMaxLength(255);
            entity.Property(e => e.TimeZone).HasMaxLength(255);
        });

        modelBuilder.Entity<MpUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId }).HasName("PK_dbo.AspNetUserLogins");

            entity.ToTable("mp_UserLogins");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);
            entity.Property(e => e.UserId).HasMaxLength(128);
        });

        modelBuilder.Entity<MpUserPage>(entity =>
        {
            entity.HasKey(e => e.UserPageId);

            entity.ToTable("mp_UserPages");

            entity.Property(e => e.UserPageId)
                .ValueGeneratedNever()
                .HasColumnName("UserPageID");
            entity.Property(e => e.PageName).HasMaxLength(255);
            entity.Property(e => e.PageOrder).HasDefaultValue(3, "DF_mp_UserPages_PageOrder");
            entity.Property(e => e.PagePath).HasMaxLength(255);
            entity.Property(e => e.SiteId).HasColumnName("SiteID");
        });

        modelBuilder.Entity<MpUserProperty>(entity =>
        {
            entity.HasKey(e => e.PropertyId);

            entity.ToTable("mp_UserProperties");

            entity.Property(e => e.PropertyId)
                .HasDefaultValueSql("(newid())", "DF_mp_UserProperties_PropertyID")
                .HasColumnName("PropertyID");
            entity.Property(e => e.LastUpdatedDate)
                .HasDefaultValueSql("(getdate())", "DF_mp_UserProperties_LastUpdatedDate")
                .HasColumnType("datetime");
            entity.Property(e => e.PropertyName).HasMaxLength(255);
        });

        modelBuilder.Entity<MpUserRole>(entity =>
        {
            entity.ToTable("mp_UserRoles");

            entity.HasIndex(e => e.RoleId, "IX_UserRolesRoleID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
