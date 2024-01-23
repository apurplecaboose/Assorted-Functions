    /// <summary>
    /// Inputs are the drop percentages, output is the random drop value, make sure that p0 + p1 + p2 sum to 100%
    /// </summary>
int RandomDropPerc3Var(int p0, int p1, int p2)
    {
        //initalize
        int[] p0Array = new int[p0];
        int[] p1Array = new int[p1];
        int[] p2Array = new int[p2];
        int[] percentageArray = new int[100];
        //fill with percentages
        for (int i = 0; i < p0; i++)
        {
            p0Array[i] = 0; 
        }
        for (int i = 0; i < p1; i++)
        {
            p1Array[i] = 1;
        }
        for (int i = 0; i < p2; i++)
        {
            p2Array[i] = 2;
        }
        Array.Copy(p0Array, 0, percentageArray, 0, p0Array.Length); //transfer p0 to percentage array
        Array.Copy(p1Array, 0, percentageArray, p0Array.Length, p1Array.Length);//concat percentageArray = p0 + p1
        Array.Copy(p2Array, 0, percentageArray, p0Array.Length + p1Array.Length, p2Array.Length);//concat = p0 + p1 + p2
        int index = UnityEngine.Random.Range(0, 101);
        return percentageArray[index];
    }
