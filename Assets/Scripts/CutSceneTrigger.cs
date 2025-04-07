using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneTrigger : MonoBehaviour
{
    [SerializeField] GameObject TriggerEvent;
    [SerializeField] String DrawnLevel;
    [SerializeField] String threeDlevel;

    public void LoadDrawnLevel()
    {
        SceneManager.LoadSceneAsync(DrawnLevel);
    }

    public void Load3DLevel()
    {
        SceneManager.LoadSceneAsync(threeDlevel);
    }
}
