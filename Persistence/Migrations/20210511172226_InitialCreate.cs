using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence;
using System.Data;
using System;

namespace API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (!Exists("dbo", "Todos"))
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

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }

        public bool Exists(string schema, string tableName)
        {
            var dbHost = Environment.GetEnvironmentVariable("DBHOST");
            var dbUser = Environment.GetEnvironmentVariable("DBUSER");
            var dbPass = Environment.GetEnvironmentVariable("DBPASS");
            var dbName = "tododb";
            var connStr = $"Server="+dbHost+"; Database="+dbName+"; User ID="+dbUser+"; Password="+dbPass+";";

            var context = new DataContext(GetOptions(connStr));
            var connection = context.Database.GetDbConnection();

            if (connection.State.Equals(ConnectionState.Closed))
                connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                    SELECT 1 FROM INFORMATION_SCHEMA.TABLES 
                    WHERE TABLE_SCHEMA = @Schema
                    AND TABLE_NAME = @TableName";

                var schemaParam = command.CreateParameter();
                schemaParam.ParameterName = "@Schema";
                schemaParam.Value = schema;
                command.Parameters.Add(schemaParam);

                var tableNameParam = command.CreateParameter();
                tableNameParam.ParameterName = "@TableName";
                tableNameParam.Value = tableName;
                command.Parameters.Add(tableNameParam);

                return command.ExecuteScalar() != null;
            }
        }
    }
}
