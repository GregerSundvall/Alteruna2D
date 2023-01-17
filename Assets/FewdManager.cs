using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using Alteruna;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class FewdManager : MonoBehaviour {

    [SerializeField] private GameObject FewdPrefab;
    private List<GameObject> FewdList = new List<GameObject>();


    void Start()
    {

        
    }
    
    public void Init()
    {
        for (int i = 0; i < 20; i++)
        {
            FewdList.Add(Instantiate(FewdPrefab));
        }
    }
    

    void Update()
    {
        
    }
}
