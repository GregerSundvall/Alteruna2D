using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blip : MonoBehaviour
{
    private bool _wasHit = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_wasHit)
        {
            Reset();
            _wasHit = false;
        }
        transform.Translate(0, 1.0f * Time.deltaTime, 0, Space.Self);
        Wrap();
    }
    
    void Reset()
    {
        transform.position = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), 0);
        transform.Rotate(0, 0, Random.Range(0, 360));
    }
    
    private void Wrap()
    {
        if (transform.position.x > 5) { transform.position += Vector3.left * 10; }
        if (transform.position.x < -5) { transform.position += Vector3.right * 10; }
        if (transform.position.y > 5) { transform.position += Vector3.down * 10; }
        if (transform.position.y < -5) { transform.position += Vector3.up * 10; } 
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Fewd collided");
            _wasHit = true;
        }
    }
}
