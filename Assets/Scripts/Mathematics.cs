public class Mathematics 
{
    static public float[] ValuesWithGaussian(Matrix a,Matrix b)
    {
        if (a.rows != a.columns || b.rows != a.rows || b.columns != 1)
            return null;

        for (int k = 0; k < a.columns; k++)
        {
            for(int i = k + 1; i < a.rows; i++)
            {
                //Find cofactor for zero value
                float m = a.elements[i, k] / a.elements[k, k];

                //row operations
                for(int j = 0; j < a.columns; j++)
                {
                    a.elements[i, j] -= (m * a.elements[k, j]);
                }
                b.elements[i, 0] -= (m * b.elements[k, 0]);
            }
        }

        float temp = 0;
        float u = 0;
        float[] resultArray = new float[a.columns];
        for (int i = a.rows - 1; i >= 0; i--)
        {
            for(int j = a.columns - 1; j >= 0; j--)
            {
                if (i == j)
                {
                    u = a.elements[i, j];
                    break;
                }
                else
                {
                    temp += a.elements[i, j] * b.elements[j, 0];
                }
            }
            b.elements[i, 0] = (b.elements[i, 0] - temp) / u;
            resultArray[i] = b.elements[i, 0];
            temp = 0;
        }

        return resultArray;
    }

    
}
