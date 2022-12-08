using Domain.Entities;
using Vonage.Messaging;

namespace Services.EntitiesServices.ISmsServices
{
    public interface ISmsService
    {
        SendSmsRequest AddSmartfonOrder();
        SendSmsRequest AddKomputerOrder();
        SendSmsRequest AddTvOrder();
    }
}
