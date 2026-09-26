using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingCube : MonoBehaviour
{
    public static MovingCube CurrentCube {get; private set;}
    public static MovingCube LastCube {get; private set;}
    public MoveDirection MoveDirection {get; set;}

    public Material p1Material;
    public Material p2Material;

    [SerializeField]
    private float moveSpeed = 1f;
    [SerializeField]
    private int xDirection = 1;
    [SerializeField]
    private int zDirection = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        if (LastCube == null)
        {
            LastCube = GameObject.Find("Start").GetComponent<MovingCube>();
        }
        CurrentCube = this;

        transform.localScale = new Vector3(
            LastCube.transform.localScale.x,
            transform.localScale.y,
            LastCube.transform.localScale.z
        );
    }

    public void Initialize(MoveDirection direction)
    {
        MoveDirection = direction;
        // GetComponent<MeshRenderer>().material = p1Material;

        if (MoveDirection == MoveDirection.Z)
        {
            GetComponent<MeshRenderer>().material = p1Material;
        }
        else
        {
            GetComponent<MeshRenderer>().material = p2Material;
        }
    }

    internal void Stop()
    {
        moveSpeed = 0f;
        float hangover = GetHangover();

        float max = MoveDirection == MoveDirection.Z ? LastCube.transform.localScale.z : LastCube.transform.localScale.x;
        if (Mathf.Abs(hangover) >= max)
        {
            LastCube = null;
            CurrentCube = null;
            gameObject.AddComponent<Rigidbody>();
            Destroy(gameObject, 3f);
            GameObject.Find("GameManager").GetComponent<Game2handler>().EndGame();
        }

        float direction = hangover > 0 ? 1f: -1f; //variable that tells if cube cutoff is in front or behind, if behind, becomes -1, so multiplied in SplitCube() to flip the side
        if (MoveDirection == MoveDirection.Z)
        {
            SplitCubeOnZ(hangover, direction);
        }
        else
        {
            SplitCubeOnX(hangover, direction);
        }

        LastCube = this;
    }

    private void SplitCubeOnX(float hangover, float direction)
    {
        float newXSize = LastCube.transform.localScale.x - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.x - newXSize;

        float newXPosition = LastCube.transform.position.x + (hangover / 2);
        transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);

        float cubeEdge = transform.position.x + (newXSize / 2f * direction);
        float fallingBlockXPosition = cubeEdge + (fallingBlockSize / 2f * direction);

        SpawnDropCube(fallingBlockXPosition, fallingBlockSize);

    }

    private void SplitCubeOnZ(float hangover, float direction)
    {
        float newZSize = LastCube.transform.localScale.z - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.z - newZSize;

        float newZPosition = LastCube.transform.position.z + (hangover / 2);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);

        float cubeEdge = transform.position.z + (newZSize / 2f * direction);
        float fallingBlockZPosition = cubeEdge + (fallingBlockSize / 2f * direction);

        SpawnDropCube(fallingBlockZPosition, fallingBlockSize);

    }

    private void SpawnDropCube(float fallingBlockZPosition, float fallingBlockSize)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        if (MoveDirection == MoveDirection.Z){
        cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
        cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockZPosition);
        }
        else if (MoveDirection == MoveDirection.X){
            cube.transform.localScale = new Vector3(fallingBlockSize, transform.localScale.y, transform.localScale.z);
            cube.transform.position = new Vector3(fallingBlockZPosition, transform.position.y, transform.position.z);
        }
        
        cube.AddComponent<Rigidbody>();
        Destroy(cube.gameObject, 3f);
    }

    private float GetHangover()
    {
        if (MoveDirection == MoveDirection.Z)
        {
            return transform.position.z - LastCube.transform.position.z;
        }
        else
        {
            return transform.position.x - LastCube.transform.position.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveDirection == MoveDirection.Z)
        {
            transform.position += Vector3.forward * zDirection * moveSpeed * Time.deltaTime;

            if (transform.position.z >= 2)
            {
                zDirection = -1;
            }
            else if (transform.position.z <= -2)
            {
                zDirection = 1;
            }
        }
        else if (MoveDirection == MoveDirection.X)
        {
            transform.position += Vector3.right * xDirection * moveSpeed * Time.deltaTime;

            if (transform.position.x >= 2)
            {
                xDirection = -1;
            }
            else if (transform.position.x <= -2)
            {
                xDirection = 1;
            }
        }
    }
}
