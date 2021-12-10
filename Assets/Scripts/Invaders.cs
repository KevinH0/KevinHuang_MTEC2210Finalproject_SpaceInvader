using UnityEngine;
using UnityEngine.SceneManagement;
public class Invaders : MonoBehaviour
{
    public EnemyScript[] prefabs;
    public int rows = 5;
    public int columns = 11;
    //An XY graph. X is a percentage and Y is the speed.
    public AnimationCurve speed;
    public BulletScript EBulletprefab;
    //half-public, half private
    public int amountKilled { get; private set; }
    public int totalInvaders => this.rows * this.columns;
    public float percentKilled => (float)this.amountKilled / (float)this.totalInvaders;
    private Vector3 _direction = Vector2.right;
    public float Atkspeed = 1.0f;
    public int amountAlive => this.totalInvaders - this.amountKilled;
    private void Awake()
    {
        for(int row = 0; row < this.rows; row++)
        {//row is y axis Width and height is read to read the size of our camera to center it
            float width = 2.0f * (this.columns - 1);
            float height = 2.0f * (this.rows - 1);
            Vector2 centering = new Vector2(-width / 2, -height/2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * 2.0f), 0.0f);
            for(int col = 0; col < this.columns; col++)
            {//column is x axis
                EnemyScript invader = Instantiate(this.prefabs[row], this.transform);
                invader.killed += InvaderKilled;

                Vector3 position = rowPosition;
                position.x += col * 2.0f;
                //localposition makes the position where the parent is
                invader.transform.localPosition = position;
            }
        }
    }
    private void Start()
    {
        InvokeRepeating(nameof(Atk), this.Atkspeed, this.Atkspeed);
    }
       private void Update()
    {
        this.transform.position += _direction * this.speed.Evaluate(this.percentKilled) * Time.deltaTime;
        //Changes viewport coordinates to worldspace coordinates, basically the camera sides
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        //checking if the invader touches the edge of the screen
        foreach(Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }
            if(_direction == Vector3.right && invader.position.x >= rightEdge.x - 1.0f)
            {
                AdvanceRow();
            }else if(_direction == Vector3.left && invader.position.x <=(leftEdge.x + 1.0f))
            {
                AdvanceRow();
            }
        }
    }
    private void AdvanceRow()
    {
        _direction *= -1.0f;
        Vector3 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position;
    }
    private void Atk()
    {
        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }
            if(Random.value < (1.0f / (float)this.amountAlive))
            {
                Instantiate(this.EBulletprefab, invader.position, Quaternion.identity);
                break;
            }
        }
    }
    private void InvaderKilled()
    {
        this.amountKilled++;
        if(this.amountKilled >= this.totalInvaders)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}