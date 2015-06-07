﻿using Pliant.Terminals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pliant
{
    public class TerminalLexerRule : ILexerRule
    {
        public IGrammar Grammar { get; private set; }

        public SymbolType SymbolType { get { return SymbolType.LexerRule; } }

        public TokenType TokenType { get; private set; }

        public TerminalLexerRule(ITerminal terminal, TokenType tokenType)
        {
            Grammar = CreateGrammarForTerminal(terminal);
            TokenType = tokenType;
        }

        private IGrammar CreateGrammarForTerminal(ITerminal terminal)
        {
            var nonTerminal = new NonTerminal(terminal.ToString());
            var production = new Production(
                nonTerminal,
                terminal);
            var grammar = new Grammar(
                nonTerminal,
                new IProduction[] { production },
                new ILexerRule[] { },
                new ILexerRule[] { });
            return grammar;
        }
    }
}
