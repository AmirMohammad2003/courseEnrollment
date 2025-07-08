using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SystemGroup.Framework.Business;
using SystemGroup.Framework.Common;
using SystemGroup.Framework.Lookup;
using SystemGroup.Framework.MetaData;
using SystemGroup.Framework.MetaData.Mapping;
using SystemGroup.Framework.Service;
using SystemGroup.Framework.StateManagement;

namespace SystemGroup.General.CourseEnrollment.Common
{
    [Serializable]
    [Master(typeof(ISemesterCoursePlanBusiness))]
    [DataNature(DataNature.BusinessTransaction)]
    partial class SemesterCoursePlan : Entity
    {

        #region Methods

        public override string GetEntityName()
        {
            return "Labels_SemesterCoursePlan"; 
        }
        public override void GetColumns(List<ColumnInfo> columns)
        {
            base.GetColumns(columns);

            columns.Add(new ReferenceColumnInfo("SemesterRef", "_"));
            columns.Add(new ReferenceColumnInfo("MajorRef", "_"));
        }

        #endregion
    }
}
