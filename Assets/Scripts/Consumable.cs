using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    [SerializeField] BoxCollider2D playableArea;

    private float consumable_time=0;

    [SerializeField] private float consumable_time_interval = 5f;

    private void Start()
    {
        SpawnConsumable();
    }

    private void Update()
    {
        consumable_time += Time.deltaTime;
        if(consumable_time>=consumable_time_interval)
        {
            SpawnConsumable();
            consumable_time = 0;
        }

    }

    private void SpawnConsumable()
    {
        Bounds bounds = this.playableArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<Snake>() || collider.gameObject.GetComponent<SnakeTwo>())
        {
            SpawnConsumable();
            consumable_time = 0;
        }
    }
}
