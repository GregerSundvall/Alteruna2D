using Alteruna;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpObject : Synchronizable
{
    // Start is called before the first frame update

    private void FixedUpdate()
    {
        SyncUpdate();
    }

    public override void AssembleData(Writer writer, byte LOD = 100)
    {
        writer.Write(transform.position);
    }

    public override void DisassembleData(Reader reader, byte LOD = 100)
    {
        transform.position = reader.ReadVector3();
    }

    public void NewPosition()
    {
        transform.position = new Vector3(Random.Range(50, 150), Random.Range(50, 150), 0);
        Commit();
    }
}
