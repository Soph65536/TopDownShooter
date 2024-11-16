using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PunchingGlove : MonoBehaviour
{
    public int damage;

    [SerializeField] private BoxCollider2D punchingArea;
    private float ParentYPos;

    // Start is called before the first frame update
    void Awake()
    {
        ParentYPos = transform.parent.localPosition.y;
        punchingArea.offset = new Vector2(punchingArea.offset.x, -ParentYPos/2); //set y offset to -position/2 cus the start point of the collider is the center
        punchingArea.size = new Vector2(punchingArea.size.x, ParentYPos); //set y size 

        Destroy(gameObject, 0.1f); //destroy soon after making to give time for collisions
    }
}
