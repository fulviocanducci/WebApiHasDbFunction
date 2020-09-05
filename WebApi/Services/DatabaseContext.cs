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
        }
    }

    public class FunctionsSQLServer
    {
        public static int CharIndex(string value, string type) => 0;
        public static int Convert(string type, string value) => 0;

        internal static MethodInfo CharIndexToMethodInfo()
            => typeof(FunctionsSQLServer).GetMethod(nameof(FunctionsSQLServer.CharIndex));
        internal static MethodInfo ConvertToMethodInfo()
            => typeof(FunctionsSQLServer).GetMethod(nameof(FunctionsSQLServer.Convert));
    }
}
