using ProductSpace.Services;

namespace ProductSpace.Middleware
{
    public class TenantResolver
    {
        private readonly RequestDelegate _next;

        public TenantResolver(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ICurrentTeanantService currentTeanantService)
        {
            context.Request.Headers.TryGetValue("tenant", out var tenantFromHeader);
            if(string.IsNullOrEmpty(tenantFromHeader) == false)
            {
               await currentTeanantService.SetTenant(tenantFromHeader);

            }
            await _next(context);
        }
    }
}
