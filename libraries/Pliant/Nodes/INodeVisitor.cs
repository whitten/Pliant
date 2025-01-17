﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pliant.Nodes
{
    public interface INodeVisitor
    {
        void Visit(ITerminalNode node);
        void Visit(ISymbolNode node);
        void Visit(IIntermediateNode node);
        void Visit(ITokenNode tokenNode);
    }
}
