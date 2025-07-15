using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void Reload()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
