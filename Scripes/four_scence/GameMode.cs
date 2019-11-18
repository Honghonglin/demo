using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameMode : MonoBehaviour
{
    public static GameMode instance;


    [HideInInspector]
    public GameObject guideobj;

    private int buildfound = 0;

    public GameObject roadTemplate;
    public Transform guideTrs;
    public List<GameObject> roads;


    public bool isBuilDirRoad;
    int dirRoadType;
    int dirRoadNumber;
    [HideInInspector]
    public int goldNumber;
    public Text numberText;


    private void Awake()
    {
        instance = this; 
    }

    private void Start()
    {
        numberText = GameObject.Find("Canvas/GoldNumber").GetComponent<Text>();
        guideobj = Instantiate(roadTemplate);
        guideobj.transform.position = Vector3.zero;
        guideobj.transform.rotation = Quaternion.identity;
        guideobj.name = "Gold";
        guideTrs = guideobj.transform;

        //预先生成20格道路
        for (int i = 0; i < 20;i++)
        {
            var tmp = Instantiate(roadTemplate, guideTrs.position, guideTrs.rotation);
            guideTrs.position += guideTrs.forward;
        }

        for(int i=0;i<30;i++)
        {
            BuildRoad();
        }


    }




    private void Update()
    {
        numberText.text = "当前金币数：" + goldNumber.ToString();
    }


    public void BuildRoad()
    {

        if(isBuilDirRoad&&dirRoadNumber>0)//////////////
        {
            switch (dirRoadType)
            {
                case 1:
                    BuildUpTerrain();
                    dirRoadNumber--;
                    break;
                case 2:
                    BuildDownTerrain();
                    dirRoadNumber--;
                    break;
                case 3:
                    BuildLeftTerrain();
                    dirRoadNumber--;
                    break;
                case 4:
                    BuildRightTerrain();
                    dirRoadNumber--;
                    break;
                    
            }
            if(dirRoadNumber<=0)
            {
                isBuilDirRoad = false;
            }
        }
        else
        {
            int turnSeed = Random.Range(1, 10);
            if (turnSeed == 1 && buildfound <= 0)
            {
                buildfound = 10;
                int dictionSeed = Random.Range(1, 3);
                for (int i = 0; i < 3; i++)
                {
                    var tmpRoad = Instantiate(roadTemplate, guideTrs.position, guideTrs.rotation);
                    PlayerRoadAnimator(tmpRoad);
                    roads.Add(tmpRoad);
                    guideTrs.position += guideTrs.forward;
                }
                if (dictionSeed == 1)
                {
                    guideTrs.position -= guideTrs.forward * 2;
                    guideTrs.Rotate(Vector3.up, 90);
                    guideTrs.position += guideTrs.forward * 2;
                }
                else
                {
                    guideTrs.position -= guideTrs.forward * 2;
                    guideTrs.Rotate(Vector3.up, -90);
                    guideTrs.position += guideTrs.forward * 2;
                }
            }
            else if(turnSeed==3)
            {
                int trunTerrain = Random.Range(1, 5);
                isBuilDirRoad = true;
                dirRoadType = trunTerrain;
                dirRoadNumber = 10;
                switch (dirRoadType)
                {
                    case 1:
                        BuildUpTerrain();
                        dirRoadNumber--;
                        break;
                    case 2:
                        BuildDownTerrain();
                        dirRoadNumber--;
                        break;
                    case 3:
                        BuildLeftTerrain();
                        dirRoadNumber--;
                        break;
                    case 4:
                        BuildRightTerrain();
                        dirRoadNumber--;
                        break;

                }
            }
            else if(turnSeed==6)
            {
                
            }
            else
            {
                var tmpRoad = Instantiate(roadTemplate, guideTrs.position, guideTrs.rotation);
                PlayerRoadAnimator(tmpRoad);
                roads.Add(tmpRoad);
                guideTrs.position += guideTrs.forward;
            }
            buildfound--;
        }

    }



    public void BuildTrapRoad()
    {
        guideTrs.position += guideTrs.forward;
        var tmpRoad = Instantiate(roadTemplate, guideTrs.position, guideTrs.rotation);
        var tmpController = tmpRoad.GetComponent<RoadController>();
        tmpController.ChangeRoadType();


        tmpRoad.transform.Rotate(Vector3.up, 90);
        int trapType = Random.Range(1, 4);
        switch(trapType)
        {
            case 1:
                tmpRoad.transform.position += tmpRoad.transform.forward;
                break;
            case 2:
                break;
            case 3:
                tmpRoad.transform.position -= tmpRoad.transform.forward;
                break;
        }
        guideTrs.position += guideTrs.forward * 2;
        PlayerRoadAnimator(tmpRoad);
        roads.Add(tmpRoad);

    }

    public void PlayerRoadAnimator(GameObject road)
    {
        var tmpRoadController = road.GetComponent<RoadController>();
        if (tmpRoadController == null)
            return;
        tmpRoadController.ChangeChildrens();
        

    }


    public void BuildUpTerrain()
    {
        var tmpRoad = Instantiate(roadTemplate, guideTrs.position, guideTrs.rotation);
        PlayerRoadAnimator(tmpRoad);
        roads.Add(tmpRoad);
        guideTrs.position += guideTrs.forward;
        guideTrs.position += guideTrs.up * 0.2f;

    }

    public void BuildDownTerrain()
    {
        var tmpRoad = Instantiate(roadTemplate, guideTrs.position, guideTrs.rotation);
        PlayerRoadAnimator(tmpRoad);
        roads.Add(tmpRoad);
        guideTrs.position += guideTrs.forward;
        guideTrs.position -= guideTrs.up * 0.2f;

    }
    public void BuildLeftTerrain()
    {
        var tmpRoad = Instantiate(roadTemplate, guideTrs.position, guideTrs.rotation);
        PlayerRoadAnimator(tmpRoad);
        roads.Add(tmpRoad);
        guideTrs.position += guideTrs.forward;
        guideTrs.position -= guideTrs.right * 0.2f;

    }
    public void BuildRightTerrain()
    {
        var tmpRoad = Instantiate(roadTemplate, guideTrs.position, guideTrs.rotation);
        PlayerRoadAnimator(tmpRoad);
        roads.Add(tmpRoad);
        guideTrs.position += guideTrs.forward;
        guideTrs.position += guideTrs.right * 0.2f;

    }

    public void CloseRoadAnimator()
    {
        if (roads.Count <= 0) { return; }
        var tmpRoadController = roads[0].GetComponent<RoadController>();
        if(tmpRoadController!=null)
        {
            tmpRoadController.InitChildrens();
        }
        roads.RemoveAt(0);
    }

}
