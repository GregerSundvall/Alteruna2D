using UnityEngine;

public class FoodAudio : MonoBehaviour
{
    [SerializeField]private AudioSource _audioSource;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Fewd"))
        {
            _audioSource.PlayOneShot(_audioSource.clip);
        }
    }
}
