using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public string RoomCode { get; set; }
    public string Nickname { get; set; }
    public Sprite Avatar { get; set; }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
