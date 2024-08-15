
using ChatCommon.Abstarcts;
using ChatCommon.Models;
using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Service
{
    public class MessageSource : IMessageSourse<RouterSocket>
    {
        // Получаем сообщение от библиотеки NetMQ
        public NetMessage ReceiveMQ(RouterSocket socket) // Тут а нас сложная манипуляция. Есть вариант по проще, но н требует изменения логики. Но мне интересно было сохранить "легаси код".
        {
            var answerSocket = socket.ReceiveMultipartMessage(); // Получаем сообщение. Формат NetMQMessage, состоящий из сообщения, в котором храниться сериализованный класс NetMessage и Адрес отправителя.
            
            
            byte[] buffer = answerSocket.Last.ToByteArray();   // Идея в том, что получать именно NetMessage на выходе, но с сохранённым адресом отправителя. Для начала получаем самк Класс сообщения. Он в байтах, поэтому получаем массив байт.
            string str = Encoding.UTF8.GetString(buffer);  // Декодируем в строку. 
            if (answerSocket == null || str.Length == 0) // Проверочки на то, что нам пришло хоть что-то. Если нет, то мы возращаем пустой класс, который просто игнорируется.
            { 
                return new NetMessage();
            }
            else // Если есть хоть что-то, то мы пытаемся десериализовать это в NetMessage
            {

                var temp = NetMessage.DeserializeMessgeFromJSON(str); // Но не возращаем его сразу же, а сохраняем.
                if (answerSocket.First == null && 0 < answerSocket.First.MessageSize)
                {
                    Console.WriteLine("Отпровитель неизвестен. Содержимое сообщения очищено");
                    return new NetMessage();
                }
                else
                {
                    var NetMq = new NetMQMessage();
                    NetMq.Append(answerSocket.First);
                    temp.NetMQFrames = NetMq; // И добавляем в новое поле. NetMQFrames данные об отправителе. По сути это наш аналог Эндпоинта для UDP
                }
                return temp;
            }
                
        }

        // Отправляем сообщение от библиотеки NetMQ
        public async Task SendAsyncMQ(NetMessage message, NetMQMessage netMQFrames, RouterSocket socket) // Пересылка сообщения с сервера. Получаем класс сообщения, получаем адрес, и сокет для метода отправки.
        {
            message.NetMQFrames = null;                                                                  // ВОТ ЭТА ГАДОСТЬ ПОТРЕБОВАЛА С ПАРУ ЧАСОВ ДЕБАГА. ппздц
            byte[] buffer = Encoding.UTF8.GetBytes(message.SerialazeMessagerToJSON());                   // Сереализуем класс сообщения.
            netMQFrames.Append(buffer);                                                                  // Добавляем результат MQ сообщение вторым фреймом. По идеи, у нас теперь есть адрес и само сериализованное сообщение.
            socket.SendMultipartMessage(netMQFrames);                                                    // Отправляем по адресу. Пересылка завершена. 
        }

        public RouterSocket CreatSocket()
        {
            return new RouterSocket();
        }

        /* Этот метод не нужен(как и много чего ещё), даже если пытаться наятнуть сову на глобус. Глобус - квадратный.
        public RouterSocket CopySocket(RouterSocket socket)
        {
            return new RouterSocket(); //???
        }
        */





        // Отправка сообщения.
        /*
        public async Task SendAsync(NetMessage message, IPEndPoint iPEndPoint, UdpClient UdpClient)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message.SerialazeMessagerToJSON());
            await UdpClient.SendAsync(buffer, buffer.Length, iPEndPoint);
        }
        // Получение сообщения.
        public NetMessage Receive(ref IPEndPoint iPEndPoint, ref UdpClient UdpClient)
        {
            byte[] buffer = UdpClient.Receive(ref iPEndPoint);
            string str = Encoding.UTF8.GetString(buffer);
            if (str == null || str.Length == 0) { return new NetMessage(); } else return NetMessage.DeserializeMessgeFromJSON(str);
        }
        */
    }
}
