using System;
using System.Collections.Generic;
using UnityEngine;

namespace Event_Channels
{
    public abstract class EventChannelVoid<TKey> : ScriptableObject where TKey : Enum
    {
        private readonly Dictionary<TKey, Action> _listeners = new Dictionary<TKey, Action>();

        public void RegisterListener(TKey eventType, Action listener)
        {
            if (!_listeners.ContainsKey(eventType)) _listeners.Add(eventType, listener);
            else _listeners[eventType] += listener;
        }

        public void UnregisterListener(TKey eventType, Action listener)
        {
            if (_listeners.ContainsKey(eventType)) _listeners[eventType] -= listener;
        }

        public void RaiseEvent(TKey eventType)
        {
            if (_listeners.TryGetValue(eventType, out Action eventFound))
            {
                eventFound?.Invoke();
            }
        }
    }
}