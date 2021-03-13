using Akka.Actor;
using Akka.Cluster.Tools.PublishSubscribe;
using Akka.Event;
using ClusterTest.Core.Messages;
using System;
using System.Timers;

namespace ClusterTest.Core.Actors
{
    public class WrongTimerActor : ReceiveActor
    {
        protected ILoggingAdapter Log = Context.GetLogger();
        
        private Timer _tmr;
        private int _counter = 0;
        private IActorRef _mediator;

        public WrongTimerActor()
        {
            _mediator = DistributedPubSub.Get(Context.System).Mediator;
            Init();
        }

        private void Init()
        {
            _tmr = new Timer(30 * 1000);
            _tmr.AutoReset = true;
            _tmr.Enabled = true;
            _tmr.Elapsed += _tmr_Elapsed;
        }

        private void _tmr_Elapsed(object sender, ElapsedEventArgs e)
        {
            _counter++;
            //_mediator.Tell(new Publish("general", new WrongTimerMessage { Message = $"{Self.Path.Name} sent counter value {_counter}..." }));
        }
    }
}
