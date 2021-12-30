using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    private void Start()
    {
        

    }
    public void SceneChange(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
