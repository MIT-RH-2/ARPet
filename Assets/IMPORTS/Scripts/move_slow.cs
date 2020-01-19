using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_slow : MonoBehaviour
{
    public float speed; // speed of object
    public float distanceBetween; // distance between temporary target and cat object
    public int numOfStages;
    //public bool do_i_move; // check to see if cat needs to move or not
    public Transform temporary;
    public QRParentHandler qrParent;

    //private bool tempLoaded; // did temp loade
    private bool catLoaded;
    GameObject[] tempArray;

    int stage; // where cat is currently
    public bool command_made = false; // wait for command

    ///ANIMATIONS
    public Animator anim;

    ///ANIMATIONS
    GameObject[] poiArray;

    void Start()
    {
        ////Access Animator component
        anim = GetComponent<Animator>();

        stage = 0;
        distanceBetween = 0.1f;
        catLoaded = false;

        //AnimationLibrary = GetComponent<Animate>();
        poiArray = new GameObject[5];

        poiArray[0] = qrParent.p1;
        poiArray[1] = qrParent.p1Kitchen;
        poiArray[2] = qrParent.p1MensBathroom;
        poiArray[3] = qrParent.p2;
        poiArray[4] = qrParent.p2WomensBathroom;
    }
    //FUNCTION FOR ANIMATIONS
    void CatActions(int option) // Function to call cat actions
    {
        if (option == 0)
        {
            anim.SetInteger("toWalk", 0);
            anim.SetInteger("noRunning", 1);
            anim.SetInteger("isRunning", 0);
            anim.SetInteger("noWalk", 1);
            Debug.Log("idle is being executed");
        }

        if (option == 1)
        {
            anim.SetInteger("toWalk", 1);
            anim.SetInteger("noRunning", 1);
            anim.SetInteger("isRunning", 0);
            anim.SetInteger("noWalk", 0);
            Debug.Log("walking is being executed");
        }
        else if (option == 2)
        {
            anim.SetInteger("isRunning", 1);
            anim.SetInteger("noWalk", 1);
            anim.SetInteger("toWalk", 0);
            anim.SetInteger("noRunning", 0);
            Debug.Log("option 1 is being executed");


        }
    }

    // Update is called once per frame
    void Update()
    {
        // first thing to happen
        if (poiArray[stage] != null)
        {
            Debug.Log("poiarray setup");

            // check if cat object has been created
            if (transform != null && !catLoaded)
            {
                transform.position = new Vector3(Random.Range(0, 3), 0.0f, Random.Range(-2, 0)); // initial position of cat
                catLoaded = true;
                Debug.Log("cat loaded");
            }

            if (!command_made) // if no command has been made
            {
                transform.LookAt(Camera.main.transform);
                Debug.Log("cat is following the user");
                // this will make cat walk to user
                if (Vector3.Distance(transform.position, Camera.main.transform.position) > 1.5f)
                {
                    CatActions(1);
                    SetSpeed(1);
                    transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
                }
            }
            else
            {
                
                Debug.Log("cat has the command, lets go");
                if (Vector3.Distance(transform.position, Camera.main.transform.position) <= 2)
                {
                    transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
                    CatActions(1);
                    SetSpeed(1);
                    transform.LookAt(temporary);
                    Debug.Log("user is doing ok");

                }
                else
                {
                    transform.LookAt(Camera.main.transform);
                    CatActions(0);
                    SetSpeed(0);
                    Debug.Log("user is slow, catch up!");
                }

                if (Vector3.Distance(transform.position, temporary.position) <= distanceBetween)
                {

                    Debug.Log("distance between points is: " + distanceBetween);
                    if (stage == (numOfStages - 1))
                    {
                        stage = 0;
                        numOfStages = 0;
                        tempArray = null;
                        command_made = false;
                    }
                    else
                    {
                        stage++;
                        temporary = tempArray[stage].transform;
                        Debug.Log("go to next point, OK?");

                        //if (stage == 0 || stage == 2 || stage == 4)
                        //{
                        //    CatActions(1);
                        //    SetSpeed(1);
                        //    Debug.Log("1 was pressed");
                        //}
                        //else if (stage == 1 || stage == 3)
                        //{
                        //    CatActions(2);
                        //    SetSpeed(2);
                        //    Debug.Log("2 was pressed");
                        //}

                    }
                }
            }
        }
    }

    void SetSpeed(int option)
    {
        if (option == 0)
        {
            speed = 0.0f;
            transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
        }
        else if (option == 1)
        {
            speed = 0.5f;
            transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
        }
        else if (option == 2)
        {
            speed = 1.5f;
            transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
        }
    }

    public void MensBathroom()
    {
        CommandMade(1);
        Debug.Log("mens washroom clicked");
    }

    public void WomensBathroom()
    {
        CommandMade(2);
        Debug.Log("womens washroom clicked");
    }

    public void Kitchen()
    {
        CommandMade(0);
        Debug.Log("kitchen clicked");
    }

    void CommandMade(int command)
    {
        command_made = true;
        if(command ==null)
        {
            Debug.Log("No Command");
        }

        switch (command)
        {
            case 0:
                tempArray = new GameObject[2];
                tempArray[0] = poiArray[0];
                tempArray[1] = poiArray[1];

                numOfStages = tempArray.Length;

                temporary = tempArray[0].transform;
                Debug.Log("kitchen setup done");
                break;

            case 1:
                // cat go to p1, p1.mens
                tempArray = new GameObject[2];
                tempArray[0] = poiArray[0];
                tempArray[1] = poiArray[2];

                numOfStages = tempArray.Length;

                temporary = tempArray[0].transform;
                Debug.Log("mens setup done");
                break;

            case 2:
                // cat goes to p1, then p2, then p2.womens
                tempArray = new GameObject[3];
                tempArray[0] = poiArray[0];
                tempArray[1] = poiArray[3];
                tempArray[2] = poiArray[4];

                numOfStages = tempArray.Length;

                temporary = tempArray[0].transform;
                Debug.Log("womens setup done");
                break;

            default:
                break;
        }
    }
}


