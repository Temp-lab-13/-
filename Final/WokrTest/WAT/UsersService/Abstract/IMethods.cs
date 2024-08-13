using UsersService.Models.EssenceModel;

namespace UsersService.Abstract
{
    public interface IMethods // Сори за ужасное название, спешу хоть что-то реализовать
    {
        public bool sendMessedg(string adress, string topic, string text); // отправляем сообщение юзеру с таким то адресом.
                                                                           // Указывая оглавление сообщения(тему), и само сообщение.
        public IEnumerable<Message> sendMessedg(UserModel user); // Список полученных сообщений. В теории.
    }
}
