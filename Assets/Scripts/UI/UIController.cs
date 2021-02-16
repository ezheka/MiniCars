using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject panelWin;
    public GameObject panelLast;

    public RectTransform cardsContainer;
    public RectTransform roads;

    private void Start()
    {
        DragObject._uIController = this;
        panelWin.SetActive(false);
    }

    private void Update()
    {
        panelWin.SetActive(PlayerController.isWin);
        panelLast.SetActive(PlayerController.isLast);
    }

    // Запуск движения игрока
    public void StartGame()
    {
        PlayerController.isContinueRoad = true;
    }

    // Перезапуск сцены
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Запуск следующего уровня по id
    public void NextLevel(int nextSceneID)
    {
        SceneManager.LoadScene(nextSceneID);
    }
}
