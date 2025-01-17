﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pliant.Grammars
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

        const string ToStringValue = @"\s";

        public override string ToString()
        {
            return ToStringValue;
        }

        public override bool Equals(object obj)
        {
            return obj is WhitespaceTerminal;
        }

        public override int GetHashCode()
        {
            return ToStringValue.GetHashCode();
        }
    }
}
