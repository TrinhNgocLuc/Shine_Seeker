using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ControlCharacter : MonoBehaviour
{
    private float speed = 5f;
    private Vector3 movement; 
    private Rigidbody2D rb;
    public int coutDiamond;
    public int coutOxi = 10;
    public int coutOxiMax;
    public Text coutdianmondText;
    public Text coutOxiText;
    public bool isShop = false, conectShop = false, isDig = false, isWin = false, isLose = false;
    private float moveInput;   
    private float moveVertical;
    private AXMovement axMovement;
    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject ax;
    public GameObject lightCharacter;
    public Animator animator;

    private void Awake()
    {
        axMovement = FindAnyObjectByType<AXMovement>();
    }
    void Start()
    {
        coutOxiMax = coutOxi;
        rb = GetComponent<Rigidbody2D>();
        coutdianmondText.text = coutDiamond.ToString();
        coutOxiText.text = coutOxiMax.ToString();
    }

    void Update()
    {
            rb.velocity = new Vector2(moveInput * speed, moveVertical * speed);
            animator = GetComponent<Animator>();
        Debug.Log(gameObject.transform.localScale);
    }

    public void MoveLeft()
    {
            moveInput = -1f;
           //gameObject.transform.localScale = new Vector3(0.35f,0.35f,1f);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public void MoveRight()
    {
            moveInput = 1f;
           //gameObject.transform.localScale = new Vector3(-0.35f, 0.35f, 1f);
        transform.rotation = Quaternion.Euler(0f, -180f, 0f);

    }

    public void MoveUp()
    {
            moveVertical = 1f;
    }

    public void MoveDown()
    {
            moveVertical = -1f;
    }

    public void StopMove()
    {
            moveInput = 0f;
            moveVertical = 0f;
    }

    public void Interact()
    {
        if (!conectShop)
        {
            animator.SetTrigger("Action");
            Debug.Log("Tương tác");
            StartCoroutine(ResetAnimation());
        }
        else
        {
            isShop = !isShop;
            if (isShop == true)
            {
                shop.SetActive(true);
            }
            else if (isShop == false) 
            {
                shop.SetActive(false);
            }
        }
        if (isDig)
        {
            axMovement.DigRock();        
        }
    }
    public void MineIn()
    {
        if (coutOxi > 0 && coutOxi <= coutOxiMax)
        {
            coutOxi--;
            if( coutOxi > 0 && coutOxi < 5)
            {

            }
            coutOxiText.text = coutOxi.ToString();
        }
        
    }

    public void MineOut()
    {
        if (coutOxi > 0 && coutOxi < coutOxiMax)
        {
            coutOxi++;
            coutOxiText.text = coutOxi.ToString();
        }

    }

    private void OnCollisionEnter2D( Collision2D collision)
    {
        if (collision.gameObject.tag == "Gate")
        {
            Animator gateAnimator = collision.gameObject.GetComponent<Animator>();
            BoxCollider2D gateCollider = collision.gameObject.GetComponent<BoxCollider2D>();

            if (gateAnimator != null)
            {
                gateAnimator.SetTrigger("Open");
            }
            if (gateCollider != null)
            {
                gateCollider.isTrigger = true; 
            }
        }
        if (collision.gameObject.tag == "Goal")
        {
            isWin = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Diamond")
        {
            coutDiamond++;
            coutdianmondText.text = coutDiamond.ToString();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Mine" || collision.gameObject.tag == "Abyss")
        {
            lightCharacter.SetActive(true);
            CancelInvoke();
            InvokeRepeating("MineIn", 0.2f,1.5f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Gate")
        {
            Animator gateAnimator = collision.gameObject.GetComponent<Animator>();
            BoxCollider2D gateCollider = collision.gameObject.GetComponent<BoxCollider2D>();
            gateAnimator.SetTrigger("Close");
            if (gateCollider != null)
            {
                gateCollider.isTrigger = false;
            }
        }
        if (collision.gameObject.tag == "Mine")
        {
            lightCharacter.SetActive(false);
            CancelInvoke();
            InvokeRepeating("MineOut", 0f, 0.2f);

        }
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    private IEnumerator ResetAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.ResetTrigger("Action");
    }
   
}