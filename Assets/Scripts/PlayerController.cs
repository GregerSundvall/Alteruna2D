using Alteruna;
using Alteruna.Trinity;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    public float speed = 10.0f;
    public float rotationSpeed = 180.0f;
    public float size = 1;
    public int score = 0;
    public Alteruna.Avatar avatar;
    public bool sizeWasUpdated;

    private SpriteRenderer _renderer;
    private Camera _camera;
    private RigidbodySynchronizable rigidbodySync;
    private Multiplayer _multiplayer;


    void Start()
    {
        avatar = GetComponent<Alteruna.Avatar>();
        _renderer = GetComponent<SpriteRenderer>();
        _camera = Camera.main;
        rigidbodySync = GetComponent<RigidbodySynchronizable>();
        FindObjectOfType<Score>().AddPlayer(transform);
        _multiplayer = FindObjectOfType<Multiplayer>();
        _multiplayer.RegisterRemoteProcedure("PlayerSizeIncrease", OnSizeIncreaseFunction);
        _multiplayer.RegisterRemoteProcedure("PlayerSizeReset", OnSizeResetFunction);
        _multiplayer.RegisterRemoteProcedure("PlayerSizeJump", OnSizeJumpFunction);
    }

    void Update()
    {
        // Only let input affect the avatar if it belongs to me
        if (sizeWasUpdated) // remnant
        {
            transform.localScale = new Vector3(size, size, 1);
            sizeWasUpdated = false;
        }
        if (avatar.IsMe)
        {
            // Set the avatar representing me to be green
            _renderer.color = Color.green;

            float rotationInput = -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
            Vector3 rotation = new Vector3(0, 0, rotationInput) + rigidbodySync.rotation.eulerAngles;
            rigidbodySync.MoveRotation(Quaternion.Euler(rotation));
            
            float moveInput = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            var rads = rigidbodySync.rotation.eulerAngles.z * math.PI/180;

            var cos = math.cos(rads);
            var sin = math.sin(rads);
            var newX = 0 * cos - moveInput * sin; 
            var newY = 0 * sin + moveInput * cos;
            var translation = new Vector3(newX, newY, 0);
            
            rigidbodySync.position += translation;
            _camera.transform.position = new Vector3(rigidbodySync.position.x, rigidbodySync.position.y, -20 * size);
            
            Wrap();
        }
    }
    
    private void Wrap()
    {
        if (transform.position.x > 150) { transform.position += Vector3.left * 100; }
        if (transform.position.x < 50) { transform.position += Vector3.right * 100; }
        if (transform.position.y > 150) { transform.position += Vector3.down * 100; }
        if (transform.position.y < 50) { transform.position += Vector3.up * 100; }
    }

    private void EatFewd()
    {
        sizeWasUpdated = true;
        score += 1;
        size += 0.01f;
        _multiplayer.InvokeRemoteProcedure("PlayerSizeIncrease", UserId.All);
    }
    private void OnSizeIncreaseFunction(ushort User, ProcedureParameters parameters, uint callId,
        ITransportStreamReader processor)
    {
        if (!avatar.IsMe)
        {
            sizeWasUpdated = true;
            size += 0.01f;
            score += 1;
        }
    }

    public void EatOther()
    {
        if (avatar.IsMe)
        {
            size += 0.1f;
            score += 10;
            sizeWasUpdated = true;
            _multiplayer.InvokeRemoteProcedure("PlayerSizeJump", UserId.All);
        }
    }

    private void OnSizeJumpFunction(ushort User, ProcedureParameters parameters, uint callId,
        ITransportStreamReader processor)
    {
        if (!avatar.IsMe)
        {
            size += 0.1f;
            score += 10;
            sizeWasUpdated = true;
        }
    }

    public void ResetSize()
    {
        if (avatar.IsMe)
        {
            size = 1;
            sizeWasUpdated = true;
            _multiplayer.InvokeRemoteProcedure("PlayerSizeReset", UserId.All);
        }
    }
    private void OnSizeResetFunction(ushort User, ProcedureParameters parameters, uint callId,
        ITransportStreamReader processor)
    {
        if (!avatar.IsMe)
        {
            size = 1;
            sizeWasUpdated = true;
        }
    }
    
    private void OnCollisionEnter(Collision col)
    {
        if (avatar.IsMe)
        {
            if (col.gameObject.CompareTag("Fewd"))
            {   
                EatFewd();
            }
        }
    }
}
