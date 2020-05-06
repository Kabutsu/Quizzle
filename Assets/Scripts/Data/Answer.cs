using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answer
{
    public Guid Id { get; }
    public string Text { get; }
    public HashSet<Guid> Votes { get; set; }
    public HashSet<Guid> PointlessVotes { get; set; }

    public Answer(string text)
    {
        Id = Guid.NewGuid();
        Text = text;
        Votes = new HashSet<Guid>();
        PointlessVotes = new HashSet<Guid>();
    }

    public void Vote(Guid userId) => Votes.Add(userId);
    public void PointlessVote(Guid userId) => Votes.Add(userId);
}
