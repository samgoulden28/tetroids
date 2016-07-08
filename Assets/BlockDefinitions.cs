using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockDefinitions : MonoBehaviour {

    //I

    private static int[,] iRotation1 = new int[,]  {     { 0, 1, 0, 0 },
                                                         { 0, 1, 0, 0 },
                                                         { 0, 1, 0, 0 },
                                                         { 0, 1, 0, 0 } };


    private static int[,] iRotation2 = new int[,]  {     { 0, 0, 0, 0 },
                                                         { 1, 1, 1, 1 },
                                                         { 0, 0, 0, 0 },
                                                         { 0, 0, 0, 0 } };

    private static int[,] iRotation3 = new int[,]  {     { 0, 0, 1, 0 },
                                                         { 0, 0, 1, 0 },
                                                         { 0, 0, 1, 0 },
                                                         { 0, 0, 1, 0 } };

    private static int[,] iRotation4 = new int[,]  {     { 0, 0, 0, 0 },
                                                         { 0, 0, 0, 0 },
                                                         { 1, 1, 1, 1 },
                                                         { 0, 0, 0, 0 } };

    //J

    private static int[,] jRotation1 = new int[,]  {     { 0, 1, 0, 0 },
                                                         { 0, 1, 0, 0 },
                                                         { 1, 1, 0, 0 },
                                                         { 0, 0, 0, 0 } };


    private static int[,] jRotation2 = new int[,]  {     { 1, 0, 0, 0 },
                                                         { 1, 1, 1, 0 },
                                                         { 0, 0, 0, 0 },
                                                         { 0, 0, 0, 0 } };

    private static int[,] jRotation3 = new int[,]  {     { 0, 1, 1, 0 },
                                                         { 0, 1, 0, 0 },
                                                         { 0, 1, 0, 0 },
                                                         { 0, 0, 0, 0 } };

    private static int[,] jRotation4 = new int[,]  {     { 0, 0, 0, 0 },
                                                         { 1, 1, 1, 0 },
                                                         { 0, 0, 1, 0 },
                                                         { 0, 0, 0, 0 } };

    //L

    private static int[,] lRotation1 = new int[,]  {     { 0, 1, 0, 0 },
                                                         { 0, 1, 0, 0 },
                                                         { 0, 1, 1, 0 },
                                                         { 0, 0, 0, 0 } };


    private static int[,] lRotation2 = new int[,]  {     { 0, 0, 0, 0 },
                                                         { 1, 1, 1, 0 },
                                                         { 1, 0, 0, 0 },
                                                         { 0, 0, 0, 0 } };

    private static int[,] lRotation3 = new int[,]  {     { 1, 1, 0, 0 },
                                                         { 0, 1, 0, 0 },
                                                         { 0, 1, 0, 0 },
                                                         { 0, 0, 0, 0 } };

    private static int[,] lRotation4 = new int[,]  {     { 0, 0, 1, 0 },
                                                         { 1, 1, 1, 0 },
                                                         { 0, 0, 0, 0 },
                                                         { 0, 0, 0, 0 } };

    //O

    private static int[,] oRotation1 = new int[,]  {     { 1, 1, 0, 0 },
                                                         { 1, 1, 0, 0 },
                                                         { 0, 0, 0, 0 },
                                                         { 0, 0, 0, 0 } };


    private static int[,] oRotation2 = new int[,]  {     { 1, 1, 0, 0 },
                                                         { 1, 1, 0, 0 },
                                                         { 0, 0, 0, 0 },
                                                         { 0, 0, 0, 0 } };

    private static int[,] oRotation3 = new int[,]  {     { 1, 1, 0, 0 },
                                                         { 1, 1, 0, 0 },
                                                         { 0, 0, 0, 0 },
                                                         { 0, 0, 0, 0 } };

    private static int[,] oRotation4 = new int[,]  {     { 1, 1, 0, 0 },
                                                         { 1, 1, 0, 0 },
                                                         { 0, 0, 0, 0 },
                                                         { 0, 0, 0, 0 } };

    //S

    private static int[,] sRotation1 = new int[,]  {     { 0, 0, 0, 0 },
                                                         { 0, 1, 1, 0 },
                                                         { 1, 1, 0, 0 },
                                                         { 0, 0, 0, 0 } };


    private static int[,] sRotation2 = new int[,]  {     { 1, 0, 0, 0 },
                                                         { 1, 1, 0, 0 },
                                                         { 0, 1, 0, 0 },
                                                         { 0, 0, 0, 0 } };

    private static int[,] sRotation3 = new int[,]  {     { 0, 1, 1, 0 },
                                                         { 1, 1, 0, 0 },
                                                         { 0, 0, 0, 0 },
                                                         { 0, 0, 0, 0 } };

    private static int[,] sRotation4 = new int[,]  {     { 0, 1, 0, 0 },
                                                         { 0, 1, 1, 0 },
                                                         { 0, 0, 1, 0 },
                                                         { 0, 0, 0, 0 } };

    //T

    private static int[,] tRotation1 = new int[,]  {     { 0, 0, 0, 0 },
                                                         { 1, 1, 1, 0 },
                                                         { 0, 1, 0, 0 },
                                                         { 0, 0, 0, 0 } };


    private static int[,] tRotation2 = new int[,]  {     { 0, 1, 0, 0 },
                                                         { 1, 1, 0, 0 },
                                                         { 0, 1, 0, 0 },
                                                         { 0, 0, 0, 0 } };

    private static int[,] tRotation3 = new int[,]  {     { 0, 1, 0, 0 },
                                                         { 1, 1, 1, 0 },
                                                         { 0, 0, 0, 0 },
                                                         { 0, 0, 0, 0 } };

    private static int[,] tRotation4 = new int[,]  {     { 0, 1, 0, 0 },
                                                         { 0, 1, 1, 0 },
                                                         { 0, 1, 0, 0 },
                                                         { 0, 0, 0, 0 } };

    //Z

    private static int[,] zRotation1 = new int[,]  {     { 0, 0, 0, 0 },
                                                         { 1, 1, 0, 0 },
                                                         { 0, 1, 1, 0 },
                                                         { 0, 0, 0, 0 } };


    private static int[,] zRotation2 = new int[,]  {     { 0, 1, 0, 0 },
                                                         { 0, 1, 0, 0 },
                                                         { 1, 1, 0, 0 },
                                                         { 0, 0, 0, 0 } };

    private static int[,] zRotation3 = new int[,]  {     { 1, 1, 0, 0 },
                                                         { 0, 1, 1, 0 },
                                                         { 0, 0, 0, 0 },
                                                         { 0, 0, 0, 0 } };

    private static int[,] zRotation4 = new int[,]  {     { 0, 0, 1, 0 },
                                                         { 0, 1, 1, 0 },
                                                         { 0, 1, 0, 0 },
                                                         { 0, 0, 0, 0 } };



    public static List<int[,]> I()
    {
        List<int[,]> iRotations = new List<int[,]>();
        iRotations.Add(iRotation1);
        iRotations.Add(iRotation2);
        iRotations.Add(iRotation3);
        iRotations.Add(iRotation4);
        return iRotations;
    }

    public static List<int[,]> J()
    {
        List<int[,]> jRotations = new List<int[,]>();
        jRotations.Add(jRotation1);
        jRotations.Add(jRotation2);
        jRotations.Add(jRotation3);
        jRotations.Add(jRotation4);
        return jRotations;
    }

    public static List<int[,]> L()
    {
        List<int[,]> lRotations = new List<int[,]>();
        lRotations.Add(lRotation1);
        lRotations.Add(lRotation2);
        lRotations.Add(lRotation3);
        lRotations.Add(lRotation4);
        return lRotations;
    }

    public static List<int[,]> O()
    {
        List<int[,]> oRotations = new List<int[,]>();
        oRotations.Add(oRotation1);
        oRotations.Add(oRotation2);
        oRotations.Add(oRotation3);
        oRotations.Add(oRotation4);
        return oRotations;
    }

    public static List<int[,]> S()
    {
        List<int[,]> sRotations = new List<int[,]>();
        sRotations.Add(sRotation1);
        sRotations.Add(sRotation2);
        sRotations.Add(sRotation3);
        sRotations.Add(sRotation4);
        return sRotations;
    }

    public static List<int[,]> T()
    {
        List<int[,]> tRotations = new List<int[,]>();
        tRotations.Add(tRotation1);
        tRotations.Add(tRotation2);
        tRotations.Add(tRotation3);
        tRotations.Add(tRotation4);
        return tRotations;
    }

    public static List<int[,]> Z()
    {
        List<int[,]> zRotations = new List<int[,]>();
        zRotations.Add(zRotation1);
        zRotations.Add(zRotation2);
        zRotations.Add(zRotation3);
        zRotations.Add(zRotation4);
        return zRotations;
    }
}
