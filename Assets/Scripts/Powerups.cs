using System;
using System.Collections;
using System.Collections.Generic;
using Alteruna;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections.Generic;






public class Powerups : Synchronizable
{
    

    //private List<Powerups> PowerupsList = new List<Powerups>();
    //private float testfloat = 1.4f;
    //public Vector2 OldPosition = new Vector2(Random.Range(100, 150), Random.Range(100, 150)); 
        

   

  
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    public override void AssembleData(Writer writer, byte LOD = 100)
    {


         //writer.Write((_transform.localPosition));
        //throw new System.NotImplementedException();

    }

    public override void DisassembleData(Reader reader, byte LOD = 100)
    {

        //_transform.localPosition = reader.ReadVector2();
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
        //RandomizePosition();
        timer = 10.0f;
    }

    //Message ewilsandman, 𒉭 Seglarn 🎀, Greger

}
