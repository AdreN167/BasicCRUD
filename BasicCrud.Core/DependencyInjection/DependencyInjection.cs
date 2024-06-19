using BasicCrud.Core.Services;
using BasicCrud.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BasicCrud.Core.DependencyInjection;

public static class DependencyInjection
{
    public static void AddCore(this IServiceCollection services)
    {
        services.AddScoped<INoteService, NoteService>();
    }
}

