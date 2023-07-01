using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KantorServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialDatabaseData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Currencies(Name,Symbol) values('EURO','EUR')");
            migrationBuilder.Sql("INSERT INTO Currencies(Name,Symbol) values('US DOLAR','USD')");
            migrationBuilder.Sql("INSERT INTO Currencies(Name,Symbol) values('DOLAR KANADYJSKI','CAD')");
            migrationBuilder.Sql("INSERT INTO Currencies(Name,Symbol) values('FRANK SZWACJARSKI','CHF')");
            migrationBuilder.Sql("INSERT INTO Currencies(Name,Symbol) values('KORONA CZESKA','CZK')");
            migrationBuilder.Sql("INSERT INTO Currencies(Name,Symbol) values('FUNT BRYTYJSKI','GBP')");
            migrationBuilder.Sql("INSERT INTO Currencies(Name,Symbol) values('HRYWNA','UAH')");
            migrationBuilder.Sql("INSERT INTO Currencies(Name,Symbol) values('DOLAR AUSTRALIJSKI','AUD')");
            migrationBuilder.Sql("INSERT INTO Currencies(Name,Symbol) values('KORONA NORWESKA','NOK')");
            migrationBuilder.Sql("INSERT INTO Currencies(Name,Symbol) values('JEN JAPOŃSKI','JPY')");
            migrationBuilder.Sql("INSERT INTO Currencies(Name,Symbol) values('KORONA SZWEDZKA','SEK')");

            migrationBuilder.Sql("INSERT INTO Users(Login,Password,Name,Permission,Valid) values('Test','cc03e747a6afbbcbf8be7668acfebee5','Test User',3,1)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
