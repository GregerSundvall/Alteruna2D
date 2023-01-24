using System;
using Alteruna;


public class FewdData : Synchronizable
{
    public float posX = 0;
    public float posY = 0;
    public float rotZ = 0;
    
    public override void AssembleData(Writer writer, byte LOD = 100)
    {
        throw new NotImplementedException();
    }

    public override void DisassembleData(Reader reader, byte LOD = 100)
    {
        throw new NotImplementedException();
    }
}
