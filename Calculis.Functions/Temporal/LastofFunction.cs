﻿using System;
using System.Collections.Generic;
using System.Linq;
using Calculis.Core;

namespace Calculis.Functions
{
    //Parameters:
    //1 - interval id: 0 - munute; 1 - hour;
    //
    public class LastofFunction : TemporalFunction
    {
        private readonly int _interval;
        private readonly int _number;


        public LastofFunction(IList<IValueItem> args) : base(args)
        {
            _interval = (int)args[1].Value;
            _number = (int)args[2].Value;

            Name = "LASTOF";
            Function = () =>
            {
                if (_isInitialized)
                {
                    var segmentNumber = ((int)(Timestamp - Timestamp.Date).TotalSeconds) / _interval;
                    var end = Timestamp.Date.AddSeconds((segmentNumber - _number + 1) * _interval);
                    var selected = _cash.Where(x => x.Timestamp < end)?.OrderByDescending(x => x.Timestamp).FirstOrDefault();

                    return selected?.Value ?? 0;
                }

                return 0;
            };
        }

        protected override void Initialize()
        {
            InitializeCash((int)(_args[1].Value * _args[2].Value) + 1);
        }
    }
}
