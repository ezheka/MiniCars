using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoadsTypes;
using Size;

public class PlaceForRoad : MonoBehaviour
{
    public TypeRoad roadType;

    private RectTransform _rectTransform;
    private BoxCollider2D _boxCollider;


    [System.NonSerialized]
    public TypeRoad curentRoadType,
                    curentPlayerRoadType;
    
    //[System.NonSerialized]
    public bool isBusy;
    private void Start()
    {
        isBusy = false;

        // инициализация объектов
        curentRoadType = roadType;
        _rectTransform = GetComponent<RectTransform>();
        _boxCollider = GetComponent<BoxCollider2D>();

        // адаптация размера
        SizeImage.AdaptationSize(_rectTransform, _boxCollider);
    }
}
