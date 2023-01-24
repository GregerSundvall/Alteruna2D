using System.Collections.Generic;
using Alteruna;
using UnityEngine;

public class FewdManager : MonoBehaviour
{
    public List<Fewd> listOfFewd;

    private void Start()
    {
        var array = FindObjectsOfType<Fewd>();
        foreach (var obj in array)
        {
            listOfFewd.Add(obj);
        }
    }
    
    public void OnJoinRoom()
    {
        var alteruna = FindObjectOfType<Multiplayer>();
        if (alteruna.Me == alteruna.GetUser(0))
        {
            for (int i = 0; i < listOfFewd.Count; i++)
            {
                listOfFewd[i].ResetPosition();
            }
        }
    }

    public void PublishAllPositions()
    {
        for (int i = 0; i < listOfFewd.Count; i++)
        {
            listOfFewd[i].Commit();
        }
    }

}