using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IEventManager
{
    public void register<Param>(Action<IEventParam> action);
    public void notify(IEventParam event_param);
}


public class EventManager:  MonoBehaviour, IEventManager
{
    private Dictionary<Type, List<Action<IEventParam>>> events = new();
    public void register<Param>(Action<IEventParam> action)
    {
        Type event_type = typeof(Param);
        if (!events.ContainsKey(event_type))
            events[event_type] = new();
        events[event_type].Add(action);
    }

    public void notify(IEventParam event_param)
    {
        Type event_type = event_param.GetType();
        if (!events.ContainsKey(event_type))
            return;
            
        foreach (Action<IEventParam> action in events[event_type])
            action(event_param);
    }
}