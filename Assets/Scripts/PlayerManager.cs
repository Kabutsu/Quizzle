using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public string RoomCode { get; set; }
    public string Nickname { get; set; }
    public Sprite Avatar { get; set; }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
    }

    void Update()
    {
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "LobbyWaitingRoom")
        {
            GameObject
                .FindGameObjectWithTag("SceneController")
                .GetComponent<WaitingRoomController>()
                .AddProfile(Avatar, Nickname, true);
        }
    }
}
