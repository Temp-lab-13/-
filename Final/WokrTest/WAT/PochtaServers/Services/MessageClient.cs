using AutoMapper;
using PochtaServers.Abstract;
using PochtaServers.Models.Context;
using PochtaServers.Models.EssenceModel;
using PochtaServers.Models.EssenceModel.Dto;
using System.Linq;

namespace PochtaServers.Services
{
    public class MessageClient : IMessageClient
    {
        private IMapper _mapper;
        private AppDbContext _context;

        public MessageClient(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _context = appDbContext;
        }
        public void addClient(ClientDto client)
        {
            using (_context)
            {
                _context.Clients.Add(_mapper.Map<Client>(client));
                _context.SaveChanges();
            }
        }

        public void addEmail(MessageDto email)
        {
            _context.Messages.Add(_mapper.Map<Message>(email));
            _context.SaveChanges();
        }

        public IEnumerable<ClientDto> getClient()
        {
            var client = _context.Clients.Select(_mapper.Map<ClientDto>).ToList();
            return client;
        }

        public IEnumerable<MessageDto> getEmail()
        {
            var mail = _context.Messages.Select(_mapper.Map<MessageDto>).ToList();
            return mail;
        }
    }
}
