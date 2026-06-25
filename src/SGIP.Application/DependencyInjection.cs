namespace SGIP.Application;

using Microsoft.Extensions.DependencyInjection;
using SGIP.Application.Interfaces.Services;
using SGIP.Application.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<LoanCalculatorService>();
        services.AddScoped<ILoanService, LoanService>();
        services.AddScoped<ITransactionService, TransactionService>();
        return services;
    }
}
