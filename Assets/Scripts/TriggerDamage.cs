using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    [SerializeField] private bool isDestroyingAfterCollision;
    [SerializeField] private int damage;
    private IObjectDestroyer destroyer;
    public void Init(IObjectDestroyer destroyer) //???
    {
        this.destroyer = destroyer;
    }
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    private GameObject parent;
    public GameObject Parent
    {
        get { return parent; }
        set { parent = value; }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //var health = collision.gameObject.GetComponent <Health>();
        if (collision.gameObject == parent) //Не наносим урон родительскому объекту
            return;
        if (GameManager.Instance.healthContainer.ContainsKey(collision.gameObject))
        {
            GameManager.Instance.healthContainer[collision.gameObject].TakeHit(damage);
        }
        if (isDestroyingAfterCollision)
        {
            if (destroyer == null) // Проверка на инициализацию интерфейса, если нет, то удаляем, как раньше
                Destroy(gameObject);
            else destroyer.Destroy(gameObject); //Иначе вызываем, метод Destroy у нашего интерфейса
        }
    } 
}

public interface IObjectDestroyer
{
    void Destroy(GameObject gameObject);
}