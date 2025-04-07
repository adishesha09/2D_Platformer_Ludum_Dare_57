using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    public bool facingLeft = true;
    public float moveSpeed = 2f;
    [SerializeField] public Transform checkpoint;
    public float distance = 1f;
    public LayerMask layerMask;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * (Time.deltaTime * moveSpeed));
        RaycastHit2D hit = Physics2D.Raycast(checkpoint.position, Vector2.down, distance, layerMask);

        if (hit == false && facingLeft)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            facingLeft = false;
        }
        else if (hit == false && facingLeft == false)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}