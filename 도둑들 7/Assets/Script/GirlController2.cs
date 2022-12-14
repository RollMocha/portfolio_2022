using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlController2 : MonoBehaviour
{

    public GameManager gameManager;
    public AudioClip audioJump;
    public AudioClip audioAttack;
    public AudioClip audioDamaged;
    public AudioClip audioItem;
    public AudioClip audioDie;
    public AudioClip audioFinish;


    public float Speed;
    [SerializeField]
    public float power;
    [SerializeField]
    Transform pos;
    [SerializeField]
    float checkRadius;
    [SerializeField]
    LayerMask islayer;



    public int jumpPower;





    private float curTime;
    public float coolTime;
    public Vector2 boxSize;
    public Transform po;


    bool isGround;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;
    CircleCollider2D circlecollider;
    AudioSource audioSource;




    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        circlecollider = GetComponent<CircleCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 1f, rigid.velocity.normalized.y);
        }
        if (Input.GetButtonDown("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == 1;


        if (Input.GetButtonDown("Jump") && !animator.GetBool("isJumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
            audioSource.clip = audioJump;
            audioSource.Play();
        }


        if (Mathf.Abs(rigid.velocity.x) < 0.01)
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }
        animator.SetBool("Walk", true);





        if (curTime <= 0)
        {
            //공격
            if (Input.GetKeyDown(KeyCode.Z))
            {

                animator.SetTrigger("atk");
                curTime = coolTime;
            }

            else
            {
                curTime -= Time.deltaTime;
            }
        }

    }



    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");


        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > Speed)
            rigid.velocity = new Vector2(Speed, rigid.velocity.y);

        else if (rigid.velocity.x < Speed * (-1))
            rigid.velocity = new Vector2(Speed * (-1), rigid.velocity.y);

        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

            if (rayHit.collider != null)
            {
                animator.SetBool("isJumping", false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            gameManager.stagePoint += 100;

            collision.gameObject.SetActive(false);
          

            audioSource.clip = audioItem;
            audioSource.Play();
        }

        else if (collision.gameObject.tag == "Finish")
        {
            audioSource.clip = audioFinish;
            audioSource.Play();

            gameManager.stagePoint += 500;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
      

        if (collision.gameObject.tag == "Obstacle")
        {

            //Attack
            if (rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack(collision.transform);
            }
            else //Damaged
                OnDamaged(collision.transform.position);
        }


    }

    void OnAttack(Transform enemy)
    {

        gameManager.stagePoint += 100;

        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();

        enemyMove.OnDamaged();

        audioSource.clip = audioAttack;
        audioSource.Play();
    }

    void OnDamaged(Vector2 targetPos)
    {

        gameManager.HealthDown();

        gameObject.layer = 11;

        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -5;
        rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);

        Invoke("OffDamaged", 2);
    }

    void OffDamaged()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public void OnDie()
    {
        audioSource.clip = audioDie;
        audioSource.Play();

        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        spriteRenderer.flipY = true;
        circlecollider.enabled = false;
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
    }

}