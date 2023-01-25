using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable
{
    public object SaveState();
    public void LoadState(string jsonData);
    public void LoadByDefault();
}
