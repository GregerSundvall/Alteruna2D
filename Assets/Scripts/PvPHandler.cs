using Alteruna;
using UnityEngine;
using Avatar = Alteruna.Avatar;

public class PvPHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private User _serverPlayer;
    private PlayerController _playerController;
    private Multiplayer _multiplayerComponent;
    private string idToSend;

    void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _multiplayerComponent = FindObjectOfType<Multiplayer>();
        _serverPlayer = _multiplayerComponent.GetUser(0);

        if (_serverPlayer == _multiplayerComponent.Me)
        {}
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

                    if (otherPlayerController.size < _playerController.size)
                    {
                        Debug.Log("I'm bigger than you");
                        Debug.Log(GetComponent<UniqueID>().UIDString);
                        _playerController.EatOther();
                    }
                    else
                    {
                        _playerController.ResetSize();
                    }
                }
            }
        }
    }
}
