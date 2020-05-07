using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private QuestionManager QuestionManager;
    private VoteController VoteController;

    private Button[] VoteButtons = new Button[4];

    private User UserData { get; set; } = new User();
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
    {
        UserData.Nickname = nickname;
        UserData.Avatar = avatar;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "LobbyWaitingRoom":
                GameObject
                    .FindGameObjectWithTag("SceneController")
                    .GetComponent<WaitingRoomController>()
                    .AddProfile(UserData);

                QuestionManager = GameObject
                    .FindGameObjectWithTag("QuestionManager")
                    .GetComponent<QuestionManager>();
                break;
            case "GameAnswer":
            case "GameVote":
                VoteButtons = GameObject
                    .FindGameObjectsWithTag("VoteButton")
                    .Select(x => x.GetComponent<Button>())
                    .ToArray();

                foreach(Button voteButton in VoteButtons)
                {
                    voteButton.onClick.AddListener(
                        () => Vote(voteButton.GetComponent<VoteButtonController>()));
                }
                break;
            default:
                break;
        }
    }

    public void Vote(VoteButtonController controller)
        => controller.Vote(UserData.Id);
}
