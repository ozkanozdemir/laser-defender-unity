using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float paddingLeft;
    [SerializeField] private float paddingRight;
    [SerializeField] private float paddingTop;
    [SerializeField] private float paddingBottom;
    
    private Vector2 _rawInput;
    private Vector2 _minBounds;
    private Vector2 _maxBounds;
    private Shooter _shooter;

    private void Awake()
    {
        _shooter = GetComponent<Shooter>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        InitBounds();
    }

    private void InitBounds()
    {
        Camera mainCamera = Camera.main;
        _minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        _maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    // Update is called once per frame
    private void Update()
    {
        Move(); 
    }

    private void Move()
    {
        Vector3 delta = _rawInput * Time.deltaTime * moveSpeed;
        Vector2 newPosition = new Vector2();

        newPosition.x = Mathf.Clamp(transform.position.x + delta.x, _minBounds.x + paddingLeft, _maxBounds.x - paddingRight);
        newPosition.y = Mathf.Clamp(transform.position.y + delta.y, _minBounds.y + paddingBottom, _maxBounds.y - paddingTop);
        
        transform.position = newPosition;
    }

    private void OnMove(InputValue value)
    {
        _rawInput = value.Get<Vector2>();
    }
    
    private void OnFire(InputValue value)
    {
        if (_shooter != null)
        {
            _shooter.isFiring = value.isPressed;
        }
    }
}
