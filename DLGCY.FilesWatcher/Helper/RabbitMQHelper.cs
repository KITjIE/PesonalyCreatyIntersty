using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLGCY.FilesWatcher.Helper
{
    public class RabbitMQHelper : IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _exchangeName;

        public RabbitMQHelper(string hostname, int port, string exchangeName)
        {
            var factory = new ConnectionFactory()
            {
                HostName = hostname,
                Port = port,
                UserName = "guest", // 默认用户名
                Password = "guest"  // 默认密码
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _exchangeName = exchangeName;

            _channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Topic, durable: true);
        }

        public void SendMessage(string message, string routingKey)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: _exchangeName, routingKey: routingKey, basicProperties: null, body: body);
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }
}


