using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using Alteruna;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class FewdManager : MonoBehaviour {

    [SerializeField] private GameObject FewdPrefab;
    private List<GameObject> FewdList = new List<GameObject>();
    [SerializeField] private Spawner spawner;
    

    void Start()
    {
        
        

        
    }
    
    public void Init()
    {
        for (int i = 0; i < 1; i++)
        {
            FewdList.Add(spawner.Spawn(0));
        }
    }
    

    void Update()
    {
        
    }
}
