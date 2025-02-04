using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    [SerializeField] private BoxCollider2D playableArea;

    private float consumableTime=0;

    [SerializeField] private float consumableTimeInterval = 5f;

    private void Start()
    {
        SpawnConsumable();
    }

    private void Update()
    {
        consumableTime += Time.deltaTime;
        if(consumableTime >= consumableTimeInterval)
        {
            SpawnConsumable();
            consumableTime = 0;
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
            consumableTime = 0;
        }
    }
}
