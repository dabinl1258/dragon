using UnityEngine;
using System.Collections;

public class DPlayers : MonoBehaviour
{
    

    #region Physics Variable

    private Vector3 pos; // 목표 위치
    private Camera cam = null;
    public float lerpSpeed; // 이동속도
    public float stopGravityConst = 0.3f;// 중력이 멈추는 범위

    public float dragonSpeed = 10.0f;
    public GameObject dragonObj = null;
    private bool drgonMode = true;// 드레곤을 타고 있을 때

    private bool flyState = false; // 나는 경우(fly)  true아닌 경우 false
    private float gravityScale; // 중력
    #endregion

    #region Variable

    int combo = 0;
    public Animator animator = null;
    public string deadScene;
    public static DPlayers instance = null;


    public GameObject particle = null;

    public bool isMagnet = true;


    private Vector3 screenCal;

    private Vector3 movePosition;
   
    #endregion

    #region state Valiable
    [SerializeField]
    private float energy = 100.0f;

    private bool hyperAble = false;
    public GameObject hyperEffect = null;
    public DAfterImage afterImage = null;
    #endregion

    #region HyperValiable
    public float hyperSpeed = 10.0f;
    public float hyperVerticalSpeed = 1.0f;
    public float hyperVerticalRange = 1.0f;
    #endregion

    #region Virtual Function
    // Use this for initialization
    void Start()
    {
        hyperEffect.SetActive(hyperAble);
        cam = GameObject.Find("3DGameCamera").camera;
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
        if (Input.GetMouseButtonDown(0) || hyperAble)
        {
            rigidbody2D.velocity = Vector2.zero;
            if (transform.position.x < 0.0f && !hyperAble)
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
    private void HyperFixedUpdate()
    {
        
        transform.position = new Vector3(transform.position.x + Time.deltaTime * hyperSpeed, Mathf.Sin(Time.time * hyperVerticalSpeed) * hyperVerticalRange, 0);
        print("코드가 사용됨");
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
        if(hyperAble)
        {
            HyperFixedUpdate();
            return ;
        }
        rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
        if(flyState)
            transform.position = Vector3.Lerp(transform.position, pos, Time.unscaledDeltaTime * lerpSpeed);
        StopGravity();
        rigidbody2D.gravityScale = gravityScale;
        if(flyState)
            rigidbody2D.gravityScale = 0;
        
    }
    #endregion 

    #region Hyper Function

    IEnumerator HyperTimer(float _time)
    {
        yield return new WaitForSeconds(_time);
        DisEnableHyper();
    }

    public void EnableHyper(float _time = 8.0f)
    {
        rigidbody2D.gravityScale = 0.0f;
        StartCoroutine( HyperTimer(DPlayerData.instance.itemTimeRate *_time));
        hyperAble = true;
        hyperEffect.SetActive(hyperAble);
    }

    private void DisEnableHyper()
    {
        rigidbody2D.gravityScale = gravityScale;
        hyperAble = false;
        hyperEffect.SetActive(hyperAble);
        pos = transform.position;
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

            afterImage.NotSetPos();
            DInGameScore.instance.UpScore(combo * combo * 100);
            combo = 0;
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

        afterImage.Line(transform.position);
        flyState = true;
        DoAnimation("Attack");
        gameObject.SetActive(false);
        gameObject.SetActive(true);
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
        //_item.SendMessage("HitWithPlayer");
    }

    void TriggerWithEnemy(GameObject _enemy)
    {
        if (flyState) {
            DLoad.Instance.CallHitWithPlayer(_enemy.name);
            //_enemy.SendMessage("HitWithPlayer");
            //_enemy.GetComponent<DEnemyObj>().HitWithPlayer();
        }
    }
    #endregion

    #region ChaingevalueFunction 밖에서 값을 변경하는 함수
    public void RestEnegy(float _restEnegy)
    {
        energy -= _restEnegy * DPlayerData.instance.restEnergyRate;
    }
    public void UpCombo()
    {
        combo++;
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

    #region ItemEffects

    public bool getOnMagnet()
    {
        return isMagnet;
    }

    public void OnMagnet()
    {
        isMagnet = true;
    }

    #endregion


}
