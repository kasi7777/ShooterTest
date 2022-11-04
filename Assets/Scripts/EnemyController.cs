using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _enemyPrefab;
    private GameObject _enemy;

    private void Update()
    {
        //создаем нового врага, если врагов нет
        if(_enemy == null)
        {
            int randEnemy = Random.Range(1, _enemyPrefab.Length);//случайно выбираем врага
            _enemy = Instantiate(_enemyPrefab[randEnemy]) as GameObject;//создаем клона на игровой объект
            _enemy.transform.position = new Vector3(0, 3, 0);//задаем позицию появления
            float angle = Random.Range(0, 360);
            _enemy.transform.Rotate(0, angle, 0);//поворачиваем
        }
    }
}
