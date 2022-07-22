<html xmlns="http://www.w3.org/1999/xhtml"><head><meta content="text/xhtml;charset=UTF-8" http-equiv="Content-Type"></meta><meta content="IE=9" http-equiv="X-UA-Compatible"></meta><meta content="Doxygen 1.8.17" name="generator"></meta><meta content="width=device-width, initial-scale=1" name="viewport"></meta><title>relay-dot-net: RelayDotNet.Relay Class Reference</title><link href="tabs.css" rel="stylesheet" type="text/css"></link><script src="jquery.js" type="text/javascript"></script><script src="dynsections.js" type="text/javascript"></script><link href="search/search.css" rel="stylesheet" type="text/css"></link><script src="search/searchdata.js" type="text/javascript"></script><script src="search/search.js" type="text/javascript"></script><link href="doxygen.css" rel="stylesheet" type="text/css"></link></head><body><div id="top"><div id="titlearea"><table cellpadding="0" cellspacing="0"> <tbody> <tr style="height: 56px;"> <td id="projectalign" style="padding-left: 0.5em;"><div id="projectname">relay-dot-net </div> </td> </tr> </tbody></table>

</div><script type="text/javascript">
/* @license magnet:?xt=urn:btih:cf05388f2679ee054f2beb29a391d25f4e673ac3&amp;dn=gpl-2.0.txt GPL-v2 */
var searchBox = new SearchBox("searchBox", "search",false,'Search');
/* @license-end */
</script><script src="menudata.js" type="text/javascript"></script><script src="menu.js" type="text/javascript"></script><script type="text/javascript">
/* @license magnet:?xt=urn:btih:cf05388f2679ee054f2beb29a391d25f4e673ac3&amp;dn=gpl-2.0.txt GPL-v2 */
$(function() {
  initMenu('',true,false,'search.php','Search');
  $(document).ready(function() { init_search(); });
});
/* @license-end */</script><div id="main-nav"></div><div id="MSearchSelectWindow" onkeydown="return searchBox.OnSearchSelectKey(event)" onmouseout="return searchBox.OnSearchSelectHide()" onmouseover="return searchBox.OnSearchSelectShow()"></div><div id="MSearchResultsWindow"><iframe frameborder="0" id="MSearchResults" name="MSearchResults" src="javascript:void(0)"></iframe></div><div class="navpath" id="nav-path">- [RelayDotNet](namespaceRelayDotNet.html)
- [Relay](classRelayDotNet_1_1Relay.html)
 
