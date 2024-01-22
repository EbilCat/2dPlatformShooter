using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float jumpVelocity = 5.0f;
    [SerializeField] private float movementSpeed = 1.0f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Transform facingControl;
    private PlayerData playerData;
    private Vector3 lastFramePos_World = Vector3.zero;


//*====================
//* UNITY
//*====================
    private void Awake()
    {
        this.playerData = this.GetComponent<PlayerData>();
        this.rigidBody = this.GetComponent<Rigidbody2D>();
        this.lastFramePos_World = this.transform.position;
    }

    private void FixedUpdate()
    {
        if (this.playerData.IsLocalPlayer)
        {
            float yVelocity = GetYVelocity();
            float xVelocity = GetXVelocity();

            this.rigidBody.velocity = new Vector2(xVelocity, yVelocity);
        }
        else
        {
            this.rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        ProcessCharacterFacing();
    }


//*====================
//* PRIVATE
//*====================
    private float GetYVelocity()
    {
        float yVelocity = this.rigidBody.velocity.y;
        bool isGrounded = Physics2D.Raycast(this.transform.position, Vector2.down, 0.1f);//, LayerMask.GetMask("Ground"));

        if (isGrounded && this.playerData.IsDead == false)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                yVelocity = jumpVelocity;
                isGrounded = false;
            }
            else
            {
                yVelocity = 0.0f;
            }
        }

        return yVelocity;
    }

    private float GetXVelocity()
    {
        float xVelocity = 0.0f;

        if (this.playerData.IsDead == false)
        {
            xVelocity += (Input.GetKey(KeyCode.D)) ? movementSpeed : 0.0f;
            xVelocity += (Input.GetKey(KeyCode.A)) ? -movementSpeed : 0.0f;
        }
        return xVelocity;
    }

    private void ProcessCharacterFacing()
    {
        float xDelta = this.transform.position.x - lastFramePos_World.x;
        if (Mathf.Abs(xDelta) > 0.05f)
        {
            this.facingControl.localScale = new Vector3((xDelta > 0.0f) ? 1.0f : -1.0f, 1.0f, 1.0f);
            this.lastFramePos_World = this.transform.position;
        }
    }
}
