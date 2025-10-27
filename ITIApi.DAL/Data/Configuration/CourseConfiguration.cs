using ITIApi.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITIApi.DAL.Data.Configuration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(C => C.Id);
            builder.Property(C => C.Id).UseIdentityColumn(1, 1);
            builder.Property(C => C.Name).HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(C => C.Description).HasColumnType("nvarchar").HasMaxLength(150);
            builder.HasData(
                       new Course
                       {
                           Id = 1,
                           Name = "C# Fundamentals",
                           Description = "Introduction to C# programming and .NET basics.",
                           Duration = 40
                       },
                       new Course
                       {
                           Id = 2,
                           Name = "ASP.NET Core MVC",
                           Description = "Building web applications using ASP.NET Core MVC framework.",
                           Duration = 60
                       },
                       new Course
                       {
                           Id = 3,
                           Name = "Entity Framework Core",
                           Description = "Working with databases using EF Core.",
                           Duration = 50
                       }
                   );
        }
    }
}
