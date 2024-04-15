using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehaviour : MonoBehaviour
{

    [SerializeField] DoorBehaviour doorBehaviour;

    [SerializeField] bool isDoorOpenSwitch;
    [SerializeField] bool isDoorClosedSwitch;

    float switchSizeY;
    Vector3 switchUpPos;
    Vector3 switchDownPos;
    float switchSpeed = 2.0f;
    float switchDelay = 0.2f;
    bool isPressingSwitch = false;
  
    // Start is called before the first frame update
    void Awake()
    {
        switchSizeY = transform.localScale.y / 2;

        switchUpPos = transform.position;
        switchDownPos = new Vector3(transform.position.x, transform.position.y - switchSizeY, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressingSwitch)
        {
            MoveSwitchDown();
            Debug.Log("Switch is pressed");
        }
        else if (!isPressingSwitch)
        {
            MoveSwitchUp();
        }
    }

    void MoveSwitchDown()
    {
        if (transform.position != switchDownPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, switchDownPos, switchSpeed * Time.deltaTime);
        }
    }

    void MoveSwitchUp()
    {
        if (transform.position != switchUpPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, switchUpPos, switchSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPressingSwitch = !isPressingSwitch;
            if (isDoorOpenSwitch && !doorBehaviour.isDoorOpen)
            {
                doorBehaviour.isDoorOpen = !doorBehaviour.isDoorOpen;
            }
            else if (isDoorClosedSwitch && doorBehaviour.isDoorOpen)
            {
                doorBehaviour.isDoorOpen = !doorBehaviour.isDoorOpen;
            }

        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(SwitchUpDelay(switchDelay));
        }
    }

    IEnumerator SwitchUpDelay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isPressingSwitch = false;
    }
}
