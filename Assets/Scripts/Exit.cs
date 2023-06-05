using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(LoadNextLevel());
        }
    }

    [System.Obsolete]
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        int intScrene = SceneManager.GetActiveScene().buildIndex+1;
        if(intScrene < SceneManager.sceneCountInBuildSettings)
        {
            FindObjectOfType<ScenePersist>().ResetScenePresist();
            SceneManager.LoadScene(intScrene);
        }
    }
}
