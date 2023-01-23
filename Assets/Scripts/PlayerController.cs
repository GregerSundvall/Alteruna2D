
using Alteruna;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    public float Speed = 10.0f;
    public float RotationSpeed = 180.0f;
    public float Size = 1;

    public Alteruna.Avatar Avatar;
    private SpriteRenderer _renderer;
    private Camera _camera;
    private RigidbodySynchronizable rigidbodySync;
    public bool sizeWasUpdated;
    

    void Start()
    {
        Avatar = GetComponent<Alteruna.Avatar>();
        _renderer = GetComponent<SpriteRenderer>();
        _camera = Camera.main;
        rigidbodySync = GetComponent<RigidbodySynchronizable>();
        FindObjectOfType<Score>().AddPlayer(transform);
    }

    void Update()
    {
        // Only let input affect the avatar if it belongs to me
        if (Avatar.IsMe)
        {
            if (sizeWasUpdated)
            {
                transform.localScale = new Vector3(Size, Size, 1);
                sizeWasUpdated = false;
            }
            // Set the avatar representing me to be green
            _renderer.color = Color.green;

            float rotationInput = -Input.GetAxis("Horizontal") * RotationSpeed * Time.deltaTime;
            Vector3 rotation = new Vector3(0, 0, rotationInput) + rigidbodySync.rotation.eulerAngles;
            rigidbodySync.MoveRotation(Quaternion.Euler(rotation));
            
            float moveInput = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
            var rads = rigidbodySync.rotation.eulerAngles.z * math.PI/180;

            var cos = math.cos(rads);
            var sin = math.sin(rads);
            var newX = 0 * cos - moveInput * sin; 
            var newY = 0 * sin + moveInput * cos;
            var translation = new Vector3(newX, newY, 0);
            
            rigidbodySync.position += translation;
            // rigidbodySync.MovePosition(translation);
            // rigidbodySync.MovePosition(rigidbodySync.position + translation);
            
            _camera.transform.position = new Vector3(rigidbodySync.position.x, rigidbodySync.position.y, -20 * Size);
            
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
    
    

    private void OnCollisionEnter(Collision col)
    {
        //if (Avatar.IsMe) This might be the reason we are desyncing in size
        {
            if (col.gameObject.CompareTag("Fewd"))
            {
                sizeWasUpdated = true;
                Size += 0.01f;
            }
        }
    }
    
}
