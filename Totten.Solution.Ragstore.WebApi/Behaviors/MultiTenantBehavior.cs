namespace Totten.Solution.Ragstore.WebApi.Behaviors;

using Autofac;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;

public class MultiTenantBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, Result<Exception, TResponse>> where TRequest : notnull
{

    public MultiTenantBehavior()
    {
    }

    public Task<Result<Exception, TResponse>> Handle(TRequest request,
        RequestHandlerDelegate<Result<Exception, TResponse>> next,
        CancellationToken cancellationToken)
    {
        //string dbName = "DBTesting";

        //var dbContextOptionsBuilder = new DbContextOptionsBuilder<RagnaStoreContext>();
        //dbContextOptionsBuilder.UseSqlServer(SysConstantDBConfig.DEFAULT_CONNECTION_STRING.Replace("{db}", dbName ?? "RagnaStoreContext"));


        return next();
    }
}
