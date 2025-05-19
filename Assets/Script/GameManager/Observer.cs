using System;
using System.Collections.Generic;

public class Observer : SingleTon<Observer>
{
    Dictionary<string, List<Delegate>> observerList = new Dictionary<string, List<Delegate>>();
    public void AddToList(string name, Action action)
    {
        if (!observerList.ContainsKey(name))
        {
            observerList[name] = new List<Delegate>();
        }
        observerList[name].Add(action);
    }
    public void AddToList<T>(string name, Action<T> action)
    {
        if (!observerList.ContainsKey(name))
        {
            observerList[name] = new List<Delegate>();
        }
        observerList[name].Add(action);
    }

    public void RemoveToList(string name, Action action)
    {
        if (!observerList.ContainsKey(name))
            return;
        observerList[name].Remove(action);
    }
    public void RemoveToList<T>(string name, Action<T> action)
    {
        if (!observerList.ContainsKey(name))
            return;
        observerList[name].Remove(action);
    }
    public void Notify(string name)
    {
        if (!observerList.ContainsKey(name))
            return;
        foreach (Delegate del in observerList[name])
        {
            if (del is Action action)
            {
                action?.Invoke();
            }
        }
    }
    public void Notify<T>(string name, T param)
    {
        if (!observerList.ContainsKey(name))
            return;
        foreach (Delegate del in observerList[name])
        {
            if (del is Action<T> action)
            {
                action?.Invoke(param);
            }
        }
    }
}
