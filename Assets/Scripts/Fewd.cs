using Alteruna;
using UnityEngine;
using Avatar = Alteruna.Avatar;
using Random = UnityEngine.Random;

public class Fewd : Synchronizable 
{

    void Update()
    {
        transform.Translate(0, 1.0f * Time.deltaTime, 0, Space.Self);
        Wrap();
        
        SyncUpdate();
    }

    private void Wrap()
    {
        if (transform.position.x > 150) { transform.position += Vector3.left * 100;}
        if (transform.position.x < 50) { transform.position += Vector3.right * 100; }
        if (transform.position.y > 150) { transform.position += Vector3.down * 100; }
        if (transform.position.y < 50) { transform.position += Vector3.up * 100; } 
    }
    
    public void ResetPosition()
    {
        transform.position = new Vector3(Random.Range(50, 150), Random.Range(50, 150), 0);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
        Commit();
    }

    public override void AssembleData(Writer writer, byte LOD = 100)
    {
        writer.Write(transform.position);
        writer.Write(transform.rotation.eulerAngles.z);
    }

    public override void DisassembleData(Reader reader, byte LOD = 100)
    {
        transform.position = reader.ReadVector3();
        transform.rotation = Quaternion.Euler(0, 0, reader.ReadFloat());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Avatar>().IsMe)
        {
            ResetPosition();
        }
    }
}