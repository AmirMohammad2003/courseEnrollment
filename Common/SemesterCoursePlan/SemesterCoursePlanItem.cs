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
    [DetailOf(typeof(SemesterCoursePlan), "SemesterCoursePlanRef")]
    [AssociatedWith(typeof(Course), "CourseRef", AssociationType.ManyToOne)]
    partial class SemesterCoursePlanItem : Entity
    {
        #region Properties
        
        public string CourseName { get; set; }
        public string PartyName { get; set; }

        #endregion

        #region Methods

        public override void SetDefaultValues()
        {
            base.SetDefaultValues();

            this.Taken = 0;
            this.Capacity = 0;
        }

        public override string GetEntityName()
        {
            return "درس جاری ترم"; 
        }

        public override void GetColumns(List<ColumnInfo> columns)
        {
            base.GetColumns(columns);

            columns.Add(new ReferenceColumnInfo("SemesterCoursePlanRef", "_"));
            columns.Add(new ReferenceColumnInfo("PartyRef", "_"));
            columns.Add(new ReferenceColumnInfo("CourseRef", "_"));
            columns.Add(new NumericColumnInfo("Capacity", "ظرفیت", NumericType.Integer));
            columns.Add(new NumericColumnInfo("Taken", "اخذ شده", NumericType.Integer));
        }

        public static void FillExtraProperties(IList<SemesterCoursePlanItem> list)
        {
            if (list == null || list.Count == 0)
            {
                return;
            }
            var items = (from item in list
            join course in ServiceFactory.Create<ICourseBusiness>().FetchAll() 
            on item.CourseRef equals course.ID
            join party in ServiceFactory.Create<IPartyManagementService>().FetchParties() 
            on item.PartyRef equals party.ID
            select new { item.ID, CourseName= course.Name, PartyName= party.FullName }).Distinct().ToDictionary(i => i.ID);

            foreach (var item in list)
            {
                item.CourseName = items[item.ID].CourseName;
                item.PartyName = items[item.ID].PartyName;
            }
        }
        #endregion
    }
}
