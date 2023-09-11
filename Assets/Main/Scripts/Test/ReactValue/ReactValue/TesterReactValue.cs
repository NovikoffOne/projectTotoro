using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

    public class TesterReactValue<T> : ReactValue<T>, IReactValue<T>
    {
        public TesterReactValue(T value) : base(value)
        {
        }
    }
