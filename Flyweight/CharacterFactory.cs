using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labs_RPM.Flyweight
{
    public class CharacterFactory
    {
        private Dictionary<string, Character> _characters = new();

        public Character GetCharacter(char symbol, string font)
        {
            string key = $"{symbol}_{font}";
            if (!_characters.ContainsKey(key))
            {
                _characters[key] = new Character(symbol, font);
            }
            return _characters[key];
        }
    }
}
