using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorBox : MonoBehaviour
{
    public string drumType;
    public InputManager inputs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.up * 0.081f);

        if (transform.position.y > 5)
        {
            Destroy(gameObject);
            inputs.TOTAL_SCORE -= 1;
        }
    }

    public void setInitialValues(string dt)
    {
        inputs = GameObject.FindGameObjectWithTag("Manager").GetComponent<InputManager>();

        drumType = dt;
        if (dt == "Bass")
        {
            this.transform.position = new Vector3(3.5f, -5f);
            this.GetComponent<SpriteRenderer>().color = Color.red;
        } else if (dt == "Snare")
        {
            this.transform.position = new Vector3(4.8f, -5f);
            this.GetComponent<SpriteRenderer>().color = Color.blue;
        } else if (dt == "Hihat")
        {
            this.transform.position = new Vector3(6.1f, -5f);
            this.GetComponent<SpriteRenderer>().color = Color.green;
        } else if (dt == "Crash")
        {
            this.transform.position = new Vector3(7.4f, -5f);
            this.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }
}
