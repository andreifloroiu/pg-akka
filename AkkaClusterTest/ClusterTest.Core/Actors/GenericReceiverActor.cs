using Akka.Actor;
using Akka.Cluster.Tools.PublishSubscribe;
using Akka.Event;
using ClusterTest.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClusterTest.Core.Actors
{
    public class GenericReceiverActor : ReceiveActor
    {

        protected ILoggingAdapter Log = Context.GetLogger();

        public GenericReceiverActor()
        {
            var mediator = DistributedPubSub.Get(Context.System).Mediator;
            mediator.Tell(new Subscribe("general", Self));
            Receive<AkkaTimerMessage>(msg => Log.Info($"{Self.Path.Name} => AkkaTimerMessage: {msg?.Message}"));
            Receive<GeneralMessage>(msg => Log.Info($"{Self.Path.Name} => GeneralMessage: {msg?.Message}"));
            Receive<WrongTimerMessage>(msg => Log.Info($"{Self.Path.Name} => WrongTimerMessage: {msg?.Message}"));     
        }
    }
}
