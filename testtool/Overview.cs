/**
{
  "management_version": "3.7.7",
  "rates_mode": "basic",
  "exchange_types": [
    {
      "name": "headers",
      "description": "AMQP headers exchange, as per the AMQP specification",
      "enabled": true
    },
    {
      "name": "direct",
      "description": "AMQP direct exchange, as per the AMQP specification",
      "enabled": true
    },
    {
      "name": "topic",
      "description": "AMQP topic exchange, as per the AMQP specification",
      "enabled": true
    },
    {
      "name": "fanout",
      "description": "AMQP fanout exchange, as per the AMQP specification",
      "enabled": true
    }
  ],
  "rabbitmq_version": "3.7.7",
  "cluster_name": "rabbit@kanin1",
  "erlang_version": "20.3.8.1",
  "erlang_full_version": "Erlang/OTP 20 [erts-9.3.3] [source] [64-bit] [smp:2:2] [ds:2:2:10] [async-threads:64] [hipe] [kernel-poll:true]",
  "message_stats": {},
  "queue_totals": {},
  "object_totals": {
    "channels": 0,
    "connections": 0,
    "consumers": 0,
    "exchanges": 7,
    "queues": 0
  },
  "statistics_db_event_queue": 0,
  "node": "rabbit@kanin1",
  "listeners": [
    {
      "node": "rabbit@kanin1",
      "protocol": "amqp",
      "ip_address": "::",
      "port": 5672,
      "socket_opts": {
        "backlog": 128,
        "nodelay": true,
        "linger": [
          true,
          0
        ],
        "exit_on_close": false
      }
    },
    {
      "node": "rabbit@kanin1",
      "protocol": "clustering",
      "ip_address": "::",
      "port": 25672,
      "socket_opts": []
    },
    {
      "node": "rabbit@kanin1",
      "protocol": "http",
      "ip_address": "::",
      "port": 15672,
      "socket_opts": {
        "ssl": false,
        "port": 15672
      }
    }
  ],
  "contexts": [
    {
      "ssl_opts": [],
      "node": "rabbit@kanin1",
      "description": "RabbitMQ Management",
      "path": "/",
      "ssl": "false",
      "port": "15672"
    }
  ]
}*/
namespace RabbitREPL
{
    internal class Overview
    {
        public string ManagementVersion { get; set; }

        public string RabbitmqVersion { get; set; }

        public string ErlangVersion { get; set; }

        public string ClusterName { get; set; }

        public string Node { get; set; }

        public override string ToString()
        {
            return string.Format("Connected to cluster: {0}, at the node: {1}, running RabbitMQ version: {2}", ClusterName, Node, RabbitmqVersion);
        }

    }
}