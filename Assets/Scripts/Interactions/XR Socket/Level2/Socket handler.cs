using UnityEngine;
using UnityEngine.SceneManagement;

public class DualSocketSceneLoader : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string sceneName;

    private bool socket1Filled = false;
    private bool socket2Filled = false;

    public void ReportSocket1Filled()
    {
        socket1Filled = true;
        CheckBoth();
    }

    public void ReportSocket2Filled()
    {
        socket2Filled = true;
        CheckBoth();
    }

    public void ReportSocket1Emptied() => socket1Filled = false;
    public void ReportSocket2Emptied() => socket2Filled = false;

    private void CheckBoth()
    {
        if (socket1Filled && socket2Filled)
        {
            Debug.Log($"Both sockets filled. Loading scene '{sceneName}'...");
            SceneManager.LoadScene(sceneName);
        }
    }
}