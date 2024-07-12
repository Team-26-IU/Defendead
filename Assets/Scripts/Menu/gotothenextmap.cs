using UnityEngine;
using UnityEngine.SceneManagement;

public class gotothenextmap : MonoBehaviour
{
    
    public void Left()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Right()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
