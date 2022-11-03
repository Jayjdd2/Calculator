
internal class Program
{
    //Summary
    //I took around 6 hours to complete this test.
    public static double Calculate(string input)
    {
        double sum = 0, index1Value, index2Value;
        var matchedOperatorIndex = 0;
        string[] inputString = input.Split(' ');
        var cloneInputString = inputString.ToList();
        try
        {
            #region Bracket
            int[] allMatchedLeftBracketIndex = inputString.Select((value, index) => new { Value = value, Index = index }).
                                                           Where(x => x.Value.Contains('(')).
                                                           Select(x => x.Index).
                                                           OrderByDescending(x => x).
                                                           ToArray();

            int[] allMatchedRightBracketIndex = inputString.Select((value, index) => new { Value = value, Index = index }).
                                                            Where(x => x.Value.Contains(')')).
                                                            Select(x => x.Index).
                                                            ToArray();

            if (allMatchedLeftBracketIndex.Length != allMatchedRightBracketIndex.Length)
            {
                Console.WriteLine("Please check bracket for " + input);
                return 0;


            }
            else
            {
                var totalOfBracket = allMatchedLeftBracketIndex.Length;
                for (var index = 0; index < totalOfBracket; index++)
                {
                    var allMatchedRightBracketIndexDescending = allMatchedRightBracketIndex.OrderByDescending(x => x).ToArray();

                    List<string> inBracketQuestion = cloneInputString.Skip(allMatchedLeftBracketIndex[0] + 1).
                                                                      Take(allMatchedRightBracketIndexDescending[0] - allMatchedLeftBracketIndex[0] - allMatchedLeftBracketIndex.Length).
                                                                      ToList();

                    cloneInputString.RemoveRange(allMatchedLeftBracketIndex[0], allMatchedRightBracketIndex[0] - allMatchedLeftBracketIndex[0] + 1);


                    #region Multiply and Divide
                    matchedOperatorIndex = inBracketQuestion.Select((value, index) => new { Value = value, Index = index }).
                                                             Where(x => x.Value == "*" || x.Value == "/").
                                                             Select(x => x.Index).
                                                             FirstOrDefault();

                    while (matchedOperatorIndex > 0)
                    {
                        var selectedOperator = inBracketQuestion[matchedOperatorIndex];
                        switch (selectedOperator)
                        {
                            case "*":
                                if (matchedOperatorIndex - 1 < inBracketQuestion.Count && double.TryParse(inBracketQuestion[matchedOperatorIndex - 1], out index1Value) &&
                                    matchedOperatorIndex + 1 < inBracketQuestion.Count && double.TryParse(inBracketQuestion[matchedOperatorIndex + 1], out index2Value))
                                {
                                    sum = index1Value * index2Value;

                                    inBracketQuestion.RemoveRange(matchedOperatorIndex - 1, 3);
                                }

                                inBracketQuestion.Insert(matchedOperatorIndex - 1, sum.ToString());
                                break;
                            case "/":
                                if (matchedOperatorIndex - 1 < inBracketQuestion.Count && double.TryParse(inBracketQuestion[matchedOperatorIndex - 1], out index1Value) &&
                                    matchedOperatorIndex + 1 < inBracketQuestion.Count && double.TryParse(inBracketQuestion[matchedOperatorIndex + 1], out index2Value))
                                {
                                    sum = index1Value / index2Value;

                                    inBracketQuestion.RemoveRange(matchedOperatorIndex - 1, 3);
                                }


                                inBracketQuestion.Insert(matchedOperatorIndex - 1, sum.ToString());
                                break;
                        }

                        matchedOperatorIndex = inBracketQuestion.Select((value, index) => new { Value = value, Index = index }).
                                                                 Where(x => x.Value == "*" || x.Value == "/").
                                                                 Select(x => x.Index).
                                                                 SingleOrDefault();
                    }

                    #endregion

                    #region Plus and Minus

                    matchedOperatorIndex = inBracketQuestion.Select((value, index) => new { Value = value, Index = index }).
                                                             Where(x => x.Value == "+" || x.Value == "-").
                                                             Select(x => x.Index).
                                                             FirstOrDefault();

                    while (matchedOperatorIndex > 0)
                    {
                        var selectedOperator = inBracketQuestion[matchedOperatorIndex];
                        switch (selectedOperator)
                        {
                            case "+":

                                if (matchedOperatorIndex - 1 < inBracketQuestion.Count && double.TryParse(inBracketQuestion[matchedOperatorIndex - 1], out index1Value) &&
                                    matchedOperatorIndex + 1 < inBracketQuestion.Count && double.TryParse(inBracketQuestion[matchedOperatorIndex + 1], out index2Value))
                                {
                                    sum = index1Value + index2Value;

                                    inBracketQuestion.RemoveRange(matchedOperatorIndex - 1, 3);
                                }

                                inBracketQuestion.Insert(matchedOperatorIndex - 1, sum.ToString());

                                break;

                            case "-":

                                if (matchedOperatorIndex - 1 < inBracketQuestion.Count && double.TryParse(inBracketQuestion[matchedOperatorIndex - 1], out index1Value) &&
                                    matchedOperatorIndex + 1 < inBracketQuestion.Count && double.TryParse(inBracketQuestion[matchedOperatorIndex + 1], out index2Value))
                                {
                                    sum = index1Value - index2Value;

                                    inBracketQuestion.RemoveRange(matchedOperatorIndex - 1, 3);
                                }

                                inBracketQuestion.Insert(matchedOperatorIndex - 1, sum.ToString());
                                break;
                        }
                        matchedOperatorIndex = inBracketQuestion.Select((value, index) => new { Value = value, Index = index }).
                                                                 Where(x => x.Value == "+" || x.Value == "-").
                                                                 Select(x => x.Index).
                                                                 SingleOrDefault();
                    }
                    #endregion


                    cloneInputString.Insert(allMatchedLeftBracketIndex[0], sum.ToString());

                    //To check if there is still bracket
                    if (cloneInputString.Contains("(") && cloneInputString.Contains(")"))
                    {
                        allMatchedLeftBracketIndex = cloneInputString.Select((value, index) => new { Value = value, Index = index }).
                                                                      Where(x => x.Value.Contains('(')).
                                                                      Select(x => x.Index).
                                                                      OrderByDescending(x => x).
                                                                      ToArray();

                        allMatchedRightBracketIndex = cloneInputString.Select((value, index) => new { Value = value, Index = index }).
                                                                       Where(x => x.Value.Contains(')')).
                                                                       Select(x => x.Index).
                                                                       ToArray();
                    }
                }


                #region Multiple and Divide
                matchedOperatorIndex = cloneInputString.Select((value, index) => new { Value = value, Index = index }).
                                                            Where(x => x.Value == "*" || x.Value == "/").
                                                            Select(x => x.Index).
                                                            FirstOrDefault();

                while (matchedOperatorIndex > 0)
                {
                    var selectedOperator = cloneInputString[matchedOperatorIndex];
                    switch (selectedOperator)
                    {
                        case "*":
                            if (matchedOperatorIndex - 1 < cloneInputString.Count && double.TryParse(cloneInputString[matchedOperatorIndex - 1], out index1Value) &&
                        matchedOperatorIndex + 1 < cloneInputString.Count && double.TryParse(cloneInputString[matchedOperatorIndex + 1], out index2Value))
                            {
                                sum = index1Value * index2Value;

                                cloneInputString.RemoveRange(matchedOperatorIndex - 1, 3);

                            }

                            cloneInputString.Insert(matchedOperatorIndex - 1, sum.ToString());
                            break;

                        case "/":
                            if (matchedOperatorIndex - 1 < cloneInputString.Count && double.TryParse(cloneInputString[matchedOperatorIndex - 1], out index1Value) &&
                        matchedOperatorIndex + 1 < cloneInputString.Count && double.TryParse(cloneInputString[matchedOperatorIndex + 1], out index2Value))
                            {
                                sum = index1Value / index2Value;

                                cloneInputString.RemoveRange(matchedOperatorIndex - 1, 3);
                            }

                            cloneInputString.Insert(matchedOperatorIndex - 1, sum.ToString());
                            break;

                    }

                    matchedOperatorIndex = cloneInputString.Select((value, index) => new { Value = value, Index = index }).
                                                            Where(x => x.Value == "*" || x.Value == "/").
                                                            Select(x => x.Index).
                                                            FirstOrDefault();
                }

                #endregion

                #region Plus and Minus
                matchedOperatorIndex = cloneInputString.Select((value, index) => new { Value = value, Index = index }).
                                                            Where(x => x.Value == "+" || x.Value == "-").
                                                            Select(x => x.Index).
                                                            FirstOrDefault();

                while (matchedOperatorIndex > 0)
                {
                    var selectedOperator = cloneInputString[matchedOperatorIndex];
                    switch (selectedOperator)
                    {
                        case "+":

                            if (matchedOperatorIndex - 1 < cloneInputString.Count && double.TryParse(cloneInputString[matchedOperatorIndex - 1], out index1Value) &&
                            matchedOperatorIndex + 1 < cloneInputString.Count && double.TryParse(cloneInputString[matchedOperatorIndex + 1], out index2Value))
                            {
                                sum = index1Value + index2Value;

                                cloneInputString.RemoveRange(matchedOperatorIndex - 1, 3);
                            }

                            cloneInputString.Insert(matchedOperatorIndex - 1, sum.ToString());

                            break;

                        case "-":

                            if (matchedOperatorIndex - 1 < cloneInputString.Count && double.TryParse(cloneInputString[matchedOperatorIndex - 1], out index1Value) &&
                            matchedOperatorIndex + 1 < cloneInputString.Count && double.TryParse(cloneInputString[matchedOperatorIndex + 1], out index2Value))
                            {
                                sum = index1Value - index2Value;

                                cloneInputString.RemoveRange(matchedOperatorIndex - 1, 3);
                            }

                            cloneInputString.Insert(matchedOperatorIndex - 1, sum.ToString());
                            break;
                    }
                    matchedOperatorIndex = cloneInputString.Select((value, index) => new { Value = value, Index = index }).
                                                            Where(x => x.Value == "+" || x.Value == "-").
                                                            Select(x => x.Index).
                                                            FirstOrDefault();
                }
                #endregion
            }
            #endregion
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: "+ ex);
            return 0;
        }

        Console.WriteLine(sum);
        return sum;
    }

    private static void Main(string[] args)
    {
        var stringInput = new List<string> {
            "1 + 1",
            "2 * 2",
            "1 + 2 + 3",
            "6 / 2",
            "11 + 23",
            "11.1 + 23",
            "1 + 1 * 3",
            "( 11.5 + 15.4 ) + 10.1",
            "23 - ( 29.3 - 12.5 )",
            "( 1 / 2 ) - 1 + 1",
            "10 - ( 2 + 3 * ( 7 - 5 ) )" };
        foreach (var i in stringInput)
        {
            Calculate(i);
        }
    }
}