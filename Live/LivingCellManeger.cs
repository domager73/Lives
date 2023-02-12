using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Live
{
    class LivingCellManeger
    {
        public int minY, maxY, maxX, minX;
        public Random rnd;
        public LivingCell[] livingCell;

        public LivingCellManeger(int minY, int maxY, int minX, int maxX, int lenghLivingCell)
        {
            this.rnd = new Random();
            this.minY = minY;
            this.maxY = maxY;
            this.maxX = maxX;
            this.minX = minX;
            this.livingCell = new LivingCell[lenghLivingCell];

            InitLivingCell();
        }

        public void ResizeArray()
        {
            LivingCell[] newArray = new LivingCell[livingCell.Length + 1];

            for (int i = 0; i < newArray.Length - 1; i++)
            {
                newArray[i] = livingCell[i];
            }

            livingCell = newArray;
        }

        public void AddNewCell() 
        {
            livingCell[livingCell.Length - 1] = new LivingCell(
                    rnd.Next(minX + 1, maxX - 1),
                    rnd.Next(minY + 1, maxY - 1),
                    rnd.Next(1, 10),
                    rnd.Next(0, 1 + 1)
                    );
        }

        public void InitLivingCell()
        {
            char[] ganders = ("@#").ToCharArray();

            for (int i = 0; i < livingCell.Length; i++)
            {
                livingCell[i] = new LivingCell(
                    rnd.Next(minX + 1, maxX - 1),
                    rnd.Next(minY + 1, maxY - 1),
                    rnd.Next(1, 3 + 1),
                    rnd.Next(0, 1 + 1)
                    );
            }
        }

        private void MoveLivingCell()
        {
            for (int i = 0; i < livingCell.Length; i++)
            {
                if (livingCell[i].GetY() != 0 && livingCell[i].GetX() != 0)
                {
                    livingCell[i].Move();

                    if (livingCell[i].GetY() < minY || livingCell[i].GetX() < minX || livingCell[i].GetX() > maxX || livingCell[i].GetY() > maxY)
                    {
                        livingCell[i].SetX(rnd.Next(minX + 2, maxX - 2));
                        livingCell[i].SetY(rnd.Next(minY + 2, maxY - 2));
                    }
                }
            }
        }

        public void CheckFight()
        {
            bool checkCount = true;

            for (int i = 0; i < livingCell.Length; i++)
            {
                for (int j = 0; j < livingCell.Length; j++)
                {
                    if (livingCell[i].GetX() == livingCell[j].GetX() && // проверка на одинаковые координаты
                        livingCell[i].GetY() == livingCell[j].GetY() && i != j && checkCount)
                    {
                        if (livingCell[i].GetGender() == livingCell[j].GetGender()) //  проверка на одинаковые гендэры
                        {
                            if (livingCell[i].GetHp() > livingCell[j].GetHp()) // проверка на кол hp
                            {
                                livingCell[j].SetY(0);
                                livingCell[j].SetX(0);

                                livingCell[i].SetHp(livingCell[i].GetHp() + livingCell[j].GetHp() / 2); 
                            }
                            else
                            {
                                livingCell[i].SetY(0);
                                livingCell[i].SetX(0);

                                livingCell[i].SetHp(livingCell[j].GetHp() + livingCell[i].GetHp() / 2);
                            }
                        }
                        else
                        {
                            if (rnd.Next(1, 30 + 1) == 4)
                            {
                                ResizeArray();

                                AddNewCell();
                            }

                            checkCount = false;
                        }
                    }
                }
                checkCount = true;
            }
        }

        private void Draw()
        {
            for (int i = 0; i < livingCell.Length; i++)
            {
                if (livingCell[i].GetX() != 0 && livingCell[i].GetY() != 0)
                {
                    livingCell[i].Draw();
                }
            }
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();

                CheckFight();

                MoveLivingCell();

                Draw();

                Thread.Sleep(100);
            }
        }
    }
}