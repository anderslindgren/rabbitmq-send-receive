namespace RabbitREPL

/**
 [
    {
        "message_stats": {
            "deliver_get_details": {
                "rate": 0.0
            },
            "deliver_get": 6,
            "ack_details": {
                "rate": 0.0
            },
            "ack": 0,
            "redeliver_details": {
                "rate": 0.0
            },
            "redeliver": 0,
            "deliver_no_ack_details": {
                "rate": 0.0
            },
            "deliver_no_ack": 0,
            "deliver_details": {
                "rate": 0.0
            },
            "deliver": 0,
            "get_no_ack_details": {
                "rate": 0.0
            },
            "get_no_ack": 6,
            "get_details": {
                "rate": 0.0
            },
            "get": 0,
            "return_unroutable_details": {
                "rate": 0.0
            },
            "return_unroutable": 0,
            "confirm_details": {
                "rate": 0.0
            },
            "confirm": 0,
            "publish_details": {
                "rate": 0.0
            },
            "publish": 6
        },
        "messages_details": {
            "rate": 0.0
        },
        "messages": 0,
        "messages_unacknowledged_details": {
            "rate": 0.0
        },
        "messages_unacknowledged": 0,
        "messages_ready_details": {
            "rate": 0.0
        },
        "messages_ready": 0,
        "cluster_state": {
            "rabbit@kanin1": "running"
        },
        "tracing": false,
        "name": "/"
    }
]
 */
{
    internal class VHost
    {
        public int Name { get; set; }
        public int ClusterState { get; set; }
        public int Messages { get; set; }
        public int MessagesReady { get; set; }
        public int MessagesUnacknowledged { get; set; }

        public override string ToString()
        {
            return $"VHost: {Name}\nCluster State: {ClusterState}\nMessages: {Messages}\n" +
                $"Messages Ready: {MessagesReady}\nMessages Unacked: {MessagesUnacknowledged}";
        }
    }
}