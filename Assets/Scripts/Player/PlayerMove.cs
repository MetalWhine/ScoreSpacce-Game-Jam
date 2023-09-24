using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField]
    [Tooltip("Sets how fast player moves")]
    private float playerSpeed = 10f;

    private Rigidbody2D rb;
    private Camera playerCam;

    private Vector2 movement;
    private Vector2 mousePosition;

    public void setPlayerSpeed(float speed)
    {
        playerSpeed = speed;
    }

    public float getPlayerSpeed()
    {
        return playerSpeed;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCam = FindObjectOfType<Camera>();
        if(playerCam == null)
        {
            Debug.LogWarning("Player Camera does not exist");
        }
    }

    private Vector2 getPlayerInput()
    {
        float deltaX = Input.GetAxisRaw("Horizontal") * playerSpeed;
        float deltaY = Input.GetAxisRaw("Vertical") * playerSpeed;
        Vector2 resultingForce = new Vector2(deltaX, deltaY);
        resultingForce = Vector2.ClampMagnitude(resultingForce, playerSpeed);
        resultingForce = resultingForce * Time.deltaTime;
        return resultingForce;
    }

    private void followMouseRotatePlayer()
    {
        Vector2 lookDirection = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    private void Update()
    {
        movement = getPlayerInput();
        mousePosition = playerCam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement);
        followMouseRotatePlayer();
    }
}
