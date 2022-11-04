using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    //объект камеры
    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();

        //скрыть курсор мыши
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //проверяем, когда делаем выстрел
        if (Input.GetMouseButtonDown(0))
        {
            //запоминаем центр экрана
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);

            //Пускаем луч из центра экрана относительно дальности
            Ray ray = _camera.ScreenPointToRay(screenCenter);
            RaycastHit hit;//улавливаем попадание в эту переменную

            //Если попали в какой то объект
            if (Physics.Raycast(ray, out hit))
            {
                //Распознавание попаданий в цель
                GameObject hitObject = hit.transform.gameObject; //получаем объект в который попали
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();//получаем компонент этого объекта

                //проверим, попадание в мишень
                if (target != null)
                {
                    target.ReactToHit();
                }
                else
                {
                    //запускаем программу
                    StartCoroutine(SphereInicatorCoroutine(hit.point));
                    //рисуем отладочную линию чтобы проследить траекторию луча
                    Debug.DrawLine(this.transform.position, hit.point, Color.green, 6);
                }
            }
        }
    }

    private void OnGUI()
    {
        //Добавление визуального индикатора в центре экрана
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }

    private IEnumerator SphereInicatorCoroutine(Vector3 pos)
    {
        //Создаем сферу
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;//указываем позицию сферы

        //пора остановиться
        yield return new WaitForSeconds(6);
        //после ожидания вернуться в эту часть сопрограммы
        Destroy(sphere);//удалить сферу
    }
}
