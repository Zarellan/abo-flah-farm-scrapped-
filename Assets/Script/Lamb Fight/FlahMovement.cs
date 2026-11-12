using UnityEngine;
using PrimeTween;
public class FlahMovement : MonoBehaviour
{
    public static FlahMovement instance;
    Rigidbody2D rb;
    Vector2 scaleLocal;
    [SerializeField] float moveSpeed, jumpPower, slideStrength, gravityDefault, gravityFall;
    float dashSpeed = 1;
    [SerializeField] float minX, maxX, minY, maxY;
    public GameObject pickedItem;
    bool jumped = false; public bool Jumped { get{ return jumped; }set{ jumped = value; }}
    bool canDash = true; public bool CanDash { get{ return canDash; }set{ canDash = value; }}
    Tween coyoteJump;
    Tween jumpX, jumpY;
    Tween cameraScale;
    Animator anim;
    
    [SerializeField] float maxAcceleration, accelerationRate;
    Tween hoeTween;
    Tween hoeTween2;
    Tween aboFlah;
    Tween aboFlah2;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        scaleLocal = transform.localScale;
        if (instance == null)
            instance = this;
    }
    void Start()
    {
        hoeTween = new Tween();
        hoeTween2 = new Tween();
        aboFlah = new Tween();
        aboFlah2 = new Tween();
        //Tween.PositionY(gameObject.transform,gameObject.transform.position.y + 2,1,Ease.OutCirc);
    }

    void Update()
    {
        if (MinecraftInventory.instance.itemOwned.itemType == "weapon" && Input.GetMouseButtonDown(0))
        {
            ItemAction.ItemAct(MinecraftInventory.instance.itemOwned);
        }

        if (MinecraftInventory.instance.itemOwned.itemType == "consumeable"&& Input.GetMouseButtonDown(1))
        {
            ItemAction.ItemAct(MinecraftInventory.instance.itemOwned);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !jumped)
        {
            rb.gravityScale = gravityDefault;
            rb.linearVelocityY = jumpPower;
            jumped = true;
            coyoteJump.Stop();
            jumpX = Tween.ScaleX(transform, scaleLocal.x - 0.15f, scaleLocal.x, 0.4f, Ease.OutCirc);
            jumpY = Tween.ScaleY(transform, scaleLocal.y + 0.15f, scaleLocal.y, 0.4f, Ease.OutCirc);

            HungerSystem.instance.SaturationConsume(5);
        }
        else if (Input.GetKeyUp(KeyCode.Space) && rb.linearVelocityY > 0.00001)
        {
            rb.gravityScale = gravityFall;
        }

        if (Mathf.Abs(rb.linearVelocity.y) < 0.001f)
        {
            rb.gravityScale = gravityDefault;
            jumped = false;
            canDash = true;
        }

        if (!jumped && Mathf.Abs(rb.linearVelocity.y) > 0.001f)
        {
            coyoteJump = Tween.Delay(0.15f).OnComplete(() =>
            {
                jumped = true;
            });
        }
        DamageCoolDown();
        MouseFollow();

        if (canDash && jumped && Input.GetKeyDown(KeyCode.LeftShift))
        {
            rb.gravityScale = gravityDefault * 2f;
            rb.linearVelocityY = jumpPower / 1.5f;
            dashSpeed = 3;
            canDash = false;
            //Tween.Delay(0.4f).OnComplete(() => { dashSpeed = 1; });
                Tween.Custom(3, 1, duration: 0.4f, onValueChange: newVal => {
					dashSpeed = newVal;
				},Ease.Linear);
            HungerSystem.instance.SaturationConsume(7);
        }
    }
    bool hitted = false;
    public void BreakingBlock()
    {
        if (!hitted)
        {
            hitted = true;
            hoeTween.Stop();
            hoeTween2.Stop();
            hoeTween = Tween.Rotation(pickedItem.transform, new Vector3(0, 0, -130), 0.05f, Ease.Linear).OnComplete(()=>HoeTween2(0.1f));
            aboFlah.Stop();
            aboFlah2.Stop();
            aboFlah = Tween.ScaleY(transform, 0.33f, 0.05f, Ease.Linear).OnComplete(()=>FlahTween2(0.1f));
        }
    }
    public void Eating(int num)
    {
        print("eat: "+num);
    }
    public void Attack()
    {
        hoeTween.Stop();
        hoeTween2.Stop();
        hoeTween = Tween.Rotation(pickedItem.transform, new Vector3(0, 0, -130), 0.05f, Ease.Linear).OnComplete(() => HoeTween2());
        aboFlah.Stop();
        aboFlah2.Stop();
        aboFlah = Tween.ScaleY(transform, 0.33f, 0.05f, Ease.Linear).OnComplete(() => FlahTween2());
    }
    void MouseFollow()
    {
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            HoePos(new Vector2(-1.74f,0.93f), true);
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
            HoePos(new Vector2(1.74f,0.93f), false);
        }
    }
    void HoePos(Vector2 pos, bool flip)
    {
        pickedItem.transform.localPosition = pos;
        pickedItem.transform.localRotation = Quaternion.Euler(0,flip?-180:0,pickedItem.transform.eulerAngles.z);        
    }
    void HoeTween2(float duration = 0.3f)
    {
        hoeTween2 = Tween.Rotation(pickedItem.transform,new Vector3(0,0,-60.6f),duration,Ease.OutCirc);
    }
    void FlahTween2(float duration = 0.3f)
    {
        aboFlah2 = Tween.ScaleY(transform,0.38f,duration,Ease.OutCirc).OnComplete(()=>hitted = false);
    }

    void FixedUpdate()
    {
        MovementFixed();
        CamFollow();
    }
    float satureTime = 0.10f;
    void MovementFixed()
    {
        Vector2 movement = transform.position;
        movement.x += Input.GetAxisRaw("Horizontal") * moveSpeed * dashSpeed * Time.fixedDeltaTime;
        transform.position = movement;

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (satureTime > 0.0f)
                satureTime -= Time.fixedDeltaTime;
            else
            {
                HungerSystem.instance.SaturationConsume(1);
                satureTime = 0.10f;
            }
            //print(HungerSystem.instance.saturation);
        }
    }
    float damageCoolDown;
    public bool cooldownDamageState;
    void DamageCoolDown()
    {
        if (cooldownDamageState)
        {
            damageCoolDown -= Time.deltaTime;
            if (damageCoolDown < 0)
            {
                GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
                cooldownDamageState = false;
            }
        }
    }


    string animName;
    public void ChangeAnimation(string currentAnim)
    {
        if (animName == currentAnim)
            return;

        animName = currentAnim;

        anim.Play(currentAnim);
    }
    Vector3 velocity = Vector3.zero;
    void CamFollow()
    {
        Vector3 target = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX) + 0.5f, Mathf.Clamp(transform.position.y, minY, maxY) + 0.5f, -10);

        Vector3 mCam = GameObject.FindWithTag("MainCamera").transform.position;

        GameObject.FindWithTag("MainCamera").transform.position = Vector3.SmoothDamp(mCam, target, ref velocity, 0.25f);

    }

    public void ForceJump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.gravityScale = gravityDefault;
            rb.linearVelocityY = jumpPower;
        }
        else
        {
            rb.gravityScale = gravityFall;
            rb.linearVelocityY = jumpPower;
        }
        jumped = true;
        coyoteJump.Stop();
        jumpX = Tween.ScaleX(transform, scaleLocal.x - 0.15f, scaleLocal.x, 0.4f, Ease.OutCirc);
        jumpY = Tween.ScaleY(transform, scaleLocal.y + 0.15f, scaleLocal.y, 0.4f, Ease.OutCirc);

    }
    public void Damaged(float delta)
    {
        //HealthSystem.instance.Damage();
        damageCoolDown = 3;
        cooldownDamageState = true;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        if (delta > 0.01f)
        {
            rb.linearVelocity = new Vector2(-4, 4);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (delta < -0.01f)
        {
            rb.linearVelocity = new Vector2(4, 4);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        HungerSystem.instance.SaturationConsume(15);
        jumped = true;
    }
}
