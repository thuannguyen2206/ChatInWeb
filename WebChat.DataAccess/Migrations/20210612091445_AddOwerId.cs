using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebChat.DataAccess.Migrations
{
    public partial class AddOwerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CallUsers_CallTypes_CallTypeId",
                table: "CallUsers");

            migrationBuilder.DropTable(
                name: "CallTypes");

            migrationBuilder.DropIndex(
                name: "IX_CallUsers_CallTypeId",
                table: "CallUsers");

            migrationBuilder.RenameColumn(
                name: "CallTypeId",
                table: "CallUsers",
                newName: "CallType");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 6, 12, 16, 14, 44, 159, DateTimeKind.Local).AddTicks(4847),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 17, 17, 42, 11, 715, DateTimeKind.Local).AddTicks(9709));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "UserConnections",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 6, 12, 16, 14, 44, 156, DateTimeKind.Local).AddTicks(8108),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 5, 17, 17, 42, 11, 713, DateTimeKind.Local).AddTicks(4585));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "NotificationUsers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 6, 12, 16, 14, 44, 151, DateTimeKind.Local).AddTicks(5608),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 5, 17, 17, 42, 11, 708, DateTimeKind.Local).AddTicks(306));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ChatLogs",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 6, 12, 16, 14, 44, 147, DateTimeKind.Local).AddTicks(1610),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 5, 17, 17, 42, 11, 704, DateTimeKind.Local).AddTicks(1915));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ChatGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 6, 12, 16, 14, 44, 133, DateTimeKind.Local).AddTicks(929),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 5, 17, 17, 42, 11, 699, DateTimeKind.Local).AddTicks(9484));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "ChatGroups",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("24426be2-1dd4-4f73-8e55-0a263982f901"),
                column: "ConcurrencyStamp",
                value: "de55a335-d0f6-4d21-a183-d70b9e11ed0c");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("443d58a9-0d4d-4e60-b94e-64a496dacc65"),
                column: "ConcurrencyStamp",
                value: "4a093380-5a36-4eae-a0b3-d01729fef6b9");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("87e68219-5095-4363-89a4-704d87f7f71e"),
                columns: new[] { "ConcurrencyStamp", "DateCreate", "PasswordHash" },
                values: new object[] { "fb96921b-e048-4094-843e-9ed27b631220", new DateTime(2021, 6, 12, 16, 14, 44, 375, DateTimeKind.Local).AddTicks(2769), "AQAAAAEAACcQAAAAEMzSN2A3lG5N36rCEWuBZsG/ZEpXiwjWDXhO2cdbDG+FCLfT5KKtn2ms9x0RNxpLiA==" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e1b62ba2-ea51-4c27-a9f8-7e65901f395a"),
                columns: new[] { "ConcurrencyStamp", "DateCreate", "PasswordHash" },
                values: new object[] { "4b48d779-464e-4f0e-87a6-25eb23f6645b", new DateTime(2021, 6, 12, 16, 14, 44, 385, DateTimeKind.Local).AddTicks(1058), "AQAAAAEAACcQAAAAEHOpIE19IEOH46YWanGEwhsxpJjQIDp1p0kv2FxPAaOVaTCT6nwmmmcMNy01MDsjLw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "ChatGroups");

            migrationBuilder.RenameColumn(
                name: "CallType",
                table: "CallUsers",
                newName: "CallTypeId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 17, 17, 42, 11, 715, DateTimeKind.Local).AddTicks(9709),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 6, 12, 16, 14, 44, 159, DateTimeKind.Local).AddTicks(4847));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "UserConnections",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 5, 17, 17, 42, 11, 713, DateTimeKind.Local).AddTicks(4585),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 6, 12, 16, 14, 44, 156, DateTimeKind.Local).AddTicks(8108));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "NotificationUsers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 5, 17, 17, 42, 11, 708, DateTimeKind.Local).AddTicks(306),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 6, 12, 16, 14, 44, 151, DateTimeKind.Local).AddTicks(5608));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ChatLogs",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 5, 17, 17, 42, 11, 704, DateTimeKind.Local).AddTicks(1915),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 6, 12, 16, 14, 44, 147, DateTimeKind.Local).AddTicks(1610));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ChatGroups",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2021, 5, 17, 17, 42, 11, 699, DateTimeKind.Local).AddTicks(9484),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2021, 6, 12, 16, 14, 44, 133, DateTimeKind.Local).AddTicks(929));

            migrationBuilder.CreateTable(
                name: "CallTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    LastModifyAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CallTypes",
                columns: new[] { "Id", "CreatedAt", "Description", "IsActive", "LastModifyAt", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 5, 17, 17, 42, 11, 768, DateTimeKind.Local).AddTicks(7865), "Gọi điện thoại", true, null, "Voice Call" },
                    { 2, new DateTime(2021, 5, 17, 17, 42, 11, 768, DateTimeKind.Local).AddTicks(9957), "Gọi video", true, null, "Video Call" }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_CallUsers_CallTypeId",
                table: "CallUsers",
                column: "CallTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CallUsers_CallTypes_CallTypeId",
                table: "CallUsers",
                column: "CallTypeId",
                principalTable: "CallTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
