using Alteruna;
using Alteruna.Trinity;
using UnityEngine;
using Avatar = Alteruna.Avatar;

public class PvPHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private User _serverPlayer;
    private PlayerController _playerController;
    private Multiplayer _multiplayerComponent;
    //[SerializeField] private GameObject PowerupRef;
    
    void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _multiplayerComponent = FindObjectOfType<Multiplayer>();
        _serverPlayer = _multiplayerComponent.GetUser(0);
        //PowerupRef = GetComponent<GameObject>();
        
        if (_serverPlayer == _multiplayerComponent.Me)
        {}
        _multiplayerComponent.RegisterRemoteProcedure("KillPlayer", KillProcedureFunction);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Avatar>() != null)
        {
            var PWRef = other.gameObject.GetComponent<Powerups>();
            if (PWRef != null)
            {
                if (!PWRef.isInvincible)
                {
                    PlayerController otherPlayerController = other.gameObject.GetComponent<PlayerController>();

                    if (otherPlayerController.Size < _playerController.Size)
                    {
                        Debug.Log("I'm bigger than you");
                        Debug.Log(GetComponent<UniqueID>().UIDString);
                        other.gameObject.GetComponent<PvPHandler>().Kill();
                    }
                }
            }
        }
    }
    
    private void Kill()
    {
        _multiplayerComponent.InvokeRemoteProcedure("KillPlayer", UserId.All);
    }
    
    private void KillProcedureFunction(ushort userToKill, ProcedureParameters parameters, uint callId, ITransportStreamReader processor)
    {
        Debug.Log("KILL PROCEDURE RUN");
        transform.position = Vector3.zero; // placeholder position
        _playerController.Size = 1f;
        _playerController.sizeWasUpdated = true;
        //
        // foreach (GameObject gObject in SceneManager.GetActiveScene().GetRootGameObjects()) // there must be a better way
        // {
        //     if (gameObject.GetComponent<UniqueID>().UIDString == otherGuid)
        //     {
        //         Debug.Log("RESETTING LOSING PLAYER POSITION ETC");
        //        PlayerController otherPlayerController = gObject.GetComponent<PlayerController>();
        //        gObject.transform.position = Vector3.zero;
        //        otherPlayerController.Size = 1;
        //        otherPlayerController.sizeWasUpdated = true;
        //     }
        // }
    }
}
