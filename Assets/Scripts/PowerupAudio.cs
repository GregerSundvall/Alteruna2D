using UnityEngine;

public class PowerupAudio : MonoBehaviour
{
    [SerializeField]private AudioSource _audioSource;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _audioSource.PlayOneShot(_audioSource.clip);
        }
    }
}
