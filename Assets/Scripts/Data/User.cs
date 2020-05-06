using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public Guid Id { get; }
    public string Nickname { get; set; }
    public Sprite Avatar { get; set; }
    public int Score { get; set; }

    public User(string nickname, Sprite avatar)
    {
        Id = Guid.NewGuid();
        Nickname = nickname;
        Avatar = avatar;
        Score = 0;
    }
}
