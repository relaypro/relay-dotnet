// Copyright © 2022 Relay Inc.

using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RelayDotNet
{
    /// <summary>
    /// The RelayUri class is responsible for defining functions that construct
    ///  a URN from a given device or group id or name, as well as functions that parse
    /// out certain components from a given Relay URN.
    /// </summary>
    public class RelayUri
    {
        private const string Scheme = "urn";
        private const string RootPath = "relay-resource";
        private const string Id = "id";
        private const string Name = "name";
        private const string All = "all";

        private const string Group = "group";
        private const string Device = "device";
        private const string Interaction = "interaction";
        private const string Status = "status";
        private const string Server = "server";
        private const string Ibot = "ibot";

        private static readonly string _AllDevicesOnAcct = $"{Scheme}:{RootPath}:{All}:{Device}";
        private static readonly string _RelayUriStartsWith = $"{Scheme}:{RootPath}";
        
        private static string Nidnss(string resourceType, string idType, string idOrName)
        {
            return $"{RootPath}:{idType}:{resourceType}:{HttpUtility.UrlPathEncode(idOrName)}";
        }

        /// <summary>
        /// Creates a URN from a group ID.
        /// </summary>
        /// <param name="groupId">the ID of the group</param>
        /// <param name="filter">optional filter for constructing the URN.</param>
        /// <returns>the newly constructed URN.</returns>
        public static string GroupId(string groupId, List<KeyValuePair<string, string>> filter = null)
        {
            return Construct(Group, Id, groupId, filter);
        }
        
        /// <summary>
        /// Creates a URN from a group name.
        /// </summary>
        /// <param name="groupName">the name of the group.</param>
        /// <param name="filter">optional filter for constructing the URN.</param>
        /// <returns>the newly constructed URN.</returns>
        public static string GroupName(string groupName, List<KeyValuePair<string, string>> filter = null)
        {
            return Construct(Group, Name, groupName, filter);
        }

        /// <summary>
        /// Creates a URN for a group member
        /// </summary>
        /// <param name="groupName">the name of the group that the device belongs to.</param>
        /// <param name="deviceName">the device ID or name.</param>
        /// <returns>the newly constructed URN.</returns>
        public static string GroupMember(string groupName, string deviceName)
        {
            var filter = new List<KeyValuePair<string, string>>()
            {
                new(Device, DeviceName(deviceName))
            };

            return GroupName(groupName, filter); 
        }
        
        /// <summary>
        /// Creates a URN from a device ID.
        /// </summary>
        /// <param name="deviceId">the ID of the device.</param>
        /// <returns>the newly constructed URN.</returns>
        public static string DeviceId(string deviceId)
        {
            return Construct(Device, Id, deviceId);
        }

        /// <summary>
        /// Creates a URN from a device name.
        /// </summary>
        /// <param name="deviceName">the name of the device.</param>
        /// <returns>the newly constructed URN.</returns>
        public static string DeviceName(string deviceName)
        {
            return Construct(Device, Name, deviceName);
        }

        /// <summary>
        /// Creates a URN from an interaction name.
        /// </summary>
        /// <param name="name">the name of the interaction.</param>
        /// <returns>the newly constructed URN.</returns>
        public static string InteractionName(string name)
        {
            return Construct(Interaction, Name, name);
        }

        /// <summary>
        /// Creates a URN from an interaction name and devices.
        /// </summary>
        /// <param name="name">the name of the interaction.</param>
        /// <param name="devices">a list of devices.</param>
        /// <returns>the newly constructed URN.</returns>
        public static string InteractionName(string name, IEnumerable<string> devices)
        {
            var filter = devices.Select(deviceName => new KeyValuePair<string, string>(Device, DeviceName(deviceName))).ToList();

            return Construct(Interaction, Name, name, filter);
        }

        /// <summary>
        /// Returns a URN containing all of the devices with the specified status.
        /// </summary>
        /// <param name="interactionName">the name of the interaction.</param>
        /// <param name="status">the status of the devices.</param>
        /// <returns>a URN containing all of the devices with the status.</returns>
        public static string AllDevicesWithStatus(string interactionName, string status)
        {
            return Construct(Interaction, Name, interactionName, new List<KeyValuePair<string, string>>()
            {
                new(Status, status)
            });
        }

        /// <summary>
        /// Retrieves all of the devices associated with the account.
        /// </summary>
        /// <returns>the devices.</returns>
        public static string AllDevices()
        {
            return _AllDevicesOnAcct;
        }

        /// <summary>
        /// Creates a URN containing server information.
        /// </summary>
        /// <returns>the newly constructed URN.</returns>
        public static string GenericOriginator()
        {
            return Construct(Server, Name, Ibot);
        }

        private static string Construct(string resourceType, string idType, string idOrName, List<KeyValuePair<string, string>> filter = null)
        {
            if (filter is {Count: > 0})
            {
                var query = HttpUtility.ParseQueryString(string.Empty);

                foreach (var (key, value) in filter)
                {
                    query.Add(key, value);
                }

                return $"{Scheme}:{Nidnss(resourceType, idType, idOrName)}?{query}";
            }
            else
            {
                return $"{Scheme}:{Nidnss(resourceType, idType, idOrName)}";
            }
        }

        private class ParseResult
        {
            public string ResourceType { get; init; }

            public string IdType { get; init; }

            public string IdOrName { get; init; }

            public List<KeyValuePair<string, string>> Filter { get; init; }
        }

        private static ParseResult Parse(string uri)
        {
            if (null == uri)
            {
                return null;
            }

            var uriParts = uri.Split(':');
            switch (uriParts.Length)
            {
                case 5 when Scheme != uriParts[0] || RootPath != uriParts[1]:
                    return null;
                case 5 when Id == uriParts[2] || Name == uriParts[2]:
                {
                    var lastUriParts = uriParts[4].Split('?');
                    List<KeyValuePair<string, string>> filter;
                    if (2 == lastUriParts.Length)
                    {
                        var queryString = HttpUtility.ParseQueryString(lastUriParts[1]);
                        filter = queryString.AllKeys.Select(key => new KeyValuePair<string, string>(key, queryString.Get(key))).ToList();
                    }
                    else if (1 == lastUriParts.Length) 
                    {
                        filter = null;
                    }
                    else
                    {
                        return null;
                    }
                    
                    return new ParseResult()
                    {
                        ResourceType = uriParts[3],
                        IdType = uriParts[2],
                        IdOrName = HttpUtility.UrlDecode(lastUriParts[0]),
                        Filter = filter
                    };
                }
                case 4 when Scheme != uriParts[0] || RootPath != uriParts[1] || All != uriParts[2]:
                    return null;
                case 4:
                    return new ParseResult()
                    {
                        ResourceType = uriParts[3],
                        IdType = All,
                        IdOrName = null,
                        Filter = null
                    };
                default:
                    return null;
            }
        }

        /// <summary>
        /// Parses out a device name from a device or interaction URN.
        /// </summary>
        /// <param name="uri">the device or interaction URN that you would like to extract the device name from.</param>
        /// <returns>the device name.</returns>
        public static string ParseDeviceName(string uri)
        {
            var parseResult = Parse(uri);

            if (null == parseResult)
            {
                return null;
            }

            switch (parseResult.ResourceType)
            {
                case Device when Name == parseResult.IdType:
                    return parseResult.IdOrName;
                case Interaction:
                {
                    var device = parseResult.Filter?.FirstOrDefault(pair => Device == pair.Key);
                    return ParseDeviceName(device?.Value);
                }

                default:
                    return null;
            }
        }

        /// <summary>
        /// Parses out a device ID from a device or interaction URN.
        /// </summary>
        /// <param name="uri">the device or interaction URN that you would like to extract the device ID from.</param>
        /// <returns>the device ID.</returns>
        public static string ParseDeviceId(string uri)
        {
            var parseResult = Parse(uri);

            if (null == parseResult)
            {
                return null;
            }

            switch (parseResult.ResourceType)
            {
                case Device when Id == parseResult.IdType:
                    return parseResult.IdOrName;
                case Interaction:
                {
                    var device = parseResult.Filter?.FirstOrDefault(pair => Device == pair.Key);
                    return ParseDeviceId(device?.Value);
                }

                default:
                    return null;
            }
        }

        /// <summary>
        /// Parses out a group name from a group URN.
        /// </summary>
        /// <param name="uri">the URN that you would like to extract the group name from.</param>
        /// <returns>the group name.</returns>
        public static string ParseGroupName(string uri)
        {
            var parseResult = Parse(uri);

            if (null == parseResult)
            {
                return null;
            }

            return parseResult.ResourceType switch
            {
                Group when Name == parseResult.IdType => 
                    parseResult.IdOrName,
                _ => 
                    null
            };
        }

        /// <summary>
        /// Checks if the URN is a Relay URN.
        /// </summary>
        /// <param name="uri">the device, group, or interaction URN.</param>
        /// <returns>true if the URN is a Relay URN, false otherwise.</returns>
        public static bool IsRelayUri(string uri)
        {
            return null != uri && uri.StartsWith(_RelayUriStartsWith);
        }
    }
}