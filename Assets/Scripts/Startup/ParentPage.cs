using UnityEngine;

public class ParentPage : MonoBehaviour
{
    [SerializeField] private GameObject childPage;
    [SerializeField] private int pageNumber;
    [SerializeField] private SwipeController swCo;

    // Update is called once per frame
    void Update()
    {
        if (SwipeController.currentPage != pageNumber)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
            childPage.SetActive(true);
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}
