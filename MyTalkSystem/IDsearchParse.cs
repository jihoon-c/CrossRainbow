using UnityEngine;
using System.IO;

public class IDsearchParse : MonoBehaviour
{
    public static IDsearchParse IDSP;

    public string csvFilePath;      // CSV ���� ���
    public int targetID;            // ã�� ID
    public float needAmount, milkWant, juiceWant, alcholWant, lemonWant;
    public bool coldness,isSugar,isLime,isGum,isFeed;
    [SerializeField] public TextAsset recipe;
    private void Start()
    {

    }

    public void SearchWant(int i)
    {
        // CSV ���� �о����
        string[] lines = recipe.text.Split('\n'); //File.ReadAllLines("Assets/Resources/Recipe.csv");

        // �����Ͱ� �ִ� ù ��° ����� ������ ����� Ž��
        for (int rowIndex = 1; rowIndex < lines.Length; rowIndex++)
        {
            // ���� �� ����
            string[] fields = lines[rowIndex].Split(',');

            // 1���� ���� ID �� ��������
            if (!int.TryParse(fields[0], out int id))
            {
                // 1���� ���ڰ� �ƴ� ���, �ش� �� �����ϰ� ���� ������ �Ѿ��
                continue;
            }

            // ���ϴ� ID�� ��ġ���� ������ ���� ������ �Ѿ��
            if (id != i)
            {
                continue;
            }

            // 2������ 12������ ���� �о�� ������ ����
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

            // �ʿ��� �۾� ����
            Debug.Log($"ID: {id}, needAmount: {needAmount}, milkWant: {milkWant}, juiceWant: {juiceWant}, alcholWant: {alcholWant}, lemonWant: {lemonWant}, coldness: {coldness}, isSugar: {isSugar}, isLime: {isLime}, isGum: {isGum}, isFeed: {isFeed}");

            // ���ϴ� ���� �߰ߵǸ� �� �̻� Ž������ �ʰ� ����
            return;
        }

        // ���ϴ� ID�� ��ġ�ϴ� ���� ���� ���
        Debug.LogError("Target ID not found in CSV file!");
    }

    public Color bubbleColor(int i)
    {
        SearchWant(i);
        Color milkColor = Color.white;
        Color juiceColor = Color.red;
        Color alcholColor = Color.green;
        Color lemonColor = Color.blue;


        // ������ ����Ͽ� �� ���� ����ġ�� ����
        float totalValue = milkWant + juiceWant + alcholWant + lemonWant;

        float weight1 = milkWant / totalValue;
        float weight2 = juiceWant / totalValue;
        float weight3 = alcholWant / totalValue;
        float weight4 = lemonWant / totalValue;

        // ����ġ�� �����Ͽ� ���� ����
        Color mixedColor = weight1 * milkColor + weight2 * juiceColor + weight3 * alcholColor + weight4 * lemonColor;

        return mixedColor;
    }
}

