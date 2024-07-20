using UnityEngine;
using UnityEngine.SceneManagement;

public class gotomenu : MonoBehaviour
{
    public void LoadgotomenuScene()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadGoToChooseMap1()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadGoToChooseMap2()
    {
        SceneManager.LoadScene(2);
    }
}