using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject playerToFollow;
    [SerializeField] Vector3 adjustmentFollowPos = new Vector3(0f, 5f, -6f);
    [SerializeField] float adjustedZ = -6f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        adjustmentFollowPos.z = playerToFollow.transform.position.z + adjustedZ;
        transform.position = adjustmentFollowPos;
    }
}
