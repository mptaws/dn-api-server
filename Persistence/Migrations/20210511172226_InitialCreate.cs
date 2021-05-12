using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (!Exists("dbo.Todos"))
            {
                migrationBuilder.CreateTable(
                    name: "Todos",
                    columns: table => new
                    {
                        Id = table.Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                        completed = table.Column<bool>(type: "bit", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Todos", x => x.Id);
                    });
            }

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Todos");
        }

        private static bool Exists(string tableName)
        {
            using (var context = new DbContext(config.GetValue<string>("CONNSTR"))
            {
                var count = context.Database.SqlQuery<int>("SELECT COUNT(OBJECT_ID(@p0, 'U'))", tableName);

                return count.Any() && count.First() > 0;
            }
        }
    }
}
