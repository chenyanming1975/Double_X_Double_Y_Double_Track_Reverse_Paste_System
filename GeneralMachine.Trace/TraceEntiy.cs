using GeneralMachine.Config;
using Stateless;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMachine.Trace
{
    public class TraceEntiy
    {
        public TraceEntiy(ConveryConfig config, TraceIO state)
        {
            this.TraceIO = state;
            this.Config = config;
        }
        /// <summary>
        /// 状态
        /// </summary>
        public enum State
        {
            Idle,
            WaitInput,
            Input,
            Output,
            WaitOuput,
        }

        /// <summary>
        /// 触发
        /// </summary>
        public enum Trigger
        {
            Start,
            Pause,
            CheckIO,
        }

        /// <summary>
        /// 状态机
        /// </summary>
        private StateMachine<State, Trigger> stateMachine = new StateMachine<State, Trigger>(State.Idle, FiringMode.Immediate);

        public ConveryConfig Config = null;

        public TraceIO TraceIO = null;
    }
}
