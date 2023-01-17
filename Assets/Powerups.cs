using System.Collections;
using System.Collections.Generic;
using Alteruna;
using UnityEngine;


//[SerializeField] private Powerups Powerups;



public class Powerups : Synchronizable
{
    private Spawner _PowerupSpawner;
    [SerializeField] private Powerups _powerups;

    private void Run()
    {
        _PowerupSpawner = GetComponent<Spawner>();
        _PowerupSpawner.SpawnableObjects.Add(_powerups.gameObject);
    }

    private void RandomizePosition()
    {
        Vector2 position = new Vector2(Random.Range(50, 150), Random.Range(50, 150));
        //Vector2 rotation = new Vector2(Random.Range(50, 150), Random.Range(50, 150));
        _PowerupSpawner.Spawn(0, position);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Run();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override void AssembleData(Writer writer, byte LOD = 100)
    {
        // TODO 
    }

    public override void DisassembleData(Reader reader, byte LOD = 100)
    {
        // TODO
    }

    //Message ewilsandman, 𒉭 Seglarn 🎀, Greger

}
