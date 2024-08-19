using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;
using Volo.Abp.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;



[DependsOn(
    typeof(AbpAspNetCoreMvcModule),
    typeof(AbpAutofacModule),
    typeof(AbpMongoDbModule)
)]
public class MinimalModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddEndpointsApiExplorer();
        context.Services.AddSwaggerGen();

        Configure<AbpMongoModelBuilderConfigurationOptions>(options =>
        {
            options.CollectionPrefix = "MyApp_";
        });

        context.Services.AddMongoDbContext<MyDbContext>(options =>
        {
            options.AddDefaultRepositories();
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
    }
}