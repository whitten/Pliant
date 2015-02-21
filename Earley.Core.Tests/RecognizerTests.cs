﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.IO;

namespace Earley.Core.Tests
{
    [TestClass]
    public class RecognizerTests
    {
        // S -> S + M | M
        // M -> M * T | T
        // T -> 1 | 2 | 3 | 4
        private readonly Grammar expressionGrammar = new Grammar(
                new Production("S", new NonTerminal("S"), new Terminal("+"), new NonTerminal("M")),
                new Production("S", new NonTerminal("M")),
                new Production("M", new NonTerminal("M"), new Terminal("*"), new NonTerminal("T")),
                new Production("M", new NonTerminal("T")),
                new Production("T", new Terminal("1")),
                new Production("T", new Terminal("2")),
                new Production("T", new Terminal("3")),
                new Production("T", new Terminal("4")));

        // A -> B C
        // B -> b
        // C -> c
        private readonly Grammar abcGrammar = new Grammar(
            new Production(
                "A",
                new NonTerminal("B"),
                new NonTerminal("C")),
            new Production(
                "B",
                new Terminal("b")),
            new Production(
                "C",
                new Terminal("c")));

        // A -> Aa
        // A -> 
        private readonly Grammar simpleRightRecursive = new Grammar(
            new Production(
                "A",
                new Terminal("a"),
                new NonTerminal("A")),
            new Production(
                "A"));

        [TestMethod]
        public void Test_Recognizer_That_Scan_Moves_Items_To_Next_Tree()
        {
            var recognizer = new Recognizer(expressionGrammar);
            var privateObject = new PrivateObject(recognizer);
            var scanState = new State(expressionGrammar.Productions[5], 0, 0);
            var chart = new Chart(expressionGrammar);
            for (int i = 0; i < expressionGrammar.Productions.Count;i++)
                chart.EnqueueAt(0, new State(expressionGrammar.Productions[i], 0, 0));
            var j = 0;
            privateObject.Invoke("Scan", scanState, j, chart, '2');
            Assert.AreEqual(1, chart[1].Count);
        }

        [TestMethod]
        public void Test_Recognizer_That_Complete_Only_Adds_States_Related_To_Completed_State()
        {
            var recognizer = new Recognizer(expressionGrammar);
            var privateObject = new PrivateObject(recognizer);
            var completeState = new State(expressionGrammar.Productions[5], 1, 0);
            var chart = new Chart(expressionGrammar);
            for (int i = 0; i < expressionGrammar.Productions.Count; i++)
                chart.EnqueueAt(0, new State(expressionGrammar.Productions[i], 0, 0));
            chart.EnqueueAt(1, completeState);
            var k = 1;
            privateObject.Invoke("Complete", completeState, k, chart);
            Assert.AreEqual(2, chart.Count);
            Assert.AreEqual(2, chart[1].Count);
        }

        [TestMethod]
        public void Test_Recognizer_That_A_B_C_Grammar_Parses_bc()
        {            
            var recognizer = new Recognizer(abcGrammar);
            var stringReader = new StringReader("bc");
            var chart = recognizer.Parse(stringReader);
            Assert.IsNotNull(chart);
            Assert.AreEqual(3, chart.Count);
        }

        [TestMethod]
        public void Test_Recognizer_That_S_M_T_Grammar_Parses_2_sum_3_mul_4()
        {
            var recognizer = new Recognizer(expressionGrammar);
            var stringReader = new StringReader("2+3*4");
            var chart = recognizer.Parse(stringReader);
            Assert.IsNotNull(chart);
            Assert.AreEqual(6, chart.Count);
        }

        [TestMethod]
        public void Test_Recognizer_That_Right_Recursion_Is_Not_O_N_3()
        {
            var grammar = simpleRightRecursive;
            var recognizer = new Recognizer(grammar);
            var stringReader = new StringReader("aaaaa");
            var chart = recognizer.Parse(stringReader);

            var matchState = new State(
                new Production("A", 
                    new Terminal("a"), 
                    new NonTerminal("A")),
                    2, 4);

            // we know the leo completion is working if the 
            // 4th index contains one state that matches the 
            // match state.
            // also there should be leo items in the chart for 1 - 5
            var matchCount = 0;
            for (var s = 0; s < chart[4].Count; s++)
            {
                var state = chart[4][s];
                if(matchState.StateType == state.StateType)
                    if(matchState.Production.Equals(state.Production))
                        if(matchState.Position == state.Position)
                            matchCount++;
            }
            Assert.AreEqual(1, matchCount);
        }

        [TestMethod]
        public void Test_Recognizer_That_Invalid_Input_Exists_Parse()
        {
            var recognizer = new Recognizer(expressionGrammar);
            var chart = recognizer.Parse(new StringReader("1+b*3"));
            Assert.IsNotNull(chart);
        }
    }
}
