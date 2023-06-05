using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    [System.Obsolete]
    private void Awake()
    {
        int countScenePersist = FindObjectsOfType<ScenePersist>().Length;
        if (countScenePersist > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void ResetScenePresist()
    {
        Destroy(gameObject);
    }
}
