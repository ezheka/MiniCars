using RoadsTypes;
using Size;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabRoad : MonoBehaviour
{
    public TypeRoad roadType;

    private RectTransform _rectTransform;
    private BoxCollider2D _boxCollider;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _boxCollider = GetComponent<BoxCollider2D>();

        SizeImage.AdaptationSize(_rectTransform, _boxCollider);
    }
}
