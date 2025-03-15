using UnityEngine;

public class Character : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public SpriteRenderer sp;
    public Animator anim;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
}
