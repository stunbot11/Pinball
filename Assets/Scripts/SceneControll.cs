using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControll : MonoBehaviour
{
    public void loadScene(string sceneName)
    {
        if (sceneName == "Main Menu" && GameObject.Find("PlayerStats") != null)
            Destroy(GameObject.Find("PlayerStats"));
        SceneManager.LoadScene(sceneName);
    }

}
