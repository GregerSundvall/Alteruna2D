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
        if (other.gameObject.GetComponent<Avatar>())
        {
            PlayerController otherPlayerController = other.gameObject.GetComponent<PlayerController>();
            
            if (otherPlayerController.Size < _playerController.Size && otherPlayerController.Size > 1)
            {
                 User otherPlayer = other.gameObject.GetComponent<Avatar>().Possessor;
                 String otherGuid = other.gameObject.GetComponent<UniqueID>().UIDString;
                 ProcedureParameters parameters = new ProcedureParameters();
                 parameters.Set("otherGuid", otherGuid);
                 _playerController.Size =+ otherPlayerController.Size / 2;
                _playerController.sizeWasUpdated = true;
                _multiplayerComponent.InvokeRemoteProcedure("KillPlayer", otherPlayer, parameters);
            }
        }
    }

    public void KillProcedureFunction(ushort userToKill, ProcedureParameters parameters, uint callId, ITransportStreamReader processor)
    {
        String otherGuid = parameters.Get("otherGuid", "");
        foreach (GameObject gObject in SceneManager.GetActiveScene().GetRootGameObjects()) // there must be a better way
        {
            if (gameObject.GetComponent<UniqueID>().UIDString == otherGuid)
            {
               PlayerController otherPlayerController = gObject.GetComponent<PlayerController>();
               gObject.transform.position = Vector3.zero;
               otherPlayerController.Size = 1;
               otherPlayerController.sizeWasUpdated = true;
            }
        }
    }
}
