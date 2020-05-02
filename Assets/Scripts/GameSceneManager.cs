using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private string NextScene;
    [SerializeField] private AudioClip SceneAudio;
    private GameObject GameController;

    void Start()
    {
        GameController = GameObject.FindGameObjectWithTag("GameController");
        GameController.GetComponent<AudioController>().Play(SceneAudio);
    }
    
    void Update()
    {
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadGameAsync(NextScene));
    }

    IEnumerator LoadGameAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while(!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
