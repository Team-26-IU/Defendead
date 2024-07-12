using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsButtonHandler : MonoBehaviour
{
    public void LoadSettingsScene()
    {
        SceneManager.LoadScene(4);
    }
}