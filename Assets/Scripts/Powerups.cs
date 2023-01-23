using System;
using System.Collections;
using System.Collections.Generic;
using Alteruna;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
// using System.Collections.Generic;
using Alteruna.Trinity;






public class Powerups : MonoBehaviour
{
    

    //private List<Powerups> PowerupsList = new List<Powerups>();
    //private float testfloat = 1.4f;
    //public Vector2 OldPosition = new Vector2(Random.Range(100, 150), Random.Range(100, 150)); 
    public bool isInvincible;
    private Multiplayer _multiplayerComponent;
    float PowerupTimer = 10.0f;

   

  
    // Start is called before the first frame update
    void Start()
    {
       _multiplayerComponent = FindObjectOfType<Multiplayer>();
       _multiplayerComponent.RegisterRemoteProcedure("PowerupEaten", TimerProcedureFunction);
        isInvincible = false;
        
        
        
        float RandomNumber = Random.Range(0, 3);

        //switch (RandomNumber)
        //{
        //    case 0:
        //        
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (isInvincible)
        {
            PowerupTimer -= Time.deltaTime;
            Debug.Log(PowerupTimer);
        }

        if (PowerupTimer <= 0)
        {
            Debug.Log("Timer reached 0");
            isInvincible = false;
            ResetTimer();
        }
    }

  //public override void AssembleData(Writer writer, byte LOD = 100)
  //{
//
//
  //    //writer.Write((_transform.localPosition));
  //    //throw new System.NotImplementedException();
//
  //}
//
  //public override void DisassembleData(Reader reader, byte LOD = 100)
  //{
//
  //    //_transform.localPosition = reader.ReadVector2();
  //    //throw new System.NotImplementedException();
  //}



  private void ResetTimer()
  {
      PowerupTimer = 10.0f;
  }

    private void StartTimer()
    {
        Debug.Log("Timer started");
        PowerupTimer = 10.0f;
        isInvincible = true;
        //if (PowerupTimer <= 0)
        //{
        //    ResetTimer();
        //    Debug.Log("timer has been reset " + PowerupTimer);
        //}
    }

    private void TimerProcedureFunction(ushort userToKill, ProcedureParameters parameters, uint callId, ITransportStreamReader processor)
    {
        StartTimer();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            isInvincible = true;
            Debug.Log("Collision happened");
            _multiplayerComponent.InvokeRemoteProcedure("PowerupEaten", UserId.All);
            //StartTimer();
            Destroy(other.gameObject);
        }
    }
}
