using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Text;

public class BadWordFilter
{
    private static string[] badWords = null;

    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";

    public static bool Filter(string _word)
    {
        if (badWords == null || badWords.Length == 0) Read();

        foreach (string bad in badWords)
        {
            if (Regex.IsMatch(_word, bad)) return true;
        }
        return false;
    }

    private static void Read()
    {
        TextAsset data = Resources.Load("Docs\\BadWords") as TextAsset;
        badWords = Regex.Split(data.text, SPLIT_RE);
    }
}