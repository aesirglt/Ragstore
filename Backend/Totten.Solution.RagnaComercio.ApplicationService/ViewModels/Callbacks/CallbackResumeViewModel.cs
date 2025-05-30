﻿namespace Totten.Solution.RagnaComercio.ApplicationService.ViewModels.Callbacks;
/// <summary>
/// 
/// </summary>
public record CallbackResumeViewModel
{
    public Guid Id { get; set; }
    public int ItemId { get; set; }
    public double ItemPrice { get; set; }
    public string ServerName { get; set; } = string.Empty;
    public string StoreType { get; set; } = string.Empty;
    public required string ItemUrl { get; set; }
}