using UnityEngine;
using UnityEngine.Rendering;

public class SortingChanger : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.GetComponent<SortingGroup>().sortingOrder = 1;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<SortingGroup>().sortingOrder = 0;
    }

}
