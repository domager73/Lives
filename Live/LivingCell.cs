using Microsoft.VisualBasic;
using System;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Live
{
    class LivingCell
    {
        private int X;
        private int Y;
        private int gender;
        private int hp;
        private ConsoleColor color;
        private char look;

        public LivingCell(int x, int y, int Hp, int gender)
        {
            this.X = x;
            this.Y = y;
            this.gender = gender;
            this.hp = Hp;
            this.color = gender == 0? ConsoleColor.Blue : ConsoleColor.Red;
            this.look = gender == 0? '#' : '@';
        }

        public int GetX(){ return X; }
        
        public int GetY() { return Y; }

        public int GetHp() { return hp; }

        public void SetX(int x) { this.X = x; }
        public void SetY(int y) { this.Y = y; }

        public int GetGender() { return gender; }

        public void Draw() 
        {
            Console.SetCursorPosition(X, Y);
            Console.ForegroundColor = color;
            Console.Write(look);
        }

        public void Move() 
        {
            Random rnd = new Random();

            int move = rnd.Next(0, 3 + 1);

            switch (move) 
            {
                case 0: 
                    {
                        this.Y++; break;
                    }
                case 1:
                    {
                        this.Y--; break;
                    }
                case 2:
                    {
                        this.X++; break;
                    }
                case 3:
                    {
                        this.X--; break;
                    }
            }    
            
        }
    }
}
        