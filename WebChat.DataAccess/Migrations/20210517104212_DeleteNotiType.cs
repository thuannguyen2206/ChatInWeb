using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebChat.DataAccess.Migrations
{
    public partial class DeleteNotiType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_NotificationTypes_NotificationTypeId",
                table: "Notifications");

            migrationBuilder.DropTable(
                name: "NotificationTypes");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_NotificationTypeId",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "NotificationTypeId",
                table: "Notifications",
                newName: "TypeNotification");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 17, 17, 42, 11, 715, DateTimeKind.Local).AddTicks(9709),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 4, 25, 16, 10, 22, 439, DateTimeKind.Local).AddTicks(6251));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "UserConnections",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 5, 17, 17, 42, 11, 713, DateTimeKind.Local).AddTicks(4585),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 4, 25, 16, 10, 22, 435, DateTimeKind.Local).AddTicks(5329));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "NotificationUsers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 5, 17, 17, 42, 11, 708, DateTimeKind.Local).AddTicks(306),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 4, 25, 16, 10, 22, 427, DateTimeKind.Local).AddTicks(9005));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ChatLogs",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 5, 17, 17, 42, 11, 704, DateTimeKind.Local).AddTicks(1915),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 4, 25, 16, 10, 22, 422, DateTimeKind.Local).AddTicks(8013));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ChatGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 5, 17, 17, 42, 11, 699, DateTimeKind.Local).AddTicks(9484),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 4, 25, 16, 10, 22, 417, DateTimeKind.Local).AddTicks(3675));

            migrationBuilder.UpdateData(
                table: "CallTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 17, 17, 42, 11, 768, DateTimeKind.Local).AddTicks(7865));

            migrationBuilder.UpdateData(
                table: "CallTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2021, 5, 17, 17, 42, 11, 768, DateTimeKind.Local).AddTicks(9957));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("24426be2-1dd4-4f73-8e55-0a263982f901"),
                column: "ConcurrencyStamp",
                value: "52299c04-2679-4d29-af65-6764364489da");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("443d58a9-0d4d-4e60-b94e-64a496dacc65"),
                column: "ConcurrencyStamp",
                value: "0b7db0ac-5cd0-48da-97af-9b096220cacb");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("87e68219-5095-4363-89a4-704d87f7f71e"),
                columns: new[] { "ConcurrencyStamp", "DateCreate", "PasswordHash" },
                values: new object[] { "e0f78c97-f62d-4703-9fd5-ca12a2156d86", new DateTime(2021, 5, 17, 17, 42, 11, 900, DateTimeKind.Local).AddTicks(4437), "AQAAAAEAACcQAAAAEG5ECHifp5oorQ74njpTqTZlX48tlYCYFC1C1RUOrEftOs47WI6uz1eVXjqD4jYjnw==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e1b62ba2-ea51-4c27-a9f8-7e65901f395a"),
                columns: new[] { "ConcurrencyStamp", "DateCreate", "PasswordHash" },
                values: new object[] { "110df08d-4025-4ad8-bd97-d4d01410020b", new DateTime(2021, 5, 17, 17, 42, 11, 907, DateTimeKind.Local).AddTicks(5923), "AQAAAAEAACcQAAAAEKS5ExabDwr0P1Bf+/AuhhwnaVBnwq/T/YdoFggDLCaHew+O/XgAoHU0HMMRqa+TQA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TypeNotification",
                table: "Notifications",
                newName: "NotificationTypeId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 4, 25, 16, 10, 22, 439, DateTimeKind.Local).AddTicks(6251),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 17, 17, 42, 11, 715, DateTimeKind.Local).AddTicks(9709));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "UserConnections",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 4, 25, 16, 10, 22, 435, DateTimeKind.Local).AddTicks(5329),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 5, 17, 17, 42, 11, 713, DateTimeKind.Local).AddTicks(4585));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "NotificationUsers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 4, 25, 16, 10, 22, 427, DateTimeKind.Local).AddTicks(9005),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 5, 17, 17, 42, 11, 708, DateTimeKind.Local).AddTicks(306));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ChatLogs",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 4, 25, 16, 10, 22, 422, DateTimeKind.Local).AddTicks(8013),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 5, 17, 17, 42, 11, 704, DateTimeKind.Local).AddTicks(1915));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ChatGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 4, 25, 16, 10, 22, 417, DateTimeKind.Local).AddTicks(3675),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 5, 17, 17, 42, 11, 699, DateTimeKind.Local).AddTicks(9484));

            migrationBuilder.CreateTable(
                name: "NotificationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 4, 25, 16, 10, 22, 448, DateTimeKind.Local).AddTicks(5199)),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTypes", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "CallTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2021, 4, 25, 16, 10, 22, 512, DateTimeKind.Local).AddTicks(4666));

            migrationBuilder.UpdateData(
                table: "CallTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2021, 4, 25, 16, 10, 22, 512, DateTimeKind.Local).AddTicks(6809));

            migrationBuilder.InsertData(
                table: "NotificationTypes",
                columns: new[] { "Id", "CreateAt", "Description" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 4, 25, 16, 10, 22, 548, DateTimeKind.Local).AddTicks(2220), "System" },
                    { 2, new DateTime(2021, 4, 25, 16, 10, 22, 548, DateTimeKind.Local).AddTicks(3263), "Chat" },
                    { 3, new DateTime(2021, 4, 25, 16, 10, 22, 548, DateTimeKind.Local).AddTicks(3285), "New contact" }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("24426be2-1dd4-4f73-8e55-0a263982f901"),
                column: "ConcurrencyStamp",
                value: "62a0bf0e-95d3-4de9-98f7-4a6d77747bac");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("443d58a9-0d4d-4e60-b94e-64a496dacc65"),
                column: "ConcurrencyStamp",
                value: "62fc28dc-0c3d-495d-bb31-d7ec916bb762");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("87e68219-5095-4363-89a4-704d87f7f71e"),
                columns: new[] { "ConcurrencyStamp", "DateCreate", "PasswordHash" },
                values: new object[] { "447743d4-e17c-4d53-a62b-b5887f3d1354", new DateTime(2021, 4, 25, 16, 10, 22, 538, DateTimeKind.Local).AddTicks(1581), "AQAAAAEAACcQAAAAEEOJmkTos2JQk7mRO8c0NRDKCbnAeL/+Fze7MqOVLR+Oy3gEyHyvjXYEhwFTKGlGSQ==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e1b62ba2-ea51-4c27-a9f8-7e65901f395a"),
                columns: new[] { "ConcurrencyStamp", "DateCreate", "PasswordHash" },
                values: new object[] { "e9f34e24-7396-4213-aaef-66117d1ca050", new DateTime(2021, 4, 25, 16, 10, 22, 547, DateTimeKind.Local).AddTicks(8235), "AQAAAAEAACcQAAAAEAfr6UcqJCDM/UfAxVfWpH4ZmUDczfjg3VoTqSbYlvnh/KaqVJxdw7l6xiRYvlkTyQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationTypeId",
                table: "Notifications",
                column: "NotificationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_NotificationTypes_NotificationTypeId",
                table: "Notifications",
                column: "NotificationTypeId",
                principalTable: "NotificationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
