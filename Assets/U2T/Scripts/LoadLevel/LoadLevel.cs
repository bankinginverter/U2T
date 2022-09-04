using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using U2T.Foundation;

public class LoadLevel : MonoBehaviour
{
    public delegate void ChangeSceneDelegate();



    private void Start()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        string currentName = current.name;

        if (currentName == null)
        {
            // Scene1 has been removed
            currentName = "Replaced";
        }
        Debug.Log("Scenes: " + currentName + ", " + next.name);
        AppStateManager.Instance.ChangeAppState(Enumulator.GameState.GAMEPLAY_START);
    }

}
