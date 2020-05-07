using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UserProfileController : MonoBehaviour
{
    private Sprite Avatar;
    private string Nickname;
    private int Score;

    [SerializeField] private Image AvatarPlacement;
    [SerializeField] private Text NicknamePlacement;
    [SerializeField] private Text ScorePlacement;

    void Start()
    {
    }

    void Update()
    {
    }

    public void SetUI(Sprite avatar, string nickname, int score = -1)
    {
        Avatar = avatar;
        Nickname = nickname;
        Score = score;

        AvatarPlacement.sprite = Avatar;
        NicknamePlacement.text = Nickname;
        if (Score > -1) ScorePlacement.text = $"Score: {Score}";
    }
}
