// Copyright © 2022 Relay Inc.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace RelayDotNet
{
    public class EventTypeTaskCompletionSource
    {
        private readonly Dictionary<string, object> _keyValuesToMatch = new Dictionary<string, object>();
        private readonly TaskCompletionSource<Dictionary<string, object>> _taskCompletionSource = new TaskCompletionSource<Dictionary<string, object>>();

        public Dictionary<string, object> KeyValuesToMatch => _keyValuesToMatch;

        public TaskCompletionSource<Dictionary<string, object>> TaskCompletionSource => _taskCompletionSource;
    }
}
