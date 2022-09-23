// Copyright © 2022 Relay Inc.

using System.Collections.Generic;
using RelayDotNet;

namespace SamplesLibrary
{
    public class HelloWorldWorkflow : AbstractRelayWorkflow
    {

        public HelloWorldWorkflow(Relay relay) : base(relay)
        {
        }

        public override void OnStart(IDictionary<string, object> startEvent)
        {
            var deviceUri = Relay.GetSourceUriFromStartEvent(startEvent);
            Relay.StartInteraction(this, deviceUri, "hello interaction");
        }

        public override async void OnInteractionLifecycle(IDictionary<string, object> lifecycleEvent)
        {
            var type = (string) lifecycleEvent["type"];
            
            if (type == InteractionLifecycleType.Started)
            {
                var sourceUri = (string) lifecycleEvent["source_uri"];
                var deviceName = await Relay.GetDeviceName(this, sourceUri);
                await Relay.SayAndWait(this, sourceUri, "What is your name?");
                var listenResponse = await Relay.Listen(this, sourceUri);
                var greeting = await Relay.GetVar(this, "greeting", "hello");
                await Relay.SayAndWait(this, sourceUri, $"{greeting} {listenResponse["text"]}! You are currently using {deviceName}");
                Relay.EndInteraction(this, sourceUri);
            }
            else if (type == InteractionLifecycleType.Ended)
            {
                Relay.Terminate(this);
            }
        }
    }
}
