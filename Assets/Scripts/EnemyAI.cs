using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 5.0f;
    public float obstacleRande = 5.0f;
    public bool _alive = true;

    //снаряды
    [SerializeField]
    private GameObject[] _fireballsPrefab;
    private GameObject _fireball;

    private void Start()
    {
        _alive = true;
    }

    private void Update()
    {
        if (_alive)
        {
            //непрерывное движение вперед
            transform.Translate(0, 0, speed * Time.deltaTime);

            //луч из объекта вперед
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;//объект удара

            //пускаем луч и проверяем
            if (Physics.Raycast(ray, out hit))
            {
                //получаем объект удара
                GameObject hitObject = hit.transform.gameObject;
                //если объект удара игрок
                if (hitObject.GetComponent<CharacterController>())
                {
                    //если огненного шара нет
                    if (_fireball == null)
                    {
                        int randFireball = Random.Range(1, _fireballsPrefab.Length);
                        _fireball = Instantiate(_fireballsPrefab[randFireball]) as GameObject;
                        _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                        _fireball.transform.rotation = transform.rotation;
                    }
                }

                //проверяем дистанцию до объекта впереди
                else if (hit.distance < obstacleRande)
                {
                    float angleRotation = Random.Range(-100, 100);//выбираем угол поворота
                    transform.Rotate(0, angleRotation, 0); //поворачиваемся
                }
            }
        }
    }

    public void SetAlive(bool alive)
    {
        _alive = alive;
    }
}
