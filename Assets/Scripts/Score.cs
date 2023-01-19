using Alteruna;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Score : MonoBehaviour
{
    public Text scoreText;
    private List<Transform> players = new List<Transform>();

    public void AddPlayer(Transform newPlayer)
    {
        //Debug.Log("Added player to list");
        players.Add(newPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;

        if (!scoreText) scoreText = GetComponent<Text>();
        scoreText.text = "";
        foreach (var player in players)
        {
            //Debug.Log("Inside player loop");
            scoreText.text += "player" + i + ": " + player.localScale.x.ToString("F2") + "\n";
            i++;
        }
        //Debug.Log(scoreText.text);
    }

}
