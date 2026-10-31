using UnityEngine;
using UnityEngine.EventSystems;

public class EqScroll : MonoBehaviour, IEndDragHandler
{
    [SerializeField] int maxPage;
    private int currentPage2;
    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform levelPagesRect;

    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;
    float dragThreshold;

    private void Awake()
    {
        currentPage2 = 1;
        targetPos = levelPagesRect.localPosition;
        dragThreshold = Screen.width / 15f;
    }

    public void Next()
    {
        if (currentPage2 < maxPage)
        {
            currentPage2++;
            targetPos += pageStep;
            MovePage();
        }
    }

    public void Prev()
    {
        if (currentPage2 >1)
        {
            currentPage2--;
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

}


// tutorial by: https://www.youtube.com/watch?v=qf9xe2mbWeU