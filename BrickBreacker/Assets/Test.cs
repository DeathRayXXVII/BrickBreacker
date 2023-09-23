using UnityEngine;
using Random = UnityEngine.Random;

public class Test: MonoBehaviour
{
    public Rigidbody rb;
    private Vector3 direction;
    private float speed = 1000f;
    public float maxSpeed;
    public vector3Data vector3DataObj;
    private bool goingLeft;
    private bool goingDown;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.velocity = direction * (speed * Time.deltaTime);

        if(transform.position.x > 16.5 && !goingLeft)
        {
            direction = new Vector3(-direction.x, direction.y);
            goingLeft = true;
        }
        if(transform.position.x < -21.5 && goingLeft)
        {
            direction = new Vector3(-direction.x, direction.y);
            goingLeft = false;
        }
        if(transform.position.y > 9 && !goingDown)
        {
            direction = new Vector3(direction.x, -direction.y);
            goingDown = true;
        }
    }

    void Start()
    {
        ResetBall();
    }

    public void ResetBall()
    {
        transform.position = vector3DataObj.value;
        rb.velocity = Vector3.zero;
        Invoke(nameof(SetRandomTrajectory), 1f);
    }
    public void SetDirection (Vector3 target)
    {
        Vector3 dir = transform.position - target;
        dir.Normalize();

        direction = dir;

        //speed += manager.ballSpeedIncrement;    //Increases the speed of the ball by the GameManager's 'ballSpeedIncrement' value

        if(speed > maxSpeed)
            speed = maxSpeed;					

        if(dir.x > 0)
            goingLeft = false;
        if(dir.x < 0)
            goingLeft = true;	
        if(dir.y > 0)							
            goingDown = false;
        if(dir.y < 0)							
            goingDown = true;
    }
    
    private void SetRandomTrajectory()
    {
        Vector3 force = Vector3.zero;
        force.x = Random.Range(-1f, 1f);
        force.y = -1f;
        rb.AddForce(force.normalized * speed);
    }
}