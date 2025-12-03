using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject boosterFlame;
    public float thrustForce = 1f;
    public float maxSpeed = 5f;
    public float scoreMultiplier = 10f;
    float score = 0f;
    float elapsedTime = 0f;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        score = Mathf.FloorToInt(elapsedTime * scoreMultiplier);
        Debug.Log("Score : " + score);

        // 마우스 왼쪽 버튼이 지금 눌렸느냐 판단
        if (Mouse.current.leftButton.isPressed) 
        {
            // 마우스 방향 정하기
            Vector3 mousePos = Camera.main.
                ScreenToWorldPoint(Mouse.current.position.value);
            Vector2 direction = 
                (mousePos - transform.position).normalized;
            
            // 마우스 방향으로 플레이어 움직이기
            transform.up = direction;
            rb.AddForce(direction * thrustForce);

            if (rb.linearVelocity.magnitude > maxSpeed) 
            {
                rb.linearVelocity = 
                    rb.linearVelocity.normalized * maxSpeed;
            }
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // 부스터 보이기
            boosterFlame.SetActive(true);
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            // 부스터 감추기
            boosterFlame.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
