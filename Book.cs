using System;
using Volo.Abp.Domain.Entities.Auditing;

public class Book : AuditedAggregateRoot<Guid>
{
    public Book(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Name { get; set; }
}
