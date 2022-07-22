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

        public override void OnStart(IDictionary<string, object> dictionary)
        {
            var trigger = (Dictionary<string, object>) dictionary["trigger"];
            var triggerArgs = (Dictionary<string, object>) trigger["args"];
            var deviceUri = (string) triggerArgs["source_uri"];

            Relay.StartInteraction(this, deviceUri, "hello world", new Dictionary<string, object>());
        }

        public override async void OnInteractionLifecycle(IDictionary<string, object> dictionary)
        {
            var type = (string) dictionary["type"];
            
            if (type == InteractionLifecycleType.Started)
            {
                var sourceUri = (string) dictionary["source_uri"];
                var deviceName = await Relay.GetDeviceName(this, sourceUri);
                await Relay.SayAndWait(this, sourceUri, "What is your name?");
                string listenResponse = await Relay.Listen(this, sourceUri);
                var greeting = await Relay.GetVar(this, "greeting", "hello");
                await Relay.SayAndWait(this, sourceUri, $"{greeting} {listenResponse}! You are currently using {deviceName}");
                Relay.EndInteraction(this, sourceUri, "hello world");
            }
            else if (type == InteractionLifecycleType.Ended)
            {
                Relay.Terminate(this);
            }
        }
    }
}