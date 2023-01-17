using System;
using System.Collections;
using System.Collections.Generic;
using Alteruna;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fewd : Synchronizable {
    private Vector3 position;
    private Vector3 rotation;
    private Vector3 oldPosition;
    private Vector3 oldRotation;
    private bool _wasHit = false;

    private Multiplayer alterunaMP;
    private bool iAmServer;

    void Start()
    {
        alterunaMP = FindObjectOfType<Multiplayer>();
        iAmServer = alterunaMP.Me == alterunaMP.GetUser(0);

        if (iAmServer)
        {
            position = new Vector3(Random.Range(50, 150), Random.Range(50, 150), 0);
            rotation = new Vector3(0, 0, Random.Range(0, 360));
        }
        //if "server", set random positions
        //if not "server", get positions
        
    }

    void Update()
    {
        iAmServer = alterunaMP.Me == alterunaMP.GetUser(0);
        if (iAmServer)
        {
            //if "server", update positions
            if (_wasHit)
            {
                Reset();
                _wasHit = false;
            }
            
            transform.Translate(0, 1.0f * Time.deltaTime, 0, Space.Self);
            position = transform.position;
            rotation = transform.rotation.eulerAngles;
            
            Wrap();
            
            Commit();
        }
        SyncUpdate();

        if (!iAmServer)
        {
            transform.position = position;
            transform.rotation = Quaternion.Euler(rotation);
        }
    }
    
    public override void AssembleData(Writer writer, byte LOD = 100)
    {
        writer.Write(position);
        writer.Write(rotation);
    }

    public override void DisassembleData(Reader reader, byte LOD = 100)
    {
        position = reader.ReadVector3();
        rotation = reader.ReadVector3();
        oldPosition = position;
        oldRotation = rotation;
    }

    private void Wrap()
    {
        if (transform.position.x > 5) { transform.position += Vector3.left * 10; }
        if (transform.position.x < 5) { transform.position += Vector3.right * 10; }
        if (transform.position.y > 5) { transform.position += Vector3.down * 10; }
        if (transform.position.y < 5) { transform.position += Vector3.up * 10; } 
    }
    
    void ResetPosition()
    {
        transform.position = new Vector3(Random.Range(50, 150), Random.Range(50, 150), 0);
        transform.Rotate(0, 0, Random.Range(0, 360));
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Fewd collided");
            _wasHit = true;
        }
    }

}