</div></div><div class="header"><div class="summary">[Public Types](#pub-types) | [Public Member Functions](#pub-methods) | [Public Attributes](#pub-attribs) | [List of all members](classRelayDotNet_1_1Relay-members.html) </div><div class="headertitle"><div class="title">RelayDotNet.Relay Class Reference</div> </div></div><div class="contents">The [Relay](classRelayDotNet_1_1Relay.html "The Relay class is responsible for defining the main functionalities that are used within workflows,...") class is responsible for defining the main functionalities that are used within workflows, such as functions for communicating with the device, sending out notifications to groups, handling workflow events, and performing physical actions on the device such as manipulating LEDs and creating vibrations. [More...](classRelayDotNet_1_1Relay.html#details)

<table class="memberdecls"><tr class="heading"><td colspan="2"><a name="pub-types"></a>Public Types
------------------------------------

</td></tr><tr class="memitem:a1bf1d579d646e9c59cd41ec8e66b6992"><td align="right" class="memItemLeft" valign="top">enum </td><td class="memItemRight" valign="bottom">[WebSocketConnector](classRelayDotNet_1_1Relay.html#a1bf1d579d646e9c59cd41ec8e66b6992) { [WebSocketConnector.Fleck](classRelayDotNet_1_1Relay.html#a1bf1d579d646e9c59cd41ec8e66b6992ab397fe519526faaf878e1f99fbdf8a1c), [WebSocketConnector.SystemHttpListener](classRelayDotNet_1_1Relay.html#a1bf1d579d646e9c59cd41ec8e66b6992abb0a32bf523203bdf99ac3c823f3f281), [WebSocketConnector.SystemKestral](classRelayDotNet_1_1Relay.html#a1bf1d579d646e9c59cd41ec8e66b6992a094bf0f4f26f8e2b9207728d8d705d17) }</td></tr><tr class="separator:a1bf1d579d646e9c59cd41ec8e66b6992"><td class="memSeparator" colspan="2"> </td></tr></table>

<table class="memberdecls"><tr class="heading"><td colspan="2"><a name="pub-methods"></a>Public Member Functions
-------------------------------------------------

</td></tr><tr class="memitem:ad058ffa5829109f8560ac37d9b52648b"><td align="right" class="memItemLeft" valign="top"> </td><td class="memItemRight" valign="bottom">[Relay](classRelayDotNet_1_1Relay.html#ad058ffa5829109f8560ac37d9b52648b) ([WebSocketConnector](classRelayDotNet_1_1Relay.html#a1bf1d579d646e9c59cd41ec8e66b6992) webSocketConnector, string ip, int port, bool secure)</td></tr><tr class="separator:ad058ffa5829109f8560ac37d9b52648b"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:aa0190530a8a1e360b8c1e9e757148bbc"><td align="right" class="memItemLeft" valign="top">async Task&lt; bool &gt; </td><td class="memItemRight" valign="bottom">[AddWorkflow](classRelayDotNet_1_1Relay.html#aa0190530a8a1e360b8c1e9e757148bbc) (string path, Type relayWorkflowType)</td></tr><tr class="separator:aa0190530a8a1e360b8c1e9e757148bbc"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a59a021ecd2a435ab930a93fedcad08d5"><td align="right" class="memItemLeft" valign="top">async Task&lt; bool &gt; </td><td class="memItemRight" valign="bottom">[RemoveWorkflow](classRelayDotNet_1_1Relay.html#a59a021ecd2a435ab930a93fedcad08d5) (string path)</td></tr><tr class="separator:a59a021ecd2a435ab930a93fedcad08d5"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a4c1285f3ae18f9f51f3a24f8345f1e77"><td align="right" class="memItemLeft" valign="top">async Task&lt; bool &gt; </td><td class="memItemRight" valign="bottom">[OnOpen](classRelayDotNet_1_1Relay.html#a4c1285f3ae18f9f51f3a24f8345f1e77) (IRelayWebSocketConnection webSocketConnection)</td></tr><tr class="separator:a4c1285f3ae18f9f51f3a24f8345f1e77"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a20439289f01f1d7b5dde8a96f0a8ef52"><td align="right" class="memItemLeft" valign="top">async void </td><td class="memItemRight" valign="bottom">[OnClose](classRelayDotNet_1_1Relay.html#a20439289f01f1d7b5dde8a96f0a8ef52) (IRelayWebSocketConnection webSocketConnection)</td></tr><tr class="separator:a20439289f01f1d7b5dde8a96f0a8ef52"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a054a6bd733831532e2b0d9b8a94ab7aa"><td align="right" class="memItemLeft" valign="top">async void </td><td class="memItemRight" valign="bottom">[OnMessage](classRelayDotNet_1_1Relay.html#a054a6bd733831532e2b0d9b8a94ab7aa) (IRelayWebSocketConnection webSocketConnection, string message)</td></tr><tr class="separator:a054a6bd733831532e2b0d9b8a94ab7aa"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:ac47e2cb4630d71d3311e0e1eabdb7d75"><td align="right" class="memItemLeft" valign="top">void </td><td class="memItemRight" valign="bottom">[Start](classRelayDotNet_1_1Relay.html#ac47e2cb4630d71d3311e0e1eabdb7d75) ()</td></tr><tr class="separator:ac47e2cb4630d71d3311e0e1eabdb7d75"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a4892a3f21417de146b2b454f14df54dc"><td align="right" class="memItemLeft" valign="top">void </td><td class="memItemRight" valign="bottom">[Dispose](classRelayDotNet_1_1Relay.html#a4892a3f21417de146b2b454f14df54dc) ()</td></tr><tr class="separator:a4892a3f21417de146b2b454f14df54dc"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a9346635f02704bc5dac4a66508aa7876"><td align="right" class="memItemLeft" valign="top">async void </td><td class="memItemRight" valign="bottom">[Terminate](classRelayDotNet_1_1Relay.html#a9346635f02704bc5dac4a66508aa7876) (IRelayWorkflow relayWorkflow)</td></tr><tr class="memdesc:a9346635f02704bc5dac4a66508aa7876"><td class="mdescLeft"> </td><td class="mdescRight">Terminates a workflow. This method is usually called after your workflow has completed and you would like to end the workflow by calling end\_interaction(), where you can then terminate the workflow. [More...](classRelayDotNet_1_1Relay.html#a9346635f02704bc5dac4a66508aa7876)  
</td></tr><tr class="separator:a9346635f02704bc5dac4a66508aa7876"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:abc10788dda6035e1c83e6d4e382ed294"><td align="right" class="memItemLeft" valign="top">async void </td><td class="memItemRight" valign="bottom">[StartInteraction](classRelayDotNet_1_1Relay.html#abc10788dda6035e1c83e6d4e382ed294) (IRelayWorkflow relayWorkflow, string sourceUri, string name, Dictionary&lt; string, object &gt; options)</td></tr><tr class="memdesc:abc10788dda6035e1c83e6d4e382ed294"><td class="mdescLeft"> </td><td class="mdescRight">Starts an interaction with the user. Triggers an INTERACTION\_STARTED event and allows the user to interact with the device via functions that require an interaction URN. [More...](classRelayDotNet_1_1Relay.html#abc10788dda6035e1c83e6d4e382ed294)  
</td></tr><tr class="separator:abc10788dda6035e1c83e6d4e382ed294"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:ab80f54bd7fdc31c436b639a72143a49b"><td align="right" class="memItemLeft" valign="top">async void </td><td class="memItemRight" valign="bottom">[EndInteraction](classRelayDotNet_1_1Relay.html#ab80f54bd7fdc31c436b639a72143a49b) (IRelayWorkflow relayWorkflow, string sourceUri, string name)</td></tr><tr class="memdesc:ab80f54bd7fdc31c436b639a72143a49b"><td class="mdescLeft"> </td><td class="mdescRight">Ends an interaction with the user. Triggers an INTERACTION\_ENDED event to signify that the user is done interacting with the device. [More...](classRelayDotNet_1_1Relay.html#ab80f54bd7fdc31c436b639a72143a49b)  
</td></tr><tr class="separator:ab80f54bd7fdc31c436b639a72143a49b"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:abe8a6952117f8a61c9505bfe1cf1cb9c"><td align="right" class="memItemLeft" valign="top">async Task&lt; string &gt; </td><td class="memItemRight" valign="bottom">[Say](classRelayDotNet_1_1Relay.html#abe8a6952117f8a61c9505bfe1cf1cb9c) (IRelayWorkflow relayWorkflow, string sourceUri, string text)</td></tr><tr class="memdesc:abe8a6952117f8a61c9505bfe1cf1cb9c"><td class="mdescLeft"> </td><td class="mdescRight">Utilizes text to speech capabilities to make the device 'speak' to the user. [More...](classRelayDotNet_1_1Relay.html#abe8a6952117f8a61c9505bfe1cf1cb9c)  
</td></tr><tr class="separator:abe8a6952117f8a61c9505bfe1cf1cb9c"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:ab80565458d0ee5c9b5712ab3a23657ad"><td align="right" class="memItemLeft" valign="top">async Task&lt; string &gt; </td><td class="memItemRight" valign="bottom">[Say](classRelayDotNet_1_1Relay.html#ab80565458d0ee5c9b5712ab3a23657ad) (IRelayWorkflow relayWorkflow, string sourceUri, string text, Language language)</td></tr><tr class="memdesc:ab80565458d0ee5c9b5712ab3a23657ad"><td class="mdescLeft"> </td><td class="mdescRight">Utilizes text to speech capabilities to make the device 'speak' to the user. [More...](classRelayDotNet_1_1Relay.html#ab80565458d0ee5c9b5712ab3a23657ad)  
</td></tr><tr class="separator:ab80565458d0ee5c9b5712ab3a23657ad"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a5d8167fc0686982c6c0e2e810f069040"><td align="right" class="memItemLeft" valign="top">async Task&lt; string &gt; </td><td class="memItemRight" valign="bottom">[SayAndWait](classRelayDotNet_1_1Relay.html#a5d8167fc0686982c6c0e2e810f069040) (IRelayWorkflow relayWorkflow, string sourceUri, string text)</td></tr><tr class="memdesc:a5d8167fc0686982c6c0e2e810f069040"><td class="mdescLeft"> </td><td class="mdescRight">Utilizes text to speech capabilities to make the device 'speak' to the user. Waits until the text is fully played out on the device before continuing. [More...](classRelayDotNet_1_1Relay.html#a5d8167fc0686982c6c0e2e810f069040)  
</td></tr><tr class="separator:a5d8167fc0686982c6c0e2e810f069040"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a7b5807f9a872e4b976840309b4f11a57"><td align="right" class="memItemLeft" valign="top">async Task&lt; string &gt; </td><td class="memItemRight" valign="bottom">[SayAndWait](classRelayDotNet_1_1Relay.html#a7b5807f9a872e4b976840309b4f11a57) (IRelayWorkflow relayWorkflow, string sourceUri, string text, Language language)</td></tr><tr class="memdesc:a7b5807f9a872e4b976840309b4f11a57"><td class="mdescLeft"> </td><td class="mdescRight">Utilizes text to speech capabilities to make the device 'speak' to the user. Waits until the text is fully played out on the device before continuing. [More...](classRelayDotNet_1_1Relay.html#a7b5807f9a872e4b976840309b4f11a57)  
</td></tr><tr class="separator:a7b5807f9a872e4b976840309b4f11a57"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a6cc51e490c65764c6b8d0de74fef4063"><td align="right" class="memItemLeft" valign="top">async Task&lt; string &gt; </td><td class="memItemRight" valign="bottom">[Play](classRelayDotNet_1_1Relay.html#a6cc51e490c65764c6b8d0de74fef4063) (IRelayWorkflow relayWorkflow, string sourceUri, string filename)</td></tr><tr class="memdesc:a6cc51e490c65764c6b8d0de74fef4063"><td class="mdescLeft"> </td><td class="mdescRight">Plays a custom audio file that was uploaded by the user. [More...](classRelayDotNet_1_1Relay.html#a6cc51e490c65764c6b8d0de74fef4063)  
</td></tr><tr class="separator:a6cc51e490c65764c6b8d0de74fef4063"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:aa71c80211bf69a8d96ed88fac5cbb5e6"><td align="right" class="memItemLeft" valign="top">async Task&lt; string &gt; </td><td class="memItemRight" valign="bottom">[PlayAndWait](classRelayDotNet_1_1Relay.html#aa71c80211bf69a8d96ed88fac5cbb5e6) (IRelayWorkflow relayWorkflow, string sourceUri, string filename)</td></tr><tr class="memdesc:aa71c80211bf69a8d96ed88fac5cbb5e6"><td class="mdescLeft"> </td><td class="mdescRight">Plays a custom audio file that was uploaded by the user. Waits until the audio file has finished playing before continuing through the workflow. [More...](classRelayDotNet_1_1Relay.html#aa71c80211bf69a8d96ed88fac5cbb5e6)  
</td></tr><tr class="separator:aa71c80211bf69a8d96ed88fac5cbb5e6"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a809b0f2ff9c903c8d9d9481af38e9489"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[StopPlayback](classRelayDotNet_1_1Relay.html#a809b0f2ff9c903c8d9d9481af38e9489) (IRelayWorkflow relayWorkflow, string sourceUri)</td></tr><tr class="separator:a809b0f2ff9c903c8d9d9481af38e9489"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:aab8c9e522d71e7c30c395ff282460932"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[StopPlayback](classRelayDotNet_1_1Relay.html#aab8c9e522d71e7c30c395ff282460932) (IRelayWorkflow relayWorkflow, string sourceUri, string id)</td></tr><tr class="separator:aab8c9e522d71e7c30c395ff282460932"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a114913d05d232247c01ad52d72a05368"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[StopPlayback](classRelayDotNet_1_1Relay.html#a114913d05d232247c01ad52d72a05368) (IRelayWorkflow relayWorkflow, string sourceUri, string\[\] ids)</td></tr><tr class="separator:a114913d05d232247c01ad52d72a05368"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:aa071d6386572e9d6cbe705e6d907c629"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[Vibrate](classRelayDotNet_1_1Relay.html#aa071d6386572e9d6cbe705e6d907c629) (IRelayWorkflow relayWorkflow, string sourceUri, int\[\] pattern)</td></tr><tr class="memdesc:aa071d6386572e9d6cbe705e6d907c629"><td class="mdescLeft"> </td><td class="mdescRight">Makes the device vibrate in a particular pattern. You can specify how many vibrations you would like, the duration of each vibration in milliseconds, and how long you would like the pauses between each vibration to last in milliseconds. [More...](classRelayDotNet_1_1Relay.html#aa071d6386572e9d6cbe705e6d907c629)  
</td></tr><tr class="separator:aa071d6386572e9d6cbe705e6d907c629"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:ade72d063b710b5eaaee8049f8ae4d728"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[StartTimer](classRelayDotNet_1_1Relay.html#ade72d063b710b5eaaee8049f8ae4d728) (IRelayWorkflow relayWorkflow, int timeout)</td></tr><tr class="memdesc:ade72d063b710b5eaaee8049f8ae4d728"><td class="mdescLeft"> </td><td class="mdescRight">Starts an unnamed timer, meaning this will be the only timer on your device. The timer will stop when it reaches the limit of the 'timeout' parameter. [More...](classRelayDotNet_1_1Relay.html#ade72d063b710b5eaaee8049f8ae4d728)  
</td></tr><tr class="separator:ade72d063b710b5eaaee8049f8ae4d728"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a2d40218be7fe15bd9a9e6de01ced0fdf"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[StopTimer](classRelayDotNet_1_1Relay.html#a2d40218be7fe15bd9a9e6de01ced0fdf) (IRelayWorkflow relayWorkflow)</td></tr><tr class="memdesc:a2d40218be7fe15bd9a9e6de01ced0fdf"><td class="mdescLeft"> </td><td class="mdescRight">Stops an unnamed timer. [More...](classRelayDotNet_1_1Relay.html#a2d40218be7fe15bd9a9e6de01ced0fdf)  
</td></tr><tr class="separator:a2d40218be7fe15bd9a9e6de01ced0fdf"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a79b14e64a15e44f66ac751d07fc64488"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[SetTimer](classRelayDotNet_1_1Relay.html#a79b14e64a15e44f66ac751d07fc64488) (IRelayWorkflow relayWorkflow, string name, string timerType, int timeout, string timeoutType)</td></tr><tr class="memdesc:a79b14e64a15e44f66ac751d07fc64488"><td class="mdescLeft"> </td><td class="mdescRight">Serves as a named timer that can be either interval or timeout. Allows you to specify the unit of time. [More...](classRelayDotNet_1_1Relay.html#a79b14e64a15e44f66ac751d07fc64488)  
</td></tr><tr class="separator:a79b14e64a15e44f66ac751d07fc64488"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a17bbf9bf5a37c27e7ad9ec73e8c1d239"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[ClearTimer](classRelayDotNet_1_1Relay.html#a17bbf9bf5a37c27e7ad9ec73e8c1d239) (IRelayWorkflow relayWorkflow, string name)</td></tr><tr class="memdesc:a17bbf9bf5a37c27e7ad9ec73e8c1d239"><td class="mdescLeft"> </td><td class="mdescRight">Clears the specified timer. [More...](classRelayDotNet_1_1Relay.html#a17bbf9bf5a37c27e7ad9ec73e8c1d239)  
</td></tr><tr class="separator:a17bbf9bf5a37c27e7ad9ec73e8c1d239"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a57f04373456084a730e531b153d7d538"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[CreateIncident](classRelayDotNet_1_1Relay.html#a57f04373456084a730e531b153d7d538) (IRelayWorkflow relayWorkflow, string originator, string iType)</td></tr><tr class="memdesc:a57f04373456084a730e531b153d7d538"><td class="mdescLeft"> </td><td class="mdescRight">Creates an incident that will alert the [Relay](classRelayDotNet_1_1Relay.html "The Relay class is responsible for defining the main functionalities that are used within workflows,...") Dash. [More...](classRelayDotNet_1_1Relay.html#a57f04373456084a730e531b153d7d538)  
</td></tr><tr class="separator:a57f04373456084a730e531b153d7d538"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:ad0473751c1f6f4b02aa89e7a426d48c2"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[ResolveIncident](classRelayDotNet_1_1Relay.html#ad0473751c1f6f4b02aa89e7a426d48c2) (IRelayWorkflow relayWorkflow, string incidentId, string reason)</td></tr><tr class="memdesc:ad0473751c1f6f4b02aa89e7a426d48c2"><td class="mdescLeft"> </td><td class="mdescRight">Resolves an incident that was created. [More...](classRelayDotNet_1_1Relay.html#ad0473751c1f6f4b02aa89e7a426d48c2)  
</td></tr><tr class="separator:ad0473751c1f6f4b02aa89e7a426d48c2"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:aadb421351633e256cbb25c4fab6802c6"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[SetLed](classRelayDotNet_1_1Relay.html#aadb421351633e256cbb25c4fab6802c6) (IRelayWorkflow relayWorkflow, string sourceUri, LedEffect ledEffect, LedInfo ledInfo)</td></tr><tr class="memdesc:aadb421351633e256cbb25c4fab6802c6"><td class="mdescLeft"> </td><td class="mdescRight">Used for performing actions on the LEDs, such as creating a rainbow, flashing, rotating, etc. [More...](classRelayDotNet_1_1Relay.html#aadb421351633e256cbb25c4fab6802c6)  
</td></tr><tr class="separator:aadb421351633e256cbb25c4fab6802c6"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a75dc6db07088ee32157d05198e1cbb95"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[SwitchLedOn](classRelayDotNet_1_1Relay.html#a75dc6db07088ee32157d05198e1cbb95) (IRelayWorkflow relayWorkflow, string sourceUri, LedIndex ledIndex, string color)</td></tr><tr class="memdesc:a75dc6db07088ee32157d05198e1cbb95"><td class="mdescLeft"> </td><td class="mdescRight">Switches on an LED at a particular index to a specified color. [More...](classRelayDotNet_1_1Relay.html#a75dc6db07088ee32157d05198e1cbb95)  
</td></tr><tr class="separator:a75dc6db07088ee32157d05198e1cbb95"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:af194291aa8ad233c8d42b6595c5c22d2"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[SwitchAllLedOn](classRelayDotNet_1_1Relay.html#af194291aa8ad233c8d42b6595c5c22d2) (IRelayWorkflow relayWorkflow, string sourceUri, string color)</td></tr><tr class="memdesc:af194291aa8ad233c8d42b6595c5c22d2"><td class="mdescLeft"> </td><td class="mdescRight">Switches all of the LEDs on a device on to a specified color. [More...](classRelayDotNet_1_1Relay.html#af194291aa8ad233c8d42b6595c5c22d2)  
</td></tr><tr class="separator:af194291aa8ad233c8d42b6595c5c22d2"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:ae14c7351d7250ae7956f495570ee8523"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[SwitchAllLedOff](classRelayDotNet_1_1Relay.html#ae14c7351d7250ae7956f495570ee8523) (IRelayWorkflow relayWorkflow, string sourceUri)</td></tr><tr class="memdesc:ae14c7351d7250ae7956f495570ee8523"><td class="mdescLeft"> </td><td class="mdescRight">Switches all of the LEDs on a device off. [More...](classRelayDotNet_1_1Relay.html#ae14c7351d7250ae7956f495570ee8523)  
</td></tr><tr class="separator:ae14c7351d7250ae7956f495570ee8523"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a460b2af0396d2629996138df383cb0ab"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[Rainbow](classRelayDotNet_1_1Relay.html#a460b2af0396d2629996138df383cb0ab) (IRelayWorkflow relayWorkflow, string sourceUri, int rotations=-1)</td></tr><tr class="memdesc:a460b2af0396d2629996138df383cb0ab"><td class="mdescLeft"> </td><td class="mdescRight">Switches all of the LEDs on to a configured rainbow pattern and rotates them a specified number of times. [More...](classRelayDotNet_1_1Relay.html#a460b2af0396d2629996138df383cb0ab)  
</td></tr><tr class="separator:a460b2af0396d2629996138df383cb0ab"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a2bc45319d86407e1dcae651657af6f72"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[Rotate](classRelayDotNet_1_1Relay.html#a2bc45319d86407e1dcae651657af6f72) (IRelayWorkflow relayWorkflow, string sourceUri, string color="FFFFFF")</td></tr><tr class="memdesc:a2bc45319d86407e1dcae651657af6f72"><td class="mdescLeft"> </td><td class="mdescRight">Switches all of the LEDs on a device to a certain color and rotates them a specified number of times. [More...](classRelayDotNet_1_1Relay.html#a2bc45319d86407e1dcae651657af6f72)  
</td></tr><tr class="separator:a2bc45319d86407e1dcae651657af6f72"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a73fdfe528e585b870d60368081b9e476"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[Flash](classRelayDotNet_1_1Relay.html#a73fdfe528e585b870d60368081b9e476) (IRelayWorkflow relayWorkflow, string sourceUri, string color="0000FF")</td></tr><tr class="memdesc:a73fdfe528e585b870d60368081b9e476"><td class="mdescLeft"> </td><td class="mdescRight">Switches all of the LEDs on a device to a certain color and flashes them a specified number of times. [More...](classRelayDotNet_1_1Relay.html#a73fdfe528e585b870d60368081b9e476)  
</td></tr><tr class="separator:a73fdfe528e585b870d60368081b9e476"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a0f033ad67f11c88195596afc32655e7b"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[Breathe](classRelayDotNet_1_1Relay.html#a0f033ad67f11c88195596afc32655e7b) (IRelayWorkflow relayWorkflow, string sourceUri, string color="0000FF")</td></tr><tr class="memdesc:a0f033ad67f11c88195596afc32655e7b"><td class="mdescLeft"> </td><td class="mdescRight">Switches all of the LEDs on a device to a certain color and creates a 'breathing' effect, where the LEDs will slowly light up a specified number of times. [More...](classRelayDotNet_1_1Relay.html#a0f033ad67f11c88195596afc32655e7b)  
</td></tr><tr class="separator:a0f033ad67f11c88195596afc32655e7b"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a967df5e6d9f6c41f4fc15f25d88914c1"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[SetVar](classRelayDotNet_1_1Relay.html#a967df5e6d9f6c41f4fc15f25d88914c1) (IRelayWorkflow relayWorkflow, string name, string value)</td></tr><tr class="memdesc:a967df5e6d9f6c41f4fc15f25d88914c1"><td class="mdescLeft"> </td><td class="mdescRight">Sets a variable with the corresponding name and value. Scope of the variable is from start to end of a workflow. [More...](classRelayDotNet_1_1Relay.html#a967df5e6d9f6c41f4fc15f25d88914c1)  
</td></tr><tr class="separator:a967df5e6d9f6c41f4fc15f25d88914c1"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a846ba92d95860532bd1c34367ff0f88d"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[UnsetVar](classRelayDotNet_1_1Relay.html#a846ba92d95860532bd1c34367ff0f88d) (IRelayWorkflow relayWorkflow, string name)</td></tr><tr class="memdesc:a846ba92d95860532bd1c34367ff0f88d"><td class="mdescLeft"> </td><td class="mdescRight">Unsets the value of a variable. [More...](classRelayDotNet_1_1Relay.html#a846ba92d95860532bd1c34367ff0f88d)  
</td></tr><tr class="separator:a846ba92d95860532bd1c34367ff0f88d"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a8e4a49291b750122f52e804e8399033e"><td align="right" class="memItemLeft" valign="top">async Task&lt; string &gt; </td><td class="memItemRight" valign="bottom">[GetVar](classRelayDotNet_1_1Relay.html#a8e4a49291b750122f52e804e8399033e) (IRelayWorkflow relayWorkflow, string name, string defaultValue=null)</td></tr><tr class="memdesc:a8e4a49291b750122f52e804e8399033e"><td class="mdescLeft"> </td><td class="mdescRight">Retrieves a variable that was set either during workflow registration or through the set\_var() function. The variable can be retrieved anywhere within the workflow, but is erased after the workflow terminates. [More...](classRelayDotNet_1_1Relay.html#a8e4a49291b750122f52e804e8399033e)  
</td></tr><tr class="separator:a8e4a49291b750122f52e804e8399033e"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a28d1ad3573ab5d7848ce8130322957ee"><td align="right" class="memItemLeft" valign="top">async Task&lt; int &gt; </td><td class="memItemRight" valign="bottom">[GetNumberVar](classRelayDotNet_1_1Relay.html#a28d1ad3573ab5d7848ce8130322957ee) (IRelayWorkflow relayWorkflow, string name, int defaultValue=-1)</td></tr><tr class="memdesc:a28d1ad3573ab5d7848ce8130322957ee"><td class="mdescLeft"> </td><td class="mdescRight">Retrieves a variable that is an integer type. [More...](classRelayDotNet_1_1Relay.html#a28d1ad3573ab5d7848ce8130322957ee)  
</td></tr><tr class="separator:a28d1ad3573ab5d7848ce8130322957ee"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:af0bae904854f2e7f9bfce0c5103338cc"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[Listen](classRelayDotNet_1_1Relay.html#af0bae904854f2e7f9bfce0c5103338cc) (IRelayWorkflow relayWorkflow, string sourceUri)</td></tr><tr class="memdesc:af0bae904854f2e7f9bfce0c5103338cc"><td class="mdescLeft"> </td><td class="mdescRight">Listens for the user to speak into the device. Utilizes speech to text functionality to interact with the user. [More...](classRelayDotNet_1_1Relay.html#af0bae904854f2e7f9bfce0c5103338cc)  
</td></tr><tr class="separator:af0bae904854f2e7f9bfce0c5103338cc"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a5814e4da1febbbef39ed1e3da77a120b"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[Listen](classRelayDotNet_1_1Relay.html#a5814e4da1febbbef39ed1e3da77a120b) (IRelayWorkflow relayWorkflow, string sourceUri, string\[\] phrases)</td></tr><tr class="memdesc:a5814e4da1febbbef39ed1e3da77a120b"><td class="mdescLeft"> </td><td class="mdescRight">Listens for the user to speak into the device. Utilizes speech to text functionality to interact with the user. [More...](classRelayDotNet_1_1Relay.html#a5814e4da1febbbef39ed1e3da77a120b)  
</td></tr><tr class="separator:a5814e4da1febbbef39ed1e3da77a120b"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:ae69a5d38c458820952ae4e54e43e66df"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[Listen](classRelayDotNet_1_1Relay.html#ae69a5d38c458820952ae4e54e43e66df) (IRelayWorkflow relayWorkflow, string sourceUri, Language language)</td></tr><tr class="memdesc:ae69a5d38c458820952ae4e54e43e66df"><td class="mdescLeft"> </td><td class="mdescRight">Listens for the user to speak into the device. Utilizes speech to text functionality to interact with the user. [More...](classRelayDotNet_1_1Relay.html#ae69a5d38c458820952ae4e54e43e66df)  
</td></tr><tr class="separator:ae69a5d38c458820952ae4e54e43e66df"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a61c0cbb0b689feecca8fa848d4066b48"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[Listen](classRelayDotNet_1_1Relay.html#a61c0cbb0b689feecca8fa848d4066b48) (IRelayWorkflow relayWorkflow, string sourceUri, string\[\] phrases, Language altLanguage, bool transcribe=true, int timeout=60)</td></tr><tr class="memdesc:a61c0cbb0b689feecca8fa848d4066b48"><td class="mdescLeft"> </td><td class="mdescRight">Listens for the user to speak into the device. Utilizes speech to text functionality to interact with the user. [More...](classRelayDotNet_1_1Relay.html#a61c0cbb0b689feecca8fa848d4066b48)  
</td></tr><tr class="separator:a61c0cbb0b689feecca8fa848d4066b48"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:ad7c302c4b23d5c0c400063761c9f2c99"><td align="right" class="memItemLeft" valign="top">async Task&lt; string &gt; </td><td class="memItemRight" valign="bottom">[Translate](classRelayDotNet_1_1Relay.html#ad7c302c4b23d5c0c400063761c9f2c99) (IRelayWorkflow relayWorkflow, string text, Language from, Language to)</td></tr><tr class="separator:ad7c302c4b23d5c0c400063761c9f2c99"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a843642df8b27e7006d7259bcbd1d68c7"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[PlayUnreadInboxMessages](classRelayDotNet_1_1Relay.html#a843642df8b27e7006d7259bcbd1d68c7) (IRelayWorkflow relayWorkflow, string sourceUri)</td></tr><tr class="separator:a843642df8b27e7006d7259bcbd1d68c7"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:aa99f9d1af832737f16225be89d1f5fad"><td align="right" class="memItemLeft" valign="top">async Task&lt; int &gt; </td><td class="memItemRight" valign="bottom">[GetUnreadInboxSize](classRelayDotNet_1_1Relay.html#aa99f9d1af832737f16225be89d1f5fad) (IRelayWorkflow relayWorkflow, string sourceUri)</td></tr><tr class="separator:aa99f9d1af832737f16225be89d1f5fad"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a902bbe1c5e343dcdc4da4f736a7b644d"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[CancelNotification](classRelayDotNet_1_1Relay.html#a902bbe1c5e343dcdc4da4f736a7b644d) (IRelayWorkflow relayWorkflow, string sourceUri, string name, string targets)</td></tr><tr class="memdesc:a902bbe1c5e343dcdc4da4f736a7b644d"><td class="mdescLeft"> </td><td class="mdescRight">Cancels a notification of any type that was sent to a group of devices. [More...](classRelayDotNet_1_1Relay.html#a902bbe1c5e343dcdc4da4f736a7b644d)  
</td></tr><tr class="separator:a902bbe1c5e343dcdc4da4f736a7b644d"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a335a03e3d1696a1b25b1d0945fe41682"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[Broadcast](classRelayDotNet_1_1Relay.html#a335a03e3d1696a1b25b1d0945fe41682) (IRelayWorkflow relayWorkflow, string targets, string sourceUri, string name, string text)</td></tr><tr class="memdesc:a335a03e3d1696a1b25b1d0945fe41682"><td class="mdescLeft"> </td><td class="mdescRight">Sends out a broadcasted message to a group of devices. The message is played out on all devices, as well as sent to the [Relay](classRelayDotNet_1_1Relay.html "The Relay class is responsible for defining the main functionalities that are used within workflows,...") Dash. [More...](classRelayDotNet_1_1Relay.html#a335a03e3d1696a1b25b1d0945fe41682)  
</td></tr><tr class="separator:a335a03e3d1696a1b25b1d0945fe41682"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a315975e070f89442d38b3a35728f16d9"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[Broadcast](classRelayDotNet_1_1Relay.html#a315975e070f89442d38b3a35728f16d9) (IRelayWorkflow relayWorkflow, string targets, string sourceUri, string name, string text, NotificationPushOptions notificationPushOptions)</td></tr><tr class="memdesc:a315975e070f89442d38b3a35728f16d9"><td class="mdescLeft"> </td><td class="mdescRight">Sends out a broadcasted message to a group of devices. The message is played out on all devices, as well as sent to the [Relay](classRelayDotNet_1_1Relay.html "The Relay class is responsible for defining the main functionalities that are used within workflows,...") Dash. [More...](classRelayDotNet_1_1Relay.html#a315975e070f89442d38b3a35728f16d9)  
</td></tr><tr class="separator:a315975e070f89442d38b3a35728f16d9"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a82217a04532c11a2eabf81f857eccc7c"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[CancelBroadcast](classRelayDotNet_1_1Relay.html#a82217a04532c11a2eabf81f857eccc7c) (IRelayWorkflow relayWorkflow, string sourceUri, string name, string targets)</td></tr><tr class="memdesc:a82217a04532c11a2eabf81f857eccc7c"><td class="mdescLeft"> </td><td class="mdescRight">Cancels the broadcast that was sent to a group of devices. [More...](classRelayDotNet_1_1Relay.html#a82217a04532c11a2eabf81f857eccc7c)  
</td></tr><tr class="separator:a82217a04532c11a2eabf81f857eccc7c"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:afd01c4889a7c3a33e1a0c790d3fb552f"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[Notify](classRelayDotNet_1_1Relay.html#afd01c4889a7c3a33e1a0c790d3fb552f) (IRelayWorkflow relayWorkflow, string targets, string sourceUri, string name, string text)</td></tr><tr class="memdesc:afd01c4889a7c3a33e1a0c790d3fb552f"><td class="mdescLeft"> </td><td class="mdescRight">Sends out a notification message to a group of devices. [More...](classRelayDotNet_1_1Relay.html#afd01c4889a7c3a33e1a0c790d3fb552f)  
</td></tr><tr class="separator:afd01c4889a7c3a33e1a0c790d3fb552f"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:ab13663f82e717a508ea9b747e3acfc38"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[Notify](classRelayDotNet_1_1Relay.html#ab13663f82e717a508ea9b747e3acfc38) (IRelayWorkflow relayWorkflow, string targets, string sourceUri, string name, string text, NotificationPushOptions notificationPushOptions)</td></tr><tr class="memdesc:ab13663f82e717a508ea9b747e3acfc38"><td class="mdescLeft"> </td><td class="mdescRight">Sends out a notification message to a group of devices. [More...](classRelayDotNet_1_1Relay.html#ab13663f82e717a508ea9b747e3acfc38)  
</td></tr><tr class="separator:ab13663f82e717a508ea9b747e3acfc38"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a461275ffe7aa16437588e216809d2ff5"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[CancelNotify](classRelayDotNet_1_1Relay.html#a461275ffe7aa16437588e216809d2ff5) (IRelayWorkflow relayWorkflow, string sourceUri, string name, string targets)</td></tr><tr class="memdesc:a461275ffe7aa16437588e216809d2ff5"><td class="mdescLeft"> </td><td class="mdescRight">Cancels the notification that was sent to a group of devices. [More...](classRelayDotNet_1_1Relay.html#a461275ffe7aa16437588e216809d2ff5)  
</td></tr><tr class="separator:a461275ffe7aa16437588e216809d2ff5"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:ab7f6431cd4f933886da8052fc47dba93"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[Alert](classRelayDotNet_1_1Relay.html#ab7f6431cd4f933886da8052fc47dba93) (IRelayWorkflow relayWorkflow, string targets, string sourceUri, string name, string text)</td></tr><tr class="memdesc:ab7f6431cd4f933886da8052fc47dba93"><td class="mdescLeft"> </td><td class="mdescRight">Sends out an alert to the specified group of devices and the [Relay](classRelayDotNet_1_1Relay.html "The Relay class is responsible for defining the main functionalities that are used within workflows,...") Dash. [More...](classRelayDotNet_1_1Relay.html#ab7f6431cd4f933886da8052fc47dba93)  
</td></tr><tr class="separator:ab7f6431cd4f933886da8052fc47dba93"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a472c0c73754eb35e3e9fdd230e61c567"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[Alert](classRelayDotNet_1_1Relay.html#a472c0c73754eb35e3e9fdd230e61c567) (IRelayWorkflow relayWorkflow, string targets, string sourceUri, string name, string text, NotificationPushOptions notificationPushOptions)</td></tr><tr class="memdesc:a472c0c73754eb35e3e9fdd230e61c567"><td class="mdescLeft"> </td><td class="mdescRight">Sends out an alert to the specified group of devices and the [Relay](classRelayDotNet_1_1Relay.html "The Relay class is responsible for defining the main functionalities that are used within workflows,...") Dash. [More...](classRelayDotNet_1_1Relay.html#a472c0c73754eb35e3e9fdd230e61c567)  
</td></tr><tr class="separator:a472c0c73754eb35e3e9fdd230e61c567"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:ae37b55385656e88e8aafb3edeed6d6a0"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[CancelAlert](classRelayDotNet_1_1Relay.html#ae37b55385656e88e8aafb3edeed6d6a0) (IRelayWorkflow relayWorkflow, string sourceUri, string name, string targets)</td></tr><tr class="memdesc:ae37b55385656e88e8aafb3edeed6d6a0"><td class="mdescLeft"> </td><td class="mdescRight">Cancels an alert that was sent to a group of devices. Particularly useful if you would like to cancel the alert on all devices after one device has acknowledged the alert. [More...](classRelayDotNet_1_1Relay.html#ae37b55385656e88e8aafb3edeed6d6a0)  
</td></tr><tr class="separator:ae37b55385656e88e8aafb3edeed6d6a0"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a100e260d4bd0107cc7b21f0e00bd50da"><td align="right" class="memItemLeft" valign="top">async Task&lt; string\[\]&gt; </td><td class="memItemRight" valign="bottom">[GetGroupMembers](classRelayDotNet_1_1Relay.html#a100e260d4bd0107cc7b21f0e00bd50da) (IRelayWorkflow relayWorkflow, string groupName)</td></tr><tr class="memdesc:a100e260d4bd0107cc7b21f0e00bd50da"><td class="mdescLeft"> </td><td class="mdescRight">Returns the members of a particular group. [More...](classRelayDotNet_1_1Relay.html#a100e260d4bd0107cc7b21f0e00bd50da)  
</td></tr><tr class="separator:a100e260d4bd0107cc7b21f0e00bd50da"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:aaad217740ce694edb1bee0946a186b1a"><td align="right" class="memItemLeft" valign="top">async Task&lt; string &gt; </td><td class="memItemRight" valign="bottom">[GetDeviceName](classRelayDotNet_1_1Relay.html#aaad217740ce694edb1bee0946a186b1a) (IRelayWorkflow relayWorkflow, string sourceUri)</td></tr><tr class="memdesc:aaad217740ce694edb1bee0946a186b1a"><td class="mdescLeft"> </td><td class="mdescRight">Returns the name of a targeted device. [More...](classRelayDotNet_1_1Relay.html#aaad217740ce694edb1bee0946a186b1a)  
</td></tr><tr class="separator:aaad217740ce694edb1bee0946a186b1a"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:ae290836a7d99bc565f4b8cd537872599"><td align="right" class="memItemLeft" valign="top">async Task&lt; string &gt; </td><td class="memItemRight" valign="bottom">[GetDeviceLocation](classRelayDotNet_1_1Relay.html#ae290836a7d99bc565f4b8cd537872599) (IRelayWorkflow relayWorkflow, string sourceUri, bool refresh)</td></tr><tr class="memdesc:ae290836a7d99bc565f4b8cd537872599"><td class="mdescLeft"> </td><td class="mdescRight">Returns the location of a targeted device. [More...](classRelayDotNet_1_1Relay.html#ae290836a7d99bc565f4b8cd537872599)  
</td></tr><tr class="separator:ae290836a7d99bc565f4b8cd537872599"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a9a523506999c0eeaaa5084a0c8c4a758"><td align="right" class="memItemLeft" valign="top">async Task&lt; string &gt; </td><td class="memItemRight" valign="bottom">[GetDeviceId](classRelayDotNet_1_1Relay.html#a9a523506999c0eeaaa5084a0c8c4a758) (IRelayWorkflow relayWorkflow, string sourceUri)</td></tr><tr class="memdesc:a9a523506999c0eeaaa5084a0c8c4a758"><td class="mdescLeft"> </td><td class="mdescRight">Returns the ID of a targeted device. [More...](classRelayDotNet_1_1Relay.html#a9a523506999c0eeaaa5084a0c8c4a758)  
</td></tr><tr class="separator:a9a523506999c0eeaaa5084a0c8c4a758"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a05c8ec04554103fec2ac28b9dd10bf54"><td align="right" class="memItemLeft" valign="top">async Task&lt; string &gt; </td><td class="memItemRight" valign="bottom">[GetDeviceAddress](classRelayDotNet_1_1Relay.html#a05c8ec04554103fec2ac28b9dd10bf54) (IRelayWorkflow relayWorkflow, string sourceUri, bool refresh)</td></tr><tr class="memdesc:a05c8ec04554103fec2ac28b9dd10bf54"><td class="mdescLeft"> </td><td class="mdescRight">Returns the address of a targeted device. [More...](classRelayDotNet_1_1Relay.html#a05c8ec04554103fec2ac28b9dd10bf54)  
</td></tr><tr class="separator:a05c8ec04554103fec2ac28b9dd10bf54"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a7d80fa53811cd7306f4c4163751c68eb"><td align="right" class="memItemLeft" valign="top">async Task&lt; float\[\]&gt; </td><td class="memItemRight" valign="bottom">[GetDeviceCoordinates](classRelayDotNet_1_1Relay.html#a7d80fa53811cd7306f4c4163751c68eb) (IRelayWorkflow relayWorkflow, string sourceUri, bool refresh)</td></tr><tr class="memdesc:a7d80fa53811cd7306f4c4163751c68eb"><td class="mdescLeft"> </td><td class="mdescRight">Retrieves the coordinates of the device's location. [More...](classRelayDotNet_1_1Relay.html#a7d80fa53811cd7306f4c4163751c68eb)  
</td></tr><tr class="separator:a7d80fa53811cd7306f4c4163751c68eb"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a4d9a3226131d0e9239ffed4161053233"><td align="right" class="memItemLeft" valign="top">async Task&lt; float\[\]&gt; </td><td class="memItemRight" valign="bottom">[GetDeviceLatLong](classRelayDotNet_1_1Relay.html#a4d9a3226131d0e9239ffed4161053233) (IRelayWorkflow relayWorkflow, string sourceUri, bool refresh)</td></tr><tr class="memdesc:a4d9a3226131d0e9239ffed4161053233"><td class="mdescLeft"> </td><td class="mdescRight">Returns the latitude and longitude coordinates of a targeted device. [More...](classRelayDotNet_1_1Relay.html#a4d9a3226131d0e9239ffed4161053233)  
</td></tr><tr class="separator:a4d9a3226131d0e9239ffed4161053233"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a38967e11d330a84ee45a2a2f7479f863"><td align="right" class="memItemLeft" valign="top">async Task&lt; float\[\]&gt; </td><td class="memItemRight" valign="bottom">[GetDeviceIndoorLocation](classRelayDotNet_1_1Relay.html#a38967e11d330a84ee45a2a2f7479f863) (IRelayWorkflow relayWorkflow, string sourceUri, bool refresh)</td></tr><tr class="memdesc:a38967e11d330a84ee45a2a2f7479f863"><td class="mdescLeft"> </td><td class="mdescRight">Returns the indoor location of a targeted device. [More...](classRelayDotNet_1_1Relay.html#a38967e11d330a84ee45a2a2f7479f863)  
</td></tr><tr class="separator:a38967e11d330a84ee45a2a2f7479f863"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:abe4dc72d3de58cc393cb7e3cb5a16f7f"><td align="right" class="memItemLeft" valign="top">async Task&lt; int &gt; </td><td class="memItemRight" valign="bottom">[GetDeviceBattery](classRelayDotNet_1_1Relay.html#abe4dc72d3de58cc393cb7e3cb5a16f7f) (IRelayWorkflow relayWorkflow, string sourceUri, bool refresh)</td></tr><tr class="memdesc:abe4dc72d3de58cc393cb7e3cb5a16f7f"><td class="mdescLeft"> </td><td class="mdescRight">Returns the battery level of a targeted device. [More...](classRelayDotNet_1_1Relay.html#abe4dc72d3de58cc393cb7e3cb5a16f7f)  
</td></tr><tr class="separator:abe4dc72d3de58cc393cb7e3cb5a16f7f"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a8e25da0c991cdc8f11b4624ded179434"><td align="right" class="memItemLeft" valign="top">async Task&lt; DeviceType &gt; </td><td class="memItemRight" valign="bottom">[GetDeviceType](classRelayDotNet_1_1Relay.html#a8e25da0c991cdc8f11b4624ded179434) (IRelayWorkflow relayWorkflow, string sourceUri, bool refresh)</td></tr><tr class="memdesc:a8e25da0c991cdc8f11b4624ded179434"><td class="mdescLeft"> </td><td class="mdescRight">Returns the device type of a targeted device, i.e. gen 2, gen 3, etc. [More...](classRelayDotNet_1_1Relay.html#a8e25da0c991cdc8f11b4624ded179434)  
</td></tr><tr class="separator:a8e25da0c991cdc8f11b4624ded179434"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a9f9c40ccbd09656476e18c6c8bf7a37a"><td align="right" class="memItemLeft" valign="top">async Task&lt; string &gt; </td><td class="memItemRight" valign="bottom">[GetUserProfile](classRelayDotNet_1_1Relay.html#a9f9c40ccbd09656476e18c6c8bf7a37a) (IRelayWorkflow relayWorkflow, string sourceUri)</td></tr><tr class="memdesc:a9f9c40ccbd09656476e18c6c8bf7a37a"><td class="mdescLeft"> </td><td class="mdescRight">Returns the user profile of a targeted device. [More...](classRelayDotNet_1_1Relay.html#a9f9c40ccbd09656476e18c6c8bf7a37a)  
</td></tr><tr class="separator:a9f9c40ccbd09656476e18c6c8bf7a37a"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:add3df2764180ddbd795fa64ca9b13d0e"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[EnableLocation](classRelayDotNet_1_1Relay.html#add3df2764180ddbd795fa64ca9b13d0e) (IRelayWorkflow relayWorkflow, string sourceUri)</td></tr><tr class="memdesc:add3df2764180ddbd795fa64ca9b13d0e"><td class="mdescLeft"> </td><td class="mdescRight">Enables the location services on a device. [More...](classRelayDotNet_1_1Relay.html#add3df2764180ddbd795fa64ca9b13d0e)  
</td></tr><tr class="separator:add3df2764180ddbd795fa64ca9b13d0e"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a79fe828aa1d44c10e0c4ed214f71aad6"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[DisableLocation](classRelayDotNet_1_1Relay.html#a79fe828aa1d44c10e0c4ed214f71aad6) (IRelayWorkflow relayWorkflow, string sourceUri)</td></tr><tr class="memdesc:a79fe828aa1d44c10e0c4ed214f71aad6"><td class="mdescLeft"> </td><td class="mdescRight">Disables the location services on a device. [More...](classRelayDotNet_1_1Relay.html#a79fe828aa1d44c10e0c4ed214f71aad6)  
</td></tr><tr class="separator:a79fe828aa1d44c10e0c4ed214f71aad6"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:ae3a2422f79662419b6b08488d24b04c3"><td align="right" class="memItemLeft" valign="top">async Task&lt; bool &gt; </td><td class="memItemRight" valign="bottom">[GetDeviceLocationEnabled](classRelayDotNet_1_1Relay.html#ae3a2422f79662419b6b08488d24b04c3) (IRelayWorkflow relayWorkflow, string sourceUri, bool refresh)</td></tr><tr class="memdesc:ae3a2422f79662419b6b08488d24b04c3"><td class="mdescLeft"> </td><td class="mdescRight">Returns true if the device's location services are enabled, false otherwise. [More...](classRelayDotNet_1_1Relay.html#ae3a2422f79662419b6b08488d24b04c3)  
</td></tr><tr class="separator:ae3a2422f79662419b6b08488d24b04c3"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:abda42a1c778d75e56244b5152b524916"><td align="right" class="memItemLeft" valign="top">async Task&lt; string\[\]&gt; </td><td class="memItemRight" valign="bottom">[SetUserProfile](classRelayDotNet_1_1Relay.html#abda42a1c778d75e56244b5152b524916) (IRelayWorkflow relayWorkflow, string sourceUri, string username, bool force=false)</td></tr><tr class="memdesc:abda42a1c778d75e56244b5152b524916"><td class="mdescLeft"> </td><td class="mdescRight">Sets the profile of a user by updating the username. [More...](classRelayDotNet_1_1Relay.html#abda42a1c778d75e56244b5152b524916)  
</td></tr><tr class="separator:abda42a1c778d75e56244b5152b524916"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a1391c0aba85616ba7b5aa2b3bbaa25ca"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[LogUserMessage](classRelayDotNet_1_1Relay.html#a1391c0aba85616ba7b5aa2b3bbaa25ca) (IRelayWorkflow relayWorkflow, string message, string sourceUri, string category)</td></tr><tr class="memdesc:a1391c0aba85616ba7b5aa2b3bbaa25ca"><td class="mdescLeft"> </td><td class="mdescRight">Log an analytic event from a workflow with the specified content and under a specified category. This includes the device who triggered the workflow that called this function. [More...](classRelayDotNet_1_1Relay.html#a1391c0aba85616ba7b5aa2b3bbaa25ca)  
</td></tr><tr class="separator:a1391c0aba85616ba7b5aa2b3bbaa25ca"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a3fedf3d1d867ab2f4fe29a1023baefe8"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[LogMessage](classRelayDotNet_1_1Relay.html#a3fedf3d1d867ab2f4fe29a1023baefe8) (IRelayWorkflow relayWorkflow, string message, string category)</td></tr><tr class="memdesc:a3fedf3d1d867ab2f4fe29a1023baefe8"><td class="mdescLeft"> </td><td class="mdescRight">Log an analytics event from a workflow with the specified content and under a specified category. This does not log the device who triggered the workflow that called this function. [More...](classRelayDotNet_1_1Relay.html#a3fedf3d1d867ab2f4fe29a1023baefe8)  
</td></tr><tr class="separator:a3fedf3d1d867ab2f4fe29a1023baefe8"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a779cda162dcb8d5cb98a2b9f11d2b099"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[SetDeviceName](classRelayDotNet_1_1Relay.html#a779cda162dcb8d5cb98a2b9f11d2b099) (IRelayWorkflow relayWorkflow, string sourceUri, string name)</td></tr><tr class="memdesc:a779cda162dcb8d5cb98a2b9f11d2b099"><td class="mdescLeft"> </td><td class="mdescRight">Sets the name of a targeted device and updates it on the [Relay](classRelayDotNet_1_1Relay.html "The Relay class is responsible for defining the main functionalities that are used within workflows,...") Dash. The name remains updated until it is set again via a workflow or updated manually on the [Relay](classRelayDotNet_1_1Relay.html "The Relay class is responsible for defining the main functionalities that are used within workflows,...") Dash. [More...](classRelayDotNet_1_1Relay.html#a779cda162dcb8d5cb98a2b9f11d2b099)  
</td></tr><tr class="separator:a779cda162dcb8d5cb98a2b9f11d2b099"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a27334c5082e7a29425bb16247cc8e20b"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[SetChannel](classRelayDotNet_1_1Relay.html#a27334c5082e7a29425bb16247cc8e20b) (IRelayWorkflow relayWorkflow, string sourceUri, string channel, string\[\] targets, bool suppressTts=false, bool disableHomeChannel=false)</td></tr><tr class="memdesc:a27334c5082e7a29425bb16247cc8e20b"><td class="mdescLeft"> </td><td class="mdescRight">Sets the channel that a device is on. This can be used to change the channel of a device during a workflow, where the channel will also be updated on the [Relay](classRelayDotNet_1_1Relay.html "The Relay class is responsible for defining the main functionalities that are used within workflows,...") Dash. [More...](classRelayDotNet_1_1Relay.html#a27334c5082e7a29425bb16247cc8e20b)  
</td></tr><tr class="separator:a27334c5082e7a29425bb16247cc8e20b"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a809c834ab24eb126ed19f2ceb7cb46e7"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[EnableHomeChannel](classRelayDotNet_1_1Relay.html#a809c834ab24eb126ed19f2ceb7cb46e7) (IRelayWorkflow relayWorkflow, string sourceUri, string target)</td></tr><tr class="memdesc:a809c834ab24eb126ed19f2ceb7cb46e7"><td class="mdescLeft"> </td><td class="mdescRight">Sets the home channel state on the device to true. [More...](classRelayDotNet_1_1Relay.html#a809c834ab24eb126ed19f2ceb7cb46e7)  
</td></tr><tr class="separator:a809c834ab24eb126ed19f2ceb7cb46e7"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a0d477deddb10414f48def5d41c657649"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[EnableHomeChannel](classRelayDotNet_1_1Relay.html#a0d477deddb10414f48def5d41c657649) (IRelayWorkflow relayWorkflow, string sourceUri, string\[\] targets)</td></tr><tr class="separator:a0d477deddb10414f48def5d41c657649"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a570df8712d6b2e86425a624bcd692f97"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[DisableHomeChannel](classRelayDotNet_1_1Relay.html#a570df8712d6b2e86425a624bcd692f97) (IRelayWorkflow relayWorkflow, string sourceUri, string target)</td></tr><tr class="memdesc:a570df8712d6b2e86425a624bcd692f97"><td class="mdescLeft"> </td><td class="mdescRight">Sets the home channel state on the device to false. [More...](classRelayDotNet_1_1Relay.html#a570df8712d6b2e86425a624bcd692f97)  
</td></tr><tr class="separator:a570df8712d6b2e86425a624bcd692f97"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a6e04a5fa2eaa5b3e2c80037fbb8ce76f"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[DisableHomeChannel](classRelayDotNet_1_1Relay.html#a6e04a5fa2eaa5b3e2c80037fbb8ce76f) (IRelayWorkflow relayWorkflow, string sourceUri, string\[\] targets)</td></tr><tr class="separator:a6e04a5fa2eaa5b3e2c80037fbb8ce76f"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:a7954a5fe4c2705741f99b64d07803b5a"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[AnswerCall](classRelayDotNet_1_1Relay.html#a7954a5fe4c2705741f99b64d07803b5a) (IRelayWorkflow relayWorkflow, string sourceUri, string callId)</td></tr><tr class="memdesc:a7954a5fe4c2705741f99b64d07803b5a"><td class="mdescLeft"> </td><td class="mdescRight">Answers a call on your device. [More...](classRelayDotNet_1_1Relay.html#a7954a5fe4c2705741f99b64d07803b5a)  
</td></tr><tr class="separator:a7954a5fe4c2705741f99b64d07803b5a"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:ab96a229ca88b009834633235f1697752"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[PlaceCall](classRelayDotNet_1_1Relay.html#ab96a229ca88b009834633235f1697752) (IRelayWorkflow relayWorkflow, string sourceUri, string uri)</td></tr><tr class="memdesc:ab96a229ca88b009834633235f1697752"><td class="mdescLeft"> </td><td class="mdescRight">Places a call to another device. [More...](classRelayDotNet_1_1Relay.html#ab96a229ca88b009834633235f1697752)  
</td></tr><tr class="separator:ab96a229ca88b009834633235f1697752"><td class="memSeparator" colspan="2"> </td></tr><tr class="memitem:acbb938659b9eb28c471993627c7ecfd9"><td align="right" class="memItemLeft" valign="top">async Task&lt; Dictionary&lt; string, object &gt; &gt; </td><td class="memItemRight" valign="bottom">[HangupCall](classRelayDotNet_1_1Relay.html#acbb938659b9eb28c471993627c7ecfd9) (IRelayWorkflow relayWorkflow, string sourceUri, string callId)</td></tr><tr class="memdesc:acbb938659b9eb28c471993627c7ecfd9"><td class="mdescLeft"> </td><td class="mdescRight">Ends a call between two devices. [More...](classRelayDotNet_1_1Relay.html#acbb938659b9eb28c471993627c7ecfd9)  
</td></tr><tr class="separator:acbb938659b9eb28c471993627c7ecfd9"><td class="memSeparator" colspan="2"> </td></tr></table>

<table class="memberdecls"><tr class="heading"><td colspan="2"><a name="pub-attribs"></a>Public Attributes
-------------------------------------------

</td></tr><tr class="memitem:af26e3167ec9929b5f652959b1348b226"><td align="right" class="memItemLeft" valign="top">IRelayWebSocketConnector </td><td class="memItemRight" valign="bottom">[RelayWebSocketConnector](classRelayDotNet_1_1Relay.html#af26e3167ec9929b5f652959b1348b226) =&gt; \_relayWebSocketConnector</td></tr><tr class="separator:af26e3167ec9929b5f652959b1348b226"><td class="memSeparator" colspan="2"> </td></tr></table>

<a id="details" name="details"></a>Detailed Description
--------------------

<div class="textblock">The [Relay](classRelayDotNet_1_1Relay.html "The Relay class is responsible for defining the main functionalities that are used within workflows,...") class is responsible for defining the main functionalities that are used within workflows, such as functions for communicating with the device, sending out notifications to groups, handling workflow events, and performing physical actions on the device such as manipulating LEDs and creating vibrations.

</div>Member Enumeration Documentation
--------------------------------

<a id="a1bf1d579d646e9c59cd41ec8e66b6992"></a><span class="permalink">[◆ ](#a1bf1d579d646e9c59cd41ec8e66b6992)</span>WebSocketConnector
-----------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">enum [RelayDotNet.Relay.WebSocketConnector](classRelayDotNet_1_1Relay.html#a1bf1d579d646e9c59cd41ec8e66b6992)</td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">strong</span></span> </td> </tr></table>

</div><div class="memdoc"><table class="fieldtable"><tr><th colspan="2">Enumerator</th></tr><tr><td class="fieldname"><a id="a1bf1d579d646e9c59cd41ec8e66b6992ab397fe519526faaf878e1f99fbdf8a1c"></a>Fleck </td><td class="fielddoc"></td></tr><tr><td class="fieldname"><a id="a1bf1d579d646e9c59cd41ec8e66b6992abb0a32bf523203bdf99ac3c823f3f281"></a>SystemHttpListener </td><td class="fielddoc"></td></tr><tr><td class="fieldname"><a id="a1bf1d579d646e9c59cd41ec8e66b6992a094bf0f4f26f8e2b9207728d8d705d17"></a>SystemKestral </td><td class="fielddoc"></td></tr></table>

</div></div>Constructor &amp; Destructor Documentation
------------------------------------------

<a id="ad058ffa5829109f8560ac37d9b52648b"></a><span class="permalink">[◆ ](#ad058ffa5829109f8560ac37d9b52648b)</span>Relay()
------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">RelayDotNet.Relay.Relay </td> <td>(</td> <td class="paramtype">[WebSocketConnector](classRelayDotNet_1_1Relay.html#a1bf1d579d646e9c59cd41ec8e66b6992) </td> <td class="paramname">*webSocketConnector*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*ip*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">int </td> <td class="paramname">*port*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">bool </td> <td class="paramname">*secure* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc"></div></div>Member Function Documentation
-----------------------------

<a id="aa0190530a8a1e360b8c1e9e757148bbc"></a><span class="permalink">[◆ ](#aa0190530a8a1e360b8c1e9e757148bbc)</span>AddWorkflow()
------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;bool&gt; RelayDotNet.Relay.AddWorkflow </td> <td>(</td> <td class="paramtype">string </td> <td class="paramname">*path*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">Type </td> <td class="paramname">*relayWorkflowType* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc"></div></div><a id="ab7f6431cd4f933886da8052fc47dba93"></a><span class="permalink">[◆ ](#ab7f6431cd4f933886da8052fc47dba93)</span>Alert() <span class="overload">\[1/2\]</span>
--------------------------------------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.Alert </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*targets*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*name*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*text* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Sends out an alert to the specified group of devices and the [Relay](classRelayDotNet_1_1Relay.html "The Relay class is responsible for defining the main functionalities that are used within workflows,...") Dash.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the URN of the device that triggered the alert.</td></tr> <tr><td class="paramname">name</td><td>a name for your alert.</td></tr> <tr><td class="paramname">text</td><td>the text that you would like to be spoken to the group as your alert.</td></tr> <tr><td class="paramname">targets</td><td>the group URN that you would like to send your alert to.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="a472c0c73754eb35e3e9fdd230e61c567"></a><span class="permalink">[◆ ](#a472c0c73754eb35e3e9fdd230e61c567)</span>Alert() <span class="overload">\[2/2\]</span>
--------------------------------------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.Alert </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*targets*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*name*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*text*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">NotificationPushOptions </td> <td class="paramname">*notificationPushOptions* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Sends out an alert to the specified group of devices and the [Relay](classRelayDotNet_1_1Relay.html "The Relay class is responsible for defining the main functionalities that are used within workflows,...") Dash.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the URN of the device that triggered the alert.</td></tr> <tr><td class="paramname">name</td><td>a name for your alert.</td></tr> <tr><td class="paramname">text</td><td>the text that you would like to be spoken to the group as your alert.</td></tr> <tr><td class="paramname">targets</td><td>the group URN that you would like to send your alert to.</td></tr> <tr><td class="paramname">notificationPushOptions</td><td>push options for if the alert is sent to the [Relay](classRelayDotNet_1_1Relay.html "The Relay class is responsible for defining the main functionalities that are used within workflows,...") app on a virtual device. Defaults to {}.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="a7954a5fe4c2705741f99b64d07803b5a"></a><span class="permalink">[◆ ](#a7954a5fe4c2705741f99b64d07803b5a)</span>AnswerCall()
-----------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.AnswerCall </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*callId* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Answers a call on your device.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the device URN.</td></tr> <tr><td class="paramname">callId</td><td>the call ID.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd></dd></dl></div></div><a id="a0f033ad67f11c88195596afc32655e7b"></a><span class="permalink">[◆ ](#a0f033ad67f11c88195596afc32655e7b)</span>Breathe()
--------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.Breathe </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*color* = `"0000FF"` </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Switches all of the LEDs on a device to a certain color and creates a 'breathing' effect, where the LEDs will slowly light up a specified number of times.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the interaction URN.</td></tr> <tr><td class="paramname">color</td><td>the hex color code you would like to turn the LEDs to. Defaults to '0000FF'.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="a335a03e3d1696a1b25b1d0945fe41682"></a><span class="permalink">[◆ ](#a335a03e3d1696a1b25b1d0945fe41682)</span>Broadcast() <span class="overload">\[1/2\]</span>
------------------------------------------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.Broadcast </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*targets*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*name*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*text* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Sends out a broadcasted message to a group of devices. The message is played out on all devices, as well as sent to the [Relay](classRelayDotNet_1_1Relay.html "The Relay class is responsible for defining the main functionalities that are used within workflows,...") Dash.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the device URN that triggered the broadcast.</td></tr> <tr><td class="paramname">name</td><td>a name for your broadcast.</td></tr> <tr><td class="paramname">text</td><td>the text that you would like to be broadcasted to your group.</td></tr> <tr><td class="paramname">targets</td><td>the group URN that you would like to broadcast your message to.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="a315975e070f89442d38b3a35728f16d9"></a><span class="permalink">[◆ ](#a315975e070f89442d38b3a35728f16d9)</span>Broadcast() <span class="overload">\[2/2\]</span>
------------------------------------------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.Broadcast </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*targets*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*name*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*text*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">NotificationPushOptions </td> <td class="paramname">*notificationPushOptions* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Sends out a broadcasted message to a group of devices. The message is played out on all devices, as well as sent to the [Relay](classRelayDotNet_1_1Relay.html "The Relay class is responsible for defining the main functionalities that are used within workflows,...") Dash.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the device URN that triggered the broadcast.</td></tr> <tr><td class="paramname">name</td><td>a name for your broadcast.</td></tr> <tr><td class="paramname">text</td><td>the text that you would like to be broadcasted to your group.</td></tr> <tr><td class="paramname">targets</td><td>the group URN that you would like to broadcast your message to.</td></tr> <tr><td class="paramname">notificationPushOptions</td><td>push options for if the notification is sent to the [Relay](classRelayDotNet_1_1Relay.html "The Relay class is responsible for defining the main functionalities that are used within workflows,...") app on a virtual device. Defaults to {}.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="ae37b55385656e88e8aafb3edeed6d6a0"></a><span class="permalink">[◆ ](#ae37b55385656e88e8aafb3edeed6d6a0)</span>CancelAlert()
------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.CancelAlert </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*name*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*targets* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Cancels an alert that was sent to a group of devices. Particularly useful if you would like to cancel the alert on all devices after one device has acknowledged the alert.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the URN of the device that acknowledged or is cancelling the alert.</td></tr> <tr><td class="paramname">name</td><td>the name of the alert.</td></tr> <tr><td class="paramname">targets</td><td>the group URN that received the alert.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="a82217a04532c11a2eabf81f857eccc7c"></a><span class="permalink">[◆ ](#a82217a04532c11a2eabf81f857eccc7c)</span>CancelBroadcast()
----------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.CancelBroadcast </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*name*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*targets* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Cancels the broadcast that was sent to a group of devices.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the device URN that is cancelling the broadcast.</td></tr> <tr><td class="paramname">name</td><td>the name of the broadcast you would like to cancel.</td></tr> <tr><td class="paramname">targets</td><td>the group URN that received the broadcast.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="a902bbe1c5e343dcdc4da4f736a7b644d"></a><span class="permalink">[◆ ](#a902bbe1c5e343dcdc4da4f736a7b644d)</span>CancelNotification()
-------------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.CancelNotification </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*name*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*targets* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Cancels a notification of any type that was sent to a group of devices.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the device or interacion URN that sent out the message.</td></tr> <tr><td class="paramname">name</td><td>the name of the notification to cancel.</td></tr> <tr><td class="paramname">targets</td><td>the group URN that received the notification.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="a461275ffe7aa16437588e216809d2ff5"></a><span class="permalink">[◆ ](#a461275ffe7aa16437588e216809d2ff5)</span>CancelNotify()
-------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.CancelNotify </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*name*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*targets* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Cancels the notification that was sent to a group of devices.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the device URN that is cancelling the notification.</td></tr> <tr><td class="paramname">name</td><td>the name of the notification that you would like to cancel.</td></tr> <tr><td class="paramname">targets</td><td>the group URN that received the notification.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="a17bbf9bf5a37c27e7ad9ec73e8c1d239"></a><span class="permalink">[◆ ](#a17bbf9bf5a37c27e7ad9ec73e8c1d239)</span>ClearTimer()
-----------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.ClearTimer </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*name* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Clears the specified timer.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">name</td><td>the name of the timer that you would like to clear.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="a57f04373456084a730e531b153d7d538"></a><span class="permalink">[◆ ](#a57f04373456084a730e531b153d7d538)</span>CreateIncident()
---------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.CreateIncident </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*originator*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*iType* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Creates an incident that will alert the [Relay](classRelayDotNet_1_1Relay.html "The Relay class is responsible for defining the main functionalities that are used within workflows,...") Dash.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">originator</td><td>the device URN that triggered the incident.</td></tr> <tr><td class="paramname">iType</td><td>the type of incident that occurred.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="a570df8712d6b2e86425a624bcd692f97"></a><span class="permalink">[◆ ](#a570df8712d6b2e86425a624bcd692f97)</span>DisableHomeChannel() <span class="overload">\[1/2\]</span>
---------------------------------------------------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.DisableHomeChannel </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*target* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Sets the home channel state on the device to false.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the URN of the device that called the function.</td></tr> <tr><td class="paramname">target</td><td>the device URN whose home channel you would like to set.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="a6e04a5fa2eaa5b3e2c80037fbb8ce76f"></a><span class="permalink">[◆ ](#a6e04a5fa2eaa5b3e2c80037fbb8ce76f)</span>DisableHomeChannel() <span class="overload">\[2/2\]</span>
---------------------------------------------------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.DisableHomeChannel </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string\[\] </td> <td class="paramname">*targets* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc"></div></div><a id="a79fe828aa1d44c10e0c4ed214f71aad6"></a><span class="permalink">[◆ ](#a79fe828aa1d44c10e0c4ed214f71aad6)</span>DisableLocation()
----------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.DisableLocation </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Disables the location services on a device.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the device or interaction URN.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="a4892a3f21417de146b2b454f14df54dc"></a><span class="permalink">[◆ ](#a4892a3f21417de146b2b454f14df54dc)</span>Dispose()
--------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">void RelayDotNet.Relay.Dispose </td> <td>(</td> <td class="paramname"></td><td>)</td> <td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc"></div></div><a id="a809c834ab24eb126ed19f2ceb7cb46e7"></a><span class="permalink">[◆ ](#a809c834ab24eb126ed19f2ceb7cb46e7)</span>EnableHomeChannel() <span class="overload">\[1/2\]</span>
--------------------------------------------------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.EnableHomeChannel </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*target* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Sets the home channel state on the device to true.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the URN of the device that called the function.</td></tr> <tr><td class="paramname">target</td><td>the device URN whose home channel you would like to set.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="a0d477deddb10414f48def5d41c657649"></a><span class="permalink">[◆ ](#a0d477deddb10414f48def5d41c657649)</span>EnableHomeChannel() <span class="overload">\[2/2\]</span>
--------------------------------------------------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.EnableHomeChannel </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string\[\] </td> <td class="paramname">*targets* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc"></div></div><a id="add3df2764180ddbd795fa64ca9b13d0e"></a><span class="permalink">[◆ ](#add3df2764180ddbd795fa64ca9b13d0e)</span>EnableLocation()
---------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.EnableLocation </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Enables the location services on a device.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the device or interaction URN.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="ab80f54bd7fdc31c436b639a72143a49b"></a><span class="permalink">[◆ ](#ab80f54bd7fdc31c436b639a72143a49b)</span>EndInteraction()
---------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async void RelayDotNet.Relay.EndInteraction </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*name* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Ends an interaction with the user. Triggers an INTERACTION\_ENDED event to signify that the user is done interacting with the device.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the device that you would like to end an interaction with.</td></tr> <tr><td class="paramname">name</td><td>the name of the interaction that you would like to end.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="a73fdfe528e585b870d60368081b9e476"></a><span class="permalink">[◆ ](#a73fdfe528e585b870d60368081b9e476)</span>Flash()
------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.Flash </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*color* = `"0000FF"` </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Switches all of the LEDs on a device to a certain color and flashes them a specified number of times.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the interaction URN.</td></tr> <tr><td class="paramname">color</td><td>the hex color code you would like to turn the LEDs to. Defaults to '0000FF'.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="a05c8ec04554103fec2ac28b9dd10bf54"></a><span class="permalink">[◆ ](#a05c8ec04554103fec2ac28b9dd10bf54)</span>GetDeviceAddress()
-----------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;string&gt; RelayDotNet.Relay.GetDeviceAddress </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">bool </td> <td class="paramname">*refresh* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Returns the address of a targeted device.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the device or interaction URN.</td></tr> <tr><td class="paramname">refresh</td><td>whether you would like to refresh before retrieving the address. Defaults to false.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the address of the device.</dd></dl></div></div><a id="abe4dc72d3de58cc393cb7e3cb5a16f7f"></a><span class="permalink">[◆ ](#abe4dc72d3de58cc393cb7e3cb5a16f7f)</span>GetDeviceBattery()
-----------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;int&gt; RelayDotNet.Relay.GetDeviceBattery </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">bool </td> <td class="paramname">*refresh* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Returns the battery level of a targeted device.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the device or interaction URN.</td></tr> <tr><td class="paramname">refresh</td><td>whether you would like to refresh before retrieving the battery. Defaults to false.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the battery level on the device.</dd></dl></div></div><a id="a7d80fa53811cd7306f4c4163751c68eb"></a><span class="permalink">[◆ ](#a7d80fa53811cd7306f4c4163751c68eb)</span>GetDeviceCoordinates()
---------------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;float\[\]&gt; RelayDotNet.Relay.GetDeviceCoordinates </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">bool </td> <td class="paramname">*refresh* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Retrieves the coordinates of the device's location.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the device or interaction URN.</td></tr> <tr><td class="paramname">refresh</td><td>whether you would like to refresh before retrieving the coordinates. Defaults to false.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the coordinates of the device's location.</dd></dl></div></div><a id="a9a523506999c0eeaaa5084a0c8c4a758"></a><span class="permalink">[◆ ](#a9a523506999c0eeaaa5084a0c8c4a758)</span>GetDeviceId()
------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;string&gt; RelayDotNet.Relay.GetDeviceId </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Returns the ID of a targeted device.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the device or interaction URN.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the device ID.</dd></dl></div></div><a id="a38967e11d330a84ee45a2a2f7479f863"></a><span class="permalink">[◆ ](#a38967e11d330a84ee45a2a2f7479f863)</span>GetDeviceIndoorLocation()
------------------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;float\[\]&gt; RelayDotNet.Relay.GetDeviceIndoorLocation </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">bool </td> <td class="paramname">*refresh* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Returns the indoor location of a targeted device.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the device or interaction URN.</td></tr> <tr><td class="paramname">refresh</td><td>whether you wouldlike to refresh before retrieving the location. Defaults to false.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the indoor location of the device.</dd></dl></div></div><a id="a4d9a3226131d0e9239ffed4161053233"></a><span class="permalink">[◆ ](#a4d9a3226131d0e9239ffed4161053233)</span>GetDeviceLatLong()
-----------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;float\[\]&gt; RelayDotNet.Relay.GetDeviceLatLong </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">bool </td> <td class="paramname">*refresh* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Returns the latitude and longitude coordinates of a targeted device.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the device or interaction URN.</td></tr> <tr><td class="paramname">refresh</td><td>whether you would like to refresh before retrieving the coordinates. Defaults to false.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>an array containing the latitude and longitude of the device's location.</dd></dl></div></div><a id="ae290836a7d99bc565f4b8cd537872599"></a><span class="permalink">[◆ ](#ae290836a7d99bc565f4b8cd537872599)</span>GetDeviceLocation()
------------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;string&gt; RelayDotNet.Relay.GetDeviceLocation </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">bool </td> <td class="paramname">*refresh* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Returns the location of a targeted device.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the device or interaction URN.</td></tr> <tr><td class="paramname">refresh</td><td>whether you would like to refresh before retrieving the location. Defaults to false.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the location of the device.</dd></dl></div></div><a id="ae3a2422f79662419b6b08488d24b04c3"></a><span class="permalink">[◆ ](#ae3a2422f79662419b6b08488d24b04c3)</span>GetDeviceLocationEnabled()
-------------------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;bool&gt; RelayDotNet.Relay.GetDeviceLocationEnabled </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">bool </td> <td class="paramname">*refresh* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Returns true if the device's location services are enabled, false otherwise.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow</td></tr> <tr><td class="paramname">sourceUri</td><td>the device or interaction URN.</td></tr> <tr><td class="paramname">refresh</td><td>whether you would like to refresh before retrieving the query information.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>true if the device's location services are enabled, false otherwise.</dd></dl></div></div><a id="aaad217740ce694edb1bee0946a186b1a"></a><span class="permalink">[◆ ](#aaad217740ce694edb1bee0946a186b1a)</span>GetDeviceName()
--------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;string&gt; RelayDotNet.Relay.GetDeviceName </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Returns the name of a targeted device.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the device or interaction URN.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the name of the device.</dd></dl></div></div><a id="a8e25da0c991cdc8f11b4624ded179434"></a><span class="permalink">[◆ ](#a8e25da0c991cdc8f11b4624ded179434)</span>GetDeviceType()
--------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;DeviceType&gt; RelayDotNet.Relay.GetDeviceType </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">bool </td> <td class="paramname">*refresh* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Returns the device type of a targeted device, i.e. gen 2, gen 3, etc.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the device or interaction URN.</td></tr> <tr><td class="paramname">refresh</td><td>whether you would like to refresh before retrieving the device type.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the device type.</dd></dl></div></div><a id="a100e260d4bd0107cc7b21f0e00bd50da"></a><span class="permalink">[◆ ](#a100e260d4bd0107cc7b21f0e00bd50da)</span>GetGroupMembers()
----------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;string\[\]&gt; RelayDotNet.Relay.GetGroupMembers </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*groupName* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Returns the members of a particular group.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">groupName</td><td>the name of the group whose members you would like to retrieve. </td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>an array containing all of the device names in the specified group.</dd></dl></div></div><a id="a28d1ad3573ab5d7848ce8130322957ee"></a><span class="permalink">[◆ ](#a28d1ad3573ab5d7848ce8130322957ee)</span>GetNumberVar()
-------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;int&gt; RelayDotNet.Relay.GetNumberVar </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*name*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">int </td> <td class="paramname">*defaultValue* = `-1` </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Retrieves a variable that is an integer type.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">name</td><td>the name of the variable to retrieve.</td></tr> <tr><td class="paramname">defaultValue</td><td>the default value for the variable if it does not exist.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the variable requested</dd></dl></div></div><a id="aa99f9d1af832737f16225be89d1f5fad"></a><span class="permalink">[◆ ](#aa99f9d1af832737f16225be89d1f5fad)</span>GetUnreadInboxSize()
-------------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;int&gt; RelayDotNet.Relay.GetUnreadInboxSize </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc"></div></div><a id="a9f9c40ccbd09656476e18c6c8bf7a37a"></a><span class="permalink">[◆ ](#a9f9c40ccbd09656476e18c6c8bf7a37a)</span>GetUserProfile()
---------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;string&gt; RelayDotNet.Relay.GetUserProfile </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Returns the user profile of a targeted device.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the device or interaction URN.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the user profile registered to the device.</dd></dl></div></div><a id="a8e4a49291b750122f52e804e8399033e"></a><span class="permalink">[◆ ](#a8e4a49291b750122f52e804e8399033e)</span>GetVar()
-------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;string&gt; RelayDotNet.Relay.GetVar </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*name*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*defaultValue* = `null` </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Retrieves a variable that was set either during workflow registration or through the set\_var() function. The variable can be retrieved anywhere within the workflow, but is erased after the workflow terminates.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">name</td><td>name of the variable to be retrieved.</td></tr> <tr><td class="paramname">defaultValue</td><td>default value of the variable if it does not exist. Defaults to undefined.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the variable requested.</dd></dl></div></div><a id="acbb938659b9eb28c471993627c7ecfd9"></a><span class="permalink">[◆ ](#acbb938659b9eb28c471993627c7ecfd9)</span>HangupCall()
-----------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.HangupCall </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*callId* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Ends a call between two devices.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the device that is hanging up the call.</td></tr> <tr><td class="paramname">callId</td><td>the call ID.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="af0bae904854f2e7f9bfce0c5103338cc"></a><span class="permalink">[◆ ](#af0bae904854f2e7f9bfce0c5103338cc)</span>Listen() <span class="overload">\[1/4\]</span>
---------------------------------------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.Listen </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Listens for the user to speak into the device. Utilizes speech to text functionality to interact with the user.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the interaction URN.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>text representation of what the user had spoken into the device.</dd></dl></div></div><a id="ae69a5d38c458820952ae4e54e43e66df"></a><span class="permalink">[◆ ](#ae69a5d38c458820952ae4e54e43e66df)</span>Listen() <span class="overload">\[2/4\]</span>
---------------------------------------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.Listen </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">Language </td> <td class="paramname">*language* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Listens for the user to speak into the device. Utilizes speech to text functionality to interact with the user.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the interaction URN.</td></tr> <tr><td class="paramname">language</td><td>the language that the device is listening for. Defaults to 'en-US'.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>text representation of what the user had spoken into the device.</dd></dl></div></div><a id="a5814e4da1febbbef39ed1e3da77a120b"></a><span class="permalink">[◆ ](#a5814e4da1febbbef39ed1e3da77a120b)</span>Listen() <span class="overload">\[3/4\]</span>
---------------------------------------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.Listen </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string\[\] </td> <td class="paramname">*phrases* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Listens for the user to speak into the device. Utilizes speech to text functionality to interact with the user.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the interaction URN.</td></tr> <tr><td class="paramname">phrases</td><td>optional phrases that you would like to limit the user's response to. Defualts to none.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>text representation of what the user had spoken into the device.</dd></dl></div></div><a id="a61c0cbb0b689feecca8fa848d4066b48"></a><span class="permalink">[◆ ](#a61c0cbb0b689feecca8fa848d4066b48)</span>Listen() <span class="overload">\[4/4\]</span>
---------------------------------------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.Listen </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string\[\] </td> <td class="paramname">*phrases*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">Language </td> <td class="paramname">*altLanguage*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">bool </td> <td class="paramname">*transcribe* = `true`, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">int </td> <td class="paramname">*timeout* = `60` </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Listens for the user to speak into the device. Utilizes speech to text functionality to interact with the user.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the interaction URN.</td></tr> <tr><td class="paramname">phrases</td><td>limits the user's response to these phrases.</td></tr> <tr><td class="paramname">altLanguage</td><td>the language that the device will listen for. Defaults to 'en-US'.</td></tr> <tr><td class="paramname">transcribe</td><td>whether you would like to transcribe.</td></tr> <tr><td class="paramname">timeout</td><td>how long to wait for a user's response before timing out.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>text representation of what the user had spoken into the device.</dd></dl></div></div><a id="a3fedf3d1d867ab2f4fe29a1023baefe8"></a><span class="permalink">[◆ ](#a3fedf3d1d867ab2f4fe29a1023baefe8)</span>LogMessage()
-----------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.LogMessage </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*message*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*category* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Log an analytics event from a workflow with the specified content and under a specified category. This does not log the device who triggered the workflow that called this function.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">message</td><td>a description for your analytical event.</td></tr> <tr><td class="paramname">category</td><td>a category for your analytical event.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd></dd></dl></div></div><a id="a1391c0aba85616ba7b5aa2b3bbaa25ca"></a><span class="permalink">[◆ ](#a1391c0aba85616ba7b5aa2b3bbaa25ca)</span>LogUserMessage()
---------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.LogUserMessage </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*message*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*category* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Log an analytic event from a workflow with the specified content and under a specified category. This includes the device who triggered the workflow that called this function.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">message</td><td>a description for your analytical event.</td></tr> <tr><td class="paramname">sourceUri</td><td>the URN of a device that triggered this function. Defaults to None.</td></tr> <tr><td class="paramname">category</td><td>a category for your analytical event.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd></dd></dl></div></div><a id="afd01c4889a7c3a33e1a0c790d3fb552f"></a><span class="permalink">[◆ ](#afd01c4889a7c3a33e1a0c790d3fb552f)</span>Notify() <span class="overload">\[1/2\]</span>
---------------------------------------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.Notify </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*targets*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*name*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*text* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Sends out a notification message to a group of devices.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the device URN that triggered then notification.</td></tr> <tr><td class="paramname">name</td><td>a name for your notification.</td></tr> <tr><td class="paramname">text</td><td>the text that you would like to be spoken out of the device as your notification.</td></tr> <tr><td class="paramname">targets</td><td>the group URN that you would like to notify</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="ab13663f82e717a508ea9b747e3acfc38"></a><span class="permalink">[◆ ](#ab13663f82e717a508ea9b747e3acfc38)</span>Notify() <span class="overload">\[2/2\]</span>
---------------------------------------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.Notify </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*targets*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*name*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*text*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">NotificationPushOptions </td> <td class="paramname">*notificationPushOptions* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Sends out a notification message to a group of devices.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the device URN that triggered the notification.</td></tr> <tr><td class="paramname">name</td><td>a name for your notification.</td></tr> <tr><td class="paramname">text</td><td>the text that you would like to be spoken out of the device as your notification.</td></tr> <tr><td class="paramname">targets</td><td>the group URN that you would like to notify.</td></tr> <tr><td class="paramname">notificationPushOptions</td><td>push options for if the notification is sent to the [Relay](classRelayDotNet_1_1Relay.html "The Relay class is responsible for defining the main functionalities that are used within workflows,...") app on a virtual device. Defaults to {}.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="a20439289f01f1d7b5dde8a96f0a8ef52"></a><span class="permalink">[◆ ](#a20439289f01f1d7b5dde8a96f0a8ef52)</span>OnClose()
--------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async void RelayDotNet.Relay.OnClose </td> <td>(</td> <td class="paramtype">IRelayWebSocketConnection </td> <td class="paramname">*webSocketConnection*</td><td>)</td> <td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc"></div></div><a id="a054a6bd733831532e2b0d9b8a94ab7aa"></a><span class="permalink">[◆ ](#a054a6bd733831532e2b0d9b8a94ab7aa)</span>OnMessage()
----------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async void RelayDotNet.Relay.OnMessage </td> <td>(</td> <td class="paramtype">IRelayWebSocketConnection </td> <td class="paramname">*webSocketConnection*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*message* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc"></div></div><a id="a4c1285f3ae18f9f51f3a24f8345f1e77"></a><span class="permalink">[◆ ](#a4c1285f3ae18f9f51f3a24f8345f1e77)</span>OnOpen()
-------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;bool&gt; RelayDotNet.Relay.OnOpen </td> <td>(</td> <td class="paramtype">IRelayWebSocketConnection </td> <td class="paramname">*webSocketConnection*</td><td>)</td> <td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc"></div></div><a id="ab96a229ca88b009834633235f1697752"></a><span class="permalink">[◆ ](#ab96a229ca88b009834633235f1697752)</span>PlaceCall()
----------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.PlaceCall </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*uri* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Places a call to another device.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the device that is placing the call.</td></tr> <tr><td class="paramname">uri</td><td>the device that you would like to call.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="a6cc51e490c65764c6b8d0de74fef4063"></a><span class="permalink">[◆ ](#a6cc51e490c65764c6b8d0de74fef4063)</span>Play()
-----------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;string&gt; RelayDotNet.Relay.Play </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*filename* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Plays a custom audio file that was uploaded by the user.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the interaction URN.</td></tr> <tr><td class="paramname">filename</td><td>the name of the audio file.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the response ID after the audio file has been played on the device.</dd></dl></div></div><a id="aa71c80211bf69a8d96ed88fac5cbb5e6"></a><span class="permalink">[◆ ](#aa71c80211bf69a8d96ed88fac5cbb5e6)</span>PlayAndWait()
------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;string&gt; RelayDotNet.Relay.PlayAndWait </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*filename* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Plays a custom audio file that was uploaded by the user. Waits until the audio file has finished playing before continuing through the workflow.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the interaction URN.</td></tr> <tr><td class="paramname">filename</td><td>the name of the audio file.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the response ID after the audio file has been played on the device.</dd></dl></div></div><a id="a843642df8b27e7006d7259bcbd1d68c7"></a><span class="permalink">[◆ ](#a843642df8b27e7006d7259bcbd1d68c7)</span>PlayUnreadInboxMessages()
------------------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.PlayUnreadInboxMessages </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc"></div></div><a id="a460b2af0396d2629996138df383cb0ab"></a><span class="permalink">[◆ ](#a460b2af0396d2629996138df383cb0ab)</span>Rainbow()
--------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.Rainbow </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">int </td> <td class="paramname">*rotations* = `-1` </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Switches all of the LEDs on to a configured rainbow pattern and rotates them a specified number of times.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the interaction URN.</td></tr> <tr><td class="paramname">rotations</td><td>the number of times you would like the rainbow to rotate. Defaults to -1, meaning the rainbow will rotate indefinitely.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="a59a021ecd2a435ab930a93fedcad08d5"></a><span class="permalink">[◆ ](#a59a021ecd2a435ab930a93fedcad08d5)</span>RemoveWorkflow()
---------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;bool&gt; RelayDotNet.Relay.RemoveWorkflow </td> <td>(</td> <td class="paramtype">string </td> <td class="paramname">*path*</td><td>)</td> <td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc"></div></div><a id="ad0473751c1f6f4b02aa89e7a426d48c2"></a><span class="permalink">[◆ ](#ad0473751c1f6f4b02aa89e7a426d48c2)</span>ResolveIncident()
----------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.ResolveIncident </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*incidentId*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*reason* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Resolves an incident that was created.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">incidentId</td><td>the ID of the incident you would like to resolve.</td></tr> <tr><td class="paramname">reason</td><td>the reason for resolving the incident.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd></dd></dl></div></div><a id="a2bc45319d86407e1dcae651657af6f72"></a><span class="permalink">[◆ ](#a2bc45319d86407e1dcae651657af6f72)</span>Rotate()
-------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.Rotate </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*color* = `"FFFFFF"` </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Switches all of the LEDs on a device to a certain color and rotates them a specified number of times.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the interaction URN.</td></tr> <tr><td class="paramname">color</td><td>the hex color code you would like to turn the LEDs to. Defaults to 'FFFFFF'.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="abe8a6952117f8a61c9505bfe1cf1cb9c"></a><span class="permalink">[◆ ](#abe8a6952117f8a61c9505bfe1cf1cb9c)</span>Say() <span class="overload">\[1/2\]</span>
------------------------------------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;string&gt; RelayDotNet.Relay.Say </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*text* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Utilizes text to speech capabilities to make the device 'speak' to the user.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the interaction URN.</td></tr> <tr><td class="paramname">text</td><td>what you would like the device to say.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the response ID after the device speaks to the user.</dd></dl></div></div><a id="ab80565458d0ee5c9b5712ab3a23657ad"></a><span class="permalink">[◆ ](#ab80565458d0ee5c9b5712ab3a23657ad)</span>Say() <span class="overload">\[2/2\]</span>
------------------------------------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;string&gt; RelayDotNet.Relay.Say </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*text*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">Language </td> <td class="paramname">*language* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Utilizes text to speech capabilities to make the device 'speak' to the user.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the interaction URN.</td></tr> <tr><td class="paramname">text</td><td>what you would like the device to say.</td></tr> <tr><td class="paramname">language</td><td>the language of the text that is being spoken. Defaults to 'en-US'.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the response ID after the device speaks to the user.</dd></dl></div></div><a id="a5d8167fc0686982c6c0e2e810f069040"></a><span class="permalink">[◆ ](#a5d8167fc0686982c6c0e2e810f069040)</span>SayAndWait() <span class="overload">\[1/2\]</span>
-------------------------------------------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;string&gt; RelayDotNet.Relay.SayAndWait </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*text* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Utilizes text to speech capabilities to make the device 'speak' to the user. Waits until the text is fully played out on the device before continuing.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the interaction URN.</td></tr> <tr><td class="paramname">text</td><td>what you would like the device to say.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the response ID after the device speaks to the user.</dd></dl></div></div><a id="a7b5807f9a872e4b976840309b4f11a57"></a><span class="permalink">[◆ ](#a7b5807f9a872e4b976840309b4f11a57)</span>SayAndWait() <span class="overload">\[2/2\]</span>
-------------------------------------------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;string&gt; RelayDotNet.Relay.SayAndWait </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*text*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">Language </td> <td class="paramname">*language* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Utilizes text to speech capabilities to make the device 'speak' to the user. Waits until the text is fully played out on the device before continuing.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the interaction URN.</td></tr> <tr><td class="paramname">text</td><td>what you would like the device to say.</td></tr> <tr><td class="paramname">language</td><td>the language of the text that is being spoken. Defaults to 'en-US'.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the resonse ID after the device speaks to the user.</dd></dl></div></div><a id="a27334c5082e7a29425bb16247cc8e20b"></a><span class="permalink">[◆ ](#a27334c5082e7a29425bb16247cc8e20b)</span>SetChannel()
-----------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.SetChannel </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*channel*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string\[\] </td> <td class="paramname">*targets*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">bool </td> <td class="paramname">*suppressTts* = `false`, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">bool </td> <td class="paramname">*disableHomeChannel* = `false` </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Sets the channel that a device is on. This can be used to change the channel of a device during a workflow, where the channel will also be updated on the [Relay](classRelayDotNet_1_1Relay.html "The Relay class is responsible for defining the main functionalities that are used within workflows,...") Dash.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the device or interaction URN.</td></tr> <tr><td class="paramname">channel</td><td>the name of the channel that you would like to set your device to.</td></tr> <tr><td class="paramname">targets</td><td>the group URN whose channel you would like to set.</td></tr> <tr><td class="paramname">suppressTts</td><td>whether you would like to surpress the text to speech. Defaults to false.</td></tr> <tr><td class="paramname">disableHomeChannel</td><td>whether you would like to disable the home channel. Defaults to false.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="a779cda162dcb8d5cb98a2b9f11d2b099"></a><span class="permalink">[◆ ](#a779cda162dcb8d5cb98a2b9f11d2b099)</span>SetDeviceName()
--------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.SetDeviceName </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*name* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Sets the name of a targeted device and updates it on the [Relay](classRelayDotNet_1_1Relay.html "The Relay class is responsible for defining the main functionalities that are used within workflows,...") Dash. The name remains updated until it is set again via a workflow or updated manually on the [Relay](classRelayDotNet_1_1Relay.html "The Relay class is responsible for defining the main functionalities that are used within workflows,...") Dash.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the device or interaction URN.</td></tr> <tr><td class="paramname">name</td><td>a new name for your device.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="aadb421351633e256cbb25c4fab6802c6"></a><span class="permalink">[◆ ](#aadb421351633e256cbb25c4fab6802c6)</span>SetLed()
-------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.SetLed </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">LedEffect </td> <td class="paramname">*ledEffect*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">LedInfo </td> <td class="paramname">*ledInfo* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Used for performing actions on the LEDs, such as creating a rainbow, flashing, rotating, etc.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the interaction URN.</td></tr> <tr><td class="paramname">ledEffect</td><td>effect to perform on the LEDs, can be 'rainbow', 'rotate', 'flash', 'breath', 'static', or 'off'.</td></tr> <tr><td class="paramname">ledInfo</td><td>information regarding the actions on the LED, such as the number of rotations, the count, etc.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="a79b14e64a15e44f66ac751d07fc64488"></a><span class="permalink">[◆ ](#a79b14e64a15e44f66ac751d07fc64488)</span>SetTimer()
---------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.SetTimer </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*name*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*timerType*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">int </td> <td class="paramname">*timeout*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*timeoutType* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Serves as a named timer that can be either interval or timeout. Allows you to specify the unit of time.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">name</td><td>a name for your timer.</td></tr> <tr><td class="paramname">timerType</td><td>can be "timeout" or "interval". Defaults to "timeout"</td></tr> <tr><td class="paramname">timeout</td><td>an integer representing when you would liek your timer to stop.</td></tr> <tr><td class="paramname">timeoutType</td><td>can be "ms", "secs", "mins", or "hrs". Defaults to "secs".</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="abda42a1c778d75e56244b5152b524916"></a><span class="permalink">[◆ ](#abda42a1c778d75e56244b5152b524916)</span>SetUserProfile()
---------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;string\[\]&gt; RelayDotNet.Relay.SetUserProfile </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*username*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">bool </td> <td class="paramname">*force* = `false` </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Sets the profile of a user by updating the username.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the device URN whose profile you would like to update.</td></tr> <tr><td class="paramname">username</td><td>the updated username for the device.</td></tr> <tr><td class="paramname">force</td><td>whether you would like to force this update. Defaults to false.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>a string array of the device names under the user's profile.</dd></dl></div></div><a id="a967df5e6d9f6c41f4fc15f25d88914c1"></a><span class="permalink">[◆ ](#a967df5e6d9f6c41f4fc15f25d88914c1)</span>SetVar()
-------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.SetVar </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*name*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*value* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Sets a variable with the corresponding name and value. Scope of the variable is from start to end of a workflow.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">name</td><td>name of the variable to be created.</td></tr> <tr><td class="paramname">value</td><td>value that the variable will hold.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="ac47e2cb4630d71d3311e0e1eabdb7d75"></a><span class="permalink">[◆ ](#ac47e2cb4630d71d3311e0e1eabdb7d75)</span>Start()
------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">void RelayDotNet.Relay.Start </td> <td>(</td> <td class="paramname"></td><td>)</td> <td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc"></div></div><a id="abc10788dda6035e1c83e6d4e382ed294"></a><span class="permalink">[◆ ](#abc10788dda6035e1c83e6d4e382ed294)</span>StartInteraction()
-----------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async void RelayDotNet.Relay.StartInteraction </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*name*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">Dictionary&lt; string, object &gt; </td> <td class="paramname">*options* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Starts an interaction with the user. Triggers an INTERACTION\_STARTED event and allows the user to interact with the device via functions that require an interaction URN.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the device that you would like to start an interaction with.</td></tr> <tr><td class="paramname">name</td><td>a name for your interaction.</td></tr> <tr><td class="paramname">options</td><td>can be color, home channel, or input types.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="ade72d063b710b5eaaee8049f8ae4d728"></a><span class="permalink">[◆ ](#ade72d063b710b5eaaee8049f8ae4d728)</span>StartTimer()
-----------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.StartTimer </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">int </td> <td class="paramname">*timeout* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Starts an unnamed timer, meaning this will be the only timer on your device. The timer will stop when it reaches the limit of the 'timeout' parameter.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">timeout</td><td>the number of seconds you would like to wait until the timer stops.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="a809b0f2ff9c903c8d9d9481af38e9489"></a><span class="permalink">[◆ ](#a809b0f2ff9c903c8d9d9481af38e9489)</span>StopPlayback() <span class="overload">\[1/3\]</span>
---------------------------------------------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.StopPlayback </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc"></div></div><a id="aab8c9e522d71e7c30c395ff282460932"></a><span class="permalink">[◆ ](#aab8c9e522d71e7c30c395ff282460932)</span>StopPlayback() <span class="overload">\[2/3\]</span>
---------------------------------------------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.StopPlayback </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*id* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc"></div></div><a id="a114913d05d232247c01ad52d72a05368"></a><span class="permalink">[◆ ](#a114913d05d232247c01ad52d72a05368)</span>StopPlayback() <span class="overload">\[3/3\]</span>
---------------------------------------------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.StopPlayback </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string\[\] </td> <td class="paramname">*ids* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc"></div></div><a id="a2d40218be7fe15bd9a9e6de01ced0fdf"></a><span class="permalink">[◆ ](#a2d40218be7fe15bd9a9e6de01ced0fdf)</span>StopTimer()
----------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.StopTimer </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*</td><td>)</td> <td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Stops an unnamed timer.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="ae14c7351d7250ae7956f495570ee8523"></a><span class="permalink">[◆ ](#ae14c7351d7250ae7956f495570ee8523)</span>SwitchAllLedOff()
----------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.SwitchAllLedOff </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Switches all of the LEDs on a device off.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the interaction URN.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="af194291aa8ad233c8d42b6595c5c22d2"></a><span class="permalink">[◆ ](#af194291aa8ad233c8d42b6595c5c22d2)</span>SwitchAllLedOn()
---------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.SwitchAllLedOn </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*color* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Switches all of the LEDs on a device on to a specified color.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the interaction URN.</td></tr> <tr><td class="paramname">color</td><td>the hex color code you would like the LEDs to be.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="a75dc6db07088ee32157d05198e1cbb95"></a><span class="permalink">[◆ ](#a75dc6db07088ee32157d05198e1cbb95)</span>SwitchLedOn()
------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.SwitchLedOn </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">LedIndex </td> <td class="paramname">*ledIndex*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*color* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Switches on an LED at a particular index to a specified color.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the interaction URN.</td></tr> <tr><td class="paramname">ledIndex</td><td>the index of the LED, numbered 1-12.</td></tr> <tr><td class="paramname">color</td><td>the hex color code that you would like to set the LED to.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="a9346635f02704bc5dac4a66508aa7876"></a><span class="permalink">[◆ ](#a9346635f02704bc5dac4a66508aa7876)</span>Terminate()
----------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async void RelayDotNet.Relay.Terminate </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*</td><td>)</td> <td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Terminates a workflow. This method is usually called after your workflow has completed and you would like to end the workflow by calling end\_interaction(), where you can then terminate the workflow.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> </table>

 </dd></dl></div></div><a id="ad7c302c4b23d5c0c400063761c9f2c99"></a><span class="permalink">[◆ ](#ad7c302c4b23d5c0c400063761c9f2c99)</span>Translate()
----------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;string&gt; RelayDotNet.Relay.Translate </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*text*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">Language </td> <td class="paramname">*from*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">Language </td> <td class="paramname">*to* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc"></div></div><a id="a846ba92d95860532bd1c34367ff0f88d"></a><span class="permalink">[◆ ](#a846ba92d95860532bd1c34367ff0f88d)</span>UnsetVar()
---------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.UnsetVar </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*name* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Unsets the value of a variable.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">name</td><td>the name of the variable whose value you would like to unset.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div><a id="aa071d6386572e9d6cbe705e6d907c629"></a><span class="permalink">[◆ ](#aa071d6386572e9d6cbe705e6d907c629)</span>Vibrate()
--------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"><table class="mlabels"> <tr> <td class="mlabels-left"> <table class="memname"> <tr> <td class="memname">async Task&lt;Dictionary&lt;string, object&gt; &gt; RelayDotNet.Relay.Vibrate </td> <td>(</td> <td class="paramtype">IRelayWorkflow </td> <td class="paramname">*relayWorkflow*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">string </td> <td class="paramname">*sourceUri*, </td> </tr> <tr> <td class="paramkey"></td> <td></td> <td class="paramtype">int\[\] </td> <td class="paramname">*pattern* </td> </tr> <tr> <td></td> <td>)</td> <td></td><td></td> </tr> </table>

 </td> <td class="mlabels-right"><span class="mlabels"><span class="mlabel">inline</span></span> </td> </tr></table>

</div><div class="memdoc">Makes the device vibrate in a particular pattern. You can specify how many vibrations you would like, the duration of each vibration in milliseconds, and how long you would like the pauses between each vibration to last in milliseconds.

<dl class="params"><dt>Parameters</dt><dd> <table class="params"> <tr><td class="paramname">relayWorkflow</td><td>the workflow.</td></tr> <tr><td class="paramname">sourceUri</td><td>the interaction URN.</td></tr> <tr><td class="paramname">pattern</td><td>an array representing the pattern of your vibration. Defaults to none.</td></tr> </table>

 </dd></dl><dl class="section return"><dt>Returns</dt><dd>the event response.</dd></dl></div></div>Member Data Documentation
-------------------------

<a id="af26e3167ec9929b5f652959b1348b226"></a><span class="permalink">[◆ ](#af26e3167ec9929b5f652959b1348b226)</span>RelayWebSocketConnector
----------------------------------------------------------------------------------------------

<div class="memitem"><div class="memproto"> <table class="memname"> <tr> <td class="memname">IRelayWebSocketConnector RelayDotNet.Relay.RelayWebSocketConnector =&gt; \_relayWebSocketConnector</td> </tr> </table>

</div><div class="memdoc"></div></div>- - - - - -

The documentation for this class was generated from the following file:- [Relay.cs](Relay_8cs.html)

</div>- - - - - -

<address class="footer"><small>Generated by [![doxygen](doxygen.png)](http://www.doxygen.org/index.html) 1.8.17 </small></address>