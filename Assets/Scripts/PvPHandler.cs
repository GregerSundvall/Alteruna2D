using System;
using System.Collections;
using System.Collections.Generic;
using Alteruna;
using Alteruna.Trinity;
using UnityEngine;
using UnityEngine.SceneManagement;
using Avatar = Alteruna.Avatar;

public class PvPHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private Avatar _playerAvatar;
    private User _serverPlayer;
    private Transform _playerTransform;
    private PlayerController _playerController;
    private Multiplayer _multiplayerComponent;
    
    void Start()
    {
        _playerController = GetComponent<PlayerController>();    
        _playerTransform = GetComponent<Transform>();
        _playerAvatar = GetComponent<Avatar>();
        _multiplayerComponent = FindObjectOfType<Multiplayer>();
        _serverPlayer = _multiplayerComponent.GetUser(0);
        
        if (_serverPlayer == _multiplayerComponent.Me)
        {}
        _multiplayerComponent.RegisterRemoteProcedure("KillPlayer", KillProcedureFunction);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Avatar>() != null)
        {
            PlayerController otherPlayerController = other.gameObject.GetComponent<PlayerController>();

            if (otherPlayerController.Size < _playerController.Size)
            {
                Debug.Log("I'm bigger than you");
                Debug.Log("other size " + otherPlayerController.Size);
                Debug.Log("my size " + _playerController.Size);
                Debug.Log(GetComponent<UniqueID>().UIDString);
                 User otherPlayer = other.gameObject.GetComponent<Avatar>().Possessor;
                 String otherGuid = other.gameObject.GetComponent<UniqueID>().UIDString;
                 
            }
        }
    }
    
    
    public void Kill()
    {
        ProcedureParameters parameters = new ProcedureParameters();
        parameters.Set("Guid", GetComponent<UniqueID>().UIDString);
        _playerController.Size *= 2.0f;
        _playerController.sizeWasUpdated = true;
        _multiplayerComponent.InvokeRemoteProcedure("KillPlayer", otherPlayer, parameters);
    }

    public void KillProcedureFunction(ushort userToKill, ProcedureParameters parameters, uint callId, ITransportStreamReader processor)
    {
        Debug.Log("KILL PROCEDURE RUN");
        String otherGuid = parameters.Get("otherGuid", "");
        Debug.Log("other " + otherGuid);
        Debug.Log("self" + GetComponent<UniqueID>().UIDString);

        transform.position = Vector3.zero;
        _playerController.Size *= 0.1f;
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
