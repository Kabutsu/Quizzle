using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UserProfileController : MonoBehaviour
{
    public Sprite Avatar { get; private set; }
    public string Nickname { get; set; }

    [SerializeField] private Image AvatarPlacement;
    [SerializeField] private Text NicknamePlacement;

    void Start()
    {
    }

    void Update()
    {
    }

    public void SetUI(Sprite avatar, string nickname)
    {
        Avatar = avatar;
        Nickname = nickname;

        AvatarPlacement.sprite = Avatar;
        NicknamePlacement.text = Nickname;
    }
}
