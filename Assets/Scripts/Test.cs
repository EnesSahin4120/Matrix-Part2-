using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        float[,] _elements = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
        float[,] _elements2 = { { 10, 11 }, { 20, 21 }, { 30, 31 } };
        float[,] _elements3 = { { 1, 2 }, { 3, 4 }, { 5, 6 }, { 7, 8 } };
        float[,] _elements4 = { { 1, -4, -5 }, { 2, 4, -2 }, { -4, 4, -6 } };
        float[,] _elements5 = { { 1 }, { -22 }, { -34 } };
        float[,] _elements6 = { { 3, 0, 2, -1 }, { 1, 2, 0, -2 }, { 4, 0, 6, -3 }, { 5, 0, 2, 0 } }; 

        Matrix testMatrix = new Matrix(2, 3, _elements);
        Matrix testMatrix2 = new Matrix(3, 2, _elements2);
        Matrix testMatrix3 = new Matrix(4, 2, _elements3);
        Matrix testMatrix4 = new Matrix(3, 3, _elements4);
        Matrix testMatrix5 = new Matrix(3, 1, _elements5);
        Matrix testMatrix6 = new Matrix(4, 4, _elements6);

        Matrix productTestMatrix = testMatrix * testMatrix2;
        Matrix transposeTestMatrix = Matrix.Transpose(testMatrix3);
        float[] gaussianSolvedTestValues=Mathematics.ValuesWithGaussian(testMatrix4, testMatrix5);
        Matrix cofactorTestMatrix = Matrix.Cofactors(testMatrix6);
        Matrix adjointTestMatrix = Matrix.Adjoint(testMatrix6);
        float determinantTestValue = Matrix.Determinant(testMatrix6);
        Matrix inverseTestMatrix = Matrix.Inverse(testMatrix6);
    }
}
