using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;
using Alteruna.Trinity;
using Random = UnityEngine.Random;

public class PowerupsV2 : MonoBehaviour
{
    public bool isInvincible;
    public bool isFaster;
    public bool isRotatingFaster;
    public bool isBigger;
    private int _RandomNumber;
    float InvincibilityTimer = 10.0f;
    float SpeedupTimer = 10.0f;
    float IncreaseRotationSpeedTimer = 10.0f;
    private float IncreaseSizeTimer = 10.0f;
    private SpriteRenderer _renderer;
    private Multiplayer _multiplayerComponent;
    private PlayerController _playerController;
    [SerializeField] private GameObject PowerupRef;
    private SpriteRenderer _colorchange;


    private void Awake()
    {
        //Sets the random seed for the powerup and picks a random number to spawn different powerup on startup.
        Random.InitState((int)System.DateTime.Now.Ticks);
        _RandomNumber = Random.Range(0, 2);
    }

    // Start is called before the first frame update
    void Start()
    {
       _multiplayerComponent = FindObjectOfType<Multiplayer>();
       _playerController = GetComponent<PlayerController>();
       _multiplayerComponent.RegisterRemoteProcedure("Invincibility", InvincibilityTimerProcedureFunction);
       _multiplayerComponent.RegisterRemoteProcedure("Speedup", SpeedupTimerProcedureFunction);
       _multiplayerComponent.RegisterRemoteProcedure("IncreasedRotation", RotationSpeedupTimerProcedureFunction);
        isInvincible = false;
        isFaster = false;
        isRotatingFaster = false;
        //_colorchange = PowerupRef.GetComponent<SpriteRenderer>();
        
        
        

       //switch (_RandomNumber)
       //{
       //    case 0:
       //        _colorchange.color = Color.red;
       //        break;
       //    case 1:
       //        _colorchange.color = Color.yellow;
       //        break;
       //    case 2:
       //        _colorchange.color = Color.magenta;
       //        break;
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
            InvincibilityTimer -= Time.deltaTime;
         //   Debug.Log("Invincibility timer " + InvincibilityTimer);
        }
        
        if (isFaster)
        {
            SpeedupTimer -= Time.deltaTime;
            Debug.Log("Speed up timer " + SpeedupTimer);
        }
        
        if (isRotatingFaster)
        {
            IncreaseRotationSpeedTimer -= Time.deltaTime;
            Debug.Log("Increased rotation timer " + IncreaseRotationSpeedTimer);
        }

        if (InvincibilityTimer <= 0)
        {
            Debug.Log("Timer reached 0");
            isInvincible = false;
            ResetInvincibilityTimer();
        }

        if (SpeedupTimer <= 0)
        {
            isFaster = false;
            _playerController.Speed = 10;
            ResetSpeedupTimer();
        }
        
        if (IncreaseRotationSpeedTimer <= 0)
        {
            isRotatingFaster = false;
            _playerController.RotationSpeed = 10;
            ResetIncreasedRotationTimer();
        }
    }
    

  private void ResetInvincibilityTimer()
  {
      InvincibilityTimer = 10.0f;
      Debug.Log("IsInvincible " + isInvincible);
  }

  private void ResetSpeedupTimer()
  {
      SpeedupTimer = 10.0f;
      Debug.Log(_playerController.Speed);
  }

  private void ResetIncreasedRotationTimer()
  {
      IncreaseRotationSpeedTimer = 10.0f;
      Debug.Log(_playerController.RotationSpeed);
  }

    private void StartInvincibilityTimer()
    {
        Debug.Log("Timer started");
        InvincibilityTimer = 10.0f;
        isInvincible = true;
    }

    private void StartSpeedupTimer()
    {
        Debug.Log("Speedup timer started");
        SpeedupTimer = 10.0f;
        isFaster = true;
        _playerController.Speed = 100;
    }

    private void StartIncreaseRotationSpeedTimer()
    {
        Debug.Log("Increased rotation speed timer started");
        IncreaseRotationSpeedTimer = 10.0f;
        isRotatingFaster = true;
        _playerController.RotationSpeed = 100;
    }

    private void InvincibilityTimerProcedureFunction(ushort userToKill, ProcedureParameters parameters, uint callId, ITransportStreamReader processor)
    {
        StartInvincibilityTimer();
    }
    
    private void SpeedupTimerProcedureFunction(ushort userToKill, ProcedureParameters parameters, uint callId, ITransportStreamReader processor)
    {
        StartSpeedupTimer();
    }
    
    private void RotationSpeedupTimerProcedureFunction(ushort userToKill, ProcedureParameters parameters, uint callId, ITransportStreamReader processor)
    {
        StartIncreaseRotationSpeedTimer();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            if (_RandomNumber == 0)
            {
                isInvincible = true;
                //_colorchange.color = Color.red;
                _multiplayerComponent.InvokeRemoteProcedure("Invincibility", UserId.All);
                Debug.Log(isInvincible);
            }

            if (_RandomNumber == 1)
            {
                isFaster = true;
                //_colorchange.color = Color.yellow;
                _multiplayerComponent.InvokeRemoteProcedure("Speedup", UserId.All);
                Debug.Log(_playerController.Speed);
            }

            if (_RandomNumber == 2)
            {
                isRotatingFaster = true;
                //_colorchange.color = Color.magenta;
                _multiplayerComponent.InvokeRemoteProcedure("IncreasedRotation", UserId.All);
                Debug.Log(_playerController.RotationSpeed);
            }
            Random.InitState(System.DateTime.Now.Minute);
            //Picks a new random number and position and a new random seed for the next powerup to spawn.
            if (gameObject.GetComponent<PlayerController>().Avatar.IsMe)
            {
                other.gameObject.GetComponent<PowerUpObject>().NewPosition();
            }
        }
    }
}
