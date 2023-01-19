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
    
    void Start()
    {
        transform.position = new Vector3(Random.Range(50, 150), Random.Range(50, 150), 0);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
        
        Commit();
    }

    void Update()
    {
        transform.Translate(0, 1.0f * Time.deltaTime, 0, Space.Self);
        Wrap();
        
        SyncUpdate();
    }
    
    void SyncPosition()
    {
        // More stuff here?
        Commit();
    }
    

    public override void AssembleData(Writer writer, byte LOD = 100)
    {
        writer.Write(transform.position);
        writer.Write(transform.rotation);
    }

    public override void DisassembleData(Reader reader, byte LOD = 100)
    {
        transform.position = reader.ReadVector3();
        transform.rotation = Quaternion.Euler(reader.ReadVector3());
    }

    private void Wrap()
    {
        if (transform.position.x > 150) { transform.position += Vector3.left * 100; Commit();}
        if (transform.position.x < 50) { transform.position += Vector3.right * 100; Commit();}
        if (transform.position.y > 150) { transform.position += Vector3.down * 100; Commit();}
        if (transform.position.y < 50) { transform.position += Vector3.up * 100; Commit();} 
    }
    
    void ResetPosition()
    {
        transform.position = new Vector3(Random.Range(50, 150), Random.Range(50, 150), 0);
        transform.Rotate(0, 0, Random.Range(0, 360));
        Commit();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            ResetPosition();
        }
    }

}
