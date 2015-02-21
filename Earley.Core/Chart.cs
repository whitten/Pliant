﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Earley
{
    public class Chart
    {
        private IGrammar _grammar;
        private IDictionary<int, List<IState>> _body;
        
        public Chart(IGrammar grammar)
        {
            Assert.IsNotNull(grammar, "grammar");
            _grammar = grammar;
            _body = new Dictionary<int, List<IState>>();
        }

        public void EnqueueAt(int index, IState state)
        {
            if (!_body.ContainsKey(index))
                _body.Add(index, new List<IState>());
            var list = _body[index];
            if (list.Any(x => x.Equals(state)))
                return;
            list.Add(state);
        }
        
        public IReadOnlyList<IState> this[int index]
        {
            get
            {
                if(index < _body.Count )
                    return new ReadOnlyList<IState>(_body[index]);
                return new ReadOnlyList<IState>(new List<IState>());
            }
        }

        public int Count
        {
            get { return _body.Count; }
        }
    }
}
