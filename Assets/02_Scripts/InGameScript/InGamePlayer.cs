using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
캐릭터가 지 좆대로 올라갑니다.
점수 체크 해주세요. 강하게 조심히 도약 햇살 구름링 같이 제가 한 부분은 제가 체크했습니둡
ex. ScoreManager.Score += 1000; //도약 했으니 1000점 추가
도약시점 체크해주세요.
=> 이부분 : if (FlyFlag == true) //끝에 다다르면 ===> 햇슴둥
 */
public class InGamePlayer : MonoBehaviour
{

    public static bool flyCheck = false;
    float BranchScore = 50;

    public GameObject _Player; //InGame의 player

    public GameObject StrongBtn; //강하게 버튼
    public GameObject WeakBtn; //약하게 버튼

    int JumpCount = 0; //점프 횟수 카운트
    int UpNum = 0; //밟은 발판 개수 카운트

    public GameObject BirdJump; //도약 슬라이더
    public GameObject Handle; //선택 바 (움직이는 바)

    Vector3 StartPosition; //선택 바 시작 위치
    public Vector3 EndPosition; //선택 바 끝나는 위치

    public float speed; //선택 바 움직이는 스피드

    int WangBog = 0; //영역 벗어난 횟수 카운트

    int CountCh = 0;

    public static int jumpNum = 0;
    public GameObject LeftJump; //왼쪽 도약 키
    public GameObject RightJump; //오른쪽 도약 키

    public static bool BigSuccess; //대성공
    public static bool Success; //성공

    public GameObject StopPanel;

    int ChangeDir;

    float LimitTime; //제한 시간 1분으로 
    float branchTimer; //나뭇가지 타이머
    float OverTimer; // 색상 변경용

    float initial_Speed = 1.0f; //발판인덱스 및 초기 속도 제어 (1부터 시작, 1초)
    float accel = 1.0f; //타이머가속

    /*============================================================*/

    //가지 오브젝트
    public GameObject BranchObject_normal1; //일반가지1
    public GameObject BranchObject_normal2; //일반가지2
    public GameObject BranchObject_leaf1; //나뭇잎가지1
    public GameObject BranchObject_leaf2; //나뭇잎가지2
    public GameObject BranchObject_flower1; //꽃1
    public GameObject BranchObject_flower2; //꽃2
    public GameObject BranchObject_fail; //시든가지
    public GameObject FlyBranch; //도약발판
    public GameObject StandardBranch; //기본 발판



    ////아이템 오브젝트
    //public GameObject Shield;
    //public GameObject superjump;
    //public GameObject booster;
    //public GameObject coin;
    //public GameObject score;
    //public GameObject flowerGarden;


    /*=========================== 리스트 =================================*/

    //기둥객체를 생성하고 관리해줄 리스트 생성
    List<GameObject> branchList = new List<GameObject>();

    //가지객체를 관리할 리스트 생성
    List<GameObject> leafList = new List<GameObject>();
    List<int> g_intList = new List<int>();
    List<bool> g_boolList = new List<bool>();

    //가지 랜덤번호를 관리할 리스트 생성
    List<int> leafNumList = new List<int>();

    //아이템객체를 관리할 리스트 생성
    List<GameObject> ItemList = new List<GameObject>();

    /*========================== 기둥용 ==================================*/


    //위치값을 받아올 기둥 빈오브젝트
    public GameObject emptybranch1;

    //기둥의 빈오브젝트의 위치값을 넣어줄 벡터
    private Vector3 lastBranchPos;

    //기둥의 빈오브젝트의 위치값을 넣어줄 벡터
    private Vector3 lastItemPos;


    //클론을 생성할 초기기둥 오브젝트
    public GameObject Branch1;
    public GameObject Branch2;
    public GameObject Branch3;


    //기둥의 회전 x값을 조정하는 변수
    int RandQNum;


    /*========================== 가지용 =================================*/

    //가지를 랜덤으로 생성할 오브젝트
    public GameObject RandomBranch_Right; //오른쪽
    public GameObject RandomBranch_Left; //왼쪽

    //가지의 right오브젝트의 위치값을 넣어줄 벡터
    private Vector3 RightBranchPos;

    //가지의 left오브젝트의 위치값을 넣어줄 벡터
    private Vector3 LeftBranchPos;

    // 가지종류전환을 위한 랜덤값생성위한 인티져값
    int RandBranchIndex;

    //아이템 종류 전환을 위한 랜덤값생성위한 인티져값
    int RandItemIndex;


    // 가지예외체크를 위한 이전가지의 종류값받아올 변수
    int PreNum = 10;

    //도약발판 생성용 플래그
    bool FlyFlag = false;

   
    void Start()
    {
        FlyBranch = GameObject.FindGameObjectWithTag("fly");

        //ClickButton.IsStrong = false;
        //ClickButton.IsWeak = false;
        ClickButton.IsLeftJump = false;
        ClickButton.IsRightJump = false;
        BigSuccess = false;
        Success = false;

        LeftJump.SetActive(false); //왼쪽 도약 안보이게함
        RightJump.SetActive(false); //오른쪽 도약 안보이게함
        BirdJump.SetActive(false); //도약 바 안보이게함
        StopPanel.SetActive(false); //옵션 창 안보이게 함

        StartPosition = Handle.transform.position;//시작위치

        //코루틴함수 호출
        StartCoroutine(BranchRandomGenerator());

        //벡터에 기둥의 빈오브젝트의 위치값을 넣어준다
        lastBranchPos = this.emptybranch1.transform.position;

        //벡터에 가지의 오른쪽 빈오브젝트의 위치값을 넣어준다
        RightBranchPos = this.RandomBranch_Right.transform.position;

        //벡터에 가지의 왼쪽 빈오브젝트의 위치값을 넣어준다
        LeftBranchPos = this.RandomBranch_Right.transform.position;

        LimitTime = 5;
    }



    void Update()
    {
        //Debug.Log(StartPosition.x);

        ////강하게 눌렀을 때 + 약하게 안 눌렀을 때 + 두 칸 다 올라왔으면
        //if (ClickButton.IsStrong && ClickButton.IsWeak == false && JumpCount == 0)
        //{
        //    //클론이 하나 남았는데 강하게를 누를 수 없도록 하기 위한 방법 필요할듯
        //    //한 칸씩 두 칸 올라가기
        //    // StartCoroutine("Sleep"); //한 칸 올리고 해당 초동안 멈추고 한 칸 더 올리기
        //    //한 번에 두 칸 올라가기
        //    for (int i = 0; i < 2; i++)
        //    {
        //        JumpCount++;
        //        UpNum++; //발판 개수 증가

        //        ScoreManager.Score += BranchScore * 2f; //점수 두배 증가
        //        if (UpNum != 1)
        //        {
        //            _Player.transform.position = new Vector3(lastItemPos.x, lastItemPos.y + 13, lastItemPos.z);
        //        }
        //        if (JumpCount == 2) //발판을 두 번 밟으면
        //        {
        //            ClickButton.IsStrong = false; //강하게 누른 것이 풀림
        //            JumpCount = 0; //점프 횟수를 초기화
        //        }
        //    }
        //}

        ////약하게 눌렀을 때 + 강하게 안 눌렀을 때 + 발판에 닿았을 때
        //if (ClickButton.IsWeak && ClickButton.IsStrong == false)
        //{
        //    JumpCount++;
        //    UpNum++;

        //    ScoreManager.Score += BranchScore * 1f; //점수 한배 증가

        //    if (g_intList[CountCh] == 1)//
        //    {
        //       if (UpNum != 1)
        //        {
        //            _Player.transform.position = new Vector3(lastItemPos.x, lastItemPos.y + 13, lastItemPos.z);
        //        }
        //        CountCh++;
        //    }

        //    else if(g_intList[CountCh] == 0)
        //    {
        //        if (UpNum != 1)
        //        {
        //            _Player.transform.position = new Vector3(lastItemPos.x, lastItemPos.y + 13, lastItemPos.z);
        //        }
        //        CountCh++;
        //    }

        //    if (JumpCount >= 1)
        //    {
        //        JumpCount = 0;
        //        ClickButton.IsWeak = false;
        //    }
        //}

        if (FlyFlag == true && flyCheck == true) //끝에 다다르면
        {
            StrongBtn.SetActive(false); //강하게 키 안보이게함
            WeakBtn.SetActive(false); //약하게 키 안보이게함

            LeftJump.SetActive(true); //왼쪽 도약 보이게함
            RightJump.SetActive(true); //오른쪽 도약 보이게함
            BirdJump.SetActive(true); //도약 바 보이게함

            // 대성공_StartPosition.x = 125, 대성공_EndPosition.x = 155,
            // 성공_StartPosition.x = 85, 성공_EndPosition.x = 190

            //오른쪽 도약 키, 왼쪽 도약 키 하나라도 눌렀으면
            if (ClickButton.IsRightJump || ClickButton.IsLeftJump)
            {
                //실패 영역에서 눌렀으면 게임 종료
                if ((Handle.transform.position.x >= StartPosition.x && Handle.transform.position.x <= 85) ||
                (Handle.transform.position.x >= 190 && Handle.transform.position.x <= EndPosition.x))
                {
#if UNITY_EDITOR

                    UnityEditor.EditorApplication.isPlaying = false;

#elif UNITY_WEBPLAYER

               Application.OpenURL("http://google.com");

#else

                 Application.Quit();

#endif
                }

                //성공 영역에서 눌렀으면
                if ((Handle.transform.position.x >= 40 && Handle.transform.position.x <= 125) ||
                (Handle.transform.position.x >= 155 && Handle.transform.position.x <= 243))
                {
                    ScoreManager.Score += 1000; //도약 했으니 1000점 추가
                    SceneManager.LoadScene("TestFly"); //Fly 시작
                    Success = true;
                }
                //대성공 영역에서 눌렀으면
                if(Handle.transform.position.x >= 125 && Handle.transform.position.x <= 155)
                {
                    ScoreManager.Score += 1000; //도약 했으니 1000점 추가
                    SceneManager.LoadScene("TestFly");
                    BigSuccess = true;
                }
            }
//            //오른쪽 도약 키, 왼쪽 도약 키 하나라도 안 눌렀으면 게임 종료
//            else
//            {
//#if UNITY_EDITOR

//                UnityEditor.EditorApplication.isPlaying = false;

//#elif UNITY_WEBPLAYER

//               Application.OpenURL("http://google.com");

//#else

//                 Application.Quit();

//#endif
//            }
            //선택 바의 좌표가 끝나는 좌표보다 크거나 같으면 또는 선택 바의 좌표가 시작 좌표보다 작으면 (빨간 영역 넘어가면)
            if (Handle.transform.position.x >= EndPosition.x || Handle.transform.position.x < StartPosition.x)
            {
                //(+)인 스피드에 (-)를 곱하여 (-)로 바꿔 왼쪽(<-)으로 움직이게 함
                //(-)인 스피드에 (-)를 곱하여 (+)로 바꿔 오른쪽(->)으로 움직이게 함
                speed = speed * (-1.0f);
                WangBog++; //벗어난 영역 횟수 증가
            }
            //선택 바 위치 움직이기
            Handle.transform.position = new Vector3(Handle.transform.position.x + speed, Handle.transform.position.y, 0);

            if (WangBog == 4) //영역 네 번 벗어나면 = 왕복 두 번
            {
                UpNum = 0; //발판 밟은 개수 초기화
                LeftJump.SetActive(false); //왼쪽 도약 안보이게함
                RightJump.SetActive(false); //오른쪽 도약 안보이게함
                BirdJump.SetActive(false); //도약 바 안보이게함
                WangBog = 0; //벗어난 영역 횟수 초기화

                //게임종료
            }
        }

        //if (ClickButton.IsStop) //옵션을 눌러 켜졌으면
        //{
        //    Time.timeScale = 0; //시간 멈춤
        //    //BackSound.IsOff = false; //노래는 멈추지 않게 함
        //    //BackSound.IsOn = true;
        //    StopPanel.SetActive(true); //옵션 판넬 보이게 함
        //}
        //if (!ClickButton.IsStop) //옵션을 눌러 꺼졌으면
        //{
        //    Time.timeScale = 1; //시간 움직임
        //    StopPanel.SetActive(false); //옵션 판넬 안보이게 함
        //}

        LimitTime -= Time.deltaTime; // 일정한 시간에 따라 감소
        branchTimer += Time.deltaTime; // 일정한 시간에 따라 증가

        if (branchTimer >= 10) // 10초로 설정. (10초가 되면 standard 기둥과 나무를 빨강으로 변경)
        {
            Renderer overRend = Branch1.GetComponent<Renderer>(); // 아래쪽 기둥의 컴포넌트를 받아옴
            overRend.material.color = new Color(255, 0, 0); //색상변경 //우선 빨강으로 설정함

            Renderer overRend2 = StandardBranch.GetComponent<Renderer>();
            overRend2.materials[0].color = new Color(255, 0, 0);
            overRend2.materials[1].color = new Color(255, 0, 0);


            OverTimer += Time.deltaTime * accel; // 일정한 시간에 따라 증가 (가속도는 나중에)
        }


        if (OverTimer >= initial_Speed) //1초 (10초 이후 1초마다 그 다음가지, 기둥 색상변경 )
        {
            Renderer SoverRend = Branch2.GetComponent<Renderer>(); // 아래쪽 (2번쨰) 기둥의 컴포넌트를 받아옴
            SoverRend.material.color = new Color(255, 0, 0); //색상변경 //우선 빨강으로 설정함

            Renderer SoverRend2 = branchList[(int)initial_Speed].GetComponent<Renderer>();
            SoverRend2.materials[0].color = new Color(255, 0, 0); //색상변경 //우선 빨강으로 설정함

            Renderer SoverRend3 = leafList[(int)initial_Speed].GetComponent<Renderer>();

            //가지마다 material 인스턴스 개수가 달라 인스턴스 개수마다 묶어서 색지정,
            if (leafNumList[(int)initial_Speed] == 0 || leafNumList[(int)initial_Speed] == 1
             || leafNumList[(int)initial_Speed] == 6)
            {
                SoverRend3.materials[0].color = new Color(255, 0, 0);//색상변경 //우선 빨강으로 설정함
            }


            else if (leafNumList[(int)initial_Speed] == 2 || leafNumList[(int)initial_Speed] == 3)
            {
                SoverRend3.materials[0].color = new Color(255, 0, 0);//색상변경 //우선 빨강으로 설정함
                SoverRend3.materials[1].color = new Color(255, 0, 0);//색상변경 //우선 빨강으로 설정함
            }

            else if (leafNumList[(int)initial_Speed] == 4 || leafNumList[(int)initial_Speed] == 5)
            {
                SoverRend3.materials[0].color = new Color(255, 0, 0);//색상변경 //우선 빨강으로 설정함
                SoverRend3.materials[1].color = new Color(255, 0, 0);//색상변경 //우선 빨강으로 설정함
                SoverRend3.materials[2].color = new Color(255, 0, 0);//색상변경 //우선 빨강으로 설정함
            }

            //Debug.Log("오버타임" + OverTimer);
            //Debug.Log("테스트  번호" + initial_Speed);

            ++initial_Speed;
        }
    }

