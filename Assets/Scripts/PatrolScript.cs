using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolScript : MonoBehaviour {

    public AudioClip ChaseClip, AttackClip;
    public string enemyState;
    public float speed = 0;
    public bool isplaying = false;
    public NavMeshAgent enemy;
    public Transform player;
    public float dangerzone, attackzone;
    public float power = 1f;
    public Transform[] waypoints;
    public int currentwp;
    private AudioSource AS;
    private Rigidbody rb;

    

    // Start is called before the first frame update
    void Start()
    {
        enemyState = "patrol";
        currentwp = 0;

        AS = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        

        float playerdistance = Vector3.Distance(enemy.gameObject.transform.position, player.position);
        
        if(playerdistance >= dangerzone)
        {
            if(enemyState != "patrol")
            {
                // play patrol audio
            }
            enemyState = "patrol";

            if (enemy.remainingDistance <= 2f)
            {
                // print(currentwp);
                currentwp++;
                if (currentwp >= waypoints.Length)
                {
                    currentwp = 0;
                }
            }


            enemy.SetDestination(waypoints[currentwp].position);
        }
        else
        {
            if (playerdistance <= attackzone)
            {
                if(enemyState != "attack")
                {
                    AS.clip = AttackClip;
                    AS.Play();

                }
                enemyState = "attack";

                player.gameObject.GetComponent<Rigidbody>().AddForce(enemy.transform.forward * power, ForceMode.Impulse);
                
            }
            else
            {
                if(enemyState != "chase")
                {
                    AS.clip = ChaseClip;
                    AS.Play();
                
                }
                enemyState = "chase";

                enemy.SetDestination(player.position);

            }
        }
        float speed = rb.velocity.magnitude;

        if (speed >= 1f)
        {
            if (!isplaying)
            {
                AS.Play();
                isplaying = true;
            }
        }
        else
        {
            AS.Pause();
            isplaying = false;
        }
    }
}
