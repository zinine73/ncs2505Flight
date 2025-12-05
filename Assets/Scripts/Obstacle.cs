using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject sePrefab;
    public float minSpeed = 50f;
    public float maxSpeed = 150f;
    public float minSize = 0.5f;
    public float maxSize = 2.0f;
    public float maxSpinSpeed = 10f;
    Rigidbody2D rb;
    Collider2D c2d;

    void Start()
    {
        float randomSize = Random.Range(minSize, maxSize);
        transform.localScale =
            new Vector3(randomSize, randomSize, 1);
        
        Vector2 randomDirection = Random.insideUnitCircle;
        float randomSpeed = Random.Range(minSpeed, maxSpeed) / randomSize;

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(randomDirection * randomSpeed);
        
        float randomTorque = Random.Range(-maxSpinSpeed, maxSpinSpeed);
        rb.AddTorque(randomTorque);

        c2d = GetComponent<Collider2D>();
        c2d.sharedMaterial.bounciness = 1.0f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        float by = c2d.sharedMaterial.bounciness;
        if (by < 2.5f) by += 0.1f;
        c2d.sharedMaterial.bounciness = by;
        Debug.Log($"by:{by}");
        Vector2 cp = collision.GetContact(0).point;
        GameObject se = Instantiate(sePrefab, cp, Quaternion.identity);
        Destroy(se, 1f);
    }
}
