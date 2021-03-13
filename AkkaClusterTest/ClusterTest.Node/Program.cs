using Akka.Actor;
using Akka.Configuration;
using Akka.Configuration.Hocon;
using ClusterTest.Core.Actors;
using System;
using System.Configuration;

namespace ClusterTest.Node
{
    class Program
    {
        private static void Main(string[] args)
        {
            StartUp(args.Length == 0 ? new String[] { "2551", "2552", "0" } : args);
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }

        public static void StartUp(string[] ports)
        {
            var section = (AkkaConfigurationSection)ConfigurationManager.GetSection("akka");
            foreach (var port in ports)
            {
                //Override the configuration of the port
                var config =
                    ConfigurationFactory.ParseString("akka.remote.dot-netty.tcp.port=" + port)
                        .WithFallback(section.AkkaConfig);

                //create an Akka system
                var system = ActorSystem.Create("ClusterSystem", config);

                //create an actor that handles cluster domain events
                system.ActorOf(Props.Create(typeof(SimpleClusterListenerActor)));
                system.ActorOf(Props.Create(typeof(GenericReceiverActor)));
                system.ActorOf(Props.Create(typeof(WrongTimerActor)));
            }
        }
    }
}
