using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public AudioSource deathClip;
    public float Pspeed;
    public float maxY;
    float yInput;
    public GameObject Death;




    void start()
    {


    }

    void Update()
    {
        // for up and down  
        yInput = Input.GetAxis("Vertical");
        transform.Translate(0, yInput * Pspeed * Time.deltaTime, 0);
        //                                 up  down
        float limitedY = Mathf.Clamp(transform.position.y, 0, 12);
        transform.position = new Vector3(transform.position.x, limitedY, transform.position.z);
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            
            GameManager.instance.GUIGameOver();
            Destroy(gameObject);
            deathClip.Play();
            
        }

    }

}
