namespace Totten.Solution.RagnaComercio.Infra.Data.Seeds;
using System;
using System.Collections.Generic;
using Totten.Solution.RagnaComercio.Domain.Features.AgentAggregation;

public static class MyAgentSeed
{
    public static List<Agent> Seed()
    => [
        new ()
        {
            Id = Guid.Parse("89300f33-33d7-4878-af08-1f7b694eca3f"),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Name = "openkore",
            IsActive = true,
            ServerId = MyServerSeed.Id
        }
    ];
}