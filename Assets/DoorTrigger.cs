using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorTrigger : MonoBehaviour
{
    public Text PressE;
    private bool checkTrigger, isOpen, isClose;
    private float timer, timer2;
    public BoxCollider parentCollider;
    public Animation parentAnim;

    // Start is called before the first frame update
    void Start()
    {
        checkTrigger = false;
        isOpen = false;
        isClose = false;
        timer = 0;
        timer2 = 0;
        parentAnim = gameObject.GetComponentInParent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (checkTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isOpen = true;
                isClose = false;
                timer = 0;
                parentCollider.enabled = false;
                parentAnim.Play("Door_Open");
            }
        }
        else
        {
            timer += Time.deltaTime;
            if (isOpen && timer > 1.5f)
            {
                parentAnim.Play("Door_Close");
                isOpen = false;
                isClose = true;
            }
            if (isClose)
            {
                timer2 += Time.deltaTime;
                if (timer2 > 2f)
                {
                    parentCollider.enabled = true;
                    timer2 = 0;
                    isClose = false;
                }
            }

            if (!isOpen)
            {
                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            checkTrigger = true;
            PressE.text = "Press E";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            checkTrigger = false;
            PressE.text = "";
        }
    }
}
