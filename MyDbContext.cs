using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

[ConnectionStringName("Default")]
public class MyDbContext : AbpMongoDbContext
{
    public IMongoCollection<Book> Books => Collection<Book>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);
        modelBuilder.Entity<Book>(b =>
        {
            b.CollectionName = "Books";
        });
    }
}
