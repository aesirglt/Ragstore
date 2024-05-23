namespace Totten.Solution.Ragstore.ApplicationService.Notifications.Stores.Handlers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.Domain.Features.HistoricAggregation.Interfaces;

public class SearchedItemNotificationHandler : INotificationHandler<SearchedItemNotification>
{
    private IItemHistoricRepository _repository;

    public SearchedItemNotificationHandler(IItemHistoricRepository repository)
    {
        _repository = repository;
    }

    public Task Handle(SearchedItemNotification notification, CancellationToken cancellationToken)
    {

        var itemHistoric = _repository.GetByItemId(notification.ItemId);

        if (itemHistoric != null)
        {
            //disparar mensagem para entrar em uma fila de processamento e dessa maneira possa nao ter risco
            //porque se atualizar aqui da maneira abaixo:
            // itemHistoric.Quantity += 1;
            // pode acarretar em falha pois pode ter outra notificação de item procurado e da concorrencia nao atualizando o valor de forma correta.
            //procurar entender uma maneira, se com rabbitmq ou outro esquema
            //talvez uma tabela que receba todos os historicos de item e depois só some,
            //ou melhor a tabela de historico de item so vai adicionando, mas dai fica muito grande.
            //melhor salvar uma tabela de tarefas e depois pegar de tempos em tempos
        }

        return Task.CompletedTask;
    }
}
