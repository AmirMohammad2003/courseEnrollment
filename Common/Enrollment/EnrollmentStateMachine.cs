using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemGroup.Framework.Security;
using SystemGroup.Framework.StateManagement.ProtoType;

namespace SystemGroup.General.CourseEnrollment.Common
{
    public class EnrollmentStateMachine : StateMachine<Enrollment, EnrollmentStatus>
    {
        protected override void InitializeStates()
        {
            var registering = new State(EnrollmentStatus.Registering, "Registering", "Labels_Registering");
            var waitingForApproval = new State(EnrollmentStatus.WaitingForApproval, "WaitingForApproval", "Labels_WaitingForApproval");
            var approved = new State(EnrollmentStatus.Approved, "Approved", "Labels_Approved");

            States.Add(registering);
            States.Add(waitingForApproval);
            States.Add(approved);

            Transitions.Add(new ManualTransition(registering, waitingForApproval, "Labels_RegisteringToWaitingForApproval",
                securityKey: "CourseEnrollment.Enrollment.Edit | CourseEnrollment.Enrollment.New"));
            Transitions.Add(new ManualTransition(waitingForApproval, approved, "Labels_WaitingForApprovalToApproved",
                securityKey: "CourseEnrollment.Enrollment.Approval"));
            Transitions.Add(new ManualTransition(waitingForApproval, registering, "Labels_WaitingForApprovalToRegistering",
                needsConfirmation: true, needsNotes: true, securityKey: "CourseEnrollment.Enrollment.Approval"));
        }

        protected override void DoChangeState(Enrollment record, EnrollmentStatus state, StateChangeContext context)
        {
            record.State = state;
        }

        protected override EnrollmentStatus GetCurrentStateCodeAsEnum(Enrollment record)
        {
            return record.State;
        }
    }
}
