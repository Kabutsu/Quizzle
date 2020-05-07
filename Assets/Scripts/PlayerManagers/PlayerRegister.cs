using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRegister : MonoBehaviour
{
    public HashSet<User> Players { get; private set; } = new HashSet<User>();

    void Start()
    {
    }

    void Update()
    {
    }

    public void Register(User newPlayer)
        => Players.Add(newPlayer);

    public User GetUser(Guid userId)
        => Players.FirstOrDefault(x => x.Id.Equals(userId));
}
