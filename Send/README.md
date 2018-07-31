# RabbitMQ Test Tool

## Start local rabbit in Docker
    docker run -d -h kanin1 --name kanin1 -p 8080:15672 -e RABBITMQ_ERLANG_COOKIE='kaka' rabbitmq:managemen
t
