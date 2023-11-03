using System.Reflection;
using Barbearia.Application.Features.Customers.Commands.CreateCustomer;
using Barbearia.Application.Features.Customers.Commands.UpdateCustomer;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Application.Features.Addresses.Commands.CreateAddress;
using Barbearia.Application.Features.Addresses.Commands.DeleteAddress;
using Barbearia.Application.Features.Addresses.Commands.UpdateAddress;
using Barbearia.Application.Features.Addresses.Queries.GetAddress;
using Barbearia.Application.Features.Telephones.Commands.CreateTelephone;
using Barbearia.Application.Features.Telephones.Commands.DeleteTelephone;
using Barbearia.Application.Features.Telephones.Commands.UpdateTelephone;
using Barbearia.Application.Features.Telephones.Query.GetTelephone;
using Barbearia.Application.Features.Customers.Commands.DeleteCustomer;
using Barbearia.Application.Features.Customers.Queries.GetAllCustomers;
using Barbearia.Application.Features.Customers.Queries.GetCustomerById;
using Barbearia.Application.Features.Customers.Queries.GetCustomerWithOrdersById;
using FluentValidation;
using MediatR;
using Barbearia.Application.Features.Orders.Commands.UpdateOrder;
using Barbearia.Application.Features.Orders.Commands.DeleteOrder;
using Barbearia.Application.Features.CustomersCollection;
using Barbearia.Application.Features.OrdersCollection;
using Barbearia.Application.Features.Orders.Queries.GetAllOrders;
using Barbearia.Application.Features.Orders.Queries.GetOrderById;
using Barbearia.Application.Features.Orders.Queries.GetOrderByNumber;
using Barbearia.Application.Features.Orders.Commands.CreateOrder;
using Barbearia.Application.Features.Payments.Queries.GetPayment;
using Barbearia.Application.Features.Payments.Commands.CreatePayment;
using Barbearia.Application.Features.Payments.Commands.UpdatePayment;
using Barbearia.Application.Features.Payments.Commands.DeletePayment;
using Barbearia.Application.Features.ProductsCollection;
using Barbearia.Application.Features.Suppliers.Queries.GetSupplierById;
using Barbearia.Application.Features.Suppliers.Commands.CreateSupplier;
using Barbearia.Application.Features.Suppliers.Commands.UpdateSupplier;
using Barbearia.Application.Features.Suppliers.Commands.DeleteSupplier;
using Barbearia.Application.Features.SuppliersCollection;
using Barbearia.Application.Features.Products.Queries.GetAllProducts;
using Barbearia.Application.Features.Products.Queries.GetProductById;
using Barbearia.Application.Features.Products.Commands.CreateProduct;
using Barbearia.Application.Features.Products.Commands.DeleteProduct;
using Barbearia.Application.Features.Products.Commands.UpdateProduct;
using Barbearia.Application.Features.Employees.Queries.GetEmployeeById;
using Barbearia.Application.Features.Employees.Commands.CreateEmployee;
using Barbearia.Application.Features.Employees.Commands.UpdateEmployee;
using Barbearia.Application.Features.Employees.Commands.DeleteEmployee;
using Barbearia.Application.Features.EmployeesCollection;
using Barbearia.Application.Features.Coupons.Queries.GetCouponById;
using Barbearia.Application.Features.Coupons.Queries.GetAllCoupons;
using Barbearia.Application.Features.Coupons.Commands.UpdateCoupon;
using Barbearia.Application.Features.Coupons.Commands.CreateCoupon;
using Barbearia.Application.Features.Coupons.Commands.DeleteCoupon;
using Barbearia.Application.Features.StockHistories.Queries.GetStockHistoryById;
using Barbearia.Application.Features.StockHistories.Commands.CreateStockHistory;
using Barbearia.Application.Features.StockHistories.Commands.UpdateStockHistory;
using Barbearia.Application.Features.StockHistories.Commands.DeleteStockHistory;
using Barbearia.Application.Features.Schedules.Commands.CreateSchedule;
using Barbearia.Application.Features.Schedules.Queries.GetScheduleById;
using Barbearia.Application.Features.SchedulesCollection;
using Barbearia.Application.Features.Schedules.Queries.GetAllSchedules;
using Barbearia.Application.Features.Schedules.Commands.DeleteSchedule;
using Barbearia.Application.Features.Schedules.Commands.UpdateSchedule;
using Barbearia.Application.Features.ProductCategories.Queries.GetAllProductCategories;
using Barbearia.Application.Features.ProductCategories.Commands.UpdateProductCategory;
using Barbearia.Application.Features.ProductCategories.Commands.DeleteProductCategory;
using Barbearia.Application.Features.ProductCategories.Commands.CreateProductCategory;
using Barbearia.Application.Features.ProductCategories.Queries.GetProductCategoryById;
using Barbearia.Application.Features.Services.Queries.GetServiceById;
using Barbearia.Application.Features.Services.Commands.UpdateService;
using Barbearia.Application.Features.Services.Commands.CreateService;
using Barbearia.Application.Features.Services.Commands.DeleteService;
using Barbearia.Application.Features.ServicesCollection;
using Barbearia.Application.Features.Roles.Queries.GetAllRoles;
using Barbearia.Application.Features.Roles.Commands.CreateRole;
using Barbearia.Application.Features.Roles.Queries.GetRoleById;
using Barbearia.Application.Features.Roles.Commands.UpdateRole;
using Barbearia.Application.Features.Roles.Commands.DeleteRole;
using Barbearia.Application.Features.ServiceCategories.Queries.GetServiceCategoryById;
using Barbearia.Application.Features.ServiceCategories.Queries.GetAllServiceCategories;
using Barbearia.Application.Features.ServiceCategories.Commands.CreateServiceCategory;
using Barbearia.Application.Features.ServiceCategories.Commands.UpdateServiceCategory;
using Barbearia.Application.Features.ServiceCategories.Commands.DeleteServiceCategory;
using Barbearia.Application.Features.Appointments.Commands.UpdateAppointment;
using Barbearia.Application.Features.Appointments.Commands.DeleteAppointment;
using Barbearia.Application.Features.Appointments.Commands.CreateAppointment;
using Barbearia.Application.Features.Appointments.Queries.GetAppointmentById;
using Barbearia.Application.Features.Appointments.Queries.GetAllAppointments;
using Barbearia.Application.Features.AppointmentsCollection;
using Barbearia.Application.Features.WorkingDays.Query.GetWorkingDay;
using Barbearia.Application.Features.WorkingDays.Commands.CreateWorkingDay;
using Barbearia.Application.Features.WorkingDays.Commands.UpdateWorkingDay;
using Barbearia.Application.Features.WorkingDays.Commands.DeleteWorkingDay;
using Barbearia.Application.Features.TimesOff.Queries.GetTimeOffById;
using Barbearia.Application.Features.TimesOff.Commands.CreateTimeOff;
using Barbearia.Application.Features.TimesOff.Commands.UpdateTimeOff;
using Barbearia.Application.Features.TimesOff.Commands.DeleteTimeOff;
using Microsoft.Extensions.DependencyInjection;

