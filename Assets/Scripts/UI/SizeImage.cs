using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Size
{
    public class SizeImage : MonoBehaviour
    {
        /*
         * Адаптация картинок под разные разрешения
         */
        public static void AdaptationSize(RectTransform rectTransform, BoxCollider2D boxCollider)
        {
            float _sizeImage = Camera.main.pixelHeight / 10 * 2;
            float _sizeCollider = Camera.main.pixelHeight / 10 * 2 - 20;

            rectTransform.sizeDelta = new Vector2(_sizeImage, _sizeImage);
            boxCollider.size = new Vector2(_sizeCollider, _sizeCollider);
        }
    }
}
