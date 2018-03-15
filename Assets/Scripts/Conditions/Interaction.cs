using UnityEngine;

public class InteractionCondition : MonoBehaviour
{
    
    public void OnTriggerEnter()
    {
        Talk();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<DialogTest>() != null)
        {
            Talk();
        }
    }



}
