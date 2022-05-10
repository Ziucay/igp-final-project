using UnityEngine;

public interface IMemorable
{
    public ISerializable SaveToMemento();

    public void RestoreFromMemento(ISerializable memento);

}




