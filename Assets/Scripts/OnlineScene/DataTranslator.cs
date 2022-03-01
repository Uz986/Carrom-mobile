using System;
using UnityEngine;

public class DataTranslator : MonoBehaviour {

    private static  string Wins_symbol = "WINS:";
    private static string Loss_symbol = "LOSS:";

    public static string ValuestoData(int wins,int loss)
    {
        return Wins_symbol + wins + "/" + Loss_symbol + loss;
    }

    public static int DatatoKills(string data)
    {
        return int.Parse(DatatoValue(data,Wins_symbol));
    }

    public static int DatatoDeaths(string data)
    {
        return int.Parse(DatatoValue(data, Loss_symbol));
    }

    private static string DatatoValue(string data,string symbol)
    {
        string[] pieces = data.Split('/');
        foreach (string piece in pieces)
        {
            if (piece.StartsWith(symbol))
            {
                return piece.Substring(symbol.Length);
            }

        }
        Debug.LogError(symbol+"not found in"+data);
        return "";
    }

}
