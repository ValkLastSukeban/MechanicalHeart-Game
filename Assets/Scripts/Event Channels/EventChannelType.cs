using System;
using System.Collections.Generic;
using UnityEngine;

namespace Event_Channels
{
    public abstract class EventChannelType<TKey, TValue> : ScriptableObject where TKey : Enum
    {
        private readonly Dictionary<TKey, Action<TValue>> _listeners = new Dictionary<TKey, Action<TValue>>();

        public void RegisterListener(TKey eventType, Action<TValue> listener)
        {
            if (!_listeners.ContainsKey(eventType))
            {
                _listeners.Add(eventType, listener);
            }
            else
            {
                _listeners[eventType] += listener;
            }
        }

        public void UnregisterListener(TKey eventType, Action<TValue> listener)
        {
            if (_listeners.ContainsKey(eventType))
            {
                _listeners[eventType] -= listener;
            }
        }

        public void RaiseEvent(TKey eventType, TValue value)
        {
            if (_listeners.TryGetValue(eventType, out Action<TValue> eventFound))
            {
                eventFound?.Invoke(value);
            }
        }
    }
}