﻿using Pliant.Grammars;
using Pliant.Nodes;

namespace Pliant.Charts
{
    public interface IState
    {
        IProduction Production { get; }
        int Origin { get; }
        StateType StateType { get; }
        ISymbol PreDotSymbol { get; }
        ISymbol PostDotSymbol { get; }
        int Length { get; }
        bool IsComplete { get; }
        IState NextState();
        IState NextState(int newOrigin);
        IState NextState(INode parseNode);
        IState NextState(int newOrigin, INode parseNode);
        INode ParseNode { get; set; }
        bool IsSource(ISymbol searchSymbol);
    }
}
