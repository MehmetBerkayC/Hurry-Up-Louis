using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerController controller;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;

    private float animationTime=0.25f;
    private int animationFrame=0;


    private Vector3 inputDirection;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        InvokeRepeating(nameof(SetAnimation), animationTime, animationTime);
    }

    private void Update()
    {
        inputDirection = controller._playerInput;
        SetDirection();
    }

    private void SetAnimation()
    {
        animationFrame++;

        if(animationFrame >= sprites.Length)
        {
            animationFrame = 0;
        }
        if(animationFrame >=0 && animationFrame < sprites.Length)
        {
            spriteRenderer.sprite = sprites[animationFrame];
        }
    }

    private void SetDirection()
    {
        if (inputDirection.x == 0 && inputDirection.y > 0) transform.rotation = Quaternion.Euler(0, 0, 180f); //Up
        else if (inputDirection.x == 0 && inputDirection.y < 0) transform.rotation = Quaternion.Euler(0, 0, 0); //Down
        else if (inputDirection.x > 0 && inputDirection.y == 0) transform.rotation = Quaternion.Euler(0, 0, 90f); //Right
        else if (inputDirection.x < 0 && inputDirection.y == 0) transform.rotation = Quaternion.Euler(0, 0, -90f); //Left
        else if (inputDirection.x > 0 && inputDirection.y > 0) transform.rotation = Quaternion.Euler(0, 0, 135f); //UpRight
        else if (inputDirection.x < 0 && inputDirection.y > 0) transform.rotation = Quaternion.Euler(0, 0, -135f); //UpLeft
        else if (inputDirection.x > 0 && inputDirection.y < 0) transform.rotation = Quaternion.Euler(0, 0, 45f); //DownRight
        else if (inputDirection.x < 0 && inputDirection.y < 0) transform.rotation = Quaternion.Euler(0, 0, -45f); //DownLeft
    }

    //private void SetColliderDirection(colliderDirection direction)
    //{
    //    switch (direction)
    //    {
    //        case colliderDirection.Horizontal:
    //            capsuleCollider.direction = CapsuleDirection2D.Horizontal;
    //            capsuleCollider.offset = new Vector2(0, -0.01f);
    //            capsuleCollider.size = new Vector2(1f, 0.41f);
    //            break;

    //        case colliderDirection.Vertical:
    //            capsuleCollider.direction = CapsuleDirection2D.Vertical;
    //            capsuleCollider.offset = new Vector2(0.05f, 0);
    //            capsuleCollider.size = new Vector2(0.41f, 1f);
    //            break;
    //    }
    //}
}
