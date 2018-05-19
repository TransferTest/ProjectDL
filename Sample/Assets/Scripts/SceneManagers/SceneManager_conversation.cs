using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this is scene manager for "conversation" scene
public class SceneManager_conversation : MonoBehaviour {

    public GameObject buttonPrefab;

    public Text textBox;//a text box contains script
    public Text nameBox;//a text box contains character's name

    public GameObject selectionBox;//a window contains choice buttons

    public float dialogueSpeed;//speed of dialogue animation -- number of characters per a second
    public List<string> previous;//previous conversation
    private Dialogue root;//root script at first, it will be current script
    private string cur;//current script

    private float t;//time passed from the start of current animation. used for dialogue animation
    private bool anim;//true when playing dialogue animation
    private bool selecting;//player is selecting
    private bool finished;//conversation is over
    private int preindex;//used for dialogue animation. the index of character that is shown at last. scene manager shows text from preindex to current index that is newly calculated
    private GameObject[] buttons;//buttons for player's choice. created at diverging point, and destryed after the player's choice

	// Use this for initialization
	void Start () {
        anim = true;//to start the first script right after the player enters this scene
        selecting = false;
        finished = false;
        preindex = 0;
        loadDialogue();
        nameBox.text = root.name;
        cur = root.content;
        textBox.text = "";
	}

	// Update is called once per frame
	void Update () {
        if (selecting)//disables all functions ecxept the selection button while player is selecting
            return;
        if (finished)//nothing to do after the conversaion is over
            return;
		if (anim)//while playing dialogue animation
        {
            if (Input.GetButtonDown("Fire1"))//if player clicks, then skip the dialogue animation and shows whole text
            {
                anim = false;
                textBox.text = cur;
                return;
            }

            t += Time.deltaTime;//t is updated
            int index = (int)(t * dialogueSpeed);//current index of character to show
            if (index > cur.Length)//current script is over
            {
                index = cur.Length;
                anim = false;
            }
            for (int i = preindex; i < index; i++)//shows all characters from preindex to index
            {
                textBox.text += cur.ToCharArray()[i];
            }
            preindex = index;//update preindexs
        }
        else//while not playing dialogue animation. waiting for player's click
        {
            if (Input.GetButtonDown("Fire1"))//player clicked. shows the next script
            {
                if (root.selectionNum == 0)//there is no choice at current script
                {
                    anim = true;
                    root = root.getNext(0);
                    if (root == null)//there is no more script which means the conversation is over
                    {
                        finished = true;
                        anim = false;
                        return;
                    }
                    nameBox.text = root.name;
                    cur = root.content;//update root
                    //initiallize those variables to start a new dialogue animation
                    t = 0;
                    preindex = 0;
                    textBox.text = "";
                }
                else//there are some choices at current script
                {
                    selecting = true;//player is selecting
                    selectionBox.active = true;//shows selection box
                    buttons = new GameObject[root.selectionNum];
                    for (int i = 0; i < buttons.Length; i++)//put buttons to the selection box
                    {
                        GameObject newButton = GameObject.Instantiate(buttonPrefab);
                        buttons[i] = newButton;
                        newButton.transform.Translate((selectionBox.transform.position - newButton.transform.position));
                        newButton.transform.Translate(Vector2.up * (buttons.Length - 1) * 24);
                        newButton.transform.Translate(Vector2.up * i * (-48));
                        newButton.transform.SetParent(selectionBox.transform);

                        newButton.transform.GetChild(0).GetComponentInChildren<Text>().text = root.selection[i];

                        int n = i;
                        newButton.GetComponent<Button>().onClick.AddListener(delegate { selection(n); });//sets i-th button's function with selection(i).
                    }
                }
            }
        }
	}