    IEnumerator Sleep()
    {
        JumpCount++; //점프 횟수 증가
        UpNum++; //발판 개수 증가
                 //해당 발판 위치로 플레이어 옮김. y + 3 한 이유 : 플레이어가 발판 사이에 끼여서 조금 띄움
        _Player.transform.position = new Vector3(lastItemPos.x, lastItemPos.y + 30, lastItemPos.z);
        yield return new WaitForSeconds(0.3f); //0.3초간 멈춤
        JumpCount++;
        UpNum++;
        _Player.transform.position = new Vector3(lastItemPos.x, lastItemPos.y + 30, lastItemPos.z);
        if (JumpCount == 2) //발판을 두 번 밟으면
        {
            //ClickButton.IsStrong = false; //강하게 누른 것이 풀림
            JumpCount = 0; //점프 횟수를 초기화
        }
    }

    //========================기둥, 가지, 아이템 생성부=========================//
    IEnumerator BranchRandomGenerator()
    {
        ////아이템을 랜덤으로 생성할 배열
        //GameObject[] ItemArray = new GameObject[6];

        //가지를 랜덤으로 생성할 배열
        GameObject[] BranchArray = new GameObject[7];

        ////////*아이템배열에 오브젝트들을 넣어줌*////////
        //ItemArray[0] = Shield;
        //ItemArray[1] = superjump;
        //ItemArray[2] = booster;
        //ItemArray[3] = coin;
        //ItemArray[4] = score;
        //ItemArray[5] = flowerGarden;

        ////////*가지배열에 오브젝트들을 넣어줌*////////
        // 노말
        BranchArray[0] = BranchObject_normal1;
        BranchArray[1] = BranchObject_normal2;

        //나뭇잎
        BranchArray[2] = BranchObject_leaf1;
        BranchArray[3] = BranchObject_leaf2;

        //꽃
        BranchArray[4] = BranchObject_flower1;
        BranchArray[5] = BranchObject_flower2;

        //시든
        BranchArray[6] = BranchObject_fail;

        ////아이템 스케일을 변환시켜준다.
        //Shield.transform.localScale = new Vector3(0.086578f, 20, 20);
        //superjump.transform.localScale = new Vector3(0.086578f, 20, 20);
        //booster.transform.localScale = new Vector3(0.086578f, 20, 20);
        //coin.transform.localScale = new Vector3(0.086578f, 20, 20);
        //score.transform.localScale = new Vector3(0.086578f, 20, 20);
        //flowerGarden.transform.localScale = new Vector3(0.086578f, 20, 20);


        //가지 스케일을 변환시켜준다.
        BranchObject_normal1.transform.localScale = new Vector3(122.3006f, 119.6194f, 103.9678f);
        BranchObject_normal2.transform.localScale = new Vector3(265.0465f, 261.7084f, 119.0892f);
        BranchObject_leaf1.transform.localScale = new Vector3(177.2886f, 118.1104f, 100.9569f);
        BranchObject_leaf2.transform.localScale = new Vector3(296.4975f, 172.7106f, 100.9569f);
        BranchObject_flower1.transform.localScale = new Vector3(34, 29, 44);
        BranchObject_flower2.transform.localScale = new Vector3(106, 117, 81);
        BranchObject_fail.transform.localScale = new Vector3(445.8f, 400, 549.2230f);

        //도약발판
        FlyBranch.transform.localScale = new Vector3(40.13705f, 1.30032f, 84.628f);

        //=================================================//
        //pos1과 pos2 ,pos3를 벡터로 선언
        Vector3 pos1, pos2, pos3;
        
        while (true) //나중에 조건식같은거를 넣어서 줄기생기는 갯수를 통제하도록 하자
        {
            //줄기의 쿼터니온을 조절 (270이 수직으로서는 각도 왼쪽으로 25도, 오른쪽으로 25도 하여 총 50도로 조절)
            RandQNum = Random.Range(245, 295); //245 ~ 294 랜덤으로 숫자 생성

            int teatrandnum = Random.Range(1, 11); //1~10까지 랜덤숫자 생성

            
     


            // << 처음 : 빈오브젝트의 위치>> 
            // << while 문 한번 돌고 : 리스트의 마지막에 있는 클론의 0,1,2번째 자식위치값>> 을 받은 벡터를 넣어준다.
            pos1 = lastBranchPos;
            pos2 = RightBranchPos;
            pos3 = LeftBranchPos;

            //가지생성방향 전환을 위한 랜덤값 생성

            //ChangeDir = Random.Range(0, 2);
            //오른쪽
            //if (ChangeDir == 0)
            //    a = true;
            //else if (ChangeDir == 1)
            //    a = false;

                // 가지종류전환을 위한 랜덤값생성
                // 예외 : 1번이 생성된다음 2번나올수 없고, 2번 다음 2번 나올수 없고, 2번 다음 1번 나올수 없다.

                //가지가 만들어졌는지 체크 -> 1오브젝트 위치에 1개만 만들수잇도록한다.
                bool checkCreate = false;


                //제한시간이 0보다 크거나 같을때 까지만 나뭇가지생성 (기둥은 계속 생성)
                if (LimitTime >= 0)
                {

                ChangeDir = Random.Range(0, 2);
                /*==========================아이템생성=========================*/

                //if (teatrandnum % 3 == 0) //1~11까지 중 3으로 나누어서 나머지가 0이 나오는 수 : 3,6,9 /// 10개 중 3개 -> 30%의 확률 
                //    {
                //        RandItemIndex = Random.Range(0, 6); //0~5까지

                //        GameObject _item = Instantiate(ItemArray[RandItemIndex], new Vector3(lastItemPos.x, lastItemPos.y + 30, lastItemPos.z), Quaternion.Euler(-0, 90, 0)) as GameObject;

                //        // 생성된 오브젝트를 leafList 에 add로 추가.
                //        ItemList.Add(_item);
                //    }

                    /*==========================오른쪽에 가지생성=========================*/

                    // 오른쪽에 가지가 생성되지않았다면 가지 생성
                    if (ChangeDir == 0 && checkCreate == false)
                    {

                        //만약 이전에 시든가지가 생성되었었다면
                        if (PreNum == 6 && checkCreate == false)
                        {
                            //랜덤값을 다시 계산하고
                            RandBranchIndex = Random.Range(0, 4); /*꽃가지는 올수없기때문에 0~3까지*/

                            leafNumList.Add(RandBranchIndex);

                            //노말가지 2일때의 쿼터니온 조절
                            if (RandBranchIndex == 1)
                            {
                                GameObject _leaf = Instantiate(BranchArray[RandBranchIndex], new Vector3(pos2.x, pos2.y, pos2.z), Quaternion.Euler(-2.554f, 34.624f, 5.799f)) as GameObject;
                                checkCreate = true;
                            ////Debug.Log(ChangeDir);
                            g_intList.Add(ChangeDir);

                            // 생성된 오브젝트를 leafList 에 add로 추가.
                            leafList.Add(_leaf);


                            }

                            //노말가지 2가 아닐때
                            else if (RandBranchIndex != 1 && checkCreate == false)
                            {
                                //배열0~4까지의 오브젝트를 랜덤생성한다. 위치는 RightBranchPos위치 
                                GameObject _leaf = Instantiate(BranchArray[RandBranchIndex], new Vector3(pos2.x, pos2.y, pos2.z), Quaternion.Euler(0f, 90f, 0f)) as GameObject;
                            ////Debug.Log(ChangeDir);
                            g_intList.Add(ChangeDir);

                            // 생성된 오브젝트를 leafList 에 add로 추가.
                            leafList.Add(_leaf);
                                checkCreate = true;
                            }
                        }

                        //이전가지가 꽃가지도 시든가지도 아니었다면
                        else if (PreNum != 4 && PreNum != 5 && PreNum != 6 && checkCreate == false)
                        {
                            //0~6까지 랜덤번호를 만들고 (전체 범위)
                            RandBranchIndex = Random.Range(0, 7);
                            leafNumList.Add(RandBranchIndex);

                            //노말가지 2일때의 쿼터니온 조절
                            if (RandBranchIndex == 1)
                            {
                                GameObject _leaf = Instantiate(BranchArray[RandBranchIndex], new Vector3(pos2.x, pos2.y, pos2.z), Quaternion.Euler(-2.554f, 34.624f, 5.799f)) as GameObject;
                                checkCreate = true;
                            g_intList.Add(ChangeDir);

                            //Debug.Log(ChangeDir);
                            // 생성된 오브젝트를 leafList 에 add로 추가.
                            leafList.Add(_leaf);
                            }


                            //꽃가지 1일때 쿼터니온 조절
                            if (RandBranchIndex == 4)
                            {
                                GameObject _leaf = Instantiate(BranchArray[RandBranchIndex], new Vector3(pos2.x, pos2.y, pos2.z), Quaternion.Euler(0f, 90, 0)) as GameObject;
                                checkCreate = true;
                            g_intList.Add(ChangeDir);

                            //Debug.Log(ChangeDir);
                            // 생성된 오브젝트를 leafList 에 add로 추가.
                            leafList.Add(_leaf);
                            }

                            if (RandBranchIndex == 5)
                            {
                                GameObject _leaf = Instantiate(BranchArray[RandBranchIndex], new Vector3(pos2.x, pos2.y, pos2.z), Quaternion.Euler(0f, 90, 0)) as GameObject;
                                checkCreate = true;
                            g_intList.Add(ChangeDir);


                            //Debug.Log(ChangeDir);
                            // 생성된 오브젝트를 leafList 에 add로 추가.
                            leafList.Add(_leaf);
                            }

                            //시든가지일때 쿼터니온 조절
                            if (RandBranchIndex == 6)
                            {
                                GameObject _leaf = Instantiate(BranchArray[RandBranchIndex], new Vector3(pos2.x, pos2.y, pos2.z), Quaternion.Euler(4.501f, 0, 0)) as GameObject;
                                checkCreate = true;
                            g_intList.Add(ChangeDir);

                            //Debug.Log(ChangeDir);
                            // 생성된 오브젝트를 leafList 에 add로 추가.
                            leafList.Add(_leaf);
                            }


                            //노말가지 2, 꽃가지1,2, 시든가지가 아닐때
                            else if (RandBranchIndex != 1 && RandBranchIndex != 4 && RandBranchIndex != 5 && RandBranchIndex != 6 && checkCreate == false)
                            {
                                //배열0~7 까지의 오브젝트를 랜덤생성한다. 위치는 RightBranchPos위치 
                                GameObject _leaf = Instantiate(BranchArray[RandBranchIndex], new Vector3(pos2.x, pos2.y, pos2.z), Quaternion.Euler(0f, 90f, 0f)) as GameObject;
                                checkCreate = true;
                            g_intList.Add(ChangeDir);

                            //Debug.Log(ChangeDir);
                            // 생성된 오브젝트를 leafList 에 add로 추가.
                            leafList.Add(_leaf);
                            }
                        }

                        //만약 이전에 꽃가지가 생성되었었다면
                        if (PreNum == 4 || PreNum == 5 && checkCreate == false)
                        {
                            //랜덤값을 다시 계산하고 (시든가지를 제외하고 0~5까지)
                            RandBranchIndex = Random.Range(0, 6);
                            leafNumList.Add(RandBranchIndex);

                            //노말가지2 일때의 쿼터니온 조절
                            if (RandBranchIndex == 1)
                            {
                                GameObject _leaf = Instantiate(BranchArray[RandBranchIndex], new Vector3(pos2.x, pos2.y, pos2.z), Quaternion.Euler(-2.554f, 34.624f, 5.799f)) as GameObject;
                                checkCreate = true;
                            g_intList.Add(ChangeDir);

       
                            //Debug.Log(ChangeDir);
                            // 생성된 오브젝트를 leafList 에 add로 추가.
                            leafList.Add(_leaf);
                            }

                            //꽃가지 1일때 쿼터니온 조절
                            if (RandBranchIndex == 4)
                            {
                                GameObject _leaf = Instantiate(BranchArray[RandBranchIndex], new Vector3(pos2.x, pos2.y, pos2.z), Quaternion.Euler(0f, 90, 0)) as GameObject;
                                checkCreate = true;
                            g_intList.Add(ChangeDir);

       
                            //Debug.Log(ChangeDir);
                            // 생성된 오브젝트를 leafList 에 add로 추가.
                            leafList.Add(_leaf);
                            }

                            if (RandBranchIndex == 5)
                            {
                                GameObject _leaf = Instantiate(BranchArray[RandBranchIndex], new Vector3(pos2.x, pos2.y, pos2.z), Quaternion.Euler(0f, 90, 0)) as GameObject;
                                checkCreate = true;
                            g_intList.Add(ChangeDir);

            
                            //Debug.Log(ChangeDir);
                            // 생성된 오브젝트를 leafList 에 add로 추가.
                            leafList.Add(_leaf);
                            }

                            //노말가지2, 꽃가지 1이 아닐떄
                            else if (RandBranchIndex != 1 && RandBranchIndex != 4 && RandBranchIndex != 5 && checkCreate == false)
                            {
                                //배열0~5까지의 오브젝트를 랜덤생성한다. 위치는 RightBranchPos위치 
                                GameObject _leaf = Instantiate(BranchArray[RandBranchIndex], new Vector3(pos2.x, pos2.y, pos2.z), Quaternion.Euler(0f, 90f, 0f)) as GameObject;
                                checkCreate = true;
                            g_intList.Add(ChangeDir);

                 
                            //Debug.Log(ChangeDir);
                            // 생성된 오브젝트를 leafList 에 add로 추가.
                            leafList.Add(_leaf);
                            }


                        }

                        //이전가지가 꽃가지가 아니었다면 (시든, 노말, 나뭇잎)
                        else if (PreNum != 4 && PreNum != 5 && checkCreate == false)
                        {
                            RandBranchIndex = Random.Range(0, 6);
                            leafNumList.Add(RandBranchIndex);
                            //Debug.Log("가지번호 : " + RandBranchIndex);


                            //노말가지2의 쿼터니온 조절
                            if (RandBranchIndex == 1)
                            {
                                GameObject _leaf = Instantiate(BranchArray[RandBranchIndex], new Vector3(pos2.x, pos2.y, pos2.z), Quaternion.Euler(-2.554f, 34.624f, 5.799f)) as GameObject;
                                checkCreate = true;
                            g_intList.Add(ChangeDir);

                            //Debug.Log(ChangeDir);
                            // 생성된 오브젝트를 leafList 에 add로 추가.
                            leafList.Add(_leaf);
                            }

                            //꽃가지 1의 쿼터니온 조절
                            if (RandBranchIndex == 4)
                            {
                                GameObject _leaf = Instantiate(BranchArray[RandBranchIndex], new Vector3(pos2.x, pos2.y, pos2.z), Quaternion.Euler(0f, 90, 0)) as GameObject;
                                checkCreate = true;
                            g_intList.Add(ChangeDir);

                            //Debug.Log(ChangeDir);
                            // 생성된 오브젝트를 leafList 에 add로 추가.
                            leafList.Add(_leaf);
                            }

                            if (RandBranchIndex == 5)
                            {
                                GameObject _leaf = Instantiate(BranchArray[RandBranchIndex], new Vector3(pos2.x, pos2.y, pos2.z), Quaternion.Euler(0f, 90, 0)) as GameObject;
                                checkCreate = true;
                            g_intList.Add(ChangeDir);

              
                            //Debug.Log(ChangeDir);
                            // 생성된 오브젝트를 leafList 에 add로 추가.
                            leafList.Add(_leaf);
                            }

                            //노말가지2, 꽃가지1이 아닐때
                            else if (RandBranchIndex != 1 && RandBranchIndex != 4 && RandBranchIndex != 5 && checkCreate == false)
                            {
                                //배열0~4까지의 오브젝트를 랜덤생성한다. 위치는 RightBranchPos위치 
                                GameObject _leaf = Instantiate(BranchArray[RandBranchIndex], new Vector3(pos2.x, pos2.y, pos2.z), Quaternion.Euler(0f, 90f, 0f)) as GameObject;
                                checkCreate = true;
                            g_intList.Add(ChangeDir);

                  
                            //Debug.Log(ChangeDir);
                            // 생성된 오브젝트를 leafList 에 add로 추가.
                            leafList.Add(_leaf);
                            }


                        }

                    }

                    /*==========================왼쪽에 가지생성=========================*/
                    //가지가 생성되지않았을 경우 왼쪽에 가지를 생성한다.
                    if (ChangeDir == 1 && checkCreate == false)
                {
                    g_intList.Add(ChangeDir);
                    g_boolList.Add(true);
                    //만약 이전에 시든가지가 생성되었었다면
                    if (PreNum == 6 && checkCreate == false)
                        {
                            //랜덤값을 다시 계산하고
                            RandBranchIndex = Random.Range(0, 4);
                            leafNumList.Add(RandBranchIndex);

                            if (RandBranchIndex == 1)
                            {
                                GameObject _leaf = Instantiate(BranchArray[RandBranchIndex], new Vector3(pos2.x, pos2.y, pos2.z), Quaternion.Euler(22.237f, -138.376f, 28.799f)) as GameObject;
                                checkCreate = true;
                            g_intList.Add(ChangeDir);

         
                            //Debug.Log(ChangeDir);
                            // 생성된 오브젝트를 leafList 에 add로 추가.
                            leafList.Add(_leaf);
                            }

                            else if (RandBranchIndex != 1 && checkCreate == false)
                            {
                                //배열0~2까지의 오브젝트를 랜덤생성한다. 위치는 RightBranchPos위치 
                                GameObject _leaf = Instantiate(BranchArray[RandBranchIndex], new Vector3(pos3.x, pos3.y, pos3.z), Quaternion.Euler(-180f, 90f, 0f)) as GameObject;
                                checkCreate = true;
                            g_intList.Add(ChangeDir);

                            //Debug.Log(ChangeDir);
                            // 생성된 오브젝트를 leafList 에 add로 추가.
                            leafList.Add(_leaf);
                            }
                        }

                        //이전가지가 꽃가지도 시든가지도 아니었다면
                        else if (PreNum != 4 && PreNum != 5 && PreNum != 6 && checkCreate == false)
                        {
                            RandBranchIndex = Random.Range(0, 7);
                            leafNumList.Add(RandBranchIndex);

                            if (RandBranchIndex == 1)
                            {
                                GameObject _leaf = Instantiate(BranchArray[RandBranchIndex], new Vector3(pos2.x, pos2.y, pos2.z), Quaternion.Euler(22.237f, -138.376f, 28.799f)) as GameObject;
                            checkCreate = true;
                            g_intList.Add(ChangeDir);

     
                            //Debug.Log(ChangeDir);
                            // 생성된 오브젝트를 leafList 에 add로 추가.
                            leafList.Add(_leaf);
                            }

                            if (RandBranchIndex == 4)
                            {
                                GameObject _leaf = Instantiate(BranchArray[RandBranchIndex], new Vector3(pos2.x, pos2.y, pos2.z), Quaternion.Euler(0f, -90, 0)) as GameObject;
                                checkCreate = true;
                            g_intList.Add(ChangeDir);

                            //Debug.Log(ChangeDir);
                            // 생성된 오브젝트를 leafList 에 add로 추가.
                            leafList.Add(_leaf);
                            }

                            if (RandBranchIndex == 5)
                            {
                                GameObject _leaf = Instantiate(BranchArray[RandBranchIndex], new Vector3(pos2.x, pos2.y, pos2.z), Quaternion.Euler(0f, -90, 0)) as GameObject;
                                checkCreate = true;
                            g_intList.Add(ChangeDir);

                            //Debug.Log(ChangeDir);
                            // 생성된 오브젝트를 leafList 에 add로 추가.
                            leafList.Add(_leaf);
                            }

                            if (RandBranchIndex == 6)
                            {
                                GameObject _leaf = Instantiate(BranchArray[RandBranchIndex], new Vector3(pos2.x, pos2.y, pos2.z), Quaternion.Euler(-3.429f, -211, 0)) as GameObject;
                                checkCreate = true;
                            g_intList.Add(ChangeDir);

                            //Debug.Log(ChangeDir);
                            // 생성된 오브젝트를 leafList 에 add로 추가.
                            leafList.Add(_leaf);
                            }

                            else if (RandBranchIndex != 1 && RandBranchIndex != 4 && RandBranchIndex != 5 && RandBranchIndex != 6 && checkCreate == false)
                            {
                                //배열0~4까지의 오브젝트를 랜덤생성한다. 위치는 RightBranchPos위치 
                                GameObject _leaf = Instantiate(BranchArray[RandBranchIndex], new Vector3(pos3.x, pos3.y, pos3.z), Quaternion.Euler(-180f, 90f, 0f)) as GameObject;
                                checkCreate = true;
                            g_intList.Add(ChangeDir);


                            //Debug.Log(ChangeDir);
                            // 생성된 오브젝트를 leafList 에 add로 추가.
                            leafList.Add(_leaf);
                            }
                        }

                        //만약 이전에 꽃가지가 생성되었었다면
                        if (PreNum == 4 || PreNum == 5 && checkCreate == false)
                        {
                            //랜덤값을 다시 계산하고
                            RandBranchIndex = Random.Range(0, 6);
                            leafNumList.Add(RandBranchIndex);

                            if (RandBranchIndex == 1)
                            {
                                GameObject _leaf = Instantiate(BranchArray[RandBranchIndex], new Vector3(pos2.x, pos2.y, pos2.z), Quaternion.Euler(22.237f, -138.376f, 28.799f)) as GameObject;
                                checkCreate = true;
                            g_intList.Add(ChangeDir);

                            //Debug.Log(ChangeDir);
                            // 생성된 오브젝트를 leafList 에 add로 추가.
                            leafList.Add(_leaf);

                            }

                            if (RandBranchIndex == 4)
                            {
                                GameObject _leaf = Instantiate(BranchArray[RandBranchIndex], new Vector3(pos2.x, pos2.y, pos2.z), Quaternion.Euler(0f, -90, 0)) as GameObject;
                                checkCreate = true;
                            g_intList.Add(ChangeDir);


                            //Debug.Log(ChangeDir);
                            // 생성된 오브젝트를 leafList 에 add로 추가.
                            leafList.Add(_leaf);
                            }

                            if (RandBranchIndex == 5)
                            {
                                GameObject _leaf = Instantiate(BranchArray[RandBranchIndex], new Vector3(pos2.x, pos2.y, pos2.z), Quaternion.Euler(0f, -90, 0)) as GameObject;
                                checkCreate = true;
                            g_intList.Add(ChangeDir);


                            //Debug.Log(ChangeDir);
                            // 생성된 오브젝트를 leafList 에 add로 추가.
                            leafList.Add(_leaf);
                            }

                            else if (RandBranchIndex != 1 && RandBranchIndex != 4 && RandBranchIndex != 5 && checkCreate == false)
                            {
                                //배열0~3까지의 오브젝트를 랜덤생성한다. 위치는 RightBranchPos위치 
                                GameObject _leaf = Instantiate(BranchArray[RandBranchIndex], new Vector3(pos3.x, pos3.y, pos3.z), Quaternion.Euler(-180f, 90f, 0f)) as GameObject;
                                checkCreate = true;
                            g_intList.Add(ChangeDir);


                            //Debug.Log(ChangeDir);
                            // 생성된 오브젝트를 leafList 에 add로 추가.
                            leafList.Add(_leaf);
                            }
                        }

                        //이전가지가 꽃가지가 아니었다면
                        else if (PreNum != 4 && PreNum != 5 && checkCreate == false)
                        {
                            RandBranchIndex = Random.Range(0, 7);
                            leafNumList.Add(RandBranchIndex);

                            if (RandBranchIndex == 1)
                            {
                                GameObject _leaf = Instantiate(BranchArray[RandBranchIndex], new Vector3(pos2.x, pos2.y, pos2.z), Quaternion.Euler(22.237f, -138.376f, 28.799f)) as GameObject;
                                checkCreate = true;
                            g_intList.Add(ChangeDir);

                            //Debug.Log(ChangeDir);
                            // 생성된 오브젝트를 leafList 에 add로 추가.
                            leafList.Add(_leaf);

                            }

                            if (RandBranchIndex == 4)
                            {
                                GameObject _leaf = Instantiate(BranchArray[RandBranchIndex], new Vector3(pos2.x, pos2.y, pos2.z), Quaternion.Euler(0f, -90, 0)) as GameObject;
                                checkCreate = true;
                            g_intList.Add(ChangeDir);

       
                            //Debug.Log(ChangeDir);
                            // 생성된 오브젝트를 leafList 에 add로 추가.
                            leafList.Add(_leaf);
                            }

                            if (RandBranchIndex == 5)
                            {
                                GameObject _leaf = Instantiate(BranchArray[RandBranchIndex], new Vector3(pos2.x, pos2.y, pos2.z), Quaternion.Euler(0f, -90, 0)) as GameObject;
                                checkCreate = true;
                            g_intList.Add(ChangeDir);

        
                            //Debug.Log(ChangeDir);
                            // 생성된 오브젝트를 leafList 에 add로 추가.
                            leafList.Add(_leaf);
                            }

                            else if (RandBranchIndex != 1 && RandBranchIndex != 4 && RandBranchIndex != 5 && checkCreate == false)
                            {
                                //배열0~4까지의 오브젝트를 랜덤생성한다. 위치는 RightBranchPos위치 
                                GameObject _leaf = Instantiate(BranchArray[RandBranchIndex], new Vector3(pos3.x, pos3.y, pos3.z), Quaternion.Euler(-180f, 90f, 0f)) as GameObject;
                                checkCreate = true;
                            g_intList.Add(ChangeDir);

        
                            //Debug.Log(ChangeDir);
                            // 생성된 오브젝트를 leafList 에 add로 추가.
                            leafList.Add(_leaf);
                            }

                        }

                    }

                }

                //제한 시간이 0미만이되면
                else if (LimitTime < 0)
                {
                    //방향이 오른쪽일때
                    if (ChangeDir == 0 && FlyFlag == false && checkCreate == false)
                    {
                    //도약발판을 생성한다.
                    GameObject _leaf = Instantiate(FlyBranch, new Vector3(pos2.x, pos2.y, pos2.z), Quaternion.Euler(0f, 90f, 0f)) as GameObject;
                    checkCreate = true;
                    leafList.Add(_leaf);
                    FlyFlag = true;

                    }

                    else if (ChangeDir == 1 && FlyFlag == false && checkCreate == false)
                    {
                    //도약발판을 생성한다.
                    GameObject _leaf = Instantiate(FlyBranch, new Vector3(pos3.x, pos3.y, pos3.z), Quaternion.Euler(-180f, 90f, 0f)) as GameObject;
                    checkCreate = true;
                    leafList.Add(_leaf);
                    FlyFlag = true;
                    }
                }


                //복제할 기둥 오브젝트(branch3)를 빈오브젝트 위치에 로테이션 x는 랜덤으로 y = 90 z = -90으로 생성하고
                GameObject _obj2 = Instantiate(Branch3, new Vector3(pos1.x, pos1.y, pos1.z), Quaternion.Euler(RandQNum, 90f, -90f)) as GameObject;

                PreNum = RandBranchIndex;

                // 생성된 오브젝트를 branchlist 에 add로 추가.
                branchList.Add(_obj2);
            

            //리스트의 마지막에 있는 클론의 0번째 자식위치값을 lastBranchPos에 받아옴
            lastBranchPos = branchList[branchList.Count - 1].transform.GetChild(0).transform.position;

            //리스트의 마지막에 있는 클론의 1번째 자식위치값을 RightBranchPos에 받아옴
            RightBranchPos = branchList[branchList.Count - 1].transform.GetChild(1).transform.position;

            //리스트의 마지막에 있는 클론의 2번째 자식위치값을 RightBranchPos에 받아옴
            LeftBranchPos = branchList[branchList.Count - 1].transform.GetChild(2).transform.position;


            //리스트의 마지막에 있는 클론의 0번째 자식위치값을 lastBranchPos에 받아옴
            if (UpNum == 0)
            {
                lastItemPos = leafList[0].transform.GetChild(0).transform.position;
            }
            else if (UpNum != 0)
            {
                lastItemPos = leafList[UpNum+1].transform.GetChild(0).transform.position;
            }

            //0번째 객체가 이상한곳에 생성되어서 false처리 해놓음...
            leafList[0].SetActive(false);
            branchList[0].SetActive(false);

            //0.5f초후에 다시 while문 돈다.
            yield return new WaitForSeconds(0.3f);
        }
    }
}