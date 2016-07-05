using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    private int count;
    public Text countText;
    public Text winText;

    Vector3 zeroAc;
    Vector3 curAc;
    float sensH = 10;
    float sensV = 10;
    float smooth = 0.5f;
    float GetAxisH = 0;
    float GetAxisV = 0;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";

        zeroAc = Input.acceleration;
        curAc = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");

        ////Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        //Vector3 movement2 = new Vector3(Input.acceleration.x, Input.acceleration.y, Input.acceleration.z);

        //rb.AddForce(movement2 * speed);

        curAc = Vector3.Lerp(curAc, Input.acceleration - zeroAc, Time.deltaTime / smooth);
        GetAxisV = Mathf.Clamp(curAc.y * sensV, -1, 1);
        GetAxisH = Mathf.Clamp(curAc.x * sensH, -1, 1);
        // now use GetAxisV and GetAxisH instead of Input.GetAxis vertical and horizontal
        // If the horizontal and vertical directions are swapped, swap curAc.y and curAc.x
        // in the above equations. If some axis is going in the wrong direction, invert the
        // signal (use -curAc.x or -curAc.y)

        Vector3 movement = new Vector3(GetAxisH, 0.0f, GetAxisV);
        rb.AddForce(movement * 5);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 10)
        {
            winText.text = "You Win";
        }
    }
}
        //Destroy(other.gameObject);
