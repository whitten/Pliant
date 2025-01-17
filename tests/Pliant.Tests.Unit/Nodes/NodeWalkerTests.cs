﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pliant.Regex;
using Pliant.Nodes;

namespace Pliant.Tests.Unit.Nodes
{
    [TestClass]
    public class NodeWalkerTests
    {
        [TestMethod]
        public void Test_NodeWalker_That_Walks_Simple_Regex()
        {
            var regexGrammar = new RegexGrammar();
            var regexParseEngine = new ParseEngine(regexGrammar);
            var regexParseInterface = new ParseInterface(regexParseEngine, @"[(]\d[)]");
            while (!regexParseInterface.EndOfStream())
            {
                if (!regexParseInterface.Read())
                    Assert.Fail("error parsing input at position {0}", regexParseInterface.Position);
            }
            Assert.IsTrue(regexParseEngine.IsAccepted());

            var nodeVisitor = new NodeVisitor();
            var root = regexParseEngine.GetRoot();
            root.Accept(nodeVisitor);
            Assert.AreEqual(34, nodeVisitor.VisitLog.Count);           
        }        
    }
}
