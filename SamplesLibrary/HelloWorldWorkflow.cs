// Copyright © 2022 Relay Inc.

using System;
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
                Dictionary<string, object> listenResponse;
                do
                {
                    await Relay.SayAndWait(this, sourceUri, "What is your name?");
                    listenResponse = await Relay.Listen(this, sourceUri, Array.Empty<string>(), Language.English, true, 5);
                    if (listenResponse.ContainsKey("error"))
                    {
                        await Relay.SayAndWait(this, sourceUri,
                            $"There was an error listening, the error was: {listenResponse["error"]}");
                    }
                    else
                    {
                        var greeting = await Relay.GetVar(this, "greeting", "hello");
                        await Relay.SayAndWait(this, sourceUri,
                            $"{greeting} {listenResponse["text"]}! You are currently using {deviceName}");
                    }
                } while (listenResponse.ContainsKey("error"));
                    
                Relay.EndInteraction(this, sourceUri);
            }
            else if (type == InteractionLifecycleType.Ended)
            {
                Relay.Terminate(this);
            }
        }
    }
}
