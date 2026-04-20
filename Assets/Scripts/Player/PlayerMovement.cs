using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // [SerializeField] private Rigidbody2D rb;

    public float moveSpeed = 5f;
    public Vector2 direction = Vector2.zero;
    public Vector2 targetPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            targetPosition = MapBoundary.Instance != null
                ? MapBoundary.Instance.Clamp(worldPos)
                : (Vector2)worldPos;
            Debug.Log("Target Position: " + targetPosition);
        }
    }

    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f || targetPosition == Vector2.zero)
        {
            transform.position = targetPosition;
            return;
        } else if (targetPosition != Vector2.zero)
        {
            direction = (targetPosition - (Vector2)transform.position).normalized;
            transform.position += (Vector3)(direction * moveSpeed * Time.fixedDeltaTime);
            if (MapBoundary.Instance != null)
                transform.position = (Vector3)MapBoundary.Instance.Clamp(transform.position);
        }

    }

}
