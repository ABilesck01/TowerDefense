using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private float speed;

	private float damage;

	public float Damage
	{
		get { return damage; }
		set { damage = value; }
	}

    private void Start()
    {
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
		rigidbody2D.AddForce(transform.right * speed, ForceMode2D.Impulse);
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out BaseUnit unit))
        {
            unit.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

}
