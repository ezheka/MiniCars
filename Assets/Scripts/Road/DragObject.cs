using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public static UIController _uIController;

    public bool isDrag = false;

    private RectTransform _rectTransform;
    private bool onPlace = false;


    private PrefabRoad _prefabRoad;
    private Collider2D _collider2D;

    private void Start()
    {
        // инициализация параметров
        _rectTransform = transform as RectTransform;
        _prefabRoad = GetComponent<PrefabRoad>();

        // параметр, отвечающий за вход в коллайдер места для дороги
        onPlace = false;
    }

    // Начало перемещения
    public void OnDrag(PointerEventData eventData)
    {
        if (isDrag)
        {
            if (!onPlace)
            {
                _rectTransform.SetParent(_uIController.roads);
            }

            // перемещаем объект за мышью
            Vector3 vector3 = Camera.main.ScreenToWorldPoint(eventData.position);
            transform.position = new Vector3(vector3.x, vector3.y, 0);
            transform.localScale = new Vector2(1.1f, 1.1f);
        }
    }

    // Конец перемещения
    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDrag)
        {
            if (!onPlace)
            {
                // если мы не поставили дорогу, то возвращаем обратно на панель
                _rectTransform.SetParent(_uIController.cardsContainer);
            }
            else
            {
                // иначе ставим дорошу по координатам места, для дороги
                _rectTransform.anchorMin = _rectTransform.anchorMax = new Vector2(.5f, .5f);
                _rectTransform.localPosition = Vector3.zero;
            }

            transform.localScale = new Vector2(1f, 1f);
            Debug.Log("OnEndDrag");

            // если дорога поставлена, то ставим влаг что это место занято
            if (onPlace && _collider2D != null)
            {
                _collider2D.GetComponent<PlaceForRoad>().isBusy = true;
                _collider2D.GetComponent<PlaceForRoad>().curentPlayerRoadType = _prefabRoad.roadType;
                Debug.Log(_collider2D.GetComponent<PlaceForRoad>().curentPlayerRoadType);
            }
        }
    }

    // Если коллайдеры дороги и места совпадают
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "placeForLoad" && isDrag)
        {
            // проверка на доступность места для вставки дороги
            if (!collision.GetComponent<PlaceForRoad>().isBusy)
            {
                onPlace = true;
                _rectTransform.SetParent(collision.gameObject.transform as RectTransform);

                _collider2D = collision;
            }
        }
    }

    // Если дорога за пределами коллайдера места
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "placeForLoad")
        {
            onPlace = false;

            // убираем дорогу с места и делаем это место свободным
            if (_collider2D != null && collision == _collider2D)
            {
                _collider2D.GetComponent<PlaceForRoad>().isBusy = false;
                _collider2D = null;
            }

        }
    }
}