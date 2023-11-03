using Barbearia.Application.Features;
using Barbearia.Domain.Entities;

namespace Barbearia.Application.Contracts.Repositories;

public interface IItemRepository
{
    Task<(IEnumerable<Product>, PaginationMetadata)> GetAllProductsAsync(string? searchQuery, int pageNumber, int pageSize);
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int productId);
    void AddProduct(Product product);
    void DeleteProduct(Product product);
    public Task<IEnumerable<StockHistory>> GetAllStockHistoriesAsync();
    public Task<StockHistory?> GetStockHistoryByIdAsync(int stockHistoryId);
    public Task<StockHistoryOrder?> GetStockHistoryOrderByIdAsync(int stockHistoryId);
    public Task<StockHistorySupplier?> GetStockHistorySupplierByIdAsync(int stockHistoryId);
    public void AddStockHistory(StockHistory stockHistory);
    public void RemoveStockHistory(StockHistory stockHistory);
    Task<bool> SaveChangesAsync();
    public Task<IEnumerable<ProductCategory>> GetAllProductCategoriesAsync();
    public Task<ProductCategory?> GetProductCategoryByIdAsync(int productCategoryId);
    public void AddProductCategory(ProductCategory productCategory);
    public void RemoveProductCategory(ProductCategory productCategory);
    void AddService(Service service);
    void DeleteService(Service service);
    Task<Service?> GetServiceByIdAsync(int serviceId);
    Task<(IEnumerable<Service>, PaginationMetadata)> GetAllServicesAsync(string? searchQuery, int pageNumber, int pageSize);
    public Task<IEnumerable<ServiceCategory?>> GetAllServiceCategory();
    public Task<ServiceCategory?> GetServiceCategoryByIdAsync(int serviceCategoryId);
    public void AddServiceCategory(ServiceCategory serviceCategory);
    public void DeleteServiceCategory(ServiceCategory serviceCategory);
    public Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
    public Task<Appointment?> GetAppointmentByIdAsync(int appointmentId);
    void AddAppointment(Appointment appointment);
    void RemoveAppointment(Appointment appointment);
    Task<(IEnumerable<Appointment>, PaginationMetadata)> GetAllAppointmentsAsync(string? searchQuery, int pageNumber, int pageSize);
    public Task<StockHistoryOrder?> GetStockHistoryOrderToOrderByIdAsync(int stockHistoryId);
    public Task<Appointment?> GetAppointmentToOrderByIdAsync(int appointmentId);
}