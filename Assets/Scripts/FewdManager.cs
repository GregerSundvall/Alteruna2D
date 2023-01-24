using System;
using System.Collections.Generic;
using System.Net.Http;
using Alteruna;
using UnityEngine;

public class FewdManager : Synchronizable 
{
    private Multiplayer alterunaMP;
    private FewdData fewdData;
    private bool iAmPlayerZero = false;
    public List<GameObject> spawnedFewd = new List<GameObject>();
    [SerializeField] private GameObject fewdPrefab;
    private int fewdCount = 50;

    private void Start()
    {
        alterunaMP = FindObjectOfType<Multiplayer>();
        fewdData = new FewdData();
    }

    private void Update()
    {
        SyncUpdate();
    }

    public void Init()
    {
        for (int i = 0; i < fewdCount; i++)
        {
            fewdData.posX[i] = 0;
            fewdData.posY[i] = 0;
            fewdData.rotZ[i] = 0;
        }
        
        for (int i = 0; i < fewdCount; i++)
        {
            var obj = Instantiate(fewdPrefab);
            obj.GetComponent<Fewd>().fewdManager = this;
            obj.GetComponent<Fewd>().index = i;
            spawnedFewd.Add(obj);
        }
        
        if (alterunaMP.Me == alterunaMP.GetUser(0)) { iAmPlayerZero = true; }
        
        if (iAmPlayerZero)
        {
            for (int i = 0; i < fewdCount; i++)
            {
                fewdData.posX[i] = spawnedFewd[i].transform.position.x;
                fewdData.posY[i] = spawnedFewd[i].transform.position.y;
                fewdData.rotZ[i] = spawnedFewd[i].transform.rotation.eulerAngles.z;
            }
            Commit();
        }
        else
        {
            SyncAllFromReceivedData();
        }
    }
    
    public void PublishCurrentPositions()
    {
        for (int i = 0; i < fewdCount; i++)
        {
            fewdData.posX[i] = spawnedFewd[i].transform.position.x;
            fewdData.posY[i] = spawnedFewd[i].transform.position.y;
            fewdData.rotZ[i] = spawnedFewd[i].transform.rotation.eulerAngles.z;
        }
        Commit();
    }
    
    void SyncAllFromReceivedData()
    {
        if (spawnedFewd.Count != fewdCount) { return; }
        
        for (int i = 0; i < fewdCount; i++)
        {
            var x = fewdData.posX[i];
            var y = fewdData.posY[i];
            Debug.Log(spawnedFewd.Count + " / " + i);
            spawnedFewd[i].transform.position = new Vector3(x, y, 0);
            spawnedFewd[i].transform.rotation = Quaternion.Euler(0, 0, fewdData.rotZ[i]);
        }
    }

    public void PublishChangedPosition(int index)
    {
        fewdData.posX[index] = spawnedFewd[index].transform.position.x;
        fewdData.posY[index] = spawnedFewd[index].transform.position.y;
        fewdData.rotZ[index] = spawnedFewd[index].transform.rotation.eulerAngles.z;
        Commit();
    }

    public override void AssembleData(Writer writer, byte LOD = 100)
    {
        writer.WriteObject(fewdData);
    }

    public override void DisassembleData(Reader reader, byte LOD = 100)
    {
        fewdData = reader.ReadObject() as FewdData;
        SyncAllFromReceivedData();
    }
}
