# relay-dotnet

relay-dotnet library is an SDK for interacting with Relay workflows in a .Net environment. For full documentation visit [developer.relaypro.com](https://developer.relaypro.com).

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

        private string interactionName = "hello interaction";

        public HelloWorldWorkflow(Relay relay) : base(relay)
        {
        }

        public override void OnStart(IDictionary<string, object> startEvent)
        {
            var deviceUri = Relay.GetSourceUriFromStartEvent(startEvent);
            Relay.StartInteraction(this, deviceUri, interactionName);
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
                Relay.EndInteraction(this, sourceUri, interactionName);
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
  a sort of "context" in which device-specific commands are sent.
* Inside the `OnInteractionLifecycle` an __interaction started__ event is passed in, the workflow
  prompts with the `SayAndWait` action. The device user will hear text-to-speech.
* The workflow awaits for a response from the device user with the `Listen` action.
* A workflow configuration variable `greeting` is retrieved as is the triggering device's name.
* The workflow then again uses text-to-speech to reply with a dynamic message.
* Finally, the workflow is terminated and the device is returned to its original state.

Using the Relay CLI, the workflow can be registered with the following command:

```bash
relay workflow:create:phrase --trigger="hello" --name="hello world" --uri=wss://yourhost:port/hellopath --install-all --arg "greeting=hi there"
```

For the above sample, a workflow callback function is registered with the name `hello world`. The path
of `hellopath` is used to map a WebSocket connection at the path `wss://yourhost:port/hellopath`
to the registered workflow callback function.  This is currently done in either `ConsoleAppSamples/Program.cs` or `WebAppSamples/Startup.cs`
like this:

```c#
var relay = new Relay(Relay.WebSocketConnector.Fleck, "0.0.0.0", 8080, false);
relay.AddWorkflow("/hello_world", typeof(HelloWorldWorkflow));
relay.Start();
```

There are 3 web server connectors available for you to choose from:
- Relay.WebSocketConnector.Fleck
- Relay.WebSocketConnector.SystemKestral
- Relay.WebSocketConnector.SystemHttpListener

## Workflow Registration

More thorough documentation on how to register your workflow on a Relay device
can be found at [https://developer.relaypro.com/docs/register-workflows](https://developer.relaypro.com/docs/register-workflows)

## Verbose Logging Mode

The log level is controlled via the appsettings.json file in the
ConsoleAppSamples project. The default is `Information`, but if desired
you can change it to `Debug` or another level, and then recompile.

## Guides Documentation

The higher-level guides are available at https://developer.relaypro.com/docs

## API Reference Documentation

The generated doxygen documentation is available at https://relaypro.github.io/relay-dotnet/

## License
[MIT](https://choosealicense.com/licenses/mit/)
