using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedWeapon : MonoBehaviour
{
    [SerializeField] private float MaxYPos;
    [SerializeField] private float YIncreaseSpeed;
    [SerializeField] private float ReloadTime;
    [SerializeField] private GameObject ObjectToInstantiate;

    private bool isReloading;
    private void Start()
    {
        isReloading = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isReloading)
        {
            if (Input.GetMouseButton(0) && transform.localPosition.y < MaxYPos)
            {
                transform.localPosition += Vector3.up * YIncreaseSpeed;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("Up");
                Instantiate(ObjectToInstantiate, transform);
                StartCoroutine("Reload");
            }
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(ReloadTime);
        transform.localPosition = Vector3.zero;
        isReloading = false;
    }
}
