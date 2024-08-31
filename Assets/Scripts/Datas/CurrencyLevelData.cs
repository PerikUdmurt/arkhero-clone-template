using ArkheroClone.Gameplay.Characters;
using System.Collections.Generic;

namespace ArkheroClone.Datas
{
    public class CurrencyLevelData
    {
        private List<Character> _characters;

        public CurrencyLevelData() 
        { 
            _characters = new List<Character>();
        }

        public List<Character> Characters { get => _characters; }

        public void AddCharacter(Character player)
            => _characters.Add(player);

        public void RemoveCharacter(Character player)
        {
            if (_characters.Contains(player))
                _characters.Remove(player);
        }

    }
}