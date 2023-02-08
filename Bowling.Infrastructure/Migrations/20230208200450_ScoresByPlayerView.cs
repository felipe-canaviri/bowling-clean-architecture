using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bowling.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ScoresByPlayerView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR REPLACE VIEW public.scores
AS SELECT p1.""GameId"",
    p1.""Id"",
    p1.""Name"",
    s.score
   FROM ""Players"" p1
     LEFT JOIN ( SELECT p.""Id"",
            sum(t.""FirstThrowing"" + t.""SecondThrowing"" + t.""ThirdThrowing"") AS score
           FROM ""Players"" p
             LEFT JOIN ""Turns"" t ON t.""PlayerId"" = p.""Id""
          GROUP BY p.""Id"") s ON p1.""Id"" = s.""Id""; ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW public.scores");
        }
    }
}
