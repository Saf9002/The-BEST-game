using UnityEngine;

public class PlayerMoveFallGuys : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;
    Vector3 startPosition;
    Animator anim; //Добавляем ссылку на аниматор
    Rigidbody rb;
    Vector3 direction;
    bool isGrounded;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>(); //Присваем компонент Animator

        startPosition = transform.position;
    }
    void Update()
    {
        if (transform.position.y<-20)
        {
            rb.velocity =Vector3.zero;
            transform.position = startPosition;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        direction = transform.TransformDirection(x, 0, z);

        //меняем анимацию персонажа
        if (direction.magnitude > 0)
        {
            anim.SetBool("Run", true);
        }
        else anim.SetBool("Run", false);
        //

        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode.Impulse);
            }
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionStay(Collision other)
    {
        if (other != null)
        {
            isGrounded = true;
            anim.SetBool("Jump", false); //Отключаем анимацию прыжка
        }
    }
    private void OnCollisionExit(Collision other)
    {
        isGrounded = false;
        anim.SetBool("Jump", true); //Включаем анимацию прыжка
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("plate"))
        {
            Destroy(other.gameObject);
        }
        if (other.CompareTag("CheckPoint"))
        {
            startPosition = transform.position;
        }
    }
}