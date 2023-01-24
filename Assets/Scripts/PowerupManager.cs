using Alteruna;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    
    [SerializeField]private Spawner _PowerupSpawner;
    //private Transform _transform;
    [SerializeField] private GameObject _powerups;
    
    
    
    private void Run()
    {
        //_PowerupSpawner.SpawnableObjects.Add(_powerups);
        _PowerupSpawner.Spawn(0);
        //_powerups = Instantiate(_powerups, Vector3.zero, Vector3.zero);
        //PowerupsList.Add();
    }
    
    private void RandomizePosition()
    {
        Vector2 position = new Vector2(Random.Range(50, 150), Random.Range(50, 150));
        //_PowerupSpawner.Spawn(0, position);
    }

    // Start is called before the first frame update
    void Start()
    {
        Run();
        RandomizePosition();
    }
}
