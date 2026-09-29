using UnityEngine;

public class CubeSpawner : MonoBehaviour
{ //shout out to the tutorial by Jason Weiman, this script is based on his tutorial on how to make a stacker game, link: https://www.youtube.com/watch?v=-6LRXBtbNHg
// iadded some changes, check ouut stardance devlogs
    [SerializeField]
    private MovingCube cubePrefab;
    [SerializeField]
    private MoveDirection moveDirection;
    public void SpawnCube() // declares this function which is used by other scripts, spanws a cube by finding the last cube and placing it on top of it, if there is no last cube, it will spawn it at the start position
    {
        var cube = Instantiate(cubePrefab);
        if (MovingCube.LastCube != null && MovingCube.LastCube.gameObject != GameObject.Find("Start"))
        {
            float x = moveDirection == MoveDirection.X ? transform.position.x : MovingCube.LastCube.transform.position.x; //cool way of doing an if statement i found, if the move direction is x, it will move the cube in the x direction, if not, it will move it in the z direction, this is because the cubes are moving in different directions depending on the player
            float z = moveDirection == MoveDirection.Z ? transform.position.z : MovingCube.LastCube.transform.position.z;
            
            cube.transform.position = new Vector3(
                x,
                MovingCube.LastCube.transform.position.y + cubePrefab.transform.localScale.y,
                z
            );
        }
        else
        {
            cube.transform.position = transform.position;
        }
        cube.Initialize(moveDirection);
    }

    private void OnDrawGizmos() //just for the devs to see where the cube will spawn, not used in the actual game
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, cubePrefab.transform.localScale);
    }
}