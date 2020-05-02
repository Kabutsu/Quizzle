using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    void Start()
    {
    }
    
    void Update()
    {
    }

    public void LoadGame()
    {
        StartCoroutine(LoadGameAsync());
    }

    IEnumerator LoadGameAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Game");

        while(!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
