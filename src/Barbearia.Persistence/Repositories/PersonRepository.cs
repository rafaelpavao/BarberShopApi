using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Application.Features;
using Barbearia.Domain.Entities;
using Barbearia.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Barbearia.Persistence.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly PersonContext _context;
    private readonly IMapper _mapper;

    public PersonRepository(PersonContext personContext, IMapper mapper)
    {
        _context = personContext ?? throw new ArgumentNullException(nameof(personContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<(IEnumerable<Customer>, PaginationMetadata)> GetAllCustomersAsync(string? searchQuery,
     int pageNumber, int pageSize)
    {
        IQueryable<Customer> collection = _context.Persons.OfType<Customer>()
        .Include(c => c.Telephones);


        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            var name = searchQuery.Trim().ToLower();
            collection = collection.Where(

                c => c.Name.ToLower().Contains(name)
            );
        }


        var totalItemCount = await collection.CountAsync();

        var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

        var customerToReturn = await collection
        .OrderBy(c => c.PersonId)
        .Skip(pageSize * (pageNumber - 1))
        .Take(pageSize)
        .ToListAsync();

        return (customerToReturn, paginationMetadata);
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        return await _context.Persons.OfType<Customer>()
        .Include(c => c.Telephones)
        .OrderBy(p => p.PersonId)
        .ToListAsync();
    }

    public async Task<Customer?> GetCustomerByIdAsync(int customerId)
    {
        return await _context.Persons.OfType<Customer>()
            .Include(c => c.Telephones)
            .Include(c => c.Addresses)
            .FirstOrDefaultAsync(p => p.PersonId == customerId);
    }

    public async Task<Customer?> GetCustomerToOrderByIdAsync(int customerId)
    {
        return await _context.Persons.OfType<Customer>()
            .FirstOrDefaultAsync(p => p.PersonId == customerId);
    }

    public void AddCustomer(Customer customer)
    {
        _context.Persons.Add(customer);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void DeleteCustomer(Customer customer)
    {
        _context.Persons.Remove(customer);
    }

    public async Task<IEnumerable<WorkingDay>?> GetWorkingDayAsync(int employeeId)
    {
        var workingDaysFromDatabase = await GetEmployeeByIdAsync(employeeId);
        return workingDaysFromDatabase?.WorkingDays;
    }

    public async Task<WorkingDay?> GetWorkingDayByIdAsync(int workingDayId)
    {
        return await _context.Persons
        .OfType<Employee>()
        .SelectMany(e => e.WorkingDays)
        .Include(w=>w.TimeOffs)
        .FirstOrDefaultAsync(workingDay => workingDay.WorkingDayId == workingDayId);
    }

    public async Task<bool> HasScheduleForWorkingDayAsync(int workingDayId)
    {
        return await _context.Schedules
        .AnyAsync(schedule => schedule.WorkingDayId == workingDayId);
    }

    public void AddWorkingDay(Employee employee, WorkingDay workingDay)
    {
        employee.WorkingDays.Add(workingDay);
    }

    public void DeleteWorkingDay(Employee employee, WorkingDay workingDay)
    {
        employee.WorkingDays.Remove(workingDay);
    }

    public async Task<TimeOff?> GetTimeOffByIdAsync(int timeOffId)
    {
        return await _context.TimeOffs.FirstOrDefaultAsync(t => t.TimeOffId == timeOffId);
    }


    public void AddTimeOff(TimeOff timeOff)
    {
        _context.TimeOffs.Add(timeOff);
    }

    public void DeleteTimeOff(TimeOff timeOff)
    {
        _context.TimeOffs.Remove(timeOff);
    }

    public async Task<Supplier?> GetSupplierByIdAsync(int supplierId)
    {
        return await _context.Persons.OfType<Supplier>()
        .Include(s => s.Telephones)
        .Include(s => s.Addresses)
        .Include(s => s.Products)
        // .Include(s => s.StockHistoriesSupplier)
        .FirstOrDefaultAsync(s => s.PersonId == supplierId);
    }

    public async Task<(IEnumerable<Supplier>, PaginationMetadata)> GetAllSuppliersAsync(string? searchQuery,
    int pageNumber, int pageSize)
    {
        IQueryable<Supplier> collection = _context.Persons.OfType<Supplier>()
        .Include(c => c.Telephones);

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            var name = searchQuery.Trim().ToLower();
            collection = collection.Where(

                c => c.Name.ToLower().Contains(name)
            );
        }

        var totalItemCount = await collection.CountAsync();

        var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

        var SupplierToReturn = await collection
        .OrderBy(c => c.PersonId)
        .Skip(pageSize * (pageNumber - 1))
        .Take(pageSize)
        .ToListAsync();

        return (SupplierToReturn, paginationMetadata);
    }

    public void AddSupplier(Supplier supplier)
    {
        _context.Persons.Add(supplier);
    }

    public void DeleteSupplier(Supplier supplier)
    {
        _context.Persons.Remove(supplier);
    }

    public async Task<IEnumerable<Address>?> GetAddressAsync(int personId)
    {
        var personFromDatabase = await GetPersonByIdAsync(personId);
        return personFromDatabase?.Addresses;
    }

    public void AddAddress(Person person, Address address)
    {
        person.Addresses.Add(address);
    }

    public void DeleteAddress(Person person, Address address)
    {
        person.Addresses.Remove(address);
    }

    public async Task<IEnumerable<Telephone>?> GetTelephoneAsync(int personId)
    {
        var personFromDatabase = await GetPersonByIdAsync(personId);
        return personFromDatabase?.Telephones;
    }

    public void AddTelephone(Person person, Telephone telephone)
    {
        person.Telephones.Add(telephone);
    }

    public void DeleteTelephone(Person person, Telephone telephone)
    {
        person.Telephones.Remove(telephone);
    }
    public async Task<Customer?> GetCustomerWithOrdersByIdAsync(int customerId)
    {
        return await _context.Persons.OfType<Customer>()
        .Include(c => c.Orders)
        .FirstOrDefaultAsync(c => c.PersonId == customerId);
    }

    public async Task<Employee?> GetEmployeeByIdAsync(int employeeId)
    {
        return await _context.Persons.OfType<Employee>()
            .Include(c => c.Telephones)
            .Include(c => c.Addresses)
            .Include(c => c.Roles)
            .Include(c => c.Services)
            .Include(c => c.WorkingDays)
                .ThenInclude(w => w.Schedule)
            .Include(c => c.WorkingDays)
                .ThenInclude(w => w.TimeOffs)
            .FirstOrDefaultAsync(p => p.PersonId == employeeId);
    }

    public void AddEmployee(Employee employee)
    {
        _context.Persons.Attach(employee);
    }

    public void DeleteEmployee(Employee employee)
    {
        _context.Persons.Remove(employee);
    }

    public async Task<(IEnumerable<Employee>, PaginationMetadata)> GetAllEmployeesAsync(string? searchQuery,
         int pageNumber, int pageSize)
    {
        IQueryable<Employee> collection = _context.Persons.OfType<Employee>()
        .Include(c => c.Telephones);

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            var name = searchQuery.Trim().ToLower();
            collection = collection.Where(

                c => c.Name.ToLower().Contains(name)
            );
        }

        var totalItemCount = await collection.CountAsync();

        var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

        var employeeToReturn = await collection
        .OrderBy(c => c.PersonId)
        .Skip(pageSize * (pageNumber - 1))
        .Take(pageSize)
        .ToListAsync();

        return (employeeToReturn, paginationMetadata);
    }
    public void AddSchedule(Schedule schedule)
    {
        _context.Schedules.Add(schedule);
    }
    public void DeleteSchedule(Schedule schedule)
    {
        _context.Schedules.Remove(schedule);
    }

    public async Task<IEnumerable<Schedule>> GetAllSchedulesAsync()
    {
        return await _context.Schedules
            .Include(s => s.WorkingDay!)
            .ThenInclude(w => w.Employee)
            .OrderBy(s => s.ScheduleId)
            .ToListAsync();
    }

    public async Task<Schedule?> GetScheduleByIdAsync(int scheduleId)
    {
        return await _context.Schedules
            .Include(s => s.WorkingDay!)
            .ThenInclude(w => w.Employee).FirstOrDefaultAsync(s => s.ScheduleId == scheduleId);
    }

    public async Task<(IEnumerable<Schedule>, PaginationMetadata)> GetAllSchedulesAsync(string? searchQuery,
         int pageNumber, int pageSize)
    {

        IQueryable<Schedule> collection = _context.Schedules.Where(s => s.WorkingDay != null).Include(s => s.WorkingDay!).ThenInclude(w => w.Employee);

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            var name = searchQuery.Trim().ToLower();
            collection = collection.Where(

                s => s.WorkingDay != null && s.WorkingDay.Employee != null && s.WorkingDay.Employee.Name.ToLower().Contains(name)
            );
        }

        var totalItemCount = await collection.CountAsync();

        var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

        var schedulesToReturn = await collection
        .OrderBy(s => s.ScheduleId)
        .Skip(pageSize * (pageNumber - 1))
        .Take(pageSize)
        .ToListAsync();

        return (schedulesToReturn, paginationMetadata);
    }

    public async Task<Person?> GetPersonByIdAsync(int personId)
    {
        return await _context.Persons
            .Include(c => c.Telephones)
            .Include(c => c.Addresses)
            .FirstOrDefaultAsync(p => p.PersonId == personId);
    }

    public async Task<IEnumerable<Role?>> GetAllRoles()
    {
        return await _context.Roles
            .Include(r => r.Employees)!
            // .ThenInclude(e => e.Employee)
            .ToListAsync();
    }

    public async Task<Role?> GetRoleByIdAsync(int roleId)
    {
        return await _context.Roles
            .Include(r => r.Employees)!
            // .ThenInclude(e => e.Employee)
            .FirstOrDefaultAsync(r => r.RoleId == roleId);
    }

    public void AddRole(Role role)
    {
        _context.Roles.Add(role);
    }

    public void DeleteRole(Role role)
    {
        _context.Roles.Remove(role);
    }

    public async Task<Service?> GetServiceByIdAsync(int serviceId)
    {
        return await _context.Services
        .FirstOrDefaultAsync(s => s.ItemId == serviceId);
    }
}