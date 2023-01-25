using UnityEngine;
using Random = UnityEngine.Random;

public class PowerupsV2 : MonoBehaviour
{
    public bool isInvincible;
    private bool _isFaster;
    private bool _isRotatingFaster;
    private int _randomNumber;
    private float _invincibilityTimer = 10.0f;
    private float _speedupTimer = 10.0f;
    private float _increaseRotationSpeedTimer = 10.0f;
    [SerializeField]private PlayerController _playerController;
    
   

    private void Awake()
    {
        //Sets the random seed for the powerup and picks a random number to spawn different powerup on startup.
        Random.InitState((int)System.DateTime.Now.Ticks);
        _randomNumber = Random.Range(0, 3);
    }

    // Start is called before the first frame update
    void Start()
    {
        isInvincible = false;
        _isFaster = false;
        _isRotatingFaster = false;
    }

   

    private void FixedUpdate()
    {
        if (isInvincible)
        {
            _invincibilityTimer -= Time.deltaTime;
        }
        
        if (_isFaster)
        {
            _speedupTimer -= Time.deltaTime;
        }
        
        if (_isRotatingFaster)
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
            _isFaster = false;
            _playerController.speed *= 0.5f;
            _speedupTimer = 10.0f;
        }
        
        if (_increaseRotationSpeedTimer <= 0)
        {
            _isRotatingFaster = false;
            _playerController.rotationSpeed *= 0.5f;
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
        _isFaster = true;
        _playerController.speed *=2;
    }

    private void StartIncreaseRotationSpeedTimer()
    {
        _increaseRotationSpeedTimer = 10.0f;
        _isRotatingFaster = true;
        _playerController.rotationSpeed *= 2;
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            if (_randomNumber == 0)
            {
                isInvincible = true;
                StartInvincibilityTimer();
            }

            if (_randomNumber == 1)
            {
                _isFaster = true;
                StartSpeedupTimer();
            }

            if (_randomNumber == 2)
            {
                _isRotatingFaster = true;
                StartIncreaseRotationSpeedTimer();
            }
            Random.InitState(System.DateTime.Now.Minute);
            _randomNumber = Random.Range(0, 3);
            if (_playerController.avatar.IsMe)
            {
                other.gameObject.GetComponent<PowerUpObject>().NewPosition();
            }
        }
    }
}
