
using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : AbsMovement
{
    private List<IObserver> observers = new List<IObserver>();
     
    public void AddObserver(IObserver observer)
    {
        observers.Add(observer);
    }
    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }
    protected void NotifyObserver()
    {
        observers.ForEach((observer) =>
        {
            observer.OnNotify();
        });
    }
}