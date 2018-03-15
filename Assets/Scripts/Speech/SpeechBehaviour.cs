
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpeechBehaviour : MonoBehaviour {


	void Update ()
    {
       
         if (Input.GetKeyDown(KeyCode.Space))
         {
             SceneManager.LoadScene("Apartment");
         } 
    }
}
