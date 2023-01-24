using Alteruna;
using UnityEngine;

public class NetworkUsage : MonoBehaviour
{
    // Start is called before the first frame update
    private Multiplayer _multiplayer;
    void Start()
    {
        _multiplayer = GetComponent<Multiplayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
         uint received = _multiplayer.NetworkStatistics.BytesPerSecondReceived;
         uint sent = _multiplayer.NetworkStatistics.BytesPerSecondSent;
         Debug.Log("Sent " + sent + " Received " + received);
        }
    }
}
