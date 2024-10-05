using UnityEngine;

public class BaseGenerator : Generator
{
    public override Obstacle GenerateObstacle()
    {
        Obstacle randomObstacle = GetRandomObstacle();

        Obstacle obstacleInstance = Instantiate(randomObstacle, _obstaclesInstancePoint.position, Quaternion.identity);

        return obstacleInstance;
    }

    private Obstacle GetRandomObstacle()
    {
        int randomIndex = Random.Range(0, _obstacles.Length);

        return _obstacles[randomIndex];
    }
}
