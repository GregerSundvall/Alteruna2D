using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using Alteruna;
using UnityEngine;

public class FewdManager : MonoBehaviour {

    [SerializeField] private Blip BlipPrefab;

    private Spawner _spawner;
    
    public void Run()
    {
        _spawner = GetComponent<Spawner>();
        _spawner.SpawnableObjects.Add(BlipPrefab.gameObject);

        for (int i = 0; i < 200; i++)
        {
            var position = new Vector3(Random.Range(50, 150), Random.Range(50, 150), 0);
            var rotation = new Vector3(0, 0, Random.Range(0, 360));
            _spawner.Spawn(0, position, rotation);
        }
        
    }
    void Start()
    {
        // _spawner = GetComponent<Spawner>();
        // _spawner.SpawnableObjects.Add(BlipPrefab.gameObject);
        //
        // var position = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0);
        // var rotation = new Vector3(0, 0, Random.Range(0, 360));
        // _spawner.Spawn(0, position, rotation);
        //
        // for (int i = 0; i < 200; i++)
        // {
        //     
        // }
        // if (_spawner.SpawnedObjects.Count < 200)
        // {
        //     var pos = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0);
        //     var rot = new Vector3(0, 0, Random.Range(0, 360));
        //     // var scale = new Vector3(.5f, .5f, 1);
        //     _spawner.Spawn(0, pos,rot, Vector3.one);
        //     // ListOfFewd.Add(Instantiate(FewdPrefab));
        // }
        // for (int i = 0; i < 200; i++)
        // {
        //     var pos = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0);
        //     var rot = new Vector3(0, 0, Random.Range(0, 360));
        //     // var scale = new Vector3(.5f, .5f, 1);
        //     _spawner.Spawn(0, pos,rot, Vector3.one);
        //     // ListOfFewd.Add(Instantiate(FewdPrefab));
        // }
        
    }
    

    void Update()
    {
        
    }
}
