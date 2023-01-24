using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Avatar = Alteruna.Avatar;

public class Score : MonoBehaviour
{
    public Text scoreText;
    private List<Transform> players = new List<Transform>();
    private List<String> names = new List<string>();

    public void AddPlayer(Transform newPlayer)
    {
        players.Add(newPlayer);
        names.Add(newPlayer.GetComponent<Avatar>().Possessor.Name); // name is synced
    }

    // Update is called once per frame
    void Update()
    {
        if (!scoreText) scoreText = GetComponent<Text>();
        scoreText.text = "";
        for (int i = 0; i < players.Count; i++)
        {
            scoreText.text += names[i] + ": " + players[i].localScale.x.ToString("F2") + "\n"; // this is only local
        }

    }

}
