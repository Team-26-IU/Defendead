using UnityEngine;
using UnityEngine.SceneManagement;

public class gotomenu : MonoBehaviour
{
    public void LoadgotomenuScene()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadgotomapmenuScene()
    {
        SceneManager.LoadScene(1);
    }
}