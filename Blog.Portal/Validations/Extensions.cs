using FluentValidation.AspNetCore;
using System.Reflection;

namespace Blog.Portal.Validations;

internal static class Extensions
{
    public static IServiceCollection AddValidators(this IServiceCollection @this)
    {
        @this.AddFluentValidation(options =>
        {
            // Validate child properties and root collection elements
            options.ImplicitlyValidateChildProperties = true;
            options.ImplicitlyValidateRootCollectionElements = true;

            // Automatic registration of validators in assembly
            options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        });
        return @this;
    }
}