    //load dialogue to play
    //for now, it makes sample scripts
    private void loadDialogue()
    {
        root = new Dialogue("프로그래머", "이것은 샘플 대사입니다. 대화창 애니메이션을 만들기 위해 넣었습니다. 대화창 하나에 얼마나 많은 대사가 들어갈지 알 수 없어서 대충 쓰고있습니다." +
            " 일단 많이 적은 다음에 조금씩 줄여가면 될 것 같아요. 코드에서 엔터키가 먹히는지 궁금해서 엔터도 쳐 보았습니다. 아마 스트링 안에서 엔터는 n으로 하는 걸로 알고있어요." +
            "여기서는 엔터를 치면 자동으로 스트링 값을 더해주네요. 신기해라.");
        Dialogue p = root;
        Dialogue c = p;
        c = new Dialogue("프로그래머", "이제 한 대사가 끝났으니 다음 대사를 써야해요. 제가 국어 능력이 부족해서 벌써부터 할 말이 떨어지기 시작했네요. 생각난 김에 ........이 어떻게 보이는지 한 번 보도록 하죠" +
            "........................................................................................................" +
            "다음 대화에는 한 글자만 넣고 넘겨볼거에요." +
            "한 글자만 있을 때는 어떻게 나오는지 한 번 보도록 하죠.");
        p.setNext(c);
        p = c;
        c = new Dialogue("프로그래머", "핳");
        p.setNext(c);
        p = c;
        c = new Dialogue("프로그래머", ".........................................................................");
        p.setNext(c);
        p = c;
        c = new Dialogue("프로그래머", "대화가 재미없으면 언제라도 왼쪽 위의 back 버튼을 누르면 돌아갈 수 있어요." +
            " 다음에는 텍스트가 비어있을 때를 볼게요.");
        p.setNext(c);
        p = c;
        c = new Dialogue("프로그래머", "");
        p.setNext(c);
        p = c;
        c = new Dialogue("프로그래머", "빈 대화창이 나왔나요? 그럼 다음에는 스페이스만 잔뜩 들어가면 어떻게 될지 봐요.");
        p.setNext(c);
        p = c;
        c = new Dialogue("프로그래머", "                                          ");
        p.setNext(c);
        p = c;
        c = new Dialogue("프로그래머", "이전 대화창에는 42개의 스페이스가 들어가있었어요. 그럼 다음에는 한 대화창 안에 엔터가 들어갈지 확인해봐요.");
        p.setNext(c);
        p = c;
        c = new Dialogue("프로그래머", "제가 이 기호를 뭐라고 읽는지 모르는데 아무튼 줄바꿈은 n입니다.\n" +
            "줄이 잘 바뀌었을 지 모르겠군요.\n이대로 계속 줄을 바꾸다 보면 대화창 밑으로 내려가버리겠죠?\n대사를 쓸 때는 그런데에 주의하도록 합시다.");
        p.setNext(c);
        p = c;
        c = new Dialogue("프로그래머(쑻)", "말하는 사람 이름을 바꿀 수도 있습니다.");
        p.setNext(c);
        p = c;
        c = new Dialogue("코딩노예", "이정도면 꽤 많은 대사를 넣은 것 같군요. 다음은 선택지 입니다.");
        p.setNext(c);
        p = c;
        c = new Dialogue("프로그래머", "1번과 2번 중에서 골라주세요.");
        p.setNext(c);
        p = c;

        Dialogue c1 = new Dialogue("프로그래머", "1번을 골라주셨군요. 지금은 선택지가 2개 밖에 없지만 더 넣을 수도 있습니다.");
        Dialogue c2 = new Dialogue("프로그래머", "2번을 골라주셨군요. 지금은 선택지가 2개 밖에 없지만 더 넣을 수도 있습니다.");

        p.setSelection(new string[] {"1번", "2번"}, new Dialogue[] {c1, c2});

        c = new Dialogue("프로그래머", "이번엔 좀 더 많은 선택지를 봅시다.");

        c1.setNext(c);
        c2.setNext(c);

        p = c;
        c = new Dialogue("프로그래머", "오늘 학식은 뭔가요?");
        p.setNext(c);

        c1 = new Dialogue("프로그래머", "오늘 학식은 돈까스군요. 개인적으로 돈까스는 카레와 같이 먹는 것을 좋아합니다.");
        c2 = new Dialogue("프로그래머", "오늘 학식은 볶음밥이군요. 매점이나 가야겠습니다.");
        Dialogue c3 = new Dialogue("프로그래머", "밥대생을 참고하세요");
        p = c;
        p.setSelection(new string[] { "돈까스", "볶음밥", "모르겠다" }, new Dialogue[] { c1, c2, c3 });

        c = new Dialogue("프로그래머", "보신 것 처럼 선택지를 늘릴 수도 있고 특정 선택지에는 되돌아가게도 할 수 있습니다.");
        c1.setNext(c);
        c2.setNext(c);
        c3.setNext(p);

        p = c;
        c = new Dialogue("프로그래머", "이제 보여 드릴 건 다 보여드린 것 같군요. 샘플 대화는 여기까지입니다. 수고하셨습니다.");
        p.setNext(c);
    }

    //player selected n-th choice
    public void selection(int n)
    {
        Debug.Log("called " + n);//prints a text in console. used for debug. useless at game play

        for (int i = 0; i < buttons.Length; i++)//destray all the selection buttons
        {
            Destroy(buttons[i]);
        }

        selecting = false;//selecting is over
        selectionBox.active = false;//removes selection box

        //start the next script
        anim = true;
        root = root.getNext(n);
        if (root == null)
        {
            finished = true;
            anim = false;
            return;
        }
        nameBox.text = root.name;
        cur = root.content;
        t = 0;
        preindex = 0;
        textBox.text = "";
    }
}
