using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public Vector2 scrollVelocity = new Vector2(0, 0);

    public bool isLinkedCamera = false;
    public bool isLooping = true;

    public List<Transform> backgrounds;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (isLooping)
        {
            backgrounds = new List<Transform>();

            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);

                if (child.GetComponent<SpriteRenderer>() != null)
                {
                    backgrounds.Add(child);
                }
            }

            backgrounds = backgrounds.OrderBy(t => t.position.y).ToList();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(scrollVelocity * Time.deltaTime);

        if (isLinkedCamera) Camera.main.transform.Translate(scrollVelocity * Time.deltaTime);

        if (isLooping)
        {
            Transform first = backgrounds.FirstOrDefault();

            if (first != null)
            {
                if (first.position.y < Camera.main.transform.position.y)
                {
                    if (!first.GetComponent<SpriteRenderer>().IsVisibleFrom(Camera.main))
                    {
                        Transform last = backgrounds.LastOrDefault();
                        Vector3 lastPosition = last.position;
                        Vector3 lastBound = last.GetComponent<SpriteRenderer>().bounds.size;

                        first.position = new Vector3(first.position.x, lastPosition.y + lastBound.y, first.position.z);

                        backgrounds.Remove(first);
                        backgrounds.Add(first);
                    }
                }
            }
        }
    }
}
