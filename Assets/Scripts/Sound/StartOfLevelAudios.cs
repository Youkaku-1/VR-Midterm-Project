using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMusic : MonoBehaviour
{
    private void Start()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Level 1":    SoundManager.Instance.Play("NaratorLvl1-1");   break;
            case "Level 2":   SoundManager.Instance.Play("NaratorLvl2-1"); break;
            case "Level 3":    SoundManager.Instance.Play("NaratorLvl3-1");   break;

        }
    }
}