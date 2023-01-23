using System;
using System.Collections;
using System.Collections.Generic;
using Alteruna;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using Alteruna.Trinity;






public class Powerups : MonoBehaviour
{
    

    //private List<Powerups> PowerupsList = new List<Powerups>();
    //private float testfloat = 1.4f;
    //public Vector2 OldPosition = new Vector2(Random.Range(100, 150), Random.Range(100, 150)); 
    public bool isInvincible;
    private Multiplayer _multiplayerComponent;
    float PowerupTimer = 10.0f;
    private PlayerController _playerController;
    

   

  
    // Start is called before the first frame update
    void Start()
    {
       _multiplayerComponent = FindObjectOfType<Multiplayer>();
       _multiplayerComponent.RegisterRemoteProcedure("Invincibility", TimerProcedureFunction);
        isInvincible = false;
        _playerController = GetComponent<PlayerController>();
        
        
        
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
           // Debug.Log(PowerupTimer);
        }

        if (PowerupTimer <= 0)
        {
            Debug.Log("Timer reached 0");
            isInvincible = false;
            ResetTimer();
        }
    }
    

  private void ResetTimer()
  {
      PowerupTimer = 10.0f;
  }

    private void StartTimer()
    {
        Debug.Log("Timer started");
        PowerupTimer = 10.0f;
        isInvincible = true;
    }

    private void TimerProcedureFunction(ushort userToKill, ProcedureParameters parameters, uint callId, ITransportStreamReader processor)
    {
        StartTimer();
    }

    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log("I collided WOAH!");
        if (other.gameObject.CompareTag("Powerup"))
        {
            isInvincible = true;
            _multiplayerComponent.InvokeRemoteProcedure("Invincibility", UserId.All);
            other.transform.position = new Vector3(Random.Range(50, 150), Random.Range(50, 150), 0);
            Debug.Log(other.gameObject.transform.position);
        }
    }
}
