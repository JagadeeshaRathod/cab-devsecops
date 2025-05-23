using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CAB.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_CabData",
                columns: table => new
                {
                    Cab_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArmyNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Complaint_Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Complaint_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Regt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Case_Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dt_Of_Complaint = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Recd_Date_Cab = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InfoDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_CabData", x => x.Cab_Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ImportDtls",
                columns: table => new
                {
                    Import_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uploaded_filename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Uploaded_dt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Uploaded_By = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ImportDtls", x => x.Import_Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_CabData");

            migrationBuilder.DropTable(
                name: "tbl_ImportDtls");
        }
    }
}
