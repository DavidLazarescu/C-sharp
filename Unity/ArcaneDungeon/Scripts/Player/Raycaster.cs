using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private LayerMask doorLayerMask;

    private Camera camera;


    [SerializeField] private float checkRange = 5;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        checkForDoor();
    }



    private void checkForDoor()
    {
        RaycastHit hit;
        if(Physics.Raycast(camera.transform.position, transform.TransformDirection(Vector3.forward), out hit, checkRange, doorLayerMask))
        {
            if(Input.GetKeyDown(KeyCode.F))
                hit.collider.GetComponentInParent<BossDoor>().bossDoorMove();
        }
    }
}
