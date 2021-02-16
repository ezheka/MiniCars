using Size;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public List<RectTransform> movingPoints;
    public float speed = 3f;

    public static bool isContinueRoad;
    public static bool isWin = false,
                       isLast = false;

    private RectTransform _rectTransform,
                          _lastPoint;

    private BoxCollider2D _boxCollider;

    void Start()
    {
        _rectTransform = transform as RectTransform;
        _lastPoint = movingPoints[movingPoints.Count - 1];
        _boxCollider = GetComponent<BoxCollider2D>();

        isContinueRoad = false;
        isWin = false;
        isLast = false;

        SizeImage.AdaptationSize(_rectTransform, _boxCollider);

        transform.position = movingPoints[0].position;
    }


    void Update()
    {
        // Двигаем объект если isContinueRoad = true
        if (movingPoints.Count > 0 && isContinueRoad) 
        {
            if (_rectTransform.position == movingPoints[0].position)
            {
                movingPoints.RemoveAt(0);
            }

            _rectTransform.position = Vector3.MoveTowards(_rectTransform.position, movingPoints[0].position, Time.deltaTime * speed);

            
            Vector3 direction = movingPoints[0].position;
            direction -= transform.position;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, -direction);
            
        }

        // Победа, если достигнута последняя точка
        if(_rectTransform.position == _lastPoint.position && !isWin)
        {
            WinGame();
        }

    }

    // проверка на клетку, что это место
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "placeForLoad")
        {
            // проверка на правильность постановки дороги
            if (collision.GetComponent<PlaceForRoad>().isBusy)
            {
                PlaceForRoad collider2D = collision.GetComponent<PlaceForRoad>();
                
                if(collider2D.curentPlayerRoadType == collider2D.curentRoadType)
                {
                    ContinueGame();
                }
                else
                {
                    StopGame();
                }
                
            }
            else
            {
                StopGame();
            }
        }
    }

    private void ContinueGame()
    {
        isContinueRoad = true;
    }

    private void StopGame()
    {
        isContinueRoad = false;
        isLast = true;
        Debug.Log("Ошибка в постройке дороги");
    }

    private void WinGame()
    {
        isContinueRoad = false;
        isWin = true;

        Vector3 direction = new Vector3(0, 0, 90);
        direction -= transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        Debug.Log("Вы выиграли!");
    }
}
