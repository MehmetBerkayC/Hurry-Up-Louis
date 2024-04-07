using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 _playerInput;

    [SerializeField] private float movementSpeed = 7f;

    private Rigidbody2D _rb;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _playerInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized;

        _rb.velocity = _playerInput * movementSpeed;
        
    }

    public void StopPlayerMovement()
    {
        _rb.velocity = Vector3.zero;
    }

    public float GetPlayerSpeed() => movementSpeed;
    public void SetPlayerSpeed(float speed) => movementSpeed = speed;
    
}
