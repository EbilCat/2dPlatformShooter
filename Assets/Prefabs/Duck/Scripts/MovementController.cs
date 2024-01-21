using Photon.Pun;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float jumpVelocity = 5.0f;
    [SerializeField] private float movementSpeed = 1.0f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Transform facingControl;
    private PlayerData playerData;

    private void Awake()
    {
        this.rigidBody = this.GetComponent<Rigidbody2D>();
        this.playerData = this.GetComponent<PlayerData>();
    }

    [SerializeField] bool isGrounded = true;

    private void FixedUpdate()
    {
        if (this.GetComponent<PhotonView>().IsMine)
        {
            float yVelocity = GetYVelocity();
            float xVelocity = GetXVelocity();

            if (Mathf.Abs(xVelocity) > 0)
            {
                // this.spriteRenderer.flipX = xVelocity > 0.0f;
                this.facingControl.localScale = new Vector3((xVelocity > 0.0f) ? 1.0f: -1.0f, 1.0f, 1.0f);
            }

            this.rigidBody.velocity = new Vector2(xVelocity, yVelocity);
        }
        else
        {
            this.rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    [SerializeField] float yVelocity = 0.0f;

    private float GetYVelocity()
    {
        yVelocity = this.rigidBody.velocity.y;
        bool isGrounded = Physics2D.Raycast(this.transform.position, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));

        if (isGrounded)
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
        xVelocity += (Input.GetKey(KeyCode.D)) ? movementSpeed : 0.0f;
        xVelocity += (Input.GetKey(KeyCode.A)) ? -movementSpeed : 0.0f;
        return xVelocity;
    }
}
