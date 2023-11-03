using Barbearia.Application.Features;
using Barbearia.Domain.Entities;

namespace Barbearia.Application.Contracts.Repositories;

public interface IPersonRepository
{
    Task<(IEnumerable<Customer>, PaginationMetadata)> GetAllCustomersAsync(string? searchQuery,
     int pageNumber, int pageSize);
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer?> GetCustomerByIdAsync(int customerId);
    void AddCustomer(Customer customer);
    void DeleteCustomer(Customer customer);
    Task<(IEnumerable<Supplier>, PaginationMetadata)> GetAllSuppliersAsync(string? searchQuery,
     int pageNumber, int pageSize);
    Task<Supplier?> GetSupplierByIdAsync(int supplierId);
    void AddSupplier(Supplier supplier);
    void DeleteSupplier(Supplier supplier);
    Task<IEnumerable<WorkingDay>?> GetWorkingDayAsync(int employeeId);
    Task<WorkingDay?> GetWorkingDayByIdAsync(int workingDayId);
    Task<bool> HasScheduleForWorkingDayAsync(int workingDayId);
    void AddWorkingDay(Employee employee, WorkingDay workingDay);
    void DeleteWorkingDay(Employee employee, WorkingDay workingDay);
    Task<TimeOff?> GetTimeOffByIdAsync(int timeOffId);
    void AddTimeOff(TimeOff timeOff);
    void DeleteTimeOff(TimeOff timeOff);
    Task<bool> SaveChangesAsync();
    Task<IEnumerable<Address>?> GetAddressAsync(int personId);
    void AddAddress(Person person, Address address);
    void DeleteAddress(Person person, Address address);
    Task<IEnumerable<Telephone>?> GetTelephoneAsync(int personId);
    void AddTelephone(Person person, Telephone telephone);
    void DeleteTelephone(Person person, Telephone telephone);
    Task<Customer?> GetCustomerWithOrdersByIdAsync(int customerId);
    Task<Employee?> GetEmployeeByIdAsync(int employeeId);
    void AddEmployee(Employee employee);
    void DeleteEmployee(Employee employee);
    Task<(IEnumerable<Employee>, PaginationMetadata)> GetAllEmployeesAsync(string? searchQuery,
     int pageNumber, int pageSize);

    Task<(IEnumerable<Schedule>, PaginationMetadata)> GetAllSchedulesAsync(string? searchQuery,
        int pageNumber, int pageSize);
    Task<Schedule?> GetScheduleByIdAsync(int scheduleId);
    Task<IEnumerable<Schedule>> GetAllSchedulesAsync();
    public void DeleteSchedule(Schedule schedule);
    public void AddSchedule(Schedule schedule);
    Task<Person?> GetPersonByIdAsync(int personId);
    public Task<IEnumerable<Role?>> GetAllRoles();
    public Task<Role?> GetRoleByIdAsync(int roleId);
    public void AddRole(Role role);
    public void DeleteRole(Role role);
    public Task<Customer?> GetCustomerToOrderByIdAsync(int customerId);
    Task<Service?> GetServiceByIdAsync(int serviceId);
}