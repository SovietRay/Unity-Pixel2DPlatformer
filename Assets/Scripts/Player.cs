using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 2.5f; // Множитель скорости
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    [SerializeField] private float force; // Сила прыжка
    public float Force
    {
        get { return force; }
        set { force = value; }
    }
    private bool isJumping;
    [SerializeField] private float minimalHeight; // Маскимальная высота проваливания игрока
    public float MinimalHeight
    { 
        get { return minimalHeight; }
        set { minimalHeight = value; }
    }
    [SerializeField] private bool testMod; // Если включен, игролк не может умереть
    
    public Rigidbody2D rigidbody;
    public GroundDetection groundDetection;
    public Vector3 direction; //Хранит направление движения
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject arrow;
    [SerializeField] private Arrow arrowFromPool;
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private float reloadTime = 1f;
    [SerializeField] private bool readyToShoot = true;
    [SerializeField] private bool shooting = false;
    [SerializeField] private int arrowsCount = 5;

    private bool fallAllowed = false;
    public int ArrowsCount
    {
        get { return arrowsCount; }
        set 
        {
            if ((value > 1) && (value < 30))
                arrowsCount = value;
        }
    }

    private List<Arrow> arrowPool; //Пул стрел типа List
    private void Start()
    {
        arrowPool = new List<Arrow>(); // Создаем пул стрел, используем коллекцию типа лист
        CreateArrowPool(arrowPool, arrowsCount); // Заполняем пул стрелами
    }

    // Update is called once per frame
    void Update()
    {
        if (animator != null)
            animator.SetBool("isGrounded", groundDetection.isGrounded);
        if (!isJumping && !groundDetection.isGrounded && fallAllowed)
            animator.SetTrigger("StartFall");
        isJumping = !groundDetection.isGrounded && isJumping;
        direction = Vector3.zero;

        if (Input.GetKey(KeyCode.A) && !shooting)
        {
            //transform.Translate(Vector2.left * Time.deltaTime * speed);
            direction = Vector3.left; // смещене -1; 0
        }
        if (Input.GetKey(KeyCode.D) && !shooting)
        {
            //transform.Translate(Vector2.right * Time.deltaTime * speed);
            direction = Vector3.right; // смещене 1; 0

        }
        direction *= speed;
        direction.y = rigidbody.velocity.y;
        rigidbody.velocity = direction;

        if (Input.GetKeyDown(KeyCode.Space) && groundDetection.isGrounded && !shooting)
        {
            rigidbody.velocity = Vector3.zero; //Обнуляем velocity, если произойдет двойной прыжок, то не будут складываться силы
            rigidbody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            animator.SetTrigger("StartJump");
            isJumping = true;
        }

        if (direction.x > 0)
            spriteRenderer.flipX = false;
        if (direction.x < 0)
            spriteRenderer.flipX = true;

        animator.SetFloat("Speed", Mathf.Abs(direction.x));
        CheckFall();

        if (Input.GetMouseButtonDown(0) && groundDetection.isGrounded && readyToShoot && !shooting) //Нажатие пр.к.м, стоим на земле, таймер стрельбы откатился и не еще не стреляем
        {
            shooting = true; // В проц. стрельбы - да
            animator.SetTrigger("StartShoot"); //Запускаем анимацию стрельбы
        }
    }

    private void CheckShoot () // Вызываем из эвента анимации Shoot на Player
    {
        var prefab = GetArrowFromPool(); //Берем из пула префаб стрелы
        prefab.SetImpulse
            (Vector2.right, spriteRenderer.flipX ? -35 : 35, this); //Даем импульс стреле вправо, если игрок смотрит влево, даем минусовой импульс, передаем ссылку на текущий экземпляр Player
        shooting = false; //Персонаж стрельнул, может двигаться и стрелять еще, после таймера
        StartCoroutine(CheckShootTime()); //Таймер на откат следующего выстрела
        readyToShoot = false; //Таймер откатит на true
    }                
    
    private  IEnumerator CheckShootTime ()
    {
        yield return new WaitForSeconds(reloadTime);
        readyToShoot = true;
        yield break;
    }

    #region Работа с пулом стрел
    private Arrow GetArrowFromPool()
    {
        if (arrowPool.Count > 0) //Если лист не пустой
        {
            var arrowTemp = arrowPool[0]; //Взять из 0 адреса из листа
            arrowPool.Remove(arrowTemp); //Удаляем ссылку из листа на взятый префаб
            arrowTemp.gameObject.SetActive(true); //Активируем префаб
            arrowTemp.transform.parent = null; //JОткрепляем от arrowSpawnPoint
            arrowTemp.transform.position = arrowSpawnPoint.transform.position; //Прикрепляем позицию к точке arrowSpawnPoint спавна на Player
            return arrowTemp; //Возвращаем префаб Arrow
        }
        return Instantiate(arrowFromPool, arrowSpawnPoint.position, 
            Quaternion.identity); // Если пул оказался пустым, то генерируем префаб Arrow в реальном времени
    } 

    public void ReturnArrowToPool(Arrow arrowTemp)
    {
        if (!arrowPool.Contains(arrowTemp)) // Проверка на совпадение ссылок, чтобы не было дублирование ссылки на одну и ту же стрелу
            arrowPool.Add(arrowTemp); // Если дубляжа нет, добавляем Arrow обратно в пул
        arrowTemp.transform.parent = arrowSpawnPoint; // Кладем стрелу в arrowSpawnPoint
        arrowTemp.transform.position = arrowSpawnPoint.transform.position; // Возвращаем координаты arrowSpawnPoint
        arrowTemp.gameObject.SetActive(false); //Выключаем
    }

    public List<Arrow> CreateArrowPool(List<Arrow> arrowPool, int arrowsCount)
    {
        if (arrowsCount > 0)
        {
            for (int i = 0; i < arrowsCount; i++)
            {
                var arrowTemp = Instantiate(arrowFromPool, arrowSpawnPoint); // Клонируем префаб Arrow
                arrowPool.Add(arrowTemp); //Добавляем в лист на место i
                arrowTemp.gameObject.SetActive(false); //Отключаем префаб на сцене
            }
            return arrowPool; //Возвращаем заполненный префабами Arrow пул
        }
        return null;
    }
    #endregion

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Platform"))
        {
            fallAllowed = false;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Platform"))
        {
            fallAllowed = true;
        }
    }

    #region Fall
    private void CheckFall()
    {
        if (transform.position.y < minimalHeight && testMod)
        {
            transform.position = new Vector3(-2, -0.9f, -10); // После падения появляемся в нулевой точке
            rigidbody.velocity = new Vector2(0, 0); // Обращение к физ. компоненту rigidbody и обнуляем velocity (вектор скорости), чтобы компенсировать ускорение
        }
        else if (transform.position.y < minimalHeight)
        {
            Destroy(gameObject); // Уничтожаем игрока, если тестовый мод не включен
        }
    }
    #endregion


}
