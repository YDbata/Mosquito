using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Mosquito.Character;

public class HeadController : MonoBehaviour
{

    public Transform target;
    public float radius = 10f;
    public float retargetSpeed = 5f;

    private List<PointOfInterest> POIs;

    private float radiusSqr;
    // Start is called before the first frame update
    void Start()
    {
        POIs = FindObjectsOfType<PointOfInterest>().ToList();
        radiusSqr = radius * radius;
    }

    // Update is called once per frame
    void Update()
    {
        Transform tracking = null;
        foreach (PointOfInterest poi in POIs)
        {
            Vector3 delta = poi.transform.position - transform.position;
            if (delta.sqrMagnitude < radius)
            {
                tracking = poi.transform;
                break;
            }
        }

        Vector3 targetPos = transform.position + (transform.forward * 2f);
        if (tracking != null)
        {
            targetPos = tracking.position;
        }

        target.position = Vector3.Lerp(target.position, targetPos, Time.deltaTime+retargetSpeed);
    }
}
