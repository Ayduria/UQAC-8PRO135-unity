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

    public Vector3 m_Acceleration = new Vector3(0.0002f, 0.0001f, 0.0002f);
    public Vector3 m_AccelerationMod = new Vector3(.0001f, 0.001f, 0.0001f);
    public Vector3 m_Rotation = new Vector3(0.0002f, 0.0001f, 0.0002f);
    public Vector3 m_RotationMod = new Vector3(.0001f, 0.001f, 0.0001f);

    NativeArray<Vector3> velocities;
    NativeArray<Vector3> rotations;
    TransformAccessArray transformsAccessArray;

    PositionUpdateJob positionJob;
    AccelerationJob accelJob;

    JobHandle positionJobHandle;
    JobHandle accelJobHandle;

    protected void Awake()
    {
        asteroids = new GameObject[asteroidCount];
        transforms = new Transform[asteroidCount];
    }

    protected void Start()
    {
        velocities = new NativeArray<Vector3>(asteroidCount, Allocator.Persistent);
        rotations = new NativeArray<Vector3>(asteroidCount, Allocator.Persistent);
        asteroids = GameObject.FindGameObjectsWithTag("Asteroid");

        for (int i = 0; i < asteroidCount; i++)
        {
            var asteroid = asteroids[i];
            transforms[i] = asteroid.transform;
        }

        transformsAccessArray = new TransformAccessArray(transforms);
    }

    struct PositionUpdateJob : IJobParallelForTransform
    {
        [ReadOnly]
        public NativeArray<Vector3> velocity;
        public NativeArray<Vector3> rotation;

        public float deltaTime;

        public void Execute(int i, TransformAccess transform)
        {
            transform.position += velocity[i] * deltaTime;
            transform.rotation *= Quaternion.Euler(rotation[i] * deltaTime);
        }
    }

    struct AccelerationJob : IJobParallelFor
    {
        public NativeArray<Vector3> velocity;
        public NativeArray<Vector3> a_Rotation;

        public Vector3 acceleration;
        public Vector3 accelerationMod;
        public Vector3 rotation;
        public Vector3 rotationMod;

        public float deltaTime;

        public void Execute(int i)
        {
            velocity[i] += (acceleration + i * accelerationMod) * deltaTime;
            a_Rotation[i] += (rotation + i * rotationMod) * deltaTime;
        }
    }

    public void Update()
    {
        accelJob = new AccelerationJob()
        {
            deltaTime = Time.deltaTime,
            velocity = velocities,
            acceleration = m_Acceleration,
            accelerationMod = m_AccelerationMod,
            a_Rotation = rotations,
            rotation = m_Rotation,
            rotationMod = m_RotationMod
        };

        positionJob = new PositionUpdateJob()
        {
            deltaTime = Time.deltaTime,
            velocity = velocities,
            rotation = rotations
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
        rotations.Dispose();
        transformsAccessArray.Dispose();
    }
}
