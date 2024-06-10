using Medicare.Domain.Entities;
using Medicare.Infrastructure.Context.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Medicare.Infrastructure.Context
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<LabTest> LabTests { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<LabTestResult> LabTestResults { get; set; }
        public DbSet<Appointment> Appointments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new OfficeConfiguration());
            modelBuilder.ApplyConfiguration(new DoctorConfiguration());
            modelBuilder.ApplyConfiguration(new LabTestConfiguration());
            modelBuilder.ApplyConfiguration(new PatientConfiguration());
            modelBuilder.ApplyConfiguration(new LabTestResultConfiguration());
            modelBuilder.ApplyConfiguration(new AppointmentConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
