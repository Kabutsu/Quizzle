using UnityEngine;

public class WaitingRoomController : MonoBehaviour
{
    [SerializeField] private GameObject UserProfilePrefab;

    void Start()
    {
    }

    void Update()
    {
    }

    public void AddProfile(Sprite avatar, string nickname)
    {
        var profile = Instantiate(UserProfilePrefab, new Vector3(-638, 132, 0), new Quaternion());
        profile.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        profile.GetComponent<UserProfileController>().SetUI(avatar, nickname);
    }
}
