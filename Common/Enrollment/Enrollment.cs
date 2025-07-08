using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SystemGroup.Framework.Business;
using SystemGroup.Framework.Common;
using SystemGroup.Framework.Exceptions;
using SystemGroup.Framework.Localization;
using SystemGroup.Framework.Logging;
using SystemGroup.Framework.Lookup;
using SystemGroup.Framework.MetaData;
using SystemGroup.Framework.MetaData.Mapping;
using SystemGroup.Framework.Service;
using SystemGroup.Framework.StateManagement;


namespace SystemGroup.General.CourseEnrollment.Common
{
    [Serializable]
    [Master(typeof(IEnrollmentBusiness))]
    [DataNature(DataNature.BusinessTransaction)]
    partial class Enrollment : Entity, ITrackedEntity
    {
        #region Properties 
        #endregion

        #region Methods

        public override void SetDefaultValues()
        {
            base.SetDefaultValues();

            this.State = EnrollmentStatus.Registering;

            var auth = ServiceFactory.GetAuthenticationService();
            var user = auth.GetCurrentUser() ?? throw new SgException("You should be logged in to use this service.");
            if (user.PartyRef == null)
            {
                throw new SgException("User should be associated with a 'Party' to Enroll.");
            }
            this.PartyRef = (long)user.PartyRef;
        }

        public override string GetEntityName()
        {
            return "Labels_Enrollment"; 
        }

        public override void GetColumns(List<ColumnInfo> columns)
        {
            base.GetColumns(columns);

            columns.Add(new ReferenceColumnInfo("SemesterCoursePlanRef", "_"));
            columns.Add(new ReferenceColumnInfo("PartyRef", "_"));
            columns.Add(new NumericColumnInfo("GPA", "Labels_GPA", NumericType.FloatingPoint));
            columns.Add(new StateColumnInfo("State", "Labels_EnrollmentStatus", typeof(Enrollment)));
        }

        #endregion
    }
}
