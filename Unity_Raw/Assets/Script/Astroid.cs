using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{

	public float ESpeed;

	// Start is called before the first frame update
	void Start()
	{
	

	}
		// Update is called once per frame
		void Update()
	{
		//transform.Translate(0, 0, FSpeed * Time.deltaTime);
		transform.Translate(ESpeed * Time.deltaTime, 0, 0);

		

		if (transform.position.x < -5f)
		{
			GameManager.instance.ScoreUp();
			Destroy(gameObject);
		}

		
	}

	
}
