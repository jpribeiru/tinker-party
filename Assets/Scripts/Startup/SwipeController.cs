using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SwipeController : MonoBehaviour, IEndDragHandler
{
    [SerializeField] int maxPage;
    [SerializeField] public static int currentPage;
    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform levelPagesRect;

    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;
    float dragThreshold;

    private void Awake()
    {
        currentPage = 1;
        targetPos = levelPagesRect.localPosition;
        dragThreshold = Screen.width / 15f;
    }

    public void Next()
    {
        if (currentPage < maxPage)
        {
            currentPage++;
            targetPos += pageStep;
            MovePage();
        }
    }

    public void Prev()
    {
        if (currentPage >1)
        {
            currentPage--;
            targetPos -= pageStep;
            MovePage();
        }
    }

    void MovePage()
    {
        levelPagesRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > dragThreshold)
        {
            if (eventData.position.x > eventData.pressPosition.x)
            {
                Prev();
            }
            else
            {
                Next();
            }
        }
        else
        {
            MovePage();
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow) && currentPage < maxPage)
        {
            Next();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentPage > 1)
        {
            Prev(); 
        }
    }

}


// tutorial by: https://www.youtube.com/watch?v=qf9xe2mbWeU