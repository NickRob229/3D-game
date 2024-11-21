using UnityEngine;
using System.Collections;

public class MoveObjectTaskCode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ExampleCoroutine");
        // start the ExampleCoroutine()
    }

    // Speed at which the object will move 
    public float moveSpeed = 5f;



    void Update()
    {
        // Move the object forward (in the object's local forward direction) 
        //transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    IEnumerator ExampleCoroutine()
    {
        for (int i = 0; i < 5; i++)
        {

            Debug.Log("Move forward for 2.5 seconds");

            // Move the object forward (in the object's local forward direction) 
            float timer = 0f;
            float moveduration = 2.5f;

            while (timer < moveduration)
            {
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                timer += Time.deltaTime;
                yield return null;
            }


            Debug.Log("Move back for 2.5 seconds");
            timer = 0f;
            moveduration = 0.5f;
            while (timer < moveduration)
            {
                transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
                timer += Time.deltaTime;
                yield return null;
            }


            Debug.Log("Wait for  1 seconds");
            yield return new WaitForSeconds(1f);


            // end of coroutine
        }

    }

}
