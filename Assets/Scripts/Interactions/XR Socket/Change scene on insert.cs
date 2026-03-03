using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class LoadSceneOnInsert : MonoBehaviour
{
    [Header("Scene Settings")]
    [Tooltip("Name of the scene to load when an object is inserted")]
    [SerializeField] private string sceneName;

    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socketInteractor;

    private void Awake()
    {
        socketInteractor = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();
    }

    private void OnEnable()
    {
        socketInteractor.selectEntered.AddListener(OnObjectInserted);
    }

    private void OnDisable()
    {
        socketInteractor.selectEntered.RemoveListener(OnObjectInserted);
    }

    private void OnObjectInserted(SelectEnterEventArgs args)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogWarning("Scene name is not set in the Inspector.");
            return;
        }

        Debug.Log($"Object inserted. Loading scene '{sceneName}'...");
        SceneManager.LoadScene(sceneName);
    }
}