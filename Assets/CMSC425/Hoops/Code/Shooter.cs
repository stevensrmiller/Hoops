using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Vector3 direction = new Vector3(0, 1, 1);
    public float forceRate = 1;
    public float waitTime = 3;

    Rigidbody rb;
    Vector3 startPos;
    bool shotPending;
    bool mouseIsDown;
    float force;
    WaitForSeconds waitABit;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        waitABit = new WaitForSeconds(waitTime);
        startPos = transform.position;
    }

    void Update()
    {
        if (mouseIsDown)
        {
            force += Time.deltaTime * forceRate;
        }
    }

    private void FixedUpdate()
    {
        if (shotPending)
        {
            rb.isKinematic = false;
            rb.AddRelativeForce(force * direction, ForceMode.Impulse);
            shotPending = false;
            force = 0;
            StartCoroutine(Reset());
        }
    }
    private void OnMouseDown()
    {
        if (!shotPending)
        {
            mouseIsDown = true;
        }
    }

    private void OnMouseUp()
    {
        mouseIsDown = false;
        shotPending = true;
    }

    IEnumerator Reset()
    {
        yield return waitABit;

        rb.isKinematic = true;
        rb.transform.position = startPos;
        rb.transform.rotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
    }
}
