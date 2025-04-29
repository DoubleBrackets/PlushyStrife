using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    [Scene]
    private string sceneName;

    public void ChangeScene()
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene name is not set.");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }
}