using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskExam
{
    internal class TaskSolver
    {
        public static void Main(string[] args)
        {
            TestGetWordSubWords();
            TestFindPath();
            TestFormatPrettyCoins();
            FindMaxRect();

            Console.WriteLine("All Test completed!");
        }

        /// задание 1) Индекс слова
        public static List<int> GetWordSubWords(List<string> words, List<string> wordsDictionary)
        {
            //код алгоритма
            List<int> n = new List<int>();
            foreach (string word in words)
            {
                int count = 0;
                foreach (string wordDictionary in wordsDictionary)
                {
                    string buff = word;
                    for (int i = 0; i < wordDictionary.Length; i++)
                    {
                        if (buff.Contains(wordDictionary.ElementAt(i)))
                        {
                            buff = buff.Remove(buff.IndexOf(wordDictionary.ElementAt(i)), 1);
                        }
                    }
                    if ((buff.Length + wordDictionary.Length) == word.Length)
                    {
                        count++;
                    }
                }
                n.Add(count);
            }
            return n;
        }

        /// задание 2) Луноход
        /// 
        public static int FindPath(int[][] gridMap, int sX, int sY, int eX, int eY, int energyAmount)
        {
            //код алгоритма
            int[][] buffMap = new int[gridMap.Length][];
            int spentEnergy = 0;

            for (int i = 0; i < gridMap.Length; i++)
            {
                buffMap[i] = gridMap[gridMap.Length - i - 1];
            }

            if ((buffMap[sX][sY] == 0) || (buffMap[eX][eY] == 0))
            {
                return -1;
            }
            spentEnergy = Find(buffMap, sX, sY, eX, eY);
            if ((spentEnergy > energyAmount))
                return -1;
            else
                return spentEnergy;
        }

        public static bool MoveDown(int[][] Map, int currX, int currY)
        {
            if ((Map.Length-1) >= currX + 1)
            {
                if (Map[currX+1][currY] == -1)
                {

                    return true;
                }
            }
            return false;
        }

        public static bool MoveUp(int[][] Map, int currX, int currY)
        {
            if (currX > 0)
            {
                if (Map[currX-1][currY] == -1)
                {

                    return true;
                }
            }
            return false;
        }

        public static bool MoveRight(int[][] Map, int currX, int currY)
        {
            if ((Map[0].Length - 1) >= currY + 1)
            {
                if (Map[currX][currY+1] == -1)
                {

                    return true;
                }
            }
            return false;
        }
        public static bool MoveLeft(int[][] Map, int currX, int currY)
        {
            if (currY > 0)
            {
                if (Map[currX][currY-1] == -1)
                {

                    return true;
                }
            }
            return false;
        }

        public static int Find(int [][]Map, int startX, int startY, int endX, int endY)
        {
            bool add = true;
            int step = 0;
            int [][] cMap = new int [Map.Length][];
            for(int i = 0; i < cMap.Length; i++)
            {
                cMap[i] = new int[Map[i].Length];
                for(int j = 0; j < cMap[0].Length; j++)
                {
                    if (Map[i][j] == 0)
                        cMap[i][j] = -2;
                    if (Map[i][j] == 1)
                        cMap[i][j] = -1;
                }
            }

            cMap[endX][endY] = 0;
            while (add)
            {
                add = false;
                for(int i = 0; i < cMap.Length; i++)
                {
                    for(int j = 0; j < cMap[i].Length; j++)
                    {
                        if(cMap[i][j] == step)
                        {
                            if(MoveUp(cMap, i, j))
                                cMap[i - 1][j] = step + 1;
                            if (MoveLeft(cMap, i, j))
                                cMap[i][j - 1] = step + 1;
                            if(MoveDown(cMap, i, j))
                                cMap[i + 1][j] = step + 1;
                            if (MoveRight(cMap, i, j))
                                cMap[i][j + 1] = step + 1;
                        }
                    }
                }
                step++;
                add = true;
                if (cMap[startX][startY] != -1)
                    add = false;
                if (step > cMap.Length * cMap[0].Length)
                    add = false;
            }
            return cMap[startX][startY];
        }

        /// задание 3) Монетки

        public static string FormatPrettyCoins(long value, char separator)
        {
            //код алгоритма
            int count = 0;
            for (int i = 1; i < 12; i++)
                if (value / (Math.Pow(10, i)) >= 1)
                    count++;
            string result = "";
            long longResult;
            string tmpStr;
            switch (count)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                    {
                        longResult = value;
                        tmpStr = longResult.ToString();
                        if (longResult / 1000 > 0)
                            result = $"{longResult/1000}{separator}{tmpStr.Substring(tmpStr.Length - 3)}";
                        else
                            result = $"{longResult}";
                        break;
                    }
                case 5:
                case 6:
                    {
                        longResult = value / 1000;
                        tmpStr = longResult.ToString();
                        if (longResult / 1000 > 0)
                            result = $"{longResult / 1000}{separator}{tmpStr.Substring(tmpStr.Length - 3)}K";
                        else
                            result = $"{longResult}K";
                        break;
                    }
                case 7:
                case 8:
                case 9:
                case 10:
                    {
                        longResult = value / 1000000;
                        tmpStr = longResult.ToString();
                        if (longResult / 1000 > 0)
                            result = $"{longResult / 1000}{separator}{tmpStr.Substring(tmpStr.Length - 3)}M";
                        else
                            result = $"{longResult}M";
                        break;
                    }
                default:
                    {
                        result = "Ошибка";
                        break;
                    }
            }
            return result;
        }

        /// задание 4) Самый большой прямоугольник на гистограмме
        public static int FindMaxRect(List<int> heights)
        {
            //algorithm
            int result = 0;
            foreach (int i in heights)
            {
                int product = 0;
                int count = 0;
                for (int j = 0; j < heights.Count; j++)
                {

                    if (heights.ElementAt(j) >= i)
                    {
                        count++;
                    }
                    if ((heights.ElementAt(j) < i) || (j == heights.Count - 1))
                    {
                        if (i * count > product)
                        {
                            product = i * count;
                        }
                        count = 0;
                    }
                }
                if (product > result)
                    result = product;
            }
            return result;
        }

        /// Тесты (можно/нужно добавлять свои тесты) 

        private static void TestGetWordSubWords()
        {
            var wordsList = new List<string>
            {
                "кот", "ток", "око", "мимо", "гром", "ром",
                "рог", "морг", "огр", "мор", "порог"
            };

            AssertSequenceEqual(GetWordSubWords(new List<string> { "кот" }, wordsList), new[] { 2 });
            AssertSequenceEqual(GetWordSubWords(new List<string> { "молоток" }, wordsList), new[] { 3 });
            AssertSequenceEqual(GetWordSubWords(new List<string> { "мама" }, wordsList), new[] { 0 });
            AssertSequenceEqual(GetWordSubWords(new List<string> { "погром", "гром" }, wordsList), new[] { 7, 6 });


        }

        private static void TestFindPath()
        {
            int[][] gridA =
            {
                new[] {1, 1, 1, 0, 1, 1},
                new[] {1, 1, 1, 0, 1, 1},
                new[] {1, 1, 1, 0, 0, 0},
                new[] {1, 1, 1, 1, 1, 1},
                new[] {1, 1, 1, 1, 1, 1},
                new[] {1, 1, 1, 1, 1, 1},
            };

            AssertEqual(FindPath(gridA, 0, 0, 2, 2, 5), 4);
            AssertEqual(FindPath(gridA, 0, 0, 5, 5, 30), -1);
            AssertEqual(FindPath(gridA, 0, 0, 0, 5, 3), -1);
        }

        private static void TestFormatPrettyCoins()
        {
            AssertEqual(FormatPrettyCoins(0, ' '), "0");
            AssertEqual(FormatPrettyCoins(5, ' '), "5");
            AssertEqual(FormatPrettyCoins(99, ' '), "99");
            AssertEqual(FormatPrettyCoins(100, ' '), "100");
            AssertEqual(FormatPrettyCoins(1000, ' '), "1 000");
            AssertEqual(FormatPrettyCoins(10000, ' '), "10 000");
            AssertEqual(FormatPrettyCoins(100000, '/'), "100K");
            AssertEqual(FormatPrettyCoins(1000000, ' '), "1 000K");
            AssertEqual(FormatPrettyCoins(10000000, ' '), "10M");
            AssertEqual(FormatPrettyCoins(9999999, '/'), "9/999K");
            AssertEqual(FormatPrettyCoins(99999999, ' '), "99M");
            AssertEqual(FormatPrettyCoins(999999999, ' '), "999M");
            AssertEqual(FormatPrettyCoins(9999999999, ' '), "9 999M");
            AssertEqual(FormatPrettyCoins(10000000000, ' '), "10 000M");
            AssertEqual(FormatPrettyCoins(99999, ' '), "99 999");
            AssertEqual(FormatPrettyCoins(11, ' '), "11");
            AssertEqual(FormatPrettyCoins(1233, ' '), "1 233");
            AssertEqual(FormatPrettyCoins(1717310, ' '), "1 717K");
            AssertEqual(FormatPrettyCoins(7172343310, ' '), "7 172M");
        }

        private static void FindMaxRect()
        {
            AssertEqual(FindMaxRect(new List<int> { 1, 2, 3, 4, 4, 4, 5, 4, 6 }), 24);
            AssertEqual(FindMaxRect(new List<int> { 1, 2, 3, 5, 5, 4, 2, 4, 6 }), 16);
            AssertEqual(FindMaxRect(new List<int> { 8, 9, 3, 5, 5, 2, 3, 4, 6, 1, 6 }), 18);
        }

        /// Тестирующая система, лучше не трогать этот код

        private static void Assert(bool value)
        {
            if (value)
            {
                return;
            }

            throw new Exception("Assertion failed");
        }

        private static void AssertEqual(object value, object expectedValue)
        {
            if (value.Equals(expectedValue))
            {
                return;
            }

            throw new Exception($"Assertion failed expected = {expectedValue} actual = {value}");
        }

        private static void AssertSequenceEqual<T>(IEnumerable<T> value, IEnumerable<T> expectedValue)
        {
            if (ReferenceEquals(value, expectedValue))
            {
                return;
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (expectedValue is null)
            {
                throw new ArgumentNullException(nameof(expectedValue));
            }

            var valueList = value.ToList();
            var expectedValueList = expectedValue.ToList();

            if (valueList.Count != expectedValueList.Count)
            {
                throw new Exception($"Assertion failed expected count = {expectedValueList.Count} actual count = {valueList.Count}");
            }

            for (var i = 0; i < valueList.Count; i++)
            {
                if (!valueList[i].Equals(expectedValueList[i]))
                {
                    throw new Exception($"Assertion failed expected value at {i} = {expectedValueList[i]} actual = {valueList[i]}");
                }
            }
        }

    }

}