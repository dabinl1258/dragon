using UnityEngine;
using System.Collections;

public class DPlayers : MonoBehaviour
{

    #region Physics Variable

    private Vector3 pos; // 목표 위치
    public Camera cam = null;
    public float lerpSpeed; // 이동속도
    public float stopGravityConst = 0.3f;// 중력이 멈추는 범위

    public float dragonSpeed = 10.0f;
    public GameObject dragonObj = null;
    private bool drgonMode = true;// 드레곤을 타고 있을 때

    private bool flyState = false; // 나는 경우(fly)  true아닌 경우 false
    private float gravityScale; // 중력

    #endregion

    #region Variable

    public DSmoothCamera smoothCamera = null;
    public Animator animator = null;
    public string deadScene;
    public static DPlayers instance = null;

    [SerializeField]
    private float  energy = 100.0f;

    public GameObject audioListener= null;
    public AudioClip audioClip = null;
    public GameObject particle = null;

    


    private Vector3 screenCal;

    private Vector3 movePosition;
   
    #endregion

    #region Virtual Function
    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1.0f;
        instance = this;
        screenCal = new Vector3(Screen.width / 2, Screen.height / 2);
        pos = transform.position;
        gravityScale = rigidbody2D.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (drgonMode)
        {
            DragonUpdate();
            return;
        }
        GameUpdate();
        CheckDead();

        
    }

    

    void FixedUpdate()
    {
        if (drgonMode)
        {
            DragonFixedUpdate();
            return;
        }
        GameFixedUpdate();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Enemy")) 
        {
            TriggerWithEnemy(coll.gameObject); return;
        }
        if(coll.CompareTag("Item"))
        {
            TriggerWithItem(coll.gameObject); return;
        }
        
    }
    #endregion

    #region Custom Update
    private void DragonUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (transform.position.x < 0.0f)
                return;
            pos = transform.position;
            Fly();
            Destroy(dragonObj);
            drgonMode = false;
        }
    }

    private void DragonFixedUpdate()
    {
        rigidbody2D.velocity = new Vector2(dragonSpeed, 0);
        rigidbody2D.gravityScale = 0.0f;
    }

    private void GameUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fly();
        }
        
        CheckState();
    }

    private void GameFixedUpdate()
    {
        rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
        if(flyState)
            transform.position = Vector3.Lerp(transform.position, pos, Time.unscaledDeltaTime * lerpSpeed);
        StopGravity();
        rigidbody2D.gravityScale = gravityScale;
        if(flyState)
            rigidbody2D.gravityScale = 0;
        
    }
    #endregion 

    #region Custum Function
    private void StopGravity()
    {
        
        if (flyState)
            rigidbody2D.velocity = Vector2.zero;
    }

    private void CheckState()
    {
        if (Vector3.Distance(transform.position, pos) < stopGravityConst)
        {
            flyState = false;
            smoothCamera.noetAble = flyState;
            transform.localEulerAngles = Vector3.zero;
        }
    }

    private void Fly()
    {
        if (energy <= 0.0f)
            return;
        if (flyState)
            return;
        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        float rate = ray.origin.z / ray.direction.z;
        pos = ray.origin - ray.direction * rate;
        Vector2 angle = new Vector2(transform.position.x - pos.x , transform.position.y - pos.y);

        transform.localEulerAngles = new Vector3(0, 0,Mathf.Rad2Deg * Mathf.Atan2( transform.position.y - pos.y , transform.position.x - pos.x ) - 180);
        transform.localScale = Vector3.one;
        if(transform.position.x > pos.x)
        {
            //transform.localEulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(transform.position.y - pos.y, transform.position.x - pos.x) - 180);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        //if (transform.position.x > pos.x)
        //{
        //    pos = transform.position;
        //    return;
        //}
        
        flyState = true;
        smoothCamera.noetAble = flyState;
        DoAnimation("Attack");
        audioListener.SendMessage("PlayAudio", audioClip);
    }
    private void Dead()
    {
        Application.LoadLevel(deadScene);
    }

    private void CheckDead()
    {
        if(transform.position.y < -10)
        {
            Dead();
        }
    }

    private void DoAnimation(string _animationName)
    {
        animator.Play(_animationName);
    }

    void TriggerWithItem(GameObject _item)
    {
        _item.SendMessage("HitWithPlayer");
    }

    void TriggerWithEnemy(GameObject _enemy)
    {
        if(flyState )
            _enemy.SendMessage("HitWithPlayer");
    }
    #endregion

    #region ChaingevalueFunction 밖에서 값을 변경하는 함수
    public void RestEnegy(float _restEnegy)
    {
        energy -= _restEnegy;
    }
    #endregion

    #region math
    private float ContAngle(Vector3 fwd, Vector3 targetDir)
    {
        float angle = Vector3.Angle(fwd, targetDir);

        if (AngleDir(fwd, targetDir, Vector3.up) == -1)
        {
            angle = 360.0f - angle;
            if (angle > 359.9999f)
                angle -= 360.0f;
            return angle;
        }
        else
            return angle;
    }

    private int AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0.0)
            return 1;
        else if (dir < 0.0)
            return -1;
        else
            return 0;
    }
    #endregion 

}
