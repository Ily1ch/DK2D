using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target; // цель, за которой будет преследовать враг
    public float speed = 5f; // скорость передвижения врага
    public float minDistance = 2f; // минимальное расстояние, при котором враг начинает преследовать игрока

    private bool isFollowing = false; // флаг, указывающий, следует ли враг за игроком

    private void Update()
    {
        if (target == null)
        {
            return; // если цель отсутствует, то враг ничего не делает
        }

        float distance = Vector3.Distance(transform.position, target.position); // вычисляем расстояние до цели

        if (distance <= minDistance)
        {
            isFollowing = true; // если игрок находится в зоне преследования, то враг начинает следовать за ним
        }

        if (isFollowing)
        {
            Vector3 direction = target.position - transform.position; // вычисляем направление к цели
            direction.Normalize(); // нормализуем направление, чтобы скорость передвижения не зависела от расстояния до цели
            transform.position += direction * speed * Time.deltaTime; // перемещаем врага в направлении цели
        }

        if (distance > minDistance * 2f)
        {
            isFollowing = false; // если игрок выходит за пределы зоны преследования, то враг останавливается
        }
    }
}
