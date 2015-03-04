﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pliant
{
    public class WhitespaceTerminal : ITerminal
    {
        public bool IsMatch(char character)
        {
            return char.IsWhiteSpace(character);
        }

        public SymbolType SymbolType
        {
            get { return SymbolType.Terminal; }
        }

        public override string ToString()
        {
            return @"\s";
        }
    }
}