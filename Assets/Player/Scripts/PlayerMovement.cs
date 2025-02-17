using System.Security;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Camera cam;
    

    [Header("Controls")]
    private float horizontal;
    private float vertical;
    [SerializeField] private float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }


    private void Update()
    {
        InputDetect();
        FaceMouse();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void InputDetect()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

    }

    private void PlayerMove()
    {
        rb.linearVelocity = new Vector2(horizontal, vertical).normalized * speed;
    }

    private void FaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = cam.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = direction;
    }

}
