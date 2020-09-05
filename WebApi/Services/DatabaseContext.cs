using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq;
using System.Reflection;
using WebApi.Models;
using WebApi.Models.Maps;

namespace WebApi.Services
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Doctor> Doctor { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DoctorConfiguration());

            modelBuilder.HasDbFunction(FunctionsSQLServer.FSGetExpertiseMethodInfo())
                    .HasTranslation((args) =>
                    {
                        var arguments = args.ToList();
                        arguments[1] = new
                            SqlFragmentExpression(
                                (string)((SqlConstantExpression)arguments.Last()).Value
                            );
                        return SqlFunctionExpression.Create("dbo.FSGetExpertise", arguments, typeof(int), null);
                    });

            modelBuilder.HasDbFunction(FunctionsSQLServer.CharIndexToMethodInfo())
                    .HasTranslation((args) =>
                    {
                        var arguments = args.ToList();
                        arguments[1] = new
                            SqlFragmentExpression(
                                (string)((SqlConstantExpression)arguments.Last()).Value
                            );
                        return SqlFunctionExpression.Create("CHARINDEX", arguments, typeof(int), null);
                    });

            # region FunctionSQLServer
            //SET ANSI_NULLS ON
            //GO
            //SET QUOTED_IDENTIFIER ON
            //GO
            //-- =============================================
            //--Author:		< Fúlvio Cezar Canducci Dias >
            //--Create date: < 04 / 09 / 2020 >
            //--Description:	< Expertise >
            //-- =============================================
            //ALTER FUNCTION dbo.FSGetExpertise
            //(
            //  @Value nvarchar(450),
            //  @Field nvarchar(450)
            //)
            //RETURNS INT
            //AS
            //BEGIN

            //    RETURN CHARINDEX(@Value, @Field COLLATE Latin1_General_CI_AS);
            //END
            //GO
            #endregion
        }
    }

    public class FunctionsSQLServer
    {
        public static int CharIndex(string value, string type)
        {
            return 0;
        }

        public static int FSGetExpertise(string value, string type)
        {
            return 0;
        }

        public static MethodInfo FSGetExpertiseMethodInfo()
            => typeof(FunctionsSQLServer).GetMethod(nameof(FunctionsSQLServer.FSGetExpertise));
        public static MethodInfo CharIndexToMethodInfo()
            => typeof(FunctionsSQLServer).GetMethod(nameof(FunctionsSQLServer.CharIndex));

    }
}
