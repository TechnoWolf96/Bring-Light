

public interface IObserver
{
    public void UpdateData();
}

public interface IObservable
{
    public void AddObserver(IObserver newObserver);
    public void RemoveObserver(IObserver removableObserver);
    public void NotifyObservers();
}
