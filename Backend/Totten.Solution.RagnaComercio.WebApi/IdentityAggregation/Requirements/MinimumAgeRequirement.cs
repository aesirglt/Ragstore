﻿namespace Totten.Solution.RagnaComercio.WebApi.IdentityAggregation.Requirements;

using Microsoft.AspNetCore.Authorization;

/// <summary>
/// 
/// </summary>
public class MinimumAgeRequirement : IAuthorizationRequirement
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="minimumAge"></param>
    public MinimumAgeRequirement(int minimumAge) => MinimumAge = minimumAge;

    /// <summary>
    /// 
    /// </summary>
    public int MinimumAge { get; }
}