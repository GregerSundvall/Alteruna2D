
using System;
using Alteruna;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fewd : MonoBehaviour 
{
    public FewdManager fewdManager;
    // private FewdData fewdData;
    public int index;

    
    void Start()
    {
        transform.position = new Vector3(Random.Range(50, 150), Random.Range(50, 150), 0);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
    }

    void Update()
    {
        transform.Translate(0, 1.0f * Time.deltaTime, 0, Space.Self);
        Wrap();
    }

    private void Wrap()
    {
        if (transform.position.x > 150) { transform.position += Vector3.left * 100;}
        if (transform.position.x < 50) { transform.position += Vector3.right * 100; }
        if (transform.position.y > 150) { transform.position += Vector3.down * 100; }
        if (transform.position.y < 50) { transform.position += Vector3.up * 100; } 
    }
    
    void ResetPosition()
    {
        transform.position = new Vector3(Random.Range(50, 150), Random.Range(50, 150), 0);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
        
        fewdManager.PublishChangedPosition(index);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ResetPosition();
        }
    }
}
