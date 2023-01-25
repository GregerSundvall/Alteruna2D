using UnityEngine;

public class FoodAudio : MonoBehaviour
{
    private AudioSource _audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Fewd"))
        {
            _audioSource.PlayOneShot(_audioSource.clip);
        }
    }
}
