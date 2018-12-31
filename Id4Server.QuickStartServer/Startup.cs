using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Id4Server.QuickStartServer
{
    public class Startup
    {       


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                //.AddSigningCredential() -- For providing the production certificate public key
                .AddInMemoryClients(Config.GetClients()) //List of Client Applications
                .AddInMemoryApiResources(Config.GetApiResources()) //List of APIs
                .AddTestUsers(Config.GetUsers()); //Need to replace with EF core
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
        }
    }
}
