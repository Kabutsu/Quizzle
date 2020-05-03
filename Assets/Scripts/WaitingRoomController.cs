using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingRoomController : MonoBehaviour
{
    [SerializeField] private GameObject UserProfilePrefab;

    private Stack<KeyValuePair<Vector3, Quaternion>> ProfileLocations;
    private KeyValuePair<Vector3, Quaternion> LeaderLocation
        = new KeyValuePair<Vector3, Quaternion>(new Vector3(780, 88), Quaternion.Euler(0, 0, 0));

    void Awake()
    {
        ProfileLocations = new Stack<KeyValuePair<Vector3, Quaternion>>(
            new KeyValuePair<Vector3, Quaternion>[]
            {
                new KeyValuePair<Vector3, Quaternion>(new Vector3(-728, 132), Quaternion.Euler(0, 0, 7.5f)),
                new KeyValuePair<Vector3, Quaternion>(new Vector3(-445, 67), Quaternion.Euler(0, 0, -2.2f)),
                new KeyValuePair<Vector3, Quaternion>(new Vector3(-30, 125), Quaternion.Euler(0, 0, 3.45f)),
                new KeyValuePair<Vector3, Quaternion>(new Vector3(311, 7), Quaternion.Euler(0, 0, -0.9f)),
                new KeyValuePair<Vector3, Quaternion>(new Vector3(-608, -202), Quaternion.Euler(0, 0, -2.66f)),
                new KeyValuePair<Vector3, Quaternion>(new Vector3(-273, -190), Quaternion.Euler(0, 0, 0.77f)),
                new KeyValuePair<Vector3, Quaternion>(new Vector3(88, -185), Quaternion.Euler(0, 0, 7.66f)),
                new KeyValuePair<Vector3, Quaternion>(new Vector3(411, -297), Quaternion.Euler(0, 0, -10f))
            });
    }

    void Update()
    {
    }

    public void AddProfile(Sprite avatar, string nickname, bool isRoomLeader = false)
    {
        KeyValuePair<Vector3, Quaternion> location = isRoomLeader
            ? LeaderLocation
            : ProfileLocations.Pop();
        var profile = Instantiate(UserProfilePrefab, location.Key, location.Value);
        profile.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        profile.GetComponent<UserProfileController>().SetUI(avatar, nickname);
    }
}
