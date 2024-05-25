namespace Totten.Solution.Ragstore.ApplicationService.Features.ItemsAggregation.Commands;

using LanguageExt.Common;
using MediatR;
using System;
using Unit = LanguageExt.Unit;

public class ItemCreateCommand : IRequest<Result<Unit>>
{
}
