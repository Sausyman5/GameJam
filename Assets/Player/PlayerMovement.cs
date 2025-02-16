using System.Security;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Controls")]
    private float horizontal;
    private float vertical;
    [SerializeField] private float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        InputDetect();
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
}
