using System.Collections.Generic;
using Alteruna;
using UnityEngine;

public class FewdManager : MonoBehaviour 
{
    [SerializeField] private Spawner spawner;
    private Multiplayer alterunaMP;
    private bool iAmPlayerZero = false;
    private List<GameObject> spawnedFewd;

    void Start()
    {
    }
    
    public void Register(GameObject go)
    {
        spawnedFewd.Add(go);
    }
    
    public void Init()
    {
        alterunaMP = FindObjectOfType<Multiplayer>();
        if (alterunaMP.Me == alterunaMP.GetUser(0))
        {
            iAmPlayerZero = true;
        }
        
        if (iAmPlayerZero)
        {
            for (int i = 0; i < 1; i++)
            {
                spawnedFewd.Add(spawner.Spawn(0));
            }
        }
        
        // SyncPositions();
    }
    
    public void SyncPositions()
    {
        foreach (var spawnedObject in spawnedFewd)
        {
            var fewd = spawnedObject.GetComponent<Fewd>();
            fewd.SyncPosition();
        }
    }
    

    void Update()
    {
        
    }
}
