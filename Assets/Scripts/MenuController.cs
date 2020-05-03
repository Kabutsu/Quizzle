using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button PlayButton;
    private PlayerManager Player;

    void Start()
    {
        Player = GameObject
            .FindGameObjectWithTag("GameController")
            .GetComponent<PlayerManager>();
    }

    void Update()
    {
    }

    public void SetRoomCode(string roomCode)
    {
        Debug.Log(roomCode);
        Player.RoomCode = roomCode;
        CheckValid();
    }
    public void SetNickname(string nickname)
    {
        Debug.Log(nickname);
        Player.Nickname = nickname;
        CheckValid();
    }
    public void SetAvatar(Sprite avatar)
    {
        Debug.Log(avatar);
        Player.Avatar = avatar;
        CheckValid();
    }

    void CheckValid()
    {
        try
        {
            PlayButton.interactable = (Player.RoomCode.Trim().Length > 0
                && Player.Nickname.Trim().Length > 0
                && Player.Avatar != null);
        } catch (NullReferenceException)
        {
            PlayButton.interactable = false;
        }
    }
}
