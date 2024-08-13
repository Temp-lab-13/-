using PochtaServers.Models.EssenceModel.Dto;
using System.Collections.Generic;

namespace PochtaServers.Abstract
{
    public interface IMessageClient
    {
        public IEnumerable<ClientDto> getClient();
        public IEnumerable<MessageDto> getEmail();
        public void addEmail(MessageDto email);
        public void addClient(ClientDto client);

    }
}
