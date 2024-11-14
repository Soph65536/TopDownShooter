using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PunchingGlove : MonoBehaviour
{
    const float colliderX = 1.5f;
    const float minColliderY = 0f;
    const float maxColliderY = 3f;

    public int damage;

    [SerializeField] private BoxCollider2D punchingArea;
    private float colliderY;
    // Start is called before the first frame update
    void Start()
    {
        punchingArea = GetComponent<BoxCollider2D>();
        punchingArea.enabled = false;
        colliderY = minColliderY;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetMouseButtonDown(0))
        {
            colliderY = minColliderY;
            punchingArea.size = new Vector2(colliderX, colliderY);
        }
        else if (Input.GetMouseButton(0) && colliderY < maxColliderY)
        {
            colliderY += 0.1f;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            punchingArea.size = new Vector2 (colliderX, colliderY);
            StartCoroutine("PunchingAreaEnable");
        }
    }

    private IEnumerator PunchingAreaEnable()
    {
        punchingArea.enabled = true;
        yield return new WaitForFixedUpdate();
        punchingArea.enabled = false;
    }
}
