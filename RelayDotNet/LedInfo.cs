// Copyright © 2022 Relay Inc.

using System.Collections.Generic;

namespace RelayDotNet
{
    public class LedInfo
    {
        private readonly Dictionary<string, object> _dictionary = new Dictionary<string, object>();

        public Dictionary<string, object> Dictionary => _dictionary;

        public void SetRotations(int rotations)
        {
            _dictionary["rotations"] = rotations;
        }

        public void SetCount(int count)
        {
            _dictionary["count"] = count;
        }

        public void SetDuration(int duration)
        {
            _dictionary["duration"] = duration;
        }

        public void SetRepeatDelay(int repeatDelay)
        {
            _dictionary["repeat_delay"] = repeatDelay;
        }

        public void SetPatternRepeats(int patternRepeats)
        {
            _dictionary["pattern_repeats"] = patternRepeats;
        }

        public void SetColor(LedIndex ledIndex, string color)
        {
            if (ledIndex.Equals(LedIndex.Ring))
            {
                Dictionary<string, object> colors = new Dictionary<string, object>();
                colors[ledIndex.SerializedName] = color;
                _dictionary["colors"] = colors;
            }
            else
            {
                Dictionary<string, object> colors;
                if (_dictionary.ContainsKey("colors"))
                {
                    colors = (Dictionary<string, object>) _dictionary["colors"];
                }
                else
                {
                    colors = new Dictionary<string, object>();
                    _dictionary["colors"] = colors;
                }

                colors[ledIndex.SerializedName] = color;
            }
        }
    }
}
