using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine.Jobs;

public class AsteroidMovement : MonoBehaviour
{
    [SerializeField]
    protected int asteroidCount = 5000;

    protected GameObject[] asteroids;
    protected Transform[] transforms;
    protected Renderer[] renderers;

    public Vector3 m_Acceleration = new Vector3(0.0002f, 0.0001f, 0.0002f);
    public Vector3 m_AccelerationMod = new Vector3(.0001f, 0.001f, 0.0001f);

    NativeArray<Vector3> velocities;
    TransformAccessArray transformsAccessArray;

    PositionUpdateJob positionJob;
    AccelerationJob accelJob;

    JobHandle positionJobHandle;
    JobHandle accelJobHandle;

    protected void Awake()
    {
        asteroids = new GameObject[asteroidCount];
        transforms = new Transform[asteroidCount];
        renderers = new Renderer[asteroidCount];
    }

    protected void Start()
    {
        velocities = new NativeArray<Vector3>(asteroidCount, Allocator.Persistent);
        asteroids = GameObject.FindGameObjectsWithTag("Asteroid");

        for (int i = 0; i < asteroidCount; i++)
        {
            var asteroid = asteroids[i];
            transforms[i] = asteroid.transform;
            renderers[i] = asteroid.GetComponent<Renderer>();
        }

        transformsAccessArray = new TransformAccessArray(transforms);
    }

    struct PositionUpdateJob : IJobParallelForTransform
    {
        [ReadOnly]
        public NativeArray<Vector3> velocity;

        public float deltaTime;

        public void Execute(int i, TransformAccess transform)
        {
            transform.position += velocity[i] * deltaTime;
        }
    }

    struct AccelerationJob : IJobParallelFor
    {
        public NativeArray<Vector3> velocity;

        public Vector3 acceleration;
        public Vector3 accelerationMod;

        public float deltaTime;

        public void Execute(int i)
        {
            velocity[i] += (acceleration + i * accelerationMod) * deltaTime;
        }
    }

    public void Update()
    {
        accelJob = new AccelerationJob()
        {
            deltaTime = Time.deltaTime,
            velocity = velocities,
            acceleration = m_Acceleration,
            accelerationMod = m_AccelerationMod
        };

        positionJob = new PositionUpdateJob()
        {
            deltaTime = Time.deltaTime,
            velocity = velocities
        };

        accelJobHandle = accelJob.Schedule(asteroidCount, 64);
        positionJobHandle = positionJob.Schedule(transformsAccessArray, accelJobHandle);
    }

    public void LateUpdate()
    {
        positionJobHandle.Complete();
    }

    private void OnDestroy()
    {
        velocities.Dispose();
        transformsAccessArray.Dispose();
    }
}
