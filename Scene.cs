using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct Point
{
    public int x, z;
    public Point(int pointX, int pointZ)
    {
        x = pointX;
        z = pointZ;
    }
    public override string ToString()
    {
        return "(" + x + " , " + z + ")";
    }
}

public class Data
{
    public const int cellWidth = 13;
    public const int cellHeight = 17;
    public enum cellTypes { empty, ground,  targetTower, cannonTower , trapGround, startGround};
}

public class Cell
{
    public Data.cellTypes kind = Data.cellTypes.empty;
    public bool IsEmpty
    {
        get { return kind == Data.cellTypes.empty; }
    }
}

public class Scene : MonoBehaviour
{
    public static int groundNumber = 34;

    public static int cannonNumber = 4;
    public static int cnstepNumber = 4;
    public static int trapNumber = 4;
    public static int tpstepNumber = 4;
    public static int iceNumber = 3;
    public static int icestepNumber = 3;
    public static int dropdownNumber = 3;
    public static int ddNumber = 3;

    public static int stoneNumber = 2;
    public static int stNumber = 2;

    public static int mon1_Number = 3;
    public static int mon1_leftNumber = 3;
    public static int mon2_Number = 3;
    public static int mon2_leftNumber = 3;

    public static int mon3_Number = 3;
    public static int mon3_leftNumber = 3;

    public static int mon4_Number = 3;
    public static int mon4_leftNumber = 3;

    public static int mon5_Number = 3;
    public static int mon5_leftNumber = 3;

    public static int mon6_Number = 3;
    public static int mon6_leftNumber = 3;

    public static int portalNumber = 2;
    public static int ptNumber = 2;

    public static bool isSetGround = false;
    public static bool isSetPortal = false;
    public static bool isReset = false;

    public enum SetStatus
    {
        Ground,
        Tower,
        Trap,
        Ice,
        dropDown,
        Stone,
    }
    public static SetStatus setStatus = SetStatus.Ground;

    public enum GameStatus
    {
        Attack,
        Defense,
    }
    public static GameStatus gameStatus = GameStatus.Defense;

    public GameObject piller;
    public GameObject targetTower;
    public Transform pillerParent;
    public GameObject trap;
    public Transform trapParent;
    public GameObject portal;
    public Transform portalParent;
    public GameObject ice;
    public Transform iceParent;
    public GameObject dropDown;
    public Transform dropParent;
    public GameObject stone;
    public Transform stoneParent;

    public Collider cellCollider;
    public Transform colliderParent;
    private List<EmptyCollider> colliderList = new List<EmptyCollider>();
    public Cell[,] cells = new Cell[Data.cellWidth, Data.cellHeight];
    public const float cellWidth = 0.987f;
    public const float cellHeight = 0.987f;
    public UIManager bm;
    public static List<GameObject> enemies = new List<GameObject>();

    public static Transform[] pillersTransform;
    private Point[] pointArray;
    public Transform piParent;

    public GameObject cannon;
    public Transform towerParent;
    public static Cannon[] cannonArray;

    public GameObject stPoint;
    public static Transform startPoint;

    public Camera mainCamera;
    public static int stepNumber = 0;

    private Point previousPoint, currentPoint;
    public Color32 color1;
    public Color32 color2;

    void Awake()
    {
        pillersTransform = new Transform[groundNumber + 1];
        pointArray = new Point[groundNumber + 1];
        cannonArray = new Cannon[cannonNumber];
    }

    void Start()
    {
        Debug.Log("gameStatus :" + gameStatus);
        SetColliders();
    }

    public void SetColliders()
    {
        for (int x = 0; x < Data.cellWidth; x++)
        {
            for (int z = 0; z < Data.cellHeight; z++)
            {
                cells[x, z] = new Cell();
                Collider cc = Instantiate(cellCollider) as Collider;
                cc.transform.parent = colliderParent;
                cc.transform.localPosition = new Vector3(-x * cellWidth, 4.0f, -z * cellHeight);
                cc.transform.localScale = Vector3.one;
                EmptyCollider pr = cc.GetComponent<EmptyCollider>();
                pr.cell = cells[x, z];
                pr.point = new Point(x, z);
                colliderList.Add(pr);
            }
        }
    }

    void Update()
    {
        if(gameStatus == GameStatus.Defense && setStatus == SetStatus.Ground)
        {
            SetGround();
        }
        if(gameStatus == GameStatus.Defense && setStatus == SetStatus.Tower)
        {
            SetTower();
        }
        if (gameStatus == GameStatus.Defense && setStatus == SetStatus.Trap)
        {
            SetTrap();
        }
        if (gameStatus == GameStatus.Defense && setStatus == SetStatus.Ice)
        {
            SetIce();
        }
        if (gameStatus == GameStatus.Defense && setStatus == SetStatus.dropDown)
        {
            SetDropDown();
        }
        if (gameStatus == GameStatus.Defense && setStatus == SetStatus.Stone)
        {
            SetStone();
        }
            if (gameStatus == GameStatus.Attack && isSetPortal == true)
        {
            SetPortal();
        }
        if (isReset == true)
        {
            ReSetAll();
        }
    }
    public void ChangeColor()
    {
        
    }

