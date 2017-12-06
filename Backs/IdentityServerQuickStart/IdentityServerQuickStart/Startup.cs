using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServerQuickStart
{
    public class Startup
    {

        //AddIdentityServer registers the IdentityServer services in DI.It also registers an in-memory store for 
        //    runtime state.This is useful for development scenarios. For production scenarios you need a persistent or 
        //        shared store like a database or cache for that.See the EntityFramework quickstart for more information.


        //The AddDeveloperSigningCredential extension creates temporary key material for signing tokens. 
        //        Again this might be useful to get started, but needs to be replaced by some persistent key material 
        //        for production scenarios. See the cryptography docs for more information.

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()
                        .AddDeveloperSigningCredential()
                            .AddInMemoryApiResources(Config.GetApiResources())
                                .AddInMemoryClients(Config.GetClients());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