namespace Barbearia.Application.Extensions;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddFluentValidationServices();

        return services;
    }
    public static void AddFluentValidationServices(this IServiceCollection services)
    {        
        // Customer commands and queries
        services.AddScoped<IRequestHandler<GetCustomersCollectionQuery, GetCustomersCollectionQueryResponse>, GetCustomersCollectionQueryHandler>();
        services.AddScoped<IRequestHandler<GetCustomerByIdQuery, GetCustomerByIdDto>, GetCustomerByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllCustomersQuery, IEnumerable<GetAllCustomersDto>>, GetAllCustomersQueryHandler>();
        services.AddScoped<IRequestHandler<GetCustomerWithOrdersByIdQuery, GetCustomerWithOrdersByIdDto>, GetCustomerWithOrdersByIdQueryHandler>();
        services.AddScoped<IRequestHandler<CreateCustomerCommand, CreateCustomerCommandResponse>, CreateCustomerCommandHandler>();
        services.AddScoped<IValidator<CreateCustomerCommand>, CreateCustomerCommandValidator>();
        services.AddScoped<IRequestHandler<UpdateCustomerCommand, UpdateCustomerCommandResponse>, UpdateCustomerCommandHandler>();
        services.AddScoped<IValidator<UpdateCustomerCommand>, UpdateCustomerCommandValidator>();
        services.AddScoped<IRequestHandler<DeleteCustomerCommand, bool>, DeleteCustomerCommandHandler>();
        // Supplier commands and queries
        services.AddScoped<IRequestHandler<GetSupplierByIdQuery, GetSupplierByIdDto>, GetSupplierByIdQueryHandler>();
        services.AddScoped<IRequestHandler<CreateSupplierCommand, CreateSupplierCommandResponse>, CreateSupplierCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateSupplierCommand, UpdateSupplierCommandResponse>, UpdateSupplierCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteSupplierCommand, bool>, DeleteSupplierCommandHandler>();
        // Address commands and queries
        services.AddScoped<IRequestHandler<GetAddressQuery, GetAddressQueryResponse>, GetAddressQueryHandler>();
        services.AddScoped<IRequestHandler<CreateAddressCommand, CreateAddressCommandResponse>, CreateAddressCommandHandler>();
        services.AddScoped<IValidator<CreateAddressCommand>, CreateAddressCommandValidator>();
        services.AddScoped<IRequestHandler<UpdateAddressCommand, UpdateAddressCommandResponse>, UpdateAddressCommandHandler>();
        services.AddScoped<IValidator<UpdateAddressCommand>, UpdateAddressCommandValidator>();
        services.AddScoped<IRequestHandler<DeleteAddressCommand, DeleteAddressCommandResponse>, DeleteAddressCommandHandler>();
        // Telephone commands and queries
        services.AddScoped<IRequestHandler<GetTelephoneQuery, GetTelephoneQueryResponse>, GetTelephoneQueryHandler>();
        services.AddScoped<IRequestHandler<CreateTelephoneCommand, CreateTelephoneCommandResponse>, CreateTelephoneCommandHandler>();
        services.AddScoped<IValidator<CreateTelephoneCommand>, CreateTelephoneCommandValidator>();
        services.AddScoped<IRequestHandler<UpdateTelephoneCommand, UpdateTelephoneCommandResponse>, UpdateTelephoneCommandHandler>();
        services.AddScoped<IValidator<UpdateTelephoneCommand>, UpdateTelephoneCommandValidator>();
        services.AddScoped<IRequestHandler<DeleteTelephoneCommand, DeleteTelephoneCommandResponse>, DeleteTelephoneCommandHandler>();
        // Order commands and queries
        services.AddScoped<IRequestHandler<GetOrdersCollectionQuery, GetOrdersCollectionQueryResponse>, GetOrdersCollectionQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllOrdersQuery, IEnumerable<GetAllOrdersDto>>, GetAllOrdersQueryHandler>();
        services.AddScoped<IRequestHandler<GetOrderByIdQuery, GetOrderByIdDto>, GetOrderByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetOrderByNumberQuery, GetOrderByNumberDto>, GetOrderByNumberQueryHandler>();
        services.AddScoped<IRequestHandler<CreateOrderCommand, CreateOrderCommandResponse>, CreateOrderCommandHandler>();
        services.AddScoped<IValidator<CreateOrderCommand>, CreateOrderCommandValidator>();
        services.AddScoped<IRequestHandler<UpdateOrderCommand, UpdateOrderCommandResponse>, UpdateOrderCommandHandler>();
        services.AddScoped<IValidator<UpdateOrderCommand>, UpdateOrderCommandValidator>();
        services.AddScoped<IRequestHandler<DeleteOrderCommand, bool>, DeleteOrderCommandHandler>();
        // Payment commands and queries
        services.AddScoped<IRequestHandler<GetSuppliersCollectionQuery, GetSuppliersCollectionQueryResponse>, GetSuppliersCollectionQueryHandler>();
        services.AddScoped<IRequestHandler<GetPaymentQuery, GetPaymentQueryResponse>, GetPaymentQueryHandler>();
        services.AddScoped<IRequestHandler<CreatePaymentCommand, CreatePaymentCommandResponse>, CreatePaymentCommandHandler>();
        services.AddScoped<IRequestHandler<UpdatePaymentCommand, UpdatePaymentCommandResponse>, UpdatePaymentCommandHandler>();
        services.AddScoped<IRequestHandler<DeletePaymentCommand, DeletePaymentCommandResponse>, DeletePaymentCommandHandler>();
        // Product commands and queries
        services.AddScoped<IRequestHandler<GetProductsCollectionQuery, GetProductsCollectionQueryResponse>, GetProductsCollectionQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllProductsQuery, IEnumerable<GetAllProductsDto>>, GetAllProductsQueryHandler>();
        services.AddScoped<IRequestHandler<GetProductByIdQuery, GetProductByIdDto>, GetProductByIdQueryHandler>();
        services.AddScoped<IRequestHandler<CreateProductCommand, CreateProductCommandResponse>, CreateProductCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteProductCommand, bool>, DeleteProductCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateProductCommand, UpdateProductCommandResponse>, UpdateProductCommandHandler>();
        // Employee commands and queries
        services.AddScoped<IRequestHandler<GetEmployeeByIdQuery, GetEmployeeByIdDto>, GetEmployeeByIdQueryHandler>();
        services.AddScoped<IRequestHandler<CreateEmployeeCommand, CreateEmployeeCommandResponse>, CreateEmployeeCommandHandler>();
        services.AddScoped<IValidator<CreateEmployeeCommand>, CreateEmployeeCommandValidator>();
        services.AddScoped<IRequestHandler<UpdateEmployeeCommand, UpdateEmployeeCommandResponse>, UpdateEmployeeCommandHandler>();
        services.AddScoped<IValidator<UpdateEmployeeCommand>, UpdateEmployeeCommandValidator>();
        services.AddScoped<IRequestHandler<DeleteEmployeeCommand, bool>, DeleteEmployeeCommandHandler>();
        services.AddScoped<IRequestHandler<GetEmployeesCollectionQuery, GetEmployeesCollectionQueryResponse>, GetEmployeesCollectionQueryHandler>();
        // Coupon commands and queries
        services.AddScoped<IRequestHandler<GetCouponByIdQuery, GetCouponByIdDto>, GetCouponByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllCouponsQuery, IEnumerable<GetAllCouponsDto>>, GetAllCouponsQueryHandler>();
        services.AddScoped<IRequestHandler<UpdateCouponCommand, UpdateCouponCommandResponse>, UpdateCouponCommandHandler>();
        services.AddScoped<IRequestHandler<CreateCouponCommand, CreateCouponCommandResponse>, CreateCouponCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteCouponCommand, bool>, DeleteCouponCommandHandler>();

        services.AddScoped<IRequestHandler<GetStockHistoryByIdQuery, GetStockHistoryByIdDto>, GetStockHistoryByIdQueryHandler>();
        // services.AddScoped<IRequestHandler<GetAllStockHistoriesQuery, IEnumerable<GetAllStockHistoriesDto>>, GetAllStockHistoriesQueryHandler>();
        services.AddScoped<IRequestHandler<CreateStockHistoryCommand, CreateStockHistoryCommandResponse>, CreateStockHistoryCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateStockHistoryCommand, UpdateStockHistoryCommandResponse>, UpdateStockHistoryCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteStockHistoryCommand, bool>, DeleteStockHistoryCommandHandler>();
        // Schedule commands and queries
        services.AddScoped<IRequestHandler<GetSchedulesCollectionQuery, GetSchedulesCollectionQueryResponse>, GetSchedulesCollectionQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllSchedulesQuery, IEnumerable<GetAllSchedulesDto>>, GetAllSchedulesQueryHandler>();
        services.AddScoped<IRequestHandler<GetScheduleByIdQuery, GetScheduleByIdDto>, GetScheduleByIdQueryHandler>();
        services.AddScoped<IRequestHandler<CreateScheduleCommand, CreateScheduleCommandResponse>, CreateScheduleCommandHandler>();
        services.AddScoped<IValidator<CreateScheduleCommand>, CreateScheduleCommandValidator>();
        services.AddScoped<IRequestHandler<DeleteScheduleCommand, bool>, DeleteScheduleCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateScheduleCommand, UpdateScheduleCommandResponse>, UpdateScheduleCommandHandler>();
        services.AddScoped<IValidator<UpdateScheduleCommand>, UpdateScheduleCommandValidator>();
        // Product Category commands and queries
        services.AddScoped<IRequestHandler<GetAllProductCategoriesQuery, IEnumerable<GetAllProductCategoriesDto>>, GetAllProductCategoriesQueryHandler>();
        services.AddScoped<IRequestHandler<GetProductCategoryByIdQuery, GetProductCategoryByIdDto>, GetProductCategoryByIdQueryHandler>();
        services.AddScoped<IRequestHandler<CreateProductCategoryCommand, CreateProductCategoryCommandResponse>, CreateProductCategoryCommandHandler>();
        services.AddScoped<IValidator<CreateProductCategoryCommand>, CreateProductCategoryCommandValidator>();
        services.AddScoped<IRequestHandler<DeleteProductCategoryCommand, bool>, DeleteProductCategoryCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateProductCategoryCommand, UpdateProductCategoryCommandResponse>, UpdateProductCategoryCommandHandler>();
        services.AddScoped<IValidator<UpdateProductCategoryCommand>, UpdateProductCategoryCommandValidator>();
        // Service commands and queries
        services.AddScoped<IRequestHandler<GetServiceByIdQuery, GetServiceByIdDto>, GetServiceByIdQueryHandler>();
        services.AddScoped<IRequestHandler<UpdateServiceCommand, UpdateServiceCommandResponse>, UpdateServiceCommandHandler>();
        services.AddScoped<IValidator<UpdateServiceCommand>, UpdateServiceCommandValidator>();
        services.AddScoped<IRequestHandler<CreateServiceCommand, CreateServiceCommandResponse>, CreateServiceCommandHandler>();
        services.AddScoped<IValidator<CreateServiceCommand>, CreateServiceCommandValidator>();
        services.AddScoped<IRequestHandler<DeleteServiceCommand, bool>, DeleteServiceCommandHandler>();
        services.AddScoped<IRequestHandler<GetServicesCollectionQuery, GetServicesCollectionQueryResponse>, GetServicesCollectionQueryHandler>();
        //Role commands and queries
        services.AddScoped<IRequestHandler<GetRoleByIdQuery, GetRoleByIdDto>, GetRoleByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllRolesQuery, IEnumerable<GetAllRolesDto>>, GetAllRolesQueryHandler>();
        services.AddScoped<IRequestHandler<UpdateRoleCommand, UpdateRoleCommandResponse>, UpdateRoleCommandHandler>();
        services.AddScoped<IRequestHandler<CreateRoleCommand, CreateRoleCommandResponse>, CreateRoleCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteRoleCommand, bool>, DeleteRoleCommandHandler>();
        //ServiceCategory commands and queries
        services.AddScoped<IRequestHandler<GetServiceCategoryByIdQuery, GetServiceCategoryByIdDto>, GetServiceCategoryByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetAllServiceCategoriesQuery, IEnumerable<GetAllServiceCategoriesDto>>, GetAllServiceCategoriesQueryHandler>();
        services.AddScoped<IRequestHandler<UpdateServiceCategoryCommand, UpdateServiceCategoryCommandResponse>, UpdateServiceCategoryCommandHandler>();
        services.AddScoped<IRequestHandler<CreateServiceCategoryCommand, CreateServiceCategoryCommandResponse>, CreateServiceCategoryCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteServiceCategoryCommand, bool>, DeleteServiceCategoryCommandHandler>();
        // Appointments Commands and Queries
        services.AddScoped<IRequestHandler<GetAllAppointmentsQuery, IEnumerable<GetAllAppointmentsDto>>, GetAllAppointmentsQueryHandler>();
        services.AddScoped<IRequestHandler<GetAppointmentByIdQuery, GetAppointmentByIdDto>, GetAppointmentByIdQueryHandler>();
        services.AddScoped<IRequestHandler<CreateAppointmentCommand, CreateAppointmentCommandResponse>, CreateAppointmentCommandHandler>();
        services.AddScoped<IValidator<CreateAppointmentCommand>, CreateAppointmentCommandValidator>();
        services.AddScoped<IRequestHandler<DeleteAppointmentCommand, bool>, DeleteAppointmentCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateAppointmentCommand, UpdateAppointmentCommandResponse>, UpdateAppointmentCommandHandler>();
        services.AddScoped<IValidator<UpdateAppointmentCommand>, UpdateAppointmentCommandValidator>();
        services.AddScoped<IRequestHandler<GetAppointmentsCollectionQuery, GetAppointmentsCollectionQueryResponse>, GetAppointmentsCollectionQueryHandler>();
        //WorkingDays Commands and queries
        services.AddScoped<IRequestHandler<GetWorkingDayQuery, GetWorkingDayQueryResponse>, GetWorkingDayQueryHandler>();
        services.AddScoped<IRequestHandler<CreateWorkingDayCommand, CreateWorkingDayCommandResponse>, CreateWorkingDayCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateWorkingDayCommand, UpdateWorkingDayCommandResponse>, UpdateWorkingDayCommandHandler>();
        services.AddScoped<IValidator<UpdateWorkingDayCommand>, UpdateWorkingDayCommandValidator>();
        services.AddScoped<IRequestHandler<DeleteWorkingDayCommand, DeleteWorkingDayCommandResponse>, DeleteWorkingDayCommandHandler>();
        //TimeOffs Commands and queries
        services.AddScoped<IRequestHandler<GetTimeOffByIdQuery, GetTimeOffByIdDto>, GetTimeOffByIdQueryHandler>();
        services.AddScoped<IRequestHandler<CreateTimeOffCommand, CreateTimeOffCommandResponse>, CreateTimeOffCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateTimeOffCommand, UpdateTimeOffCommandResponse>, UpdateTimeOffCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteTimeOffCommand, bool>, DeleteTimeOffCommandHandler>();
    }
}