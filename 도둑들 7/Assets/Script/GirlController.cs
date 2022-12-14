using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GirlController : MonoBehaviour
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

    float h;



    CircleCollider2D circlecollider;
    public float jumpPower = 5.0f;

    int MaxJumpCount = 2;
    int currJumpCount = 0;

    public bool isJump;
 



    private float curTime;
    public float coolTime;
    public Vector2 boxSize;
    public Transform po;


    bool isGround;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;
    AudioSource audioSource;

    int left_Value;
    int right_Value;
    int jump_Value;
    bool left_Down;
    bool right_Down;
    bool jump_Down;
    bool jump_Up;
    bool left_Up;
    bool right_Up;



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
        h = Input.GetAxisRaw("Horizontal") + right_Value + left_Value;
        

        bool hDown = Input.GetButtonDown("Horizontal") || right_Down || left_Down;
        bool hUp = Input.GetButtonDown("Horizontal") || right_Up || left_Up;

        


        //이동
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 1f, rigid.velocity.normalized.y);
        }
        if (Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == 1;


        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && currJumpCount < MaxJumpCount && isJump == false)
        {
            {
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                currJumpCount++;
                audioSource.clip = audioJump;
                audioSource.Play();
            }

            if (currJumpCount == MaxJumpCount)
            {
                isJump = true;
            }
        }



        //Walking

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


        left_Down = false;
        left_Up = false;
        right_Down = false;
        right_Down = false;
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
        if(collision.gameObject.tag == "Item")
        {
            gameManager.stagePoint += 100;

          

            collision.gameObject.SetActive(false);

            audioSource.clip = audioItem;
            audioSource.Play();
        }

        else if(collision.gameObject.tag == "Finish")
        {
            audioSource.clip = audioFinish;
            audioSource.Play();

            gameManager.stagePoint += 500;
        }


        if (collision.gameObject.tag == "Heart")
        {
            gameManager.stagePoint += 300;



            collision.gameObject.SetActive(false);

            audioSource.clip = audioItem;
            audioSource.Play();
        }


        if (collision.gameObject.tag == "Ring")
        {
            gameManager.stagePoint += 500;



            collision.gameObject.SetActive(false);

            audioSource.clip = audioItem;
            audioSource.Play();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
           
            //Attack
            if (rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack(collision.transform);
       


            }
            else //Damaged
             OnDamaged(collision.transform.position);
        }

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

        if (collision.collider.CompareTag("Land"))
        {
            isJump = false;
            currJumpCount = 0;
        }

        else
        {
            return;
        }
    }


    void OnAttack(Transform enemy) //enemy 공격
    {

        gameManager.stagePoint += 100;

        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();

        enemyMove.OnDamaged();

        audioSource.clip = audioAttack;
        audioSource.Play();
    }

    void OnDamaged(Vector2 targetPos) //Damage를 받음
    {

        gameManager.HealthDown();
       
        gameObject.layer = 11;

        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -5;
        rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);

        Invoke("OffDamaged", 2);

        audioSource.clip = audioDamaged;
        audioSource.Play();
    }

    void OffDamaged() // 무적상태
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);

       
    }

    public void OnDie() //Die
    {
        audioSource.clip = audioDie;
        audioSource.Play();
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        spriteRenderer.flipY = true;
        circlecollider.enabled = false;
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        
    }


    public void ButtonDown(string type)
    {
        switch (type)
        {
            case "L":
                left_Value = -1;
                left_Down = true;
                break;
            case "R":
                right_Value = 1;
                right_Down = true;
                break;
            case "SpaceBar":
                break;
        }
    }

    public void ButtonUp(string type)
    {
        switch (type)
        {
            case "L":
                left_Value = 0;
                left_Up = true;
                break;
            case "R":
                right_Value = 0;
                right_Up = true;
                break;
            case "SpaceBar":
                break;
        }
    }
    public void Jump()
    {
        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && currJumpCount < MaxJumpCount && isJump == false)
        {
            {
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                currJumpCount++;
                audioSource.clip = audioJump;
                audioSource.Play();
            }

            if (currJumpCount == MaxJumpCount)
            {
                isJump = true;
            }
        }
    }
}
