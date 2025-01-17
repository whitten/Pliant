﻿using System;
using Pliant.Lexemes;
using Pliant.Tokens;
using System.Text;

namespace Pliant.Dfa
{
    public class DfaLexeme : ILexeme
    {
        private StringBuilder _capture;
        private IDfaState _currentState;

        public DfaLexeme(IDfaState dfaState, TokenType tokenType)
        {
            _capture = new StringBuilder();
            _currentState = dfaState;
            TokenType = tokenType;
        }

        public string Capture
        {
            get { return _capture.ToString(); }
        }

        public TokenType TokenType { get; private set; }

        public bool IsAccepted()
        {
            return _currentState.IsFinal;
        }

        public bool Scan(char c)
        {
            foreach (var edge in _currentState.Edges)
            {
                if (edge.Terminal.IsMatch(c))
                {
                    _currentState = edge.Target;
                    _capture.Append(c);
                    return true;
                }
            }
            return false;
        }
    }
}
