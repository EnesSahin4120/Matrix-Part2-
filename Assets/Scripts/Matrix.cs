using UnityEngine;
public class Matrix 
{
    public int rows;
    public int columns;
    public float[,] elements;
    
    public Matrix(int r,int c, float[,] e)
    {
        rows = r;
        columns = c;
        elements = e;
    }

    public override string ToString()
    {
        string result = "";
        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                result += elements[i, j].ToString();
                result += "\n";
            }
        }
        return result;
    }

    static public Matrix operator +(Matrix a,Matrix b)
    {
        if (a.rows != b.rows || a.columns != b.columns)
            return null;

        Matrix resultMatrix = a;
        for(int i = 0; i < a.rows; i++)
        {
            for(int j = 0; j < b.columns; j++)
            {
                resultMatrix.elements[i, j] = a.elements[i, j] + b.elements[i, j];
            }
        }

        return resultMatrix;
    }

    static public Matrix operator *(Matrix a,Matrix b)
    {
        if (a.columns != b.rows)
            return null;

        float[,] resultElements = new float[a.rows, b.columns];
        for(int i = 0; i < a.rows; i++)
        {
            for(int j = 0; j < b.columns; j++)
            {
                for(int k = 0; k < a.columns;k++)
                {
                    resultElements[i, j] += a.elements[i, k] * b.elements[k, j];
                }
            }
        }

        Matrix resultMatrix = new Matrix(a.rows, b.columns, resultElements);
        return resultMatrix;
    }

    static public Matrix Transpose(Matrix a)
    {
        float[,] resultElements = new float[a.columns, a.rows];
        for(int i = 0; i < a.rows; i++)
        {
            for(int j = 0; j < a.columns; j++)
            {
                resultElements[j, i] = a.elements[i, j];
            }
        }
        Matrix resultMatrix = new Matrix(a.columns, a.rows, resultElements);
        return resultMatrix;
    }

    static public Matrix Cofactors(Matrix A)
    {
        if (A.rows != 4 || A.columns != 4)
            return null;
  
        float a = A.elements[0, 0], b = A.elements[0, 1], c = A.elements[0, 2], d = A.elements[0, 3];
        float e = A.elements[1, 0], f = A.elements[1, 1], g = A.elements[1, 2], h = A.elements[1, 3];
        float i = A.elements[2, 0], j = A.elements[2, 1], k = A.elements[2, 2], l = A.elements[2, 3];
        float m = A.elements[3, 0], n = A.elements[3, 1], o = A.elements[3, 2], p = A.elements[3, 3];

        float kp_lo = k * p - l * o;
        float jp_ln = j * p - l * n;
        float jo_kn = j * o - k * n;
        float ip_lm = i * p - l * m;
        float io_km = i * o - k * m;
        float in_jm = i * n - j * m;

        float gp_ho = g * p - h * o;
        float fp_hn = f * p - h * n;
        float fo_gn = f * o - g * n;
        float ep_hm = e * p - h * m;
        float eo_gm = e * o - g * m;
        float en_fm = e * n - f * m;

        float gl_hk = g * l - h * k;
        float fl_hj = f * l - h * j;
        float fk_gj = f * k - g * j;
        float el_hi = e * l - h * i;
        float ek_gi = e * k - g * i;
        float ej_fi = e * j - f * i;

        float a00 = +(f * kp_lo - g * jp_ln + h * jo_kn);
        float a01 = -(e * kp_lo - g * ip_lm + h * io_km);
        float a02 = +(e * jp_ln - f * ip_lm + h * in_jm);
        float a03 = -(e * jo_kn - f * io_km + g * in_jm);

        float a10 = -(b * kp_lo - c * jp_ln + d * jo_kn);
        float a11 = +(a * kp_lo - c * ip_lm + d * io_km);
        float a12 = -(a * jp_ln - b * ip_lm + d * in_jm);
        float a13 = +(a * jo_kn - b * io_km + c * in_jm);

        float a20 = +(b * gp_ho - c * fp_hn + d * fo_gn);
        float a21 = -(a * gp_ho - c * ep_hm + d * eo_gm);
        float a22 = +(a * fp_hn - b * ep_hm + d * en_fm);
        float a23 = -(a * fo_gn - b * eo_gm + c * en_fm);

        float a30 = -(b * gl_hk - c * fl_hj + d * fk_gj);
        float a31 = +(a * gl_hk - c * el_hi + d * ek_gi);
        float a32 = -(a * fl_hj - b * el_hi + d * ej_fi);
        float a33 = +(a * fk_gj - b * ek_gi + c * ej_fi);

        float[,] resultElements = new float[4, 4];
        resultElements[0, 0] = a00; resultElements[0, 1] = a01; resultElements[0, 2] = a02; resultElements[0, 3] = a03;
        resultElements[1, 0] = a10; resultElements[1, 1] = a11; resultElements[1, 2] = a12; resultElements[1, 3] = a13;
        resultElements[2, 0] = a20; resultElements[2, 1] = a21; resultElements[2, 2] = a22; resultElements[2, 3] = a23;
        resultElements[3, 0] = a30; resultElements[3, 1] = a31; resultElements[3, 2] = a32; resultElements[3, 3] = a33;

        Matrix resultMatrix = new Matrix(4, 4, resultElements);
        return resultMatrix;
    }

    static public Matrix Adjoint(Matrix a)
    {
        if (a.rows != 4 || a.columns != 4)
            return null;

        Matrix cofactorMatrix = Cofactors(a);
        Matrix result = Transpose(cofactorMatrix);

        return result;
    }

    static public float Determinant(Matrix A)
    {
        Matrix cofactorMatrix = Cofactors(A);
        float result = 0;

        for(int i = 0; i < 4; i++)
        {
            result += A.elements[0, i] * cofactorMatrix.elements[0, i];
        }

        return result;
    }
    
    static public Matrix Inverse(Matrix A)
    {
        if (A.rows != 4 || A.columns != 4)
            return null;

        Matrix adjointMatrix = Adjoint(A);
        float determinant = Determinant(A);

        float[,] resultElements = new float[4, 4];
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                resultElements[i, j] = adjointMatrix.elements[i, j] / determinant;
            }
        }

        Matrix resultMatrix = new Matrix(4, 4, resultElements);
        return resultMatrix;
    }
}
