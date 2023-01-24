using UnityEngine;
using Alteruna;
using Alteruna.Trinity;
using Random = UnityEngine.Random;

public class PowerupsV2 : MonoBehaviour
{
    public bool isInvincible;
    public bool isFaster;
    public bool isRotatingFaster;
    //public bool isBigger;
    private int _randomNumber;
    float _invincibilityTimer = 10.0f;
    float _speedupTimer = 10.0f;
    float _increaseRotationSpeedTimer = 10.0f;
    //private float _increaseSizeTimer = 10.0f;
    private SpriteRenderer _renderer;
    private Multiplayer _multiplayerComponent;
    private PlayerController _playerController;
    //[SerializeField] private GameObject PowerupRef;
    //private SpriteRenderer _colorchange;


    private void Awake()
    {
        //Sets the random seed for the powerup and picks a random number to spawn different powerup on startup.
        Random.InitState((int)System.DateTime.Now.Ticks);
        _randomNumber = Random.Range(0, 3);
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

   

    private void FixedUpdate()
    {
        if (isInvincible)
        {
            _invincibilityTimer -= Time.deltaTime;
        }
        
        if (isFaster)
        {
            _speedupTimer -= Time.deltaTime;
        }
        
        if (isRotatingFaster)
        {
            _increaseRotationSpeedTimer -= Time.deltaTime;
        }

        if (_invincibilityTimer <= 0)
        {
            isInvincible = false;
            _invincibilityTimer = 10.0f;
        }

        if (_speedupTimer <= 0)
        {
            isFaster = false;
            _playerController.Speed = 10;
            _speedupTimer = 10.0f;
        }
        
        if (_increaseRotationSpeedTimer <= 0)
        {
            isRotatingFaster = false;
            _playerController.RotationSpeed = 10;
            _invincibilityTimer = 10.0f;
        }
    }
    
    private void StartInvincibilityTimer()
    {
        _invincibilityTimer = 10.0f;
        isInvincible = true;
    }

    private void StartSpeedupTimer()
    {
        _speedupTimer = 10.0f;
        isFaster = true;
        _playerController.Speed = 100;
    }

    private void StartIncreaseRotationSpeedTimer()
    {
        _increaseRotationSpeedTimer = 10.0f;
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
            if (_randomNumber == 0)
            {
                isInvincible = true;
                //_colorchange.color = Color.red;
                _multiplayerComponent.InvokeRemoteProcedure("Invincibility", UserId.All);
            }

            if (_randomNumber == 1)
            {
                isFaster = true;
                //_colorchange.color = Color.yellow;
                _multiplayerComponent.InvokeRemoteProcedure("Speedup", UserId.All);
            }

            if (_randomNumber == 2)
            {
                isRotatingFaster = true;
                //_colorchange.color = Color.magenta;
                _multiplayerComponent.InvokeRemoteProcedure("IncreasedRotation", UserId.All);
                Debug.Log(_playerController.RotationSpeed);
            }
            Random.InitState(System.DateTime.Now.Minute);
            _randomNumber = Random.Range(0, 3);
            if (gameObject.GetComponent<PlayerController>().Avatar.IsMe)
            {
                other.gameObject.GetComponent<PowerUpObject>().NewPosition();
            }
        }
    }
}
