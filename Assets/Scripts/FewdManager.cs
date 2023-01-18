using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using Alteruna;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class FewdManager : MonoBehaviour 
{
    [SerializeField] private Spawner spawner;
    private Multiplayer alterunaMP;
    private bool iAmPlayerZero = false;

    void Start()
    {
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
                spawner.Spawn(0);
            }
        }
    }
    

    void Update()
    {
        
    }
}
