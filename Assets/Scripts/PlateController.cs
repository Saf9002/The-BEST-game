using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateController : MonoBehaviour
{
    [SerializeField] float timeToDestroy;
    float timer;
    bool startTimer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer == true)
        {
            timer += Time.deltaTime;
            if (timer > timeToDestroy)
            {
                Destroy(gameObject);
            }
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            startTimer = true;
        }
    }
}
