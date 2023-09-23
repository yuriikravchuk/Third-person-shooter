using System.Collections.Generic;
using UnityEngine;

public class Updater : MonoBehaviour
{
    private IReadOnlyList<IUpdatable> _updatables;

    public void Init(IReadOnlyList<IUpdatable> updatables)
    {
        _updatables = updatables;
    }

    void Update()
    {
        foreach(var updatable in _updatables)
            updatable.Update();
    }
}
