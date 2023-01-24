using Alteruna;
using UnityEngine;
using Random = UnityEngine.Random;
using Alteruna.Trinity;

public class Powerups : MonoBehaviour
{
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
    }

    // Update is called once per frame
    

    private void FixedUpdate()
    {
        if (isInvincible)
        {
            PowerupTimer -= Time.deltaTime;
        }

        if (PowerupTimer <= 0)
        {
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
        if (other.gameObject.CompareTag("Powerup"))
        {
            isInvincible = true;
            _multiplayerComponent.InvokeRemoteProcedure("Invincibility", UserId.All);
            other.transform.position = new Vector3(Random.Range(50, 150), Random.Range(50, 150), 0);
            Debug.Log(other.gameObject.transform.position);
        }
    }
}
