using Microsoft.EntityFrameworkCore;
using Barbearia.Domain.Entities;
using Microsoft.Extensions.Configuration;
using FluentValidation.Validators;

namespace Barbearia.Persistence.DbContexts
{
    public class PersonContext : DbContext
    {
        public PersonContext(DbContextOptions<PersonContext> options)
        : base(options) { }

        public DbSet<Person> Persons { get; set; } = null!;
        public DbSet<Telephone> Telephones { get; set; } = null!;
        public DbSet<Address> Addresses { get; set; } = null!;
        public DbSet<WorkingDay> WorkingDays { get; set; } = null!;
        public DbSet<TimeOff> TimeOffs { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Schedule> Schedules { get; set; } = null!;
        public DbSet<Order> Orders {get;set;} = null!;
        public DbSet<Service> Services{get;set;} = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var person = modelBuilder.Entity<Person>();
            var customer = modelBuilder.Entity<Customer>();
            var product = modelBuilder.Entity<Product>();
            var item = modelBuilder.Entity<Item>();
            var Supplier = modelBuilder.Entity<Supplier>();
            var telephone = modelBuilder.Entity<Telephone>();
            var address = modelBuilder.Entity<Address>();
            var workingDay = modelBuilder.Entity<WorkingDay>();
            var timeOff = modelBuilder.Entity<TimeOff>();
            var role = modelBuilder.Entity<Role>();
            var schedule = modelBuilder.Entity<Schedule>();
            var employee = modelBuilder.Entity<Employee>();
            var roleEmployee = modelBuilder.Entity<RoleEmployee>();
            var service = modelBuilder.Entity<Service>();

            modelBuilder.Entity<Order>().ToTable("Order", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<Item>().ToTable("Item", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<Product>().ToTable("Product", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<StockHistory>().ToTable("StockHistory", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<ServiceCategory>().ToTable("ServiceCategory", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<Service>().ToTable("Service", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<EmployeeService>().ToTable("EmployeeService", t => t.ExcludeFromMigrations());
            modelBuilder.Ignore<StockHistory>();
            modelBuilder.Ignore<StockHistoryOrder>();
            modelBuilder.Ignore<StockHistorySupplier>();
            modelBuilder.Ignore<Payment>();
            modelBuilder.Ignore<Coupon>();
            modelBuilder.Ignore<ProductCategory>();
            modelBuilder.Ignore<OrderProduct>();
            modelBuilder.Ignore<Appointment>();
            modelBuilder.Ignore<ServiceCategory>();
            modelBuilder.Ignore<AppointmentOrder>();
            modelBuilder.Ignore<OrderProduct>();

            

            //Necessário para declarar order nesse contexto(sem criar nada a mais) corretamente
            ////////////////////////////////////////////////////////////
            
            service
                .HasMany(s => s.Persons)
                .WithMany(e => e.Services)
                .UsingEntity<EmployeeService>();

            modelBuilder.Entity<Product>()
            .Ignore(p=>p.Orders)
            .Ignore(p=>p.OrderProducts);

            modelBuilder.Entity<Order>()
            .Ignore(o=>o.Products)
            .Ignore(o=>o.OrderProducts);
            ///////////////////////////////////////////////////////////

            person
            .ToTable("Person")
            .HasDiscriminator<int>("PersonType")
            .HasValue<Person>(1)
            .HasValue<Customer>(2)
            .HasValue<Supplier>(3)
            .HasValue<Employee>(4);

            person
                .Property(p => p.Name)
                .HasMaxLength(80)
                .IsRequired();

            person
                .Property(p => p.BirthDate)
                .HasColumnType("date");

            person
                .Property(p => p.Email)
                .HasMaxLength(80)
                .IsRequired();

            person
                .HasMany(p => p.Addresses)
                .WithOne(a => a.Person)
                .HasForeignKey(a => a.PersonId);

            person
                .HasMany(p => p.Telephones)
                .WithOne(t => t.Person)
                .HasForeignKey(t => t.PersonId)
                .IsRequired();

            address
                .ToTable("Address");

            address
                .Property(c => c.Street)
                .HasMaxLength(80);

            address
                .Property(c => c.Number);

            address
                .Property(c => c.District)
                .HasMaxLength(60);

            address
                .Property(c => c.City)
                .HasMaxLength(60);

            address
                .Property(c => c.State)
                .HasMaxLength(2);

            address
                .Property(c => c.Cep)
                .HasMaxLength(8);

            address
                .Property(c => c.Complement)
                .HasMaxLength(80);

            address
                .HasOne(p => p.Person)
                .WithMany(c => c.Addresses)
                .HasForeignKey(a => a.PersonId);

            telephone
                .ToTable("Telephone");

            telephone
                .Property(c => c.Number)
                .HasMaxLength(80)
                .IsRequired();

            telephone
                .Property(c => c.Type)
                .IsRequired();

            telephone
                .HasOne(t => t.Person)
                .WithMany(c => c.Telephones)
                .HasForeignKey(t => t.PersonId);

            customer
                .Property(p => p.Gender)
                .IsRequired();

            customer
                .Property(p => p.Cpf)
                .HasMaxLength(11)
                .IsRequired();

            item
                .HasKey(p => p.ItemId);

            Supplier
                .Property(p => p.Cnpj)
                .HasMaxLength(14);

            Supplier
                .Property(p => p.Gender);

            Supplier
                .Property(p => p.Cpf)
                .HasMaxLength(11);

            Supplier
                .Property(s => s.Status)
                .IsRequired();

            employee
                .Property(e => e.Cpf)
                .IsRequired()
                .HasMaxLength(11);

            employee
                .Property(p => p.Gender)
                .IsRequired();

            employee
                .Property(e => e.Status)
                .IsRequired();

            workingDay
                .ToTable("WorkingDay");

            workingDay
                .Property(w => w.WorkDate)
                .IsRequired();

            workingDay
                .Property(w => w.StartTime)
                .IsRequired();

            workingDay
                .Property(w => w.FinishTime)
                .IsRequired();

            workingDay
                .HasMany(w => w.TimeOffs)
                .WithOne(t => t.WorkingDay)
                .HasForeignKey(t => t.WorkingDayId);

            workingDay
                .HasOne(w => w.Schedule)
                .WithOne(s => s.WorkingDay)
                .HasForeignKey<Schedule>(s => s.WorkingDayId);

            workingDay
                .HasOne(w => w.Employee)
                .WithMany(e => e.WorkingDays)
                .HasForeignKey(w => w.PersonId);

            timeOff
                .ToTable("TimeOff");

            timeOff
                .Property(t => t.StartTime)
                .IsRequired();

            timeOff
                .Property(t => t.FinishTime)
                .IsRequired();

            role
                .ToTable("Role");

            // roleEmployee
            //     .Property(re => re.RoleId)
            //     .HasColumnName("RoleId");

            // roleEmployee
            //     .Property(re => re.EmployeeId)
            //     .HasColumnName("PersonId");  

            role
                .Property(r => r.Name)
                .HasMaxLength(80)
                .IsRequired();

            role
                .HasMany(r => r.Employees)
                .WithMany(e => e.Roles)
                .UsingEntity<RoleEmployee>();

            // role.
            //     HasMany(r => r.RoleEmployees)
            //     .WithOne(ro => ro.Role)
            //     .HasForeignKey(ro => ro.RoleId);

            schedule
                .ToTable("Schedule");

            schedule
                .Property(s => s.Status)
                .IsRequired();
            
            employee
                .HasData(
                    new Employee()
                    {
                        PersonId = 5,
                        Name = "João cabeça",
                        BirthDate = new DateOnly(2000, 8, 7),
                        Gender = 1,
                        Cpf = "73473943096",
                        Email = "joao@hotmail.com",
                        Status = Person.TypeStatus.Active,
                    },
                    new Employee()
                    {
                        PersonId = 6,
                        Name = "Bill Maluco",
                        BirthDate = new DateOnly(1990, 1, 1),
                        Gender = 1,
                        Cpf = "73473003096",
                        Email = "billdoidao@gmail.com",
                        Status = Person.TypeStatus.Inactive,
                    });

            workingDay
                .HasData(
                    new WorkingDay()
                    {
                        WorkingDayId = 1,
                        PersonId = 5,
                        WorkDate = new DateOnly(2023, 10, 10),
                        StartTime = new TimeOnly(7, 23, 11),
                        FinishTime = new TimeOnly(18, 30, 0)
                    },
                    new WorkingDay()
                    {
                        WorkingDayId = 2,
                        PersonId = 5,
                        WorkDate = new DateOnly(2023, 11, 11),
                        StartTime = new TimeOnly(8, 23, 11),
                        FinishTime = new TimeOnly(19, 30, 0)
                    }
                );

            timeOff
                .HasData(
                    new TimeOff()
                    {
                        TimeOffId = 1,
                        WorkingDayId = 1,
                        StartTime = new TimeOnly(11, 30, 0),
                        FinishTime = new TimeOnly(14, 0, 0)
                    },
                    new TimeOff()
                    {
                        TimeOffId = 2,
                        WorkingDayId = 2,
                        StartTime = new TimeOnly(12, 0, 0),
                        FinishTime = new TimeOnly(15, 0, 0)
                    }
                );

            role
                .HasData(
                    new Role()
                    {
                        RoleId = 1,
                        Name = "Barbeiro"
                    },
                    new Role()
                    {
                        RoleId = 2,
                        Name = "Barbeiro Mestre?Sla"
                    }
                );

            roleEmployee
                .HasData(
                    new RoleEmployee()
                    {
                        RoleId = 1,
                        EmployeeId = 5
                    },
                    new RoleEmployee()
                    {
                        RoleId = 2,
                        EmployeeId = 6
                    }
                );

            schedule
                .HasData(
                    new Schedule()
                    {
                        ScheduleId = 1,
                        WorkingDayId = 1,
                        Status = 1
                    },
                    new Schedule()
                    {
                        ScheduleId = 2,
                        WorkingDayId = 2,
                        Status = 2
                    }
                );



            customer
                .HasData(
                    new Customer()
                    {
                        PersonId = 1,
                        Name = "Linus Torvalds",
                        BirthDate = new DateOnly(1999, 8, 7),
                        Gender = 1,
                        Cpf = "73473943096",
                        Email = "veio@hotmail.com",
                        Status =0
                    },
                    new Customer()
                    {
                        PersonId = 2,
                        Name = "Bill Gates",
                        BirthDate = new DateOnly(2000, 1, 1),
                        Gender = 2,
                        Cpf = "73473003096",
                        Email = "bill@gmail.com",
                        Status = 0
                    });

            Supplier
                .HasData(
                    new Supplier()
                    {
                        PersonId = 3,
                        Name = "Josefina",
                        BirthDate = new DateOnly(1973, 2, 1),
                        Cnpj = "73473003096986",
                        Email = "josefacraft@hotmail.com",
                        Status = Person.TypeStatus.Active,
                    },
                    new Supplier()
                    {
                        PersonId = 4,
                        Name = "Microsoft",
                        BirthDate = new DateOnly(1975, 4, 4),
                        Cnpj = "73473003096986",
                        Email = "micro@so.ft",
                        Status = Person.TypeStatus.Inactive,
                    });

            address
                .HasData(
                    new Address()
                    {
                        AddressId = 1,
                        Street = "Rua logo ali",
                        Number = 100,
                        District = "Teste",
                        City = "Bc",
                        State = "SC",
                        Cep = "88888888",
                        Complement = "Perto de la",
                        PersonId = 1
                    },
                    new Address()
                    {
                        AddressId = 2,
                        Street = "Rua longe",
                        Number = 300,
                        District = "Perto",
                        City = "Itajaí",
                        State = "SC",
                        Cep = "88888888",
                        Complement = "Longe de la",
                        PersonId = 2
                    },
                    new Address()
                    {
                        AddressId = 3,
                        Street = "Rua velha",
                        Number = 100,
                        District = "Asilo",
                        City = "Bc",
                        State = "SC",
                        Cep = "80888088",
                        Complement = "Perto",
                        PersonId = 3
                    },
                    new Address()
                    {
                        AddressId = 4,
                        Street = "Rua micro",
                        Number = 300,
                        District = "soft",
                        City = "Floripa",
                        State = "SC",
                        Cep = "88123888",
                        Complement = "Longe",
                        PersonId = 4
                    });

            telephone
                .HasData(
                    new Telephone()
                    {
                        TelephoneId = 1,
                        Number = "47988887777",
                        Type = Telephone.TelephoneType.Mobile,
                        PersonId = 1
                    },
                    new Telephone()
                    {
                        TelephoneId = 2,
                        Number = "47988887777",
                        Type = Telephone.TelephoneType.Landline,
                        PersonId = 2
                    },
                    new Telephone()
                    {
                        TelephoneId = 3,
                        Number = "47944887777",
                        Type = Telephone.TelephoneType.Mobile,
                        PersonId = 3
                    },
                    new Telephone()
                    {
                        TelephoneId = 4,
                        Number = "55988844777",
                        Type = Telephone.TelephoneType.Landline,
                        PersonId = 4
                    });

            base.OnModelCreating(modelBuilder);
        }
    }
}

// dotnet ef migrations add Correcao --context CustomerContext --startup-project ../Barbearia.Api
// dotnet ef database update --context CustomerContext --startup-project ../Barbearia.Api