using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class betterJump : MonoBehaviour
{
    public float fallMultiplier;
    public float lowJumpMultiplier;

    Rigidbody2D rb;

    private void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate(){
        if (rb.velocity.y < 0) rb.gravityScale = fallMultiplier;
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) rb.gravityScale = lowJumpMultiplier;
        else rb.gravityScale = 5.5f;
    }

}
