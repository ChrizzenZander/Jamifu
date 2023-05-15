using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MemoryGameController : MonoBehaviour
{
    //Game Components
    [SerializeField]
    private Sprite bgSprite;

    [SerializeField]
    private Sprite fgSprite;

    private AudioSource audioSource;

    public AudioClip audioClip;


    //Arrays
    public string[] puzzlesRomaji = new string[] { "a", "i", "u", "e", "o", "ka", "ki", "ku", "ke", "ko", "sa", "shi", "su", "se", "so", "ta", "chi", "tsu", "te", "to", "na", "ni", "nu", "ne", "no", "ha", "hi", "fu", "he", "ho", "ma", "mi", "mu", "me", "mo", "ya", "yu", "yo", "ra", "ri", "ru", "re", "ro", "wa", "wo", "n", "ga", "gi", "gu", "ge", "go", "za", "ji", "zu", "ze", "zo", "da", "ji", "zu", "de", "do", "ba", "bi", "bu", "be", "bo", "pa", "pi", "pu", "pe", "po" };

    public string[] puzzlesHiragana = new string[] { "あ", "い", "う", "え", "お", "か", "き", "く", "け", "こ", "さ", "し", "す", "せ", "そ", "た", "ち", "つ", "て", "と", "な", "に", "ぬ", "ね", "の", "は", "ひ", "ふ", "へ", "ほ", "ま", "み", "む", "め", "も", "や", "ゆ", "よ", "ら", "り", "る", "れ", "ろ", "わ", "を", "ん", "が", "ぎ", "ぐ", "げ", "ご", "ざ", "じ", "ず", "ぜ", "ぞ", "だ", "ぢ", "づ", "で", "ど", "ば", "び", "ぶ", "べ", "ぼ", "ぱ", "ぴ", "ぷ", "ぺ", "ぽ" };
    
    public string[] puzzlesKatakana = new string[] { "ア", "イ", "ウ", "エ", "オ", "カ", "キ", "ク", "ケ", "コ", "サ", "シ", "ス", "セ", "ソ", "タ", "チ", "ツ", "テ", "ト", "ナ", "ニ", "ヌ", "ネ", "ノ", "ハ", "ヒ", "フ", "ヘ", "ホ", "マ", "ミ", "ム", "メ", "モ", "ヤ", "ユ", "ヨ", "ラ", "リ", "ル", "レ", "ロ", "ワ", "ヲ", "ン", "ガ", "ギ", "グ", "ゲ", "ゴ", "ザ", "ジ", "ズ", "ゼ", "ゾ", "ダ", "ヂ", "ヅ", "デ", "ド", "バ", "ビ", "ブ", "ベ", "ボ", "パ", "ピ", "プ", "ペ", "ポ" };

    //Lists
    public List<string> gamePuzzles = new List<string>();

    public List<Button> btns = new List<Button>();

    //Booleans
    private bool firstGuess, secondGuess;

    //Integers
    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;

    private int firstGuessPuzzleIndex, secondGuessPuzzleIndex;

    private int firstGuessIndex, secondGuessIndex;

    //Strings
    private string firstGuessPuzzle, secondGuessPuzzle;

    void Start() 
    {
        GetButtons();
        AddListener();
        AddGamePuzzles();
        ShuffleString(gamePuzzles);
        gameGuesses = gamePuzzles.Count / 2;
    }

    // Update is called once per frame
    void GetButtons() 
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");

        for (int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgSprite;
        }
    }

    void AddListener() 
    {
        foreach (Button btn in btns) 
        {
            btn.onClick.AddListener(()  => PickAPuzzle());
        }
    }

    void AddGamePuzzles() 
    {
        int lopper = btns.Count;
        int index = 0;

        for (int i = 0; i < lopper; i++)
        {
            if (gamePuzzles.Count < lopper / 2)
            {
                gamePuzzles.Add(puzzlesHiragana[index]);

            } else
            {
                gamePuzzles.Add(puzzlesRomaji[index - (lopper / 2)]);
            }

            index++;
        }
    }

    public void PickAPuzzle() 
    {
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        GameObject child = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject;
        TextMeshProUGUI childTMP = child.GetComponent<TextMeshProUGUI>();

        int currentGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

        if (!firstGuess)
        {
            firstGuess = true;

            firstGuessIndex = currentGuessIndex;

            if (puzzlesRomaji.Any(gamePuzzles[firstGuessIndex].Contains))
            {
                for (int i = 0; i < puzzlesRomaji.Length; i++)
                {
                    if (gamePuzzles[firstGuessIndex] == puzzlesRomaji[i])
                    {
                        firstGuessPuzzle = puzzlesRomaji[i];
                        firstGuessPuzzleIndex = i;
                        break;
                    }
                }
            } 
            else if (puzzlesHiragana.Any(gamePuzzles[firstGuessIndex].Contains))
            {
                for (int i = 0; i < puzzlesRomaji.Length; i++)
                {
                    if (gamePuzzles[firstGuessIndex] == puzzlesHiragana[i])
                    {
                        firstGuessPuzzle = puzzlesHiragana[i];
                        firstGuessPuzzleIndex = i;
                        break;
                    }
                }
            }

            audioSource = GetComponent<AudioSource>();
            audioClip = Resources.Load<AudioClip>("Audio/Speech/Split/"+ puzzlesRomaji[firstGuessPuzzleIndex]);
            audioSource.clip = audioClip;
            audioSource.Play();
            


            btns[firstGuessIndex].image.sprite = fgSprite;

            childTMP.text = gamePuzzles[firstGuessIndex];



        } else if (!secondGuess && firstGuessIndex != currentGuessIndex)
        {
            secondGuess = true;

            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            if (puzzlesRomaji.Any(gamePuzzles[secondGuessIndex].Contains))
            {
                for (int i = 0; i < puzzlesRomaji.Length; i++)
                {
                    if (gamePuzzles[secondGuessIndex] == puzzlesRomaji[i])
                    {
                        secondGuessPuzzleIndex = i;
                        break;
                    }
                }
            }
            else if (puzzlesHiragana.Any(gamePuzzles[secondGuessIndex].Contains))
            {
                for (int i = 0; i < puzzlesRomaji.Length; i++)
                {
                    if (gamePuzzles[secondGuessIndex] == puzzlesHiragana[i])
                    {
                        secondGuessPuzzleIndex = i;
                        break;
                    }
                }
            }

            audioSource = GetComponent<AudioSource>();
            audioClip = Resources.Load<AudioClip>("Audio/Speech/Split/" + puzzlesRomaji[firstGuessPuzzleIndex]);
            audioSource.clip = audioClip;
            audioSource.Play();

            btns[secondGuessIndex].image.sprite = fgSprite;

            childTMP.text = gamePuzzles[secondGuessIndex];
            
            StartCoroutine(CheckIfThePuzzlesMatch());

        }        
    }

    IEnumerator CheckIfThePuzzlesMatch()
    {
        yield return new WaitForSeconds(1f);

        countGuesses++;

        if (firstGuessPuzzleIndex == secondGuessPuzzleIndex)
        {
            yield return new WaitForSeconds(.5f);

            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;

            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            GameObject firstChild = btns[firstGuessIndex].transform.GetChild(0).gameObject;
            TextMeshProUGUI firstChildTMP = firstChild.GetComponent<TextMeshProUGUI>();
            firstChildTMP.text = "";

            GameObject secondChild = btns[secondGuessIndex].transform.GetChild(0).gameObject;
            TextMeshProUGUI secondChildTMP = secondChild.GetComponent<TextMeshProUGUI>();
            secondChildTMP.text = "";

            CheckIfGameIsFinished();
        }
        else
        {
            btns[firstGuessIndex].image.sprite = bgSprite;
            GameObject firstChild = btns[firstGuessIndex].transform.GetChild(0).gameObject;
            TextMeshProUGUI firstChildTMP = firstChild.GetComponent<TextMeshProUGUI>();
            firstChildTMP.text = "";

            btns[secondGuessIndex].image.sprite = bgSprite;
            GameObject secondChild = btns[secondGuessIndex].transform.GetChild(0).gameObject;
            TextMeshProUGUI secondChildTMP = secondChild.GetComponent<TextMeshProUGUI>();
            secondChildTMP.text = "";

        }

        yield return new WaitForSeconds(.5f);

        firstGuess = secondGuess = false;
    }

    void CheckIfGameIsFinished()
    {
        countCorrectGuesses++;

        if (countCorrectGuesses == gameGuesses)
        {
            Debug.Log("Game Finished");
            Debug.Log("It took you " + countGuesses + " guesses to finish the game");
        }

    }

    void ShuffleString(List<string> list)
    {
        for (int i = 0;i < list.Count;i++)
        {
            string temp = list[i];
            int randomIndex = UnityEngine.Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
