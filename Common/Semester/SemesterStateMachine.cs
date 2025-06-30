using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemGroup.Framework.StateManagement.ProtoType;

namespace SystemGroup.General.CourseEnrollment.Common
{
    public class SemesterStateMachine : StateMachine<Semester, SemesterStatus>
    {
        protected override void InitializeStates()
        {
            var registering = new State(SemesterStatus.Registering, "Registering", "Labels_Registering");
            var onGoing = new State(SemesterStatus.OnGoing, "OnGoing", "Labels_OnGoing");
            var finished = new State(SemesterStatus.Finished, "Finished", "Labels_Finished");
            States.Add(registering);
            States.Add(onGoing);
            States.Add(finished);
            Transitions.Add(new ManualTransition(registering, onGoing, "Labels_RegisteringToOnGoing"));
            Transitions.Add(new ManualTransition(onGoing, finished, "Labels_OnGoingToFinished"));
        }

        protected override void DoChangeState(Semester record, SemesterStatus state, StateChangeContext context)
        {
            record.State = state;
        }

        protected override SemesterStatus GetCurrentStateCodeAsEnum(Semester record)
        {
            return record.State;
        }
    }
}
