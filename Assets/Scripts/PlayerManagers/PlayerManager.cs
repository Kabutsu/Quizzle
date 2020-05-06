using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private QuestionManager QuestionManager;
    private VoteController VoteController;

    private EventSystem EventSystem;

    private User UserData { get; set; }
    public string RoomCode { get; set; }
    public string Nickname
    {
        get => UserData.Nickname;
        set => UserData.Nickname = value;
    }
    public Sprite Avatar
    {
        get => UserData.Avatar;
        set => UserData.Avatar = value;
    }
    public int Score
    {
        get => UserData.Score;
        set => UserData.Score = value;
    }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        EventSystem = EventSystem.current;
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

    public void Construct(string nickname, Sprite avatar)
        => UserData = new User(nickname, avatar);

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch(scene.name)
        {
            case "LobbyWaitingRoom":
                GameObject
                    .FindGameObjectWithTag("SceneController")
                    .GetComponent<WaitingRoomController>()
                    .AddProfile(Avatar, Nickname, true);

                QuestionManager = GameObject
                    .FindGameObjectWithTag("QuestionManager")
                    .GetComponent<QuestionManager>();
                break;
            case "GameVote":
                VoteController = GameObject
                    .FindGameObjectWithTag("SceneController")
                    .GetComponent<VoteController>();
                break;
            default:
                break;
        }
    }

    public void Vote()
        => EventSystem.currentSelectedGameObject
            .GetComponent<VoteButtonController>()
            .Vote(UserData.Id);
}
