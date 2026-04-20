using UnityEngine;

public class MapBoundary : MonoBehaviour
{
    public static MapBoundary Instance { get; private set; }

    [SerializeField] private Vector2 mapSize = new Vector2(20f, 15f);

    public Bounds Bounds => new Bounds(transform.position, new Vector3(mapSize.x, mapSize.y, 0f));

    void Awake()
    {
        Instance = this;
    }

    public Vector2 Clamp(Vector2 position)
    {
        Bounds b = Bounds;
        position.x = Mathf.Clamp(position.x, b.min.x, b.max.x);
        position.y = Mathf.Clamp(position.y, b.min.y, b.max.y);
        return position;
    }

    public bool IsInBounds(Vector2 position)
    {
        return Bounds.Contains(new Vector3(position.x, position.y, 0f));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(mapSize.x, mapSize.y, 0f));
    }
}
