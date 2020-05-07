using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingRoomController : MonoBehaviour
{
    private PlayerRegister PlayerRegister;

    [SerializeField] private GameObject UserProfilePrefab;
    [SerializeField] private GameObject LeaderSpawnPoint;
    [SerializeField] private GameObject[] SpawnPoints = new GameObject[8];
    private int currentSpawnPoint = 0;

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

    void OnEnable()
    {
        PlayerRegister = GameObject
            .FindGameObjectWithTag("QuestionManager")
            .GetComponent<PlayerRegister>();
    }

    void Update()
    {
    }

    public void AddProfile(User userInfo, bool isRoomLeader = false)
    {
        Transform spawnPoint = isRoomLeader
            ? LeaderSpawnPoint.transform
            : SpawnPoints[currentSpawnPoint].transform;

        var profile = Instantiate(UserProfilePrefab, spawnPoint.position, spawnPoint.rotation);
        profile.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, true);
        profile.GetComponent<UserProfileController>().SetUI(userInfo.Avatar, userInfo.Nickname);

        if (!isRoomLeader)
        {
            PlayerRegister.Register(userInfo);
            currentSpawnPoint++;
        }

    }
}
