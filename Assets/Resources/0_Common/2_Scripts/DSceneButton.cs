using UnityEngine;
using System.Collections;

public class DSceneButton : MonoBehaviour {
    public string sceneName;

    void OnClick()
    {
        Application.LoadLevelAsync(sceneName);
        Debug.Log("GO_" + sceneName);
    }
}
