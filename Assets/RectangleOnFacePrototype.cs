using UnityEngine;

public class BeanFaceIndicator : MonoBehaviour
{
    public GameObject rectangle;  // Reference to the rectangle (quad)
    public float distanceFromBean = 1.0f;  // Distance from the bean's face

    void Update()
    {
        // Position the rectangle in front of the bean, based on its forward vector
        rectangle.transform.position = transform.position + transform.forward * distanceFromBean;

        // Make the rectangle face the same direction as the bean
        rectangle.transform.rotation = Quaternion.LookRotation(transform.forward);
    }
}