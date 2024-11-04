using UnityEngine;

public class SawTrap : MonoBehaviour
{
    [SerializeField] private Transform waypoint1; 
    [SerializeField] private Transform waypoint2; 
    [SerializeField] private float speed = 2f; 

    private Transform targetWaypoint; 

    private void Start()
    {
        targetWaypoint = waypoint1;
        transform.position = waypoint1.position;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            targetWaypoint = targetWaypoint == waypoint1 ? waypoint2 : waypoint1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerDeathHandler>()?.TriggerDeath();
            Debug.Log("Player hit by the saw and killed");
        }
    }
}
