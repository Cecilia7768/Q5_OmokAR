using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class OmokLogic : MonoBehaviour
{
    private int stoneSize = 36;
    private int gridSize = 40;
    private int startpointSize = 16;

    private Graphics g;


    private bool turn = false;
    private enum STONE { none, black, white };
    private STONE[,] board = new STONE[19, 19];


    private bool isDrawable(int x, int y)
    {
        // �̹� � ���� ������ ���� ��
        if (!(board[x, y] == STONE.none))
            return false;

        if (turn)    // �鵹
            board[x, y] = STONE.white;
        else  // �浹
            board[x, y] = STONE.black;

        if (!turn) // �浹 ������ ���� �ݼ� üũ
        {
            if (checkForbidden(x, y))
            {
                board[x, y] = STONE.none;
                return false;
            }

            else
            {
                board[x, y] = STONE.none;
                return true;
            }
        }

        else
        {
            board[x, y] = STONE.none;
            return true;
        }
    }


    private bool checkForbidden(int x, int y)
    {
        if (forbidden_33(x, y))
            return true;

        else if (forbidden_44(x, y))
            return true;

        else
            return false;
    }

    private bool forbidden_33(int x, int y)
    {
        if (countHorizon(x, y, 3) && countVertical(x, y, 3)) // ��, ��
            return true;

        else if (countHorizon(x, y, 3) && countDiagonal_LDRU(x, y, 3)) // ��, /
            return true;

        else if (countHorizon(x, y, 3) && countDiagonal_LURD(x, y, 3)) // ��, ��
            return true;

        else if (countVertical(x, y, 3) && countDiagonal_LDRU(x, y, 3)) // ��, /
            return true;

        else if (countVertical(x, y, 3) && countDiagonal_LURD(x, y, 3)) // ��, ��
            return true;

        else if (countDiagonal_LURD(x, y, 3) && countDiagonal_LDRU(x, y, 3)) // /, ��
            return true;

        else
            return false;
    }

    private bool forbidden_44(int x, int y)
    {
        if (countHorizon(x, y, 4) && countVertical(x, y, 4)) // ��, ��
            return true;

        else if (countHorizon(x, y, 4) && countDiagonal_LDRU(x, y, 4)) // ��, /
            return true;

        else if (countHorizon(x, y, 4) && countDiagonal_LURD(x, y, 4)) // ��, ��
            return true;

        else if (countVertical(x, y, 4) && countDiagonal_LDRU(x, y, 4)) // ��, /
            return true;

        else if (countVertical(x, y, 4) && countDiagonal_LURD(x, y, 4)) // ��, ��
            return true;

        else if (countDiagonal_LURD(x, y, 4) && countDiagonal_LDRU(x, y, 4)) // /, ��
            return true;

        else
            return false;
    }

    private bool countHorizon(int x, int y, int lineCount)
    {
        int count = 0;
        int max_check;
        bool blocker = false;

        if (lineCount == 5)
            max_check = lineCount + 1;
        else
            max_check = lineCount + 2;

        for (int i = x; i <= 18 && i <= x + max_check; i++)
            if (board[i, y] == board[x, y])
                count++;
            else if (board[i, y] != STONE.none)
            {
                blocker = true;
                break;
            }
            else if (lineCount == 5 && board[i, y] == STONE.none)
                break;

        for (int i = x; i >= 0 && i >= x - max_check; i--)
            if (board[i, y] == board[x, y])
                count++;
            else if (board[i, y] != STONE.none)
            {
                blocker = true;
                break;
            }
            else if (lineCount == 5 && board[i, y] == STONE.none)
                break;

        if (count - 1 == lineCount)
        {
            if (lineCount == 3 && blocker)
                return false;
            else
                return true;
        }
        else
            return false;
    }

    private bool countVertical(int x, int y, int lineCount)
    {
        int count = 0;
        int max_check;
        bool blocker = false;

        if (lineCount == 5)
            max_check = lineCount + 1;
        else
            max_check = lineCount + 2;

        for (int i = y; i <= 18 && i <= y + max_check; i++)
            if (board[x, i] == board[x, y])
                count++;
            else if (board[x, i] != STONE.none)
            {
                blocker = true;
                break;
            }
            else if (lineCount == 5 && board[i, y] == STONE.none)
                break;

        for (int i = y; i >= 0 && i >= y - max_check; i--)
            if (board[x, i] == board[x, y])
                count++;
            else if (board[x, i] != STONE.none)
            {
                blocker = true;
                break;
            }
            else if (lineCount == 5 && board[i, y] == STONE.none)
                break;

        if (count - 1 == lineCount)
        {
            if (lineCount == 3 && blocker)
                return false;
            else
                return true;
        }
        else
            return false;
    }

    private bool countDiagonal_LURD(int x, int y, int lineCount)
    {
        int count = 0;
        int max_check;
        bool blocker = false;

        if (lineCount == 5)
            max_check = lineCount + 1;
        else
            max_check = lineCount + 2;

        for (int i = x, j = y; (i <= 18 && j >= 0) && (i <= x + max_check && j >= y - max_check); i++, j--)
            if (board[i, j] == board[x, y])
                count++;
            else if (board[i, y] != STONE.none)
            {
                blocker = true;
                break;
            }
            else if (lineCount == 5 && board[i, y] == STONE.none)
                break;

        for (int i = x, j = y; (i >= 0 && j <= 18) && (i >= x - max_check && j <= y + max_check); i--, j++)
            if (board[i, j] == board[x, y])
                count++;
            else if (board[i, y] != STONE.none)
            {
                blocker = true;
                break;
            }
            else if (lineCount == 5 && board[i, y] == STONE.none)
                break;

        if (count - 1 == lineCount)
        {
            if (lineCount == 3 && blocker)
                return false;
            else
                return true;
        }
        else
            return false;

    }

    private bool countDiagonal_LDRU(int x, int y, int lineCount)
    {
        int count = 0;
        int max_check;
        bool blocker = false;

        if (lineCount == 5)
            max_check = lineCount + 1;
        else
            max_check = lineCount + 2;

        for (int i = x, j = y; (i >= 0 && j >= 0) && (i >= x - max_check && j >= y - max_check); i--, j--)
            if (board[i, j] == board[x, y])
                count++;
            else if (board[i, y] != STONE.none)
            {
                blocker = true;
                break;
            }
            else if (lineCount == 5 && board[i, y] == STONE.none)
                break;

        for (int i = x, j = y; (i <= 18 && j <= 18) && (i <= x + max_check && j <= y + max_check); i++, j++)
            if (board[i, j] == board[x, y])
                count++;
            else if (board[i, y] != STONE.none)
            {
                blocker = true;
                break;
            }
            else if (lineCount == 5 && board[i, y] == STONE.none)
                break;

        if (count - 1 == lineCount)
        {
            if (lineCount == 3 && blocker)
                return false;
            else
                return true;
        }
        else
            return false;
    }

//    private void checkWin(int x, int y)
//    {

//        if (countHorizon(x, y, 5)) // ��
//            finishGame(x, y);

//        else if (countVertical(x, y, 5)) // �� 
//            finishGame(x, y);

//        else if (countDiagonal_LDRU(x, y, 5)) // X
//            finishGame(x, y);

//        else if (countDiagonal_LURD(x, y, 5)) // X
//            finishGame(x, y);
//    }


}
