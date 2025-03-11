using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public Transform pemain; 
    public GameObject panelGameOver;
    public float jarakDeteksi = 10f; 
    public float jarakSerang = 2f; 
    public float waktuSpawn = 100f; 
    public float damageSerangan = 20f;
    public Animator animator;
    private NavMeshAgent agent;
    private bool mengejarPemain = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();  
        InvokeRepeating("SpawnMonster", waktuSpawn, waktuSpawn);
        animator = GetComponent<Animator>();
        animator.SetBool("walk", false);
        pemain = FindObjectOfType<GerakanPemain>().transform;
    }

    void Update()
    {
        if (pemain == null) return; 

        float jarak = Vector3.Distance(transform.position, pemain.position);

        if (jarak < jarakDeteksi) 
        {
            agent.SetDestination(pemain.position);
            mengejarPemain = true;
            
        }
        else
        {
            mengejarPemain = false;
        }
        animator.SetBool("walk", mengejarPemain);
        
    }


    void SpawnMonster()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
        Instantiate(gameObject, spawnPos, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<GameOverManager>().GameOver();
        }
    }
}