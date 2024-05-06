using CompanyMVC.DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyMVC.DAL.Data.Configuration
{
    internal class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Salary).HasColumnType("decimal(18,2)");

            builder.Property(E => E.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasOne(E => E.Department)
                   .WithMany()
                   .OnDelete(DeleteBehavior.Cascade)
                   ;



        }
    }
}
