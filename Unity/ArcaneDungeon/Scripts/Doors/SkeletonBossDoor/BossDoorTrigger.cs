using UnityEngine.UI;
using UnityEngine;

public class BossDoorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject openText;


    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
            openText.SetActive(true);
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
            openText.SetActive(false);
    }

}
