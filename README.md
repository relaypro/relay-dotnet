# relay-dotnet

relay-dot-net SDK is a library for interacting with Relay. For full documentation visit [developer.relaypro.com](https://developer.relaypro.com).

## Usage

The following code snippet demonstrates a very simple "Hello World" workflow. However, it does show some of the power that is available through the Relay SDK.  
See `SamplesLibrary/HelloWorldWorkflow.cs`.

```c#
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
                var listenResponse = await Relay.Listen(this, sourceUri);
                var greeting = await Relay.GetVar(this, "greeting", "hello");
                await Relay.SayAndWait(this, sourceUri, $"{greeting} {listenResponse["text"]}! You are currently using {deviceName}");
                Relay.EndInteraction(this, sourceUri, "hello world");
            }
            else if (type == InteractionLifecycleType.Ended)
            {
                Relay.Terminate(this);
            }
        }
    }
}

```

Features demonstrated here:

* When the workflow is triggered, the `OnStart` event is emitted and the registered callback
  function is called.
* An __interaction__ is started. This creates a temporary channel on the Relay device, which provides
  a sort of "context" in which some device-specific commands are sent.
* Inside the __interaction started__ handler, the workflow prompts with the `SayAndWait` action. The device user will hear text-to-speech.
* The workflow awaits for a response from the device user with the `Listen` action.
* A workflow configuration variable `greeting` is retrieved as is the triggering device's name.
* The workflow then again uses text-to-speech to reply with a dynamic message.
* Finally, the workflow is terminated and the device is returned to its original state.

Using the Relay CLI, the workflow can be registered with the following command:

```bash
relay workflow:create:phrase --name="hello world" --trigger="hello world" --uri=wss://yourhost:port/hello_world -i 99000XXXXXXXXXX --arg="greeting=hi there"
```

For the above sample, a workflow callback function is registered with the name `hello world`. This value
of `hello_world` is used to map a WebSocket connection at the path `wss://yourhost:port/hello_world`
to the registered workflow callback function.  This is currently done in either `ConsoleAppSamples/Program.cs` or `WebAppSamples/Startup.cs`
like this:

```c#
var relay = new Relay(Relay.WebSocketConnector.Fleck, "0.0.0.0", 3000, false);
await relay.AddWorkflow("/hello_world", typeof(HelloWorldWorkflow));
relay.Start();
```

## Workflow Registration

More thorough documentation on how to register your workflow on a Relay device
can be found at [https://developer.relaypro.com/docs/register-workflows](https://developer.relaypro.com/docs/register-workflows)

## License
[MIT](https://choosealicense.com/licenses/mit/)
