namespace Common.Tests.ObjectMothers;

using Totten.Solution.Ragstore.ApplicationService.Features.Servers.Commands;
public static partial class ObjectMother
{
    public static ServerCreateCommand GetServerCreate() => new()
    {
        Name = "bRO - thor",
        SiteUrl = "https://playragnarokonlinebr.com/"
    };
}
