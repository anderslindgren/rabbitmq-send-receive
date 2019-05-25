# RabbitMQ Test Tool

## Start local rabbit in Docker

    docker run -d -h kanin1 --name kanin1 -p 8080:15672 -e RABBITMQ_ERLANG_COOKIE='kaka' rabbitmq:management

## Commands
Commands with `(*` is not yet implemented

    help
    connect admin [user password]
    connect client [user password [virtualHost]]

### Commands that will work when the Admin user is connected
      list users
      list nodes (*
      list connections (*
      list channels (*
      list consumers (*
      list exchanges (*
      list vhosts (*
      list queue (*
      list queues (*
      list bindings [vhost] [exchange (exhange|queue)] (*
      list permissions (*
      list topic-permissions (*
      list parameters [component [vhost]] (*
      list global-parameters (*
      list policies [vhost] (*
      list operator-policies [vhost] (*
      list vhost-limits [vhost] (*
      list extensions (*
      list definitions (*

      get user

      set cluster-name name (*
      set permissions user [vhost] configure read write (*
      set topic-permissions user [vhost] exchange read write (*

      purge queue name [vhost} (*
  
      remove vhost name (*
      remove user name (*

      add vhost name (*
      add user name password tags (*
      add queue name [vhost] (*
      add bindings [vhost] [exchange (exhange|queue)] (*

      test alive (*
      test health (*

      whoami (*

### Commands that will work when the Client user is connected