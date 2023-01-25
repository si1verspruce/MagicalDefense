using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour, IResetOnRestart
{
    private List<GameObject> _requestingObjects = new List<GameObject>();

    public void RequestPause(GameObject requestingObject)
    {
        if (_requestingObjects.Contains(requestingObject) == false)
        {
            _requestingObjects.Add(requestingObject);
            Time.timeScale = 0;
        }
    }

    public void RequestPlay(GameObject requestingObject)
    {
        if (_requestingObjects.Contains(requestingObject))
        {
            _requestingObjects.Remove(requestingObject);

            if (_requestingObjects.Count == 0)
                Time.timeScale = 1;
        }
    }

    public void Reset()
    {
        _requestingObjects.Clear();
        Time.timeScale = 1;
    }
}
