using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labs_RPM.Flyweight
{
    public class Character
    {
        public char Symbol { get; } // Внутреннее состояние
        public string Font { get; }   // Внутреннее состояние
        public Character(char symbol, string font)
        {
            Symbol = symbol;
            Font = font;
        }
        public void Display(int pointSize) =>
            Console.WriteLine($"Char: {Symbol}, Font: {Font}, Size: {pointSize}");
    }
}
