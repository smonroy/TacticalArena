using UnityEngine;
using UnityEngine.AI;

public class LanceMove : MonoBehaviour
{
    bool attacking;
    bool movingForward = true;
    Vector3 dest;
    public float lanceStart = 0, lanceEnd = 0.7f;

    public bool Attacking
    {
        get
        {
            return attacking;
        }

        set
        {
            attacking = value;
            if (attacking)
            {
                dest = new Vector3(transform.localPosition.x, transform.localPosition.y, lanceEnd);
            }
        }
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Attacking)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, dest, Time.deltaTime);
            if (Vector3.Distance(transform.localPosition, dest) < 0.05f)
            {
                movingForward = !movingForward;
                if (movingForward)
                {
                    dest = new Vector3(transform.localPosition.x, transform.localPosition.y, lanceEnd);
                }
                else
                {
                    dest = new Vector3(transform.localPosition.x, transform.localPosition.y, lanceStart);
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (Attacking)
        {
            Debug.Log(other.tag);
            if (other.tag.Equals("Player"))
            {
                UnitProperties eProps = other.GetComponent<UnitProperties>() ?? other.transform.parent.GetComponent<UnitProperties>();
                if (!eProps.invincible)
                {
                    eProps.Health--;
                    if (eProps != null)
                    {
                        Debug.Log("Enemy Health: " + eProps.Health);
                        transform.parent.GetComponent<Animator>().SetTrigger("Flee");
                    }
                    else {
                        Debug.Log("Enemy Health: 0");
                        transform.parent.GetComponent<Animator>().SetBool("attacking", false);
                    }
                }
            }
        }
    }
}