    public void SetGround()
    {
        if (Input.GetButton("Fire1"))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && isSetGround == false)
            {
                EmptyCollider ec = hit.collider.GetComponent<EmptyCollider>();
                if (hit.collider.tag == "EmptyCollider" && ec.cell.kind == Data.cellTypes.empty)
                {
                    if (stepNumber > 0)
                    {
                        currentPoint = hit.collider.GetComponent<EmptyCollider>().point;
                        pointArray[stepNumber] = currentPoint;
                        previousPoint = pointArray[stepNumber - 1];
                        if (!CheckClosePoint(previousPoint, currentPoint))
                        {
                            return;
                        }
                    }
                    else
                    {
                        previousPoint = hit.collider.GetComponent<EmptyCollider>().point;
                        pointArray[0] = previousPoint;
                    }

                    if (stepNumber < groundNumber)
                    {
                        GameObject pi = Instantiate(piller) as GameObject;
                        pi.transform.parent = piParent;
                        pi.transform.position = new Vector3(hit.collider.transform.position.x, 4.5f, hit.collider.transform.position.z);
                        pi.transform.localScale = Vector3.one;
                        pillersTransform[stepNumber] = pi.transform;
                        ec.cell.kind = Data.cellTypes.ground;
                        ec.setPoint = stepNumber;

                        SetStartPoint(ec);
                        stepNumber++;
                        bm.ShowText();
                    }
                    if (stepNumber == groundNumber)
                    {
                        GameObject tt = Instantiate(targetTower) as GameObject;
                        tt.transform.parent = piParent;
                        tt.transform.position = new Vector3(hit.collider.transform.position.x, 4.472f, hit.collider.transform.position.z);
                        tt.transform.localScale = Vector3.one;
                        pillersTransform[stepNumber] = tt.transform;
                        ec.cell.kind = Data.cellTypes.targetTower;
                        ec.setPoint = stepNumber;
                        stepNumber++;
                        isSetGround = true;
                    }
                    ChangeColor();
                }
            }
        }

    }

    public bool CheckClosePoint(Point point1, Point point2)
    {
        bool isPass = true;
        if ((Mathf.Abs(point2.x - point1.x)) + (Mathf.Abs(point2.z - point1.z)) > 1)
        {
            isPass = false;
        }
        //路與路不相臨
        //for (int i = 0; i < pointArray.Length; i++)
        //{
        //    if (!(pointArray[i].x == point1.x && pointArray[i].z == point1.z))
        //    {
        //        if ((Mathf.Abs(point2.x - pointArray[i].x) == 1 && point2.z == pointArray[i].z)
        //           || (Mathf.Abs(point2.z - pointArray[i].z) == 1 && point2.x == pointArray[i].x))
        //        {
        //            isPass = false;
        //        }
        //    }
        //}
        return isPass;
    }

    public void SetStartPoint(EmptyCollider ec)
    {
        if(stepNumber == 0)
        {
            GameObject stpointClone = Instantiate(stPoint) as GameObject;
            stpointClone.transform.parent = pillerParent;
            stpointClone.transform.position = new Vector3(pillersTransform[stepNumber].position.x, 4.52f, pillersTransform[stepNumber].position.z);
            stpointClone.transform.localScale = Vector3.one;
            startPoint = stpointClone.transform;
            ec.cell.kind = Data.cellTypes.startGround;
        }
    }

    public void SetTrap()
    {
        if (Input.GetButtonDown("Fire1") && tpstepNumber > 0)
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                EmptyCollider ec = hit.collider.GetComponent<EmptyCollider>();
                if (hit.collider.tag == "EmptyCollider" && ec.cell.kind == Data.cellTypes.ground)
                {
                    GameObject tp = Instantiate(trap) as GameObject;
                    tp.transform.parent = trapParent;
                    tp.transform.position = new Vector3(hit.collider.transform.position.x, 4.59f, hit.collider.transform.position.z);
                    tp.transform.localScale = Vector3.one;
                    ec.cell.kind = Data.cellTypes.trapGround;
                    tpstepNumber--;
                    bm.ShowText();
                }
            }
        }
    }
    public void SetIce()
    {
        if (Input.GetButtonDown("Fire1") && icestepNumber > 0)
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                EmptyCollider ec = hit.collider.GetComponent<EmptyCollider>();
                if (hit.collider.tag == "EmptyCollider" && ec.cell.kind == Data.cellTypes.ground)
                {
                    GameObject tp = Instantiate(ice) as GameObject;
                    tp.transform.parent = iceParent;
                    tp.transform.position = new Vector3(hit.collider.transform.position.x, 4.55f, hit.collider.transform.position.z);
                    tp.transform.localScale = new Vector3(1, 0.1f, 1);
                    ec.cell.kind = Data.cellTypes.trapGround;
                    icestepNumber--;
                    bm.ShowText();
                }
            }
        }
    }
    public void SetDropDown()
    {
        if (Input.GetButtonDown("Fire1") && ddNumber > 0)
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                EmptyCollider ec = hit.collider.GetComponent<EmptyCollider>();
                if (hit.collider.tag == "EmptyCollider" && ec.cell.kind == Data.cellTypes.ground)
                {
                    GameObject tp = Instantiate(dropDown) as GameObject;
                    tp.transform.parent = dropParent;
                    tp.transform.position = new Vector3(hit.collider.transform.position.x, 4.7f, hit.collider.transform.position.z);
                    tp.transform.localScale = Vector3.one * 0.5f;
                    ec.cell.kind = Data.cellTypes.ground;
                    ddNumber--;
                    bm.ShowText();
                }
            }
        }
    }
    public void SetStone()
    {
        if (Input.GetButtonDown("Fire1") && stNumber > 0)
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                EmptyCollider ec = hit.collider.GetComponent<EmptyCollider>();
                if (hit.collider.tag == "EmptyCollider" && ec.cell.kind == Data.cellTypes.ground)
                {
                    GameObject tp = Instantiate(stone) as GameObject;
                    tp.transform.parent = stoneParent;
                    tp.transform.position = new Vector3(hit.collider.transform.position.x, 4.7f, hit.collider.transform.position.z);
                    tp.transform.localScale = Vector3.one * 0.5f;
                    ec.cell.kind = Data.cellTypes.ground;
                    stNumber--;
                    bm.ShowText();
                }
            }
        }
    }
    public void SetTower()
    {
        if (Input.GetButtonDown("Fire1") && cnstepNumber > 0)
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                EmptyCollider ec = hit.collider.GetComponent<EmptyCollider>();
                if (hit.collider.tag == "EmptyCollider" && ec.cell.kind == Data.cellTypes.empty)
                {
                    GameObject cn = Instantiate(cannon) as GameObject;
                    cn.transform.parent = towerParent;
                    cn.transform.position = new Vector3(hit.collider.transform.position.x, 4.0f, hit.collider.transform.position.z);
                    cn.transform.localScale = Vector3.one;
                    ec.cell.kind = Data.cellTypes.cannonTower;
                    cannonArray[cannonNumber - cnstepNumber] = cn.GetComponent<Cannon>();
                    cnstepNumber--;
                    bm.ShowText();
                    //查看所有collider的Cell
                    //for (int x = 0; x < Data.cellWidth; x++)
                    //{
                    //    for (int z = 0; z < Data.cellHeight; z++)
                    //    {
                    //        Point p1 = new Point(x, z);
                    //        Debug.Log(p1 + ", " + cells[x, z].kind);
                    //    }
                    //}
                }
            }
        }

    }

    void ReSet(Transform ts)
    {
        int childInt = ts.childCount;
        for(int i = 0; i < childInt; i++)
        {
            Destroy(ts.GetChild(i).gameObject);
        }
    }

    void ReSetAll()
    {
        cnstepNumber = cannonNumber;
        tpstepNumber = trapNumber;
        icestepNumber = iceNumber;
        ddNumber = dropdownNumber;
        stNumber = stoneNumber;
        mon1_leftNumber = mon1_Number;
        mon2_leftNumber = mon2_Number;
        mon3_leftNumber = mon3_Number;
        mon4_leftNumber = mon4_Number;
        mon5_leftNumber = mon5_Number;
        mon6_leftNumber = mon6_Number;
        ptNumber = portalNumber;
        stepNumber = 0;
        gameStatus = GameStatus.Defense;
        setStatus = SetStatus.Ground;
        enemies.Clear();
        if(colliderList.Count > 0)
        {
            foreach (EmptyCollider ec in colliderList)
            {
                ec.cell.kind = Data.cellTypes.empty;
            }
        }
 
        ReSet(pillerParent);
        ReSet(trapParent);
        ReSet(portalParent);
        ReSet(towerParent);
        ReSet(iceParent);

        isReset = false;
        isSetGround = false;
        pillersTransform = new Transform[groundNumber + 1];
        pointArray = new Point[groundNumber + 1];
        cannonArray = new Cannon[cannonNumber];
        bm.ShowText();
    }

    void SetPortal()
    {
        if (Input.GetButtonDown("Fire1") && ptNumber > 0)
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                EmptyCollider ec = hit.collider.GetComponent<EmptyCollider>();
                if (hit.collider.tag == "EmptyCollider" && ec.cell.kind == Data.cellTypes.ground)
                {
                    GameObject pt = Instantiate(portal) as GameObject;
                    pt.transform.parent = portalParent;
                    pt.transform.position = new Vector3(hit.collider.transform.position.x, 4.5f, hit.collider.transform.position.z);
                    pt.transform.localScale = Vector3.one * 0.5f;
                    ptNumber--;
                    isSetPortal = false;
                    bm.ShowText();
                }
            }
        }
    }
}
