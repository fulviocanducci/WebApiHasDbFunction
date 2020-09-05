using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;

namespace WebApi.Models.Maps
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {

            builder.ToTable("Doctor");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id)
                .UseIdentityColumn();
            builder.Property(p => p.Name)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(p => p.Cpf)
                .IsRequired();
            builder.Property(p => p.Crm)
                .IsRequired();
            builder.Property(p => p.Expertise)
                .IsRequired()
                .HasConversion(
                    from => JsonConvert.SerializeObject(from),
                    to => JsonConvert.DeserializeObject<string[]>(to)
                );

            builder.HasIndex(p => p.Expertise);
        }
    }

}
