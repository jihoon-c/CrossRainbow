using UnityEngine;
using System.IO;

public class IDsearchParse : MonoBehaviour
{
    public static IDsearchParse IDSP;

    public string csvFilePath;      // CSV 파일 경로
    public int targetID;            // 찾을 ID
    public float needAmount, milkWant, juiceWant, alcholWant, lemonWant;
    public bool coldness,isSugar,isLime,isGum,isFeed;
    [SerializeField] public TextAsset recipe;
    private void Start()
    {

    }

    public void SearchWant(int i)
    {
        // CSV 파일 읽어오기
        string[] lines = recipe.text.Split('\n'); //File.ReadAllLines("Assets/Resources/Recipe.csv");

        // 데이터가 있는 첫 번째 행부터 마지막 행까지 탐색
        for (int rowIndex = 1; rowIndex < lines.Length; rowIndex++)
        {
            // 현재 행 선택
            string[] fields = lines[rowIndex].Split(',');

            // 1열의 숫자 ID 값 가져오기
            if (!int.TryParse(fields[0], out int id))
            {
                // 1열이 숫자가 아닌 경우, 해당 행 무시하고 다음 행으로 넘어가기
                continue;
            }

            // 원하는 ID와 일치하지 않으면 다음 행으로 넘어가기
            if (id != i)
            {
                continue;
            }

            // 2열부터 12열까지 값을 읽어와 변수에 대입
            needAmount = float.Parse(fields[2]);
            milkWant = float.Parse(fields[3]);
            juiceWant = float.Parse(fields[4]);
            alcholWant = float.Parse(fields[5]);
            lemonWant = float.Parse(fields[6]);
            coldness = bool.Parse(fields[7]);
            isSugar = bool.Parse(fields[8]);
            isLime = bool.Parse(fields[9]);
            isGum = bool.Parse(fields[10]);
            isFeed = bool.Parse(fields[11]);

            // 필요한 작업 수행
            Debug.Log($"ID: {id}, needAmount: {needAmount}, milkWant: {milkWant}, juiceWant: {juiceWant}, alcholWant: {alcholWant}, lemonWant: {lemonWant}, coldness: {coldness}, isSugar: {isSugar}, isLime: {isLime}, isGum: {isGum}, isFeed: {isFeed}");

            // 원하는 값이 발견되면 더 이상 탐색하지 않고 종료
            return;
        }

        // 원하는 ID와 일치하는 행이 없는 경우
        Debug.LogError("Target ID not found in CSV file!");
    }

    public Color bubbleColor(int i)
    {
        SearchWant(i);
        Color milkColor = Color.white;
        Color juiceColor = Color.red;
        Color alcholColor = Color.green;
        Color lemonColor = Color.blue;


        // 비율을 계산하여 각 색상에 가중치를 적용
        float totalValue = milkWant + juiceWant + alcholWant + lemonWant;

        float weight1 = milkWant / totalValue;
        float weight2 = juiceWant / totalValue;
        float weight3 = alcholWant / totalValue;
        float weight4 = lemonWant / totalValue;

        // 가중치를 적용하여 색을 섞음
        Color mixedColor = weight1 * milkColor + weight2 * juiceColor + weight3 * alcholColor + weight4 * lemonColor;

        return mixedColor;
    }
}

