using UnityEngine;
using Alteruna.Trinity;
using Alteruna;
using Random = UnityEngine.Random;

public class PowerupsV2 : MonoBehaviour
{
    public bool isInvincible;
    public bool isFaster;
    public bool isRotatingFaster;
    private int _randomNumber;
    float _invincibilityTimer = 10.0f;
    float _speedupTimer = 10.0f;
    float _increaseRotationSpeedTimer = 10.0f;
    private SpriteRenderer _renderer;
    private PlayerController _playerController;
   

    private void Awake()
    {
        //Sets the random seed for the powerup and picks a random number to spawn different powerup on startup.
        Random.InitState((int)System.DateTime.Now.Ticks);
        _randomNumber = Random.Range(0, 3);
    }

    // Start is called before the first frame update
    void Start()
    {
        _playerController = GetComponent<PlayerController>();
       isInvincible = false;
        isFaster = false;
        isRotatingFaster = false;
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
            _playerController.speed /= 2;
            _speedupTimer = 10.0f;
        }
        
        if (_increaseRotationSpeedTimer <= 0)
        {
            isRotatingFaster = false;
            
            _playerController.rotationSpeed /= 2;
            _increaseRotationSpeedTimer = 10.0f;
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
        _playerController.speed *=2;
    }

    private void StartIncreaseRotationSpeedTimer()
    {
        _increaseRotationSpeedTimer = 10.0f;
        isRotatingFaster = true;
        _playerController.rotationSpeed *= 2;
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            if (_randomNumber == 0)
            {
                isInvincible = true;
            }

            if (_randomNumber == 1)
            {
                isFaster = true;
                StartSpeedupTimer();
            }

            if (_randomNumber == 2)
            {
                isRotatingFaster = true;
                StartIncreaseRotationSpeedTimer();
            }
            Random.InitState(System.DateTime.Now.Minute);
            _randomNumber = Random.Range(0, 3);
            if (gameObject.GetComponent<PlayerController>().avatar.IsMe)
            {
                other.gameObject.GetComponent<PowerUpObject>().NewPosition();
            }
        }
    }
}
