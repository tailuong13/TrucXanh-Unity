using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Sprite backgroundImg;
    private List<Button> btnList = new List<Button>();
    [SerializeField] //chuyen doi Objects thanh` dang co the luu tru dc
    public List<Sprite> GameSprites = new List<Sprite>();//tao list anh sprite
    public Sprite[] SourceSprites; //tao mang chua cac anh 
    [SerializeField]

    public TextMeshProUGUI scoreText;
    private bool firstGuess, secondGuess = false;
    string firstName, secondName;
    int firstIndex, secondIndex, TotalGuess, NoOfGuess, CorrectGuess;    

    void Awake()
    {
        SourceSprites = Resources.LoadAll<Sprite> ("Sprites/GameImg");
    }

    void AddSpirtes ()
    {
        int size = btnList.Count;
        int index = 0;
        for (int i = 0; i < size; i++)
        {
            if (i == size / 2)
            {
                index = 0;

            }
            GameSprites.Add(SourceSprites[index]); //GameSprites la List nen phai dung .Add
            index++;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GetButtons();
        TotalGuess = btnList.Count / 2; //tong so lan doan dung bang so luong btn chia 2
        AddListener();
        AddSpirtes();
        Shuffle(GameSprites);
        
    }
    void GetButtons()
    {
        //Lay het ac button hien co them vao list
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");
        for (int i = 0; i < objects.Length; i++)
        {
            btnList.Add(objects[i].GetComponent<Button>());
            btnList[i].image.sprite = backgroundImg;
        }
    }

    void AddListener()
    {
        //add listener cho moi button
        foreach(Button btn in btnList) {
            btn.onClick.AddListener( () => PickPuzzle());
        }
    }

    void PickPuzzle()
    {
        if ( !firstGuess ) //neu chua duoc click lan dau
        {
            firstGuess = true; //chuyen no thanh true(kich hoat first guess)
            firstIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name); //ben trong ngoac la string nen phai ep kieu voi int parse
            firstName = GameSprites[firstIndex].name;
            btnList[firstIndex].image.sprite = GameSprites[firstIndex];
            Debug.Log("1st Index: " + firstIndex + " 1st Name: " + firstName);
        }
        else if ( !secondGuess ) {
            secondGuess = true;
            secondIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondName = GameSprites[secondIndex].name;
            btnList[secondIndex].image.sprite = GameSprites[secondIndex];
            Debug.Log("2nd Index: " + secondIndex + " 2nd Name: " + secondName);
            NoOfGuess++; //tang so lan doan sai
            //delay ham de co the nhin thay hinh anh trong lan chon thu 2
            StartCoroutine(CheckIfPuzzleMatched());
        }   
    }

    //ham xu ly khi chon 2 anh giong nhau
    IEnumerator CheckIfPuzzleMatched()
    {
        yield return new WaitForSeconds(1f);
        if (firstName == secondName && firstIndex != secondIndex)
        {
            CorrectGuess++; //1 lan dung thi tang so luong cua bien
            //lam cho 2 btn khong click vao duoc
            scoreText.text = "Score: " + CorrectGuess.ToString();
            btnList[firstIndex].interactable = false;
            btnList[secondIndex].interactable = false;

            //lam cho 2 btn mat di
            btnList[firstIndex].image.color = new Color(0, 0, 0, 0);
            btnList[secondIndex].image.color = new Color(0, 0, 0, 0);
            CheckIfFinish();
        }
        else
        {
            //khong thi tra no ve ban dau
            btnList[firstIndex].image.sprite = backgroundImg;
            btnList[secondIndex].image.sprite = backgroundImg;
        }
        firstGuess = secondGuess = false; //tra ve lai ban dau
    }

    void CheckIfFinish()
    {
        if (CorrectGuess == TotalGuess)
        {
            Debug.Log("Win with " + NoOfGuess + " time");
        }
    }

    //ham xoa tron btn
    void Shuffle(List<Sprite> list)
    {
        Sprite tmp;
        for (int i = 0; i < list.Count; i++)
        {
            tmp = list[i];
            int random = Random.Range(i, list.Count);
            list[i] = list[random];
            list[random] = tmp;
        }
    }
}
