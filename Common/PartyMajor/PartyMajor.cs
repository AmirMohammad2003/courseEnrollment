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
using SystemGroup.General.IPartyManagement.Common;

namespace SystemGroup.General.CourseEnrollment.Common
{
    [Serializable]
    [Master(typeof(IPartyMajorBusiness))]
    [DataNature(DataNature.BusinessTransaction)]
    partial class PartyMajor : Entity
    {

        #region Methods

        public override string GetEntityName()
        {
            return "ثبت نام دانشجو در دانشگاه"; 
        }
        public override void GetColumns(List<ColumnInfo> columns)
        {
            base.GetColumns(columns);

            columns.Add(new ReferenceColumnInfo("MajorRef", "_"));
            columns.Add(new ReferenceColumnInfo("PartyRef", "_"));
            columns.Add(new ReferenceColumnInfo("ProfessorPartyRef", "_"));
            columns.Add(new NumericColumnInfo("GPA", "معدل", NumericType.FloatingPoint)); 
        } 

        #endregion
    }
}
