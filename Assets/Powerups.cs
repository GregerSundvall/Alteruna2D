using System;
using System.Collections;
using System.Collections.Generic;
using Alteruna;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;






public class Powerups : Synchronizable
{
    private Spawner _PowerupSpawner;
    private Transform _transform;
    [SerializeField] private GameObject _powerups;
    //private float testfloat = 1.4f;
    //public Vector2 OldPosition = new Vector2(Random.Range(100, 150), Random.Range(100, 150)); 
        

    private void Run()
    {
        _PowerupSpawner = GetComponent<Spawner>();
        _PowerupSpawner.SpawnableObjects.Add(_powerups.gameObject);
    }

    private void RandomizePosition()
    {
        Vector2 position = new Vector2(Random.Range(50, 150), Random.Range(50, 150));
        _PowerupSpawner.Spawn(0, position);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Run();
        RandomizePosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    public override void AssembleData(Writer writer, byte LOD = 100)
    {


         writer.Write((_transform.localPosition));
        //throw new System.NotImplementedException();

    }

    public override void DisassembleData(Reader reader, byte LOD = 100)
    {

        _transform.localPosition = reader.ReadVector2();
        //throw new System.NotImplementedException();
    }

    private void OnTriggerEnter(Collider other)
    {
        StartTimer();
        Debug.Log("Collision happened");
        //Destroy();
    }



    public void StartTimer()
    {
        Debug.Log("Timer started");
        float timer = 10.9f;
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        RandomizePosition();
        timer = 10.0f;
    }

    //Message ewilsandman, 𒉭 Seglarn 🎀, Greger

}
