﻿using Pliant.Grammars;
using Pliant.Tokens;

namespace Pliant.Lexemes
{
    public class TerminalLexeme : ILexeme
    {
        public ITerminal Terminal { get; private set; }

        public string Capture { get; private set; }

        public TokenType TokenType { get; private set; }

        public TerminalLexeme(ITerminalLexerRule lexerRule)
            : this(lexerRule.Terminal, lexerRule.TokenType)
        {
        }

        public TerminalLexeme(ITerminal terminal, TokenType tokenType)
        {
            Terminal = terminal;
            TokenType = tokenType;
            Capture = string.Empty;
        }

        public bool IsAccepted()
        {
            return Capture.Length > 0;
        }

        public bool Scan(char c)
        {
            if (!IsAccepted())
            {
                if (Terminal.IsMatch(c))
                {
                    Capture = c.ToString();
                    return true;
                }
            }
            return false;
        }
    }
}
