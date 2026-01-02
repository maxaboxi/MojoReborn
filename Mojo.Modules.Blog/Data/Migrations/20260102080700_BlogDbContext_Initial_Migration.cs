using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mojo.Modules.Blog.Data.Migrations
{
    /// <inheritdoc />
    public partial class BlogDbContext_Initial_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogSubscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false),
                    SiteGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModuleId = table.Column<int>(type: "int", nullable: false),
                    ModuleGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogSubscriptions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mp_Blogs_ItemUrl",
                table: "mp_Blogs",
                column: "ItemUrl",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_mp_Blogs_ItemUrl",
                table: "mp_Blogs");
        }
    }
}
