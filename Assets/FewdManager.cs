using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using Alteruna;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class FewdManager : MonoBehaviour 
{

    // [SerializeField] private GameObject FewdPrefab;
    // private List<GameObject> FewdList = new List<GameObject>();
    [SerializeField] private Spawner spawner;
    private Multiplayer alterunaMP;
    private bool iAmServer = false;

    void Start()
    {
        alterunaMP = FindObjectOfType<Multiplayer>();


    }
    
    public void Init()
    {
        if (alterunaMP.Me == alterunaMP.GetUser(0))
        {
            iAmServer = true;
        }
        Debug.Log(alterunaMP.Me);
        
        if (iAmServer)
        {
            for (int i = 0; i < 1; i++)
            {
                spawner.Spawn(0);
                // FewdList.Add();
            }
        }

    }
    

    void Update()
    {
        
    }
}
