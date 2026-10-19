using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int maxHealth = 2;


    public void TakeDamage(int damage)
    {
        maxHealth -= damage;

        if(maxHealth <= 0)
        {
            WaveSpawner.onEnemyDestroy.Invoke();
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
