# relay-dotnet

relay-dotnet library is an SDK for interacting with Relay workflows in a .Net environment. 

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

A Relay workflow registration contains how to trigger it and which devices can trigger it, where to establish the websocket connection when the trigger is observed, and other configuration details. Here are some examples of the workflow registration documentation in the [Relay CLI](https://www.npmjs.com/package/@relaypro/cli):

```text Relay CLI
$ relay workflow --help
Manage workflow configurations

USAGE
  $ relay workflow COMMAND

TOPICS
  workflow args      Manage arguments for a workflow
  workflow create    Create or update a workflow by trigger type
  workflow instance  List workflow instances

COMMANDS
  workflow delete     Destructively delete and remove a workflow
  workflow install    Install an existing workflow into one or more devices
  workflow list       List workflow configurations
  workflow logs       Display workflow realtime logs
  workflow trigger    Trigger a workflow over HTTP
  workflow uninstall  Uninstall an existing workflow from one or more devices

$ relay workflow create --help

USAGE
  $ relay workflow create COMMAND

COMMANDS
  workflow create battery   Create or update a workflow triggered by crossing a charging or discharging threshold of any device on the account
  workflow create button    Create or update a workflow triggered by button taps
  workflow create call      Create or update a workflow triggered by inbound or outbound calling
  workflow create event     Create or update a workflow triggered by event emitted by Relay device
  workflow create geofence  Create or update a workflow triggered by geofence transition
  workflow create http      Create or update a workflow triggered by an HTTP request
  workflow create nfc       Create or update a workflow triggered by an NFC tap
  workflow create phrase    Create or update a workflow triggered by a spoken phrase
  workflow create position  Create or update a workflow triggered by a position transition
  workflow create timer     Create or update a workflow triggered immediately or with a repeating rule
```

### Install to User IDs

You do have control over which Relay device users can invoke your workflow. You might think of this as "installing the workflow" for a particular user. This is not technically accurate, as the workflow is actually installed on a workflow server. Really this is about the Relay server listening for triggers from selected Relay clients. Normally, you'll probably want your workflow to be able to get triggered from all the devices or users on your account. But in case not, we can support that too. 

To have all Relay users on your account be able to trigger your workflow, use the `--install-all` option in the `relay workflow create` command.

To instead select a subset of users that can trigger the workflow, use their User ID (starts with USER) with the `--install=` option when creating or registering a workflow. You can retrieve User IDs from the [Dash web console](https://dash.relaypro.com) under Account -> Users section or you can list with the command `relay devices` (this will include Relay Hardware Device IDs as well - we do not recommend installing workflows at the device level)


Normally you would "install" the workflow to the devices at the time of the workflow registration by using the `--install-all` or `--install=` options of the `relay workflow create` command. However, you can add additional devices later to your existing workflow by using the `relay workflow install` command.

### Triggers

Each workflow registration has a trigger action that kicks off the workflow. The trigger can be an action on the Relay device (like a button tap), a remote trigger (HTTP API call), a spoken phrase, or scheduled to occur on a specific time/date. In the example below, we are using a `phrase` event to start the workflow when the user speaks 'Hello' into the Relay Assistant (either while on the assistant channel, or using the channel button).

```text Relay CLI
$ relay workflow create phrase --trigger=hello --uri 'wss://myserver.myhost.com/helloworld' --name=hello-phrase --install_all
```


### Websocket URL and Parameters

Once triggered, the Relay server will invoke a websocket connection to the URL you provided where your Relay SDK-based workflow application lives. In addition to the URL, you can also specify arguments that are sent when the workflow starts. These are key/value pairs that your workflow can access, without those values being hardcoded in your workflow source code. These key/value pairs are provided to the workflow registration by you, and will have the same values on every workflow invocation. This can be helpful if you want to have different triggers with different behaviors all run inside one workflow definition, where that workflow definition has some `if` statements that branch on these values.

> 📘 Same workflow with different triggers
> 
> It is possible to register the same workflow multiple times, but each registration having a different trigger. Then your workflow can be invoked multiple ways, while having a single copy of your workflow source code and a single hosted environment. To do so, use the CLI's `workflow create` command with the same `wss` URI but with different triggers. For example, one via phrase, another via button press, another via http trigger, etc.

Note that each workflow instance will have some metadata available to it that describes how it was triggered: URN of originating device, transcription of phrase spoken, etc. This data comes in the `trigger` object passed in to the START event handler. For example, printing the `trigger` argument to the START event handler results in this:

```text
trigger={'args': {'phrase': 'hello', 'source_uri': 'urn:relay-resource:name:device:Bob'}, 'type': 'phrase'}
```

> 📘 Parameters to workflows
> 
> You may consider using different args (i.e., `--arg`, `--boolean', `--number`on the`workflow create\` command) on registration, especially if you have multiple registrations of the same workflow source code, if you want to have parameterized behaviors in your workflow. These key/value args are provided to your workflow instance depending on which registration was triggered.

## Verbose Logging Mode

The log level is controlled via the appsettings.json file in the
ConsoleAppSamples project. The default is `Information`, but if desired
you can change it to `Debug` or another level, and then recompile.


## API Reference Documentation

The generated doxygen documentation is available at https://relaypro.github.io/relay-dotnet/

## License
[MIT](https://choosealicense.com/licenses/mit/)
