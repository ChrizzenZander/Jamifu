using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Transform PuzzleField;

    [SerializeField]
    private GameObject card;

    public string[] puzzlesRomanji = { "a", "i", "u", "e", "o", "ka", "ki", "ku", "ke", "ko", "sa", "shi", "su", "se", "so", "ta", "chi", "tsu", "te", "to", "na", "ni", "nu", "ne", "no", "ha", "hi", "fu", "he", "ho", "ma", "mi", "mu", "me", "mo", "ya", "yu", "yo", "ra", "ri", "ru", "re", "ro", "wa", "wo", "n", "ga", "gi", "gu", "ge", "go", "za", "ji", "zu", "ze", "zo", "da", "ji", "zu", "de", "do", "ba", "bi", "bu", "be", "bo", "pa", "pi", "pu", "pe", "po" };
    public string[] puzzlesHiragana = { "あ", "い", "う", "え", "お", "か", "き", "く", "け", "こ", "さ", "し", "す", "せ", "そ", "た", "ち", "つ", "て", "と", "な", "に", "ぬ", "ね", "の", "は", "ひ", "ふ", "へ", "ほ", "ま", "み", "む", "め", "も", "や", "ゆ", "よ", "ら", "り", "る", "れ", "ろ", "わ", "を", "ん", "が", "ぎ", "ぐ", "げ", "ご", "ざ", "じ", "ず", "ぜ", "ぞ", "だ", "ぢ", "づ", "で", "ど", "ば", "び", "ぶ", "べ", "ぼ", "ぱ", "ぴ", "ぷ", "ぺ", "ぽ"};
    public string[] puzzlesKatakana = { "ア", "イ", "ウ", "エ", "オ", "カ", "キ", "ク", "ケ", "コ", "サ", "シ", "ス", "セ", "ソ", "タ", "チ", "ツ", "テ", "ト", "ナ", "ニ", "ヌ", "ネ", "ノ", "ハ", "ヒ", "フ", "ヘ", "ホ", "マ", "ミ", "ム", "メ", "モ", "ヤ", "ユ", "ヨ", "ラ", "リ", "ル", "レ", "ロ", "ワ", "ヲ", "ン", "ガ", "ギ", "グ", "ゲ", "ゴ", "ザ", "ジ", "ズ", "ゼ", "ゾ", "ダ", "ヂ", "ヅ", "デ", "ド", "バ", "ビ", "ブ", "ベ", "ボ", "パ", "ピ", "プ", "ペ", "ポ"};
 


    public List<Button> cards = new List<Button>();

    private bool firstGuess, secondGuess;

    private int FieldSize = 20;

    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;

    private int firstGuessIndex, secondGuessIndex;

    private string firstGuessPuzzle, secondGuessPuzzle;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Started");
        CreatePuzzleField();
        GetButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");

        for (int i = 0; i < objects.Length; i++)
        {
            cards.Add(objects[i].GetComponent<Button>());
            GameObject cardText = cards[i].transform.GetChild(0).gameObject;
            TextMeshProUGUI TextMeshProLable = cardText.GetComponent<TextMeshProUGUI>();
            TextMeshProLable.text = puzzlesHiragana[i];
        }
    }

    void CreatePuzzleField()
    {
        for (int i = 0; i < FieldSize; i++)
        {
            GameObject button = Instantiate(card);
            button.name = "" + i;
            button.transform.SetParent(PuzzleField, false);
        }
    }
}
