using UnityEngine;

public interface IMemorable
{
    public ScriptableObject SaveToMemento();

    public void RestoreFromMemento(ScriptableObject memento);

}