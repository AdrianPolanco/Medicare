using Medicare.Domain.Entities;
using Medicare.Domain.Repositories;
using Medicare.Infrastructure.Context;
using Medicare.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;


namespace Medicare.Tests.Integration.Repositories
{
    public class UserRepositoryTest
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _repository;

        public UserRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Crea un nuevo nombre de base de datos en memoria para cada ejecución de prueba
                .Options;

            _context = new ApplicationDbContext(options);
            _repository = new UserRepository(_context);

            SeedDatabase(); // Llena la base de datos con datos de prueba antes de cada prueba
        }

        [Fact]
        public async Task ShouldReturnOneUser()
        {
            var users = await _repository.GetByPagesAsync(1, new CancellationToken());

            Assert.Equal(1, users.Count);
        }

        [Fact]
        public async Task ShouldHasIncludes()
        {
            var user = await _repository.GetByIdAsync(_context.Users.First().Id, new CancellationToken());
            Assert.NotNull(user);
            Assert.NotNull(user.Role);
            Assert.NotNull(user.Office);
            Assert.NotNull(user.OwnedOffice);
            Assert.Equal(user.Role.Name, "Administrador");
            Assert.Equal(user.Office.Name, "Office 1");
            Assert.Equal(user.OwnedOffice.Name, "Office 1");
        }

        private void SeedDatabase()
        {
            var offices = new List<Office>
            {
                new Office { Id = Guid.NewGuid(), Name = "Office 1" },
                new Office { Id = Guid.NewGuid(), Name = "Office 2" }
                // Agrega más oficinas si es necesario para tus pruebas
            };

            var roles = new List<Role>
            {
                new Role { Id = Guid.NewGuid(), Name = "Administrador" },
                new Role { Id = Guid.NewGuid(), Name = "Asistente" }
                // Agrega más roles si es necesario para tus pruebas
            };

            _context.Offices.AddRange(offices);
            _context.Roles.AddRange(roles);

            var users = new List<User>
            {
                new User
                {
                    Name = "John",
                    Lastname = "Doe",
                    Username = "johndoe",
                    Email = "john@example.com",
                    Password = "password1",
                    OfficeId = offices[0].Id, // Asigna la primera oficina al primer usuario
                    RoleId = roles[0].Id,
                    OwnedOfficeId = offices[0].Id// Asigna el primer rol al primer usuario
                },
                // Agrega más usuarios si es necesario para tus pruebas
            };

            _context.Users.AddRange(users);

            _context.SaveChanges();
        }
    }
}
