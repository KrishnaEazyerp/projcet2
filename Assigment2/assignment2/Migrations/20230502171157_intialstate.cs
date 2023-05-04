using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace assignment2.Migrations
{
    public partial class intialstate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employeee",
                columns: table => new
                {
                    EmpId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ContactNo = table.Column<int>(type: "int", nullable: false),
                    HomeAddress = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    WorkLocation = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "('true')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Employee__AF2DBB9959CDF39F", x => x.EmpId);
                });

            migrationBuilder.CreateTable(
                name: "location",
                columns: table => new
                {
                    locationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    buildingId = table.Column<int>(type: "int", nullable: false),
                    address = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    zipCode = table.Column<int>(type: "int", nullable: false),
                    managerName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_location", x => x.locationId);
                });

            migrationBuilder.CreateTable(
                name: "Hr",
                columns: table => new
                {
                    HrId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpPayrollInfo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    SocialSecurityNo = table.Column<int>(type: "int", nullable: true),
                    Salary = table.Column<int>(type: "int", nullable: true),
                    EmpId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hr", x => x.HrId);
                    table.ForeignKey(
                        name: "FK__Hr__EmpId__681373AD",
                        column: x => x.EmpId,
                        principalTable: "Employeee",
                        principalColumn: "EmpId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hr_EmpId",
                table: "Hr",
                column: "EmpId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hr");

            migrationBuilder.DropTable(
                name: "location");

            migrationBuilder.DropTable(
                name: "Employeee");
        }
    }
}
