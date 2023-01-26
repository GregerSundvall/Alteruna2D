using UnityEngine;

public class AudioManager : MonoBehaviour
{
    
    [SerializeField]private AudioSource _audioSource1;
    [SerializeField]private AudioSource _audioSource2;
    [SerializeField] private AudioSource _audioSource3;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Fewd"))
        {
            _audioSource1.PlayOneShot(_audioSource1.clip);
        }

        if (other.gameObject.CompareTag("Powerup"))
        {
            _audioSource2.PlayOneShot(_audioSource2.clip);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            _audioSource3.PlayOneShot(_audioSource3.clip);
        }
    }
}
