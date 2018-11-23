using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    bool selected;
    NavMeshAgent agent;
    GameObject target;
    public int player;
    Material material;
    Color matColor;
    public float updateDelay = 0.2f;
    float updateTimer;
    Animator animator;
    bool startAttack;
    bool moving;
    bool startFlee;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        material = GetComponent<Renderer>().material;
        matColor = material.color;
        matColor.a = 0.7f;
        material.color = matColor;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            if (Input.GetButton("PrimaryClick"))
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    if (Vector3.Distance(transform.position, hit.point) > 1 && hit.transform.gameObject.tag.Equals("Walkable"))
                    {
                        moving = true;
                        animator.SetBool("attacking", false);
                        agent.speed = 3.5f;
                        agent.destination = hit.point;
                    }
                }
            }
            else if (Input.GetButton("SecondaryClick"))
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    if (hit.transform.gameObject.tag.Equals("Player"))
                    {
                        target = hit.transform.gameObject;
                        if (target.GetComponent<UnitMovement>().player != player)
                        {
                            moving = true;
                            Debug.Log("Target Found...");
                            animator.SetBool("attacking", true);
                            agent.speed = 3.5f;
                            agent.destination = target.transform.position;
                            updateTimer = Time.time + updateDelay;
                        }
                    }
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Chase"))
        {
            if (Time.time > updateTimer)
            {
                if (target != null)
                {
                    agent.destination = target.transform.position;
                    updateTimer = Time.time + updateDelay;
                }
                else
                {
                    animator.SetBool("attacking", false);
                }
                animator.SetFloat("distToTarget", Vector3.Distance(agent.destination, transform.position));
            }
        }
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Move"))
        {
            if (moving)
            {
                if (Vector3.Distance(agent.destination, transform.position) < 2 && agent.velocity.sqrMagnitude < 1)
                {
                    agent.speed = 0;
                    moving = false;
                    Debug.Log("Close Enough");
                }
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            if (!startAttack)
            {
                Debug.Log("Attack Time!");
                agent.destination = transform.position;
                agent.speed = 1.5f;
                startAttack = true;
                transform.GetChild(1).GetComponent<LanceMove>().Attacking = true;
                transform.GetChild(2).GetComponent<LanceMove>().Attacking = true;
            }

        }
        else if (startAttack)
        {
            startAttack = false;
            agent.speed = 3.5f;
            transform.GetChild(1).GetComponent<LanceMove>().Attacking = false;
            transform.GetChild(2).GetComponent<LanceMove>().Attacking = false;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Flee"))
        {
            if (!startFlee)
            {
                if (target != null)
                {
                    agent.destination = (transform.position - target.transform.position);
                    Debug.Log("Fleeing!");
                }
                else
                {
                    animator.SetBool("attacking", false);
                    animator.SetFloat("distToTarget", 5);
                }
                agent.speed = 3.5f;
                startFlee = true;
            }
        }
        else if (startFlee)
        {
            startFlee = false;
        }
    }

    void OnMouseDown()
    {
        selected = !selected;
        if (!selected)
        {
            matColor = material.color;
            matColor.a = 0.7f;
            material.color = matColor;
        }
        else
        {
            matColor = material.color;
            matColor.a = 1;
            material.color = matColor;
        }
        Debug.Log(selected);
    }
}
