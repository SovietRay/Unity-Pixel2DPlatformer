using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour, IObjectDestroyer
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private TriggerDamage triggerDamage;
    [SerializeField] private float force;
    [SerializeField] private float lifeTime = 1f;
    private Player player; //Ссылка на класс Player для уничтожения стрелы через собственный интрефейс
    public float Force
    {
        get { return force; }
        set { force = value; }
    }
    public float LifeTime
    {
        get { return lifeTime; }
        set { lifeTime = value; }
    }
    public void Destroy(GameObject gameObject)
    {
        player.ReturnArrowToPool(this); //? Передать ссылка на сам скрипт?
    }

    public void SetImpulse(Vector2 direction, float force, Player player) //Вместо GameObject - Player?
    {
        this.player = player; //?
        triggerDamage.Init(this); //?
        triggerDamage.Parent = player.gameObject; //?
        rigidbody2D.AddForce(direction * force, ForceMode2D.Impulse);
        if (force < 0) //Если сила меньше нуля
            transform.rotation = Quaternion.Euler(0, 180, 0); //Разворот спрайта стрелы по y
        StartCoroutine(StartLife()); //Старт таймера жизни стрелы
    }

    private IEnumerator StartLife()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
        yield break;
    }
}
