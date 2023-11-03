using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Application.Features;
using Barbearia.Domain.Entities;
using Barbearia.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Barbearia.Persistence.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly ItemContext _context;
    private readonly IMapper _mapper;

    public ItemRepository(ItemContext ItemContext, IMapper mapper)
    {
        _context = ItemContext ?? throw new ArgumentNullException(nameof(OrderContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<(IEnumerable<Product>, PaginationMetadata)> GetAllProductsAsync(string? searchQuery, int pageNumber, int pageSize)
    {
        IQueryable<Product> collection = _context.Products;

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            var category = searchQuery.Trim().ToLower();
            collection = collection.Where(
               p => p.ProductCategory != null && p.ProductCategory.Name.ToLower().Contains(category)
            );
        }

        var totalItemCount = await collection.CountAsync();

        var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

        var productToReturn = await collection
            .OrderBy(p => p.ItemId)
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return (productToReturn, paginationMetadata);
    }

    public void AddProduct(Product product)
    {
        _context.Products.Add(product);
    }

    public void DeleteProduct(Product product)
    {
        _context.Products.Remove(product);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _context.Products
        .Include(p => p.ProductCategory)
        .ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        return await _context.Products
        .Include(p => p.ProductCategory)
        .FirstOrDefaultAsync(p => p.ItemId == productId);
    }

    public async Task<IEnumerable<StockHistory>> GetAllStockHistoriesAsync()
    {
        return await _context.StockHistories
        // .Include(s => s.Supplier)
        .Include(s => s.Product)
        // .Include(s => s.Order)
        .ToListAsync();
    }

    public async Task<StockHistory?> GetStockHistoryByIdAsync(int stockHistoryId)
    {
        return await _context.StockHistories
        .FirstOrDefaultAsync(s => s.StockHistoryId == stockHistoryId);
    }

    public async Task<StockHistoryOrder?> GetStockHistoryOrderByIdAsync(int stockHistoryId)
    {
        return await _context.StockHistories.OfType<StockHistoryOrder>().AsNoTracking()
        .Include(s => s.Product)
        .Include(s => s.Order)
        .FirstOrDefaultAsync(s => s.StockHistoryId == stockHistoryId);
    }

    public async Task<StockHistoryOrder?> GetStockHistoryOrderToOrderByIdAsync(int stockHistoryId)
    {
        return await _context.StockHistories.OfType<StockHistoryOrder>()
        .FirstOrDefaultAsync(s => s.StockHistoryId == stockHistoryId);
    }

    public async Task<StockHistorySupplier?> GetStockHistorySupplierByIdAsync(int stockHistoryId)
    {
        return await _context.StockHistories.OfType<StockHistorySupplier>()
        .Include(s => s.Supplier)
        .Include(s => s.Product)
        .FirstOrDefaultAsync(s => s.StockHistoryId == stockHistoryId);
    }


    public void AddStockHistory(StockHistory stockHistory)
    {
        _context.StockHistories.Add(stockHistory);
    }

    public void RemoveStockHistory(StockHistory stockHistory)
    {
        _context.StockHistories.Remove(stockHistory);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<ProductCategory>> GetAllProductCategoriesAsync()
    {
        return await _context.ProductCategory.ToListAsync();
    }

    public async Task<ProductCategory?> GetProductCategoryByIdAsync(int productCategoryId)
    {
        return await _context.ProductCategory.FirstOrDefaultAsync(pc => pc.ProductCategoryId == productCategoryId);
    }

    public void AddProductCategory(ProductCategory productCategory)
    {
        _context.ProductCategory.Add(productCategory);
    }

    public void RemoveProductCategory(ProductCategory productCategory)
    {
        _context.ProductCategory.Remove(productCategory);
    }

    public void AddService(Service service)
    {
        _context.Services.Add(service);
    }

    public void DeleteService(Service service)
    {
        _context.Services.Remove(service);
    }

    public async Task<Service?> GetServiceByIdAsync(int serviceId)
    {
        return await _context.Services
        .Include(s => s.ServiceCategory)
        .FirstOrDefaultAsync(s => s.ItemId == serviceId);
    }

    public async Task<(IEnumerable<Service>, PaginationMetadata)> GetAllServicesAsync(string? searchQuery, int pageNumber, int pageSize)
    {
        IQueryable<Service> collection = _context.Services;

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            var category = searchQuery.Trim().ToLower();
            collection = collection.Where(
               s => s.ServiceCategory != null && s.ServiceCategory.Name.ToLower().Contains(category)
            );
        }

        var totalItemCount = await collection.CountAsync();

        var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

        var ServiceToReturn = await collection
            .OrderBy(s => s.ItemId)
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return (ServiceToReturn, paginationMetadata);
    }

    public async Task<IEnumerable<ServiceCategory?>> GetAllServiceCategory()
    {
        return await _context.ServiceCategories
            .Include(r => r.Services)         
            .ToListAsync();
    }

    public async Task<ServiceCategory?> GetServiceCategoryByIdAsync(int serviceCategoryId)
    {
        var serviceCategory = await _context.ServiceCategories
            // .Include(r => r.Roles)!
            // .ThenInclude(e => e.Role)
            .Include(r => r.Services)
            .FirstOrDefaultAsync(r => r.ServiceCategoryId == serviceCategoryId);
        // serviceCategory!.Services = await _itemContext.Services.Where(s => s.ServiceCategoryId == serviceCategoryId).ToListAsync();
        return serviceCategory;
    }

    public void AddServiceCategory(ServiceCategory serviceCategory)
    {
        _context.ServiceCategories.Add(serviceCategory);
    }

    public void DeleteServiceCategory(ServiceCategory serviceCategory)
    {
        _context.ServiceCategories.Remove(serviceCategory);
    }

    public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
    {
        return await _context.Appointments.AsNoTracking()
            .Include(a => a.Schedule)
            .Include(a => a.Person)
            .Include(a => a.Services)
            .Include(a => a.Orders)
            .ToListAsync();
    }
    public async Task<Appointment?> GetAppointmentByIdAsync(int appointmentId)
    {
        return await _context.Appointments
            .Include(a => a.Schedule)
            .Include(a => a.Person)
            .Include(a => a.Services)
            .Include(a => a.Orders)
            .FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);
    }

    public async Task<Appointment?> GetAppointmentToOrderByIdAsync(int appointmentId)
    {
        return await _context.Appointments
            .FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);
    }

    public void AddAppointment (Appointment appointment)
    {
        _context.Appointments.Add(appointment);
    }
    public void RemoveAppointment (Appointment appointment)
    {
        _context.Appointments.Remove(appointment);
    }
    public async Task<(IEnumerable<Appointment>, PaginationMetadata)> GetAllAppointmentsAsync(string? searchQuery, int pageNumber, int pageSize)
    {
        IQueryable<Appointment> collection = _context.Appointments
            .Include(a => a.Schedule)
            .Include(a => a.Person)
            .Include(a => a.Services)
            .Include(a => a.Orders);

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            var name = searchQuery.Trim().ToLower();
            collection = collection.Where(
               a=>a.Person != null && a.Person.Name.ToLower().Contains(name)
            );
        }

        var totalItemCount = await collection.CountAsync();

        var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

        var AppointmentToReturn = await collection
            .OrderBy(s => s.AppointmentId)
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return (AppointmentToReturn, paginationMetadata);
    }
}