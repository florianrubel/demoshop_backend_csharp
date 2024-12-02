using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Scalar.AspNetCore;
using Shared.Models.OpenApi;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shared.Startup
{
    public static class OpenApi
    {
        public const string DEVELOPER_EMAIL = "florianrubel@display-awesome.com";
        public const string DEVELOPER_FULL_NAME = "Florian Rubel";

        public static void Register(WebApplicationBuilder builder, OpenApiMeta meta)
        {
            builder.Services.AddOpenApi(options =>
            {
            });
        }

        public static void PostBuild(WebApplication app, OpenApiMeta meta)
        {
            app.MapOpenApi();
            app.MapScalarApiReference(options =>
            {
                options
                    .WithPreferredScheme("Bearer")
                    .WithTestRequestButton(false)
                    .WithDarkModeToggle(false)
                    .WithCustomCss(@"
.scalar-app {
    position: releative !important;
}
body:before {
    background: radial-gradient(ellipse at bottom right, hsl(323, 100%, 81%) 0%, hsl(211, 100%, 78%) 100%);
    background-position: fixed;
    background-size: 100vw 100vh;
    background-repeat: no-repeat;
    content: '';
    pointer-events: none;
    position: fixed;
    top: 0;
    left: 0;
    height: 100vh;
    width: 100vw;
    z-index: 0;
}
.references-rendered {
    background: none !important;
}
.references-navigation-list {
    background: none !important;
    backdrop-filter: brightness(0.8) !important;
}
.sidebar {
    background: none !important;
}
.property-detail-value {
    color: hsl(192, 100%, 35%) !important;
}
body .property-description * {
    opacity: 0.7;
}
body .scalar-api-reference,
body .scalar-card--muted,
body .scalar-card,
body .endpoints,
body .scalar-api-reference,
body .scalar-code-block,
body .scalar-card--contrast,
body .scalar-card-container,
body .request-body-title-select,
body .request-body-title-select select,
body .introduction-card,
body .selected-client,
body .client-libraries-content {
    background: none !important;
    background-color: transparent !important;
}
body .scalar-card,
body .request-body-title-select,
body .introduction-card,
body .server-form-container,
body .selected-client,
body .client-libraries-content {
    backdrop-filter: brightness(0.6) !important;
}
body .request-body-title-select select option {
    background: var(--scalar-background-3);
}
body .server-form-container {
    background: color-mix(in srgb, var(--scalar-background-2) 40%, transparent 60%) !important;
}

body .z-overlay .bg-b-1 {
    background: none !important;
    backdrop-filter: brightness(60%) blur(25px) !important;
}

body ul[role='listbox'] {
    padding: 0 !important;
}
body ul[role='listbox'] li {
    margin: 0 !important;
    padding: 6px !important;
}

body.dark-mode:before {
    background: radial-gradient(ellipse at bottom right, hsl(323, 100%, 21%) 0%, hsl(211, 100%, 18%) 100%);
}
                ");
            });
        }
    }
}
