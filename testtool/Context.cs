using System;
using System.Collections.Generic;
using RabbitMQ.Client;
using RestSharp;

namespace RabbitREPL
{
    internal class Context
    {
        public RestClient AdminClient { get; internal set; }
        public IConnection Connection { get; internal set; }
        public IModel Channel { get; internal set; }
        public User AdminUser { get; }
        public User User { get; internal set; }
        public string Hostname { get; }
        public int AdminPort { get; }
        public string VirtualHost { get; internal set; }

        public Dictionary<string, Type> Commands
        {
            get
            {
                if (State == MODES.ADMIN)
                {
                    return AdminCommands;
                }
                else
                {
                    return TestCommands;
                }
            }
            internal set { }
        }

        public Dictionary<string, Type> AdminCommands { get; private set; }
        public Dictionary<string, Type> TestCommands { get; private set; }

        public void AdminMode() => State = MODES.ADMIN;
        public void TestMode() => State = MODES.TEST;

        private enum MODES
        {
            ADMIN, TEST
        }
        private MODES State = MODES.ADMIN;

        public Context(Options options)
        {
            DefineCommands();
            AdminUser = new User()
            {
                Username = options.AdminUser,
                Password = options.AdminPassword,
            };
            User = new User()
            {
                Username = options.User,
                Password = options.Password,
            };
            Hostname = options.Hostname;
            AdminPort = options.AdminPort;
            if (string.IsNullOrEmpty(options.VirtualHost))
            {
                VirtualHost = ConnectionFactory.DefaultVHost;
            }
            else
            {
                VirtualHost = options.VirtualHost;
            }
        }

        private void DefineCommands()
        {
            AdminCommands = new Dictionary<string, Type>
            {
                { "overview", typeof(OverviewCommand)  },
                // cluster-name
                // nodes
                // extensions
                // definitions
                // connections
                // channels
                // consumers
                // exchanges
                // queues
                // bindings
                // vhosts
                // users
                { "user",     typeof(UserCommand)      },
                { "whoami",   typeof(WhoAmICommand)    },
                // permissions
                // topic-permissions
                // parameters
                // global-parameters
                // policies
                // operator-policies
                { "alive",    typeof(AliveCommand)     },
                // healthchecks
                // vhost-limits
                { "test",     typeof(TestCommand)      },

                { "list",     typeof(ListCommand)      },
                { "help",     typeof(AdminHelpCommand) }
            };

            TestCommands = new Dictionary<string, Type>
            {
                { "admin",    typeof(AdminCommand)    },
                { "connect",  typeof(ConnectCommand)  },
                { "channel",  typeof(ChannelCommand)  },
                { "exchange", typeof(ExchangeCommand) },
                { "queue",    typeof(QueueCommand)    },
                { "bind",     typeof(BindCommand)     },
                { "send",     typeof(SendCommand)     },
                { "receive",  typeof(ReceiveCommand)  },
                { "list",     typeof(ListCommand)     },
                { "whoami",   typeof(WhoAmICommand)   },

                { "help",     typeof(TestHelpCommand) }
            };

        }

        internal string GetPrompt()
        {
            string result = State.ToString();
            if (State == MODES.TEST && User != null)
            {
                result += GetConnetionStatus();
            }
            return result;
        }

        internal string GetConnetionStatus() => Connection == null || !Connection.IsOpen ? " [Not Connected]" : " [Connected]";
    }
}