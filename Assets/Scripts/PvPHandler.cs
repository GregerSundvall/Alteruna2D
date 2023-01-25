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
    private ProcedureParameters _Parame = new ProcedureParameters(); // wtf
    private string idToSend;
    //[SerializeField] private GameObject PowerupRef;
    
    void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _multiplayerComponent = FindObjectOfType<Multiplayer>();
        _serverPlayer = _multiplayerComponent.GetUser(0);
        //PowerupRef = GetComponent<GameObject>();
        
        if (_serverPlayer == _multiplayerComponent.Me)
        {}
        //_multiplayerComponent.RegisterRemoteProcedure("KillPlayer", KillProcedureFunction);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Avatar>() != null)
        {
            var PWRef = other.gameObject.GetComponent<PowerupsV2>();
            if (PWRef != null)
            {
                Debug.Log("Valid player target");
                if (!PWRef.isInvincible)
                {
                    Debug.Log("No longer invincible");
                    PlayerController otherPlayerController = other.gameObject.GetComponent<PlayerController>();

                    if (otherPlayerController.Size < _playerController.Size)
                    {
                        Debug.Log("I'm bigger than you");
                        Debug.Log(GetComponent<UniqueID>().UIDString);
                        _playerController.EatOther();
                        //idToSend = other.gameObject.GetComponent<UniqueID>().UIDString;
                        //Debug.Log(idToSend);
                        //_Parame.Set("id", idToSend.ToString());
                        //_multiplayerComponent.InvokeRemoteProcedure("KillPlayer", UserId.All, _Parame);
                    }
                    else
                    {
                        _playerController.ResetSize();
                    }
                }
            }
        }
    }

   /* private void Kill(GameObject to)
    { 
        idToSend = to.GetComponent<UniqueID>().UIDString;
        Debug.Log(idToSend);
        _Parame.Set("id", idToSend.ToString());
        _multiplayerComponent.InvokeRemoteProcedure("KillPlayer", UserId.All, _Parame);
    }*/
    
    /*private void KillProcedureFunction(ushort userToKill, ProcedureParameters parameters, uint callId, ITransportStreamReader processor)
    {
        string UIDString;
        UIDString = parameters.Get("id", "");
        Debug.Log("KILL PROCEDURE RUN");
        Debug.Log(gameObject.name); 
        // placeholder position
       // _playerController = GetComponent<PlayerController>();
       if (gameObject.GetComponent<UniqueID>().UIDString == UIDString)
       {
           transform.position = Vector3.zero;
           //_playerController.Size = 1f;
           _playerController.Size = 1f;
           _playerController.sizeWasUpdated = true;
       }
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
    }*/
}
