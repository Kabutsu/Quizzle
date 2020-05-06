using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject QuestionManagerPrefab;
    [SerializeField] private GameObject PlayerManagerPrefab;
    [SerializeField] private Button PlayButton;

    public string RoomCode;
    public string Nickname;
    public Sprite Avatar;

    void Start()
    {
    }

    void Update()
    {
    }

    public void SetRoomCode(string roomCode)
    {
        RoomCode = roomCode;
        CheckValid();
    }
    public void SetNickname(string nickname)
    {
        Nickname = nickname;
        CheckValid();
    }
    public void SetAvatar(Sprite avatar)
    {
        Avatar = avatar;
        CheckValid();
    }

    void CheckValid()
    {
        try
        {
            PlayButton.interactable = (RoomCode.Trim().Length > 0
                && Nickname.Trim().Length > 0
                && Avatar != null);
        } catch (NullReferenceException)
        {
            PlayButton.interactable = false;
        }
    }

    public void ConnectToGame()
    {
        /* TODO:
         * - Connect to Photon
         * - Check Room status
         * - Create Room if not exists
         * - Connect to Room
         */

        Instantiate(QuestionManagerPrefab);

        Instantiate(PlayerManagerPrefab)
            .GetComponent<PlayerManager>().Construct(Nickname, Avatar);

        GameObject
            .FindGameObjectWithTag("SceneController")
            .GetComponent<GameSceneManager>()
            .LoadNextScene();
    }
}
