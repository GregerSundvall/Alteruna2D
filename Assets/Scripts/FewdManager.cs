using System;
using System.Collections.Generic;
using Alteruna;
using UnityEngine;

public class FewdManager : MonoBehaviour 
{
    // [SerializeField] private Spawner spawner;
    private Multiplayer alterunaMP;
    // private bool iAmPlayerZero = false;
    private List<GameObject> spawnedFewd = new List<GameObject>();

    private void Start()
    {
        alterunaMP = FindObjectOfType<Multiplayer>();
        
        var objects = FindObjectsOfType<Fewd>();
        foreach (var fewd in objects)
        {
            spawnedFewd.Add(fewd.gameObject);
        }

        Debug.Log(spawnedFewd.Count + "fewd in list");
    }

    public void Register(GameObject go)
    {
        spawnedFewd.Add(go);
    }
    
    // public void Init()
    // {
    //     alterunaMP = FindObjectOfType<Multiplayer>();
    //     if (alterunaMP.Me == alterunaMP.GetUser(0))
    //     {
    //         iAmPlayerZero = true;
    //     }
    //     
    //     if (iAmPlayerZero)
    //     {
    //         for (int i = 0; i < 1; i++)
    //         {
    //             spawnedFewd.Add(spawner.Spawn(0));
    //         }
    //     }
    // }
    
    public void SyncPositions()
    {
        Debug.Log("Sync Positions");
        Debug.Log("Sync Positions as player 0");
        foreach (var spawnedObject in spawnedFewd)
        {
            var fewd = spawnedObject.GetComponent<Fewd>();
            fewd.SyncPosition();
        }

        
    }
    

}
