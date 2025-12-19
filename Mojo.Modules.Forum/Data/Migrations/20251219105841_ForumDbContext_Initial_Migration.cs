using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mojo.Modules.Forum.Data.Migrations
{
    /// <inheritdoc />
    public partial class ForumDbContext_Initial_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ForumPostReplyLinks",
                columns: table => new
                {
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumPostReplyLinks", x => x.PostId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ForumPostReplyLinks_ParentPostId",
                table: "ForumPostReplyLinks",
                column: "ParentPostId");

            migrationBuilder.CreateIndex(
                name: "IX_mp_ForumThreads_ForumID",
                table: "mp_ForumThreads",
                column: "ForumID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ForumPostReplyLinks");
            
            migrationBuilder.DropIndex(
                name: "IX_mp_ForumThreads_ForumID",
                table: "mp_ForumThreads");
        }
    }
}
