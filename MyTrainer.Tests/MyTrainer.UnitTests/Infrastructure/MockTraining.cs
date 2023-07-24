using MyTrainer.Domain;
using MyTrainer.Application.Extensions;

namespace MyTrainer.UnitTests.Infrastructure;

internal static class MockTraining
{
    public static Training Training = new Training()
    {
        CreationDate = DateTime.Now.ToDateOnly(),
        IsCompleted = false,
        Id = new Guid("0c4ca7e9-7e52-49fb-9925-7e278dff96bf"),
        TrainerId = new Guid("2127f3b0-4d7e-4d35-ab30-0c926dcc5795"),
        UserId = new Guid("2127f3b0-4d7e-4d35-ab30-0c926dcc5795"),
        Description = "foo",
        EditDate = null,
        Name = "baz"
    };

    public static Training WithId(Guid guid)
        => new Training()
        {
            CreationDate = DateTime.Now.ToDateOnly(),
            IsCompleted = false,
            Id = guid,
            TrainerId = new Guid("2127f3b0-4d7e-4d35-ab30-0c926dcc5795"),
            UserId = new Guid("2127f3b0-4d7e-4d35-ab30-0c926dcc5795"),
            Description = "foo",
            EditDate = null,
            Name = "baz"
        };
}

