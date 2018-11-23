using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Camera cam;
    float h, v;
    float zoom;
    public float speed = 1;
    public float zoomSpeed = 0.1f;
    float mult = 1;
    public float xLimit = 25.5f;
    public float yLimit = 25;

    // Use this for initialization
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("FastScroll"))
        {
            mult = 2;
        }
        else
        {
            mult = 1;
        }
        h = Input.GetAxis("Horizontal") * Time.deltaTime * speed * mult;
        v = Input.GetAxis("Vertical") * Time.deltaTime * speed * mult;
        zoom = -Input.mouseScrollDelta.y * zoomSpeed;
        Vector3 oldPos = cam.transform.position;
        Vector3 temp = cam.transform.position + new Vector3(h, zoom, v);

        if (temp.z < yLimit && temp.z > -yLimit && temp.x < xLimit && temp.x > -xLimit && temp.y > 5)
        {
            cam.transform.position = temp;
        }
    }
}
