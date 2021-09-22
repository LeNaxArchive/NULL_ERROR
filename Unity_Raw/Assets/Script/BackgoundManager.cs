using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgoundManager : MonoBehaviour
{
	
	public Transform prefab;
	public int numberOfObjects;
	public float recycleOffset;
	public Vector3 startPosition;
	public Vector3 minSize, maxSize;
	public float ESpeed;

	private Vector3 nextPosition;
	private Queue<Transform> objectQueue;

	void Start()
	{
		objectQueue = new Queue<Transform>(numberOfObjects);
		for (int i = 0; i < numberOfObjects; i++)
		{
			objectQueue.Enqueue((Transform)Instantiate(prefab));
		}
		nextPosition = startPosition;
		for (int i = 0; i < numberOfObjects; i++)
		{
			Recycle();
		}
	}

	void Update()
	{
		transform.Translate(ESpeed * Time.deltaTime, 0f, 0f);

		if (objectQueue.Peek().localPosition.x + recycleOffset < -4f)
		{
			Recycle();
		}
	}

	private void Recycle()
	{
		Vector3 scale = new Vector3(
			Random.Range(minSize.x, maxSize.x),
			Random.Range(minSize.y, maxSize.y),
			Random.Range(minSize.z, maxSize.z));

		Vector3 position = nextPosition;
		position.x += scale.x * 0.5f;
		position.y += scale.y * 0.5f;

		Transform o = objectQueue.Dequeue();
		o.localScale = scale;
		o.localPosition = position;
		nextPosition.x += scale.x;
		objectQueue.Enqueue(o);
	}
}







