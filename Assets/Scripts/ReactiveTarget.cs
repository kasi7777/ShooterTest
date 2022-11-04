using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    private EnemyAI _enemyAI;

    // Start is called before the first frame update
    private void Start()
    {
        _enemyAI = GetComponent<EnemyAI>();
    }

    public void ReactToHit()
    {
        //Если такой компонент есть
        if (_enemyAI != null)
            _enemyAI.SetAlive(false);//вызываем его открытый метод

        StartCoroutine(DieCoroutine(3));
    }

    private IEnumerator DieCoroutine(float waitSecond)
    {
        this.transform.Rotate(45, 0, 0);//поворачиваем объект имитируя поподание

        //ждем
        yield return new WaitForSeconds(waitSecond);

        //уничтожаем объект
        Destroy(this.transform.gameObject);
    }
}
