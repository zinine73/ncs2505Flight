using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public UIMgr uiMgr;
    public GameObject explosionEffect;
    public GameObject boosterFlame;
    public GameObject borders;
    public float thrustForce = 1f;
    public float maxSpeed = 5f;
    public AudioClip boosterClip;
    
    Rigidbody2D rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        MovePlayer();
        SetBoosterView();
    }

    void MovePlayer()
    {
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
    }

    void SetBoosterView()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // 부스터 보이기
            boosterFlame.SetActive(true);
            audioSource.PlayOneShot(boosterClip);
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            // 부스터 감추기
            boosterFlame.SetActive(false);
            audioSource.Stop();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        borders.SetActive(false);
        Destroy(gameObject);
        Instantiate(explosionEffect, 
            transform.position, transform.rotation);
        uiMgr.GameOver();
    }
}
