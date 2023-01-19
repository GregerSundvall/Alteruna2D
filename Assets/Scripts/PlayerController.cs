
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    public float Speed = 10.0f;
    public float RotationSpeed = 180.0f;
    public float Size = 1;

    private Alteruna.Avatar _avatar;
    private SpriteRenderer _renderer;
    private Camera _camera;
    private Transform _transform;
    public bool sizeWasUpdated;
    

    void Start()
    {
        _avatar = GetComponent<Alteruna.Avatar>();
        _renderer = GetComponent<SpriteRenderer>();
        _camera = Camera.main;
        _transform = GetComponent<Transform>();
        FindObjectOfType<Score>().AddPlayer(transform);
    }

    void Update()
    {
        // Only let input affect the avatar if it belongs to me
        if (_avatar.IsMe)
        {
            if (sizeWasUpdated)
            {
                transform.localScale = new Vector3(Size, Size, 1);
                sizeWasUpdated = false;
            }
            // Set the avatar representing me to be green
            _renderer.color = Color.green;

            float _translation = Input.GetAxis("Vertical") * Speed;
            float _rotation = -Input.GetAxis("Horizontal") * RotationSpeed;

            _translation *= Time.deltaTime;
            _rotation *= Time.deltaTime;
            
            transform.Translate(0, _translation, 0, Space.Self);
            transform.Rotate(0, 0, _rotation);
            
            // Come on camera, follow me!
            _camera.transform.position = new Vector3(transform.position.x, transform.position.y, -20 * Size);

            
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (_avatar.IsMe)
        {
            if (col.gameObject.CompareTag("Fewd"))
            {
                sizeWasUpdated = true;
                Size += 0.01f;
            }
        }
    }
    
}
