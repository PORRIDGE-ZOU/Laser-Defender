using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    Vector2 rawInput;
    [SerializeField] float moveSpeed;

    Vector2 minBound;
    Vector2 maxBound;

    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;

    Shooter shooter;

    private void Awake()
    {
        shooter = GetComponent<Shooter>();
    }


    private void Start()
    {
        InitBounds();
    }

    void Update()
    {
        Move();

    }

    private void Move()
    {
        Vector3 movement = moveSpeed * rawInput * Time.deltaTime;
        Vector2 newPosition = new Vector2();
        newPosition.x = Mathf.Clamp(transform.position.x + movement.x,
                                    minBound.x + paddingLeft,
                                    maxBound.x - paddingRight);
        newPosition.y = Mathf.Clamp(transform.position.y + movement.y,
                                    minBound.y + paddingBottom,
                                    maxBound.y - paddingTop);
        transform.position = newPosition;
    }

    void OnMove(InputValue value)
    {

        rawInput = value.Get<Vector2>();
        Debug.Log(rawInput);

    }

    void OnFire(InputValue value)
    {
        
        if(shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }

    }

    void InitBounds()
    {

        Camera mainCamera = Camera.main;
        minBound = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBound = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));

    }
}
