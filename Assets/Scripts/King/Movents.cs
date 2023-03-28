using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movents : MonoBehaviour {
    // movent
    float directionX;
    float walkSpeed = 6f;

    // ground
    bool isGround;
    float ratioFoot = .03f;

    // jump
    bool canJump = true;
    float jumpValue = .0f;

    // components player
    Rigidbody2D rb;

    public Transform foot;
    public LayerMask groundMask;
    public PhysicsMaterial2D player_bounce, player_mat;

    void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>(); // get component from gameObject of player
    }

    void Update() {
        directionX = Input.GetAxis("Horizontal");

        isGround = Physics2D.OverlapCircle(foot.position, ratioFoot, groundMask);

        if (jumpValue == .0f && isGround) rb.velocity = new Vector2(directionX * walkSpeed, rb.velocity.y);

        if (jumpValue > 0) {
            rb.sharedMaterial = player_bounce;
        } else {
            rb.sharedMaterial = player_mat;
        }

        if (Input.GetButton("Jump") && isGround && canJump) {
            jumpValue += .25f;
        }

        if (Input.GetButtonDown("Jump") && isGround && canJump) {
            rb.velocity = new Vector2(.0f, rb.velocity.y);
        }

        if (jumpValue >= 23f && isGround) {
            float tempX = directionX * walkSpeed;
            float tempY = jumpValue;

            rb.velocity = new Vector2(tempX, tempY);

            Invoke("ResetJump", .2f);
        }

        if (Input.GetButtonUp("Jump")) {
            if (isGround) {
                rb.velocity = new Vector2(directionX * walkSpeed, jumpValue);
                jumpValue = .0f;
            }
            canJump = true;
        }
    }

    private void ResetJump() {
        canJump = false;
        jumpValue = 0;
    }
}