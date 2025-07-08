using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemGroup.Framework.Common;
using SystemGroup.Framework.Service;
using SystemGroup.General.IPartyManagement.Common;

namespace SystemGroup.General.CourseEnrollment.Common
{
    public class PartyMajorProjection : EntityProjection<PartyMajor>
    {
        #region Methods

        public override IQueryable Project(IQueryable<PartyMajor> inputs)
        {

            return from item in inputs
                   join party in ServiceFactory.Create<IPartyManagementService>().FetchParties()
                   on item.PartyRef equals party.ID
                   join major in ServiceFactory.Create<IMajorBusiness>().FetchAll()
                   on item.MajorRef equals major.ID
                   join professorParty in ServiceFactory.Create<IPartyManagementService>().FetchParties()
                   on item.ProfessorPartyRef equals professorParty.ID into professor_jointable
                   from professorParty in professor_jointable.DefaultIfEmpty()
                   select new { 
                       item.ID, 
                       item.GPA,
                       item.PartyRef,
                       item.ProfessorPartyRef,
                       item.MajorRef,
                       party.FullName, 
                       ProfessorName= professorParty.FullName,
                       MajorName= major.Name
                   };

        }
        public override void GetColumns(List<ColumnInfo> columns)
        {
            base.GetColumns(columns);

            columns.Add(new TextColumnInfo("FullName", "Labels_Student"));
            columns.Add(new TextColumnInfo("ProfessorName", "Labels_AssignedProfessor"));
            columns.Add(new TextColumnInfo("MajorName", "Labels_Major"));
            columns.Add(new EntityColumnInfo<PartyMajor>("GPA"));
            columns.Add(new EntityColumnInfo<PartyMajor>("PartyRef"));
            columns.Add(new EntityColumnInfo<PartyMajor>("ProfessorPartyRef"));
            columns.Add(new EntityColumnInfo<PartyMajor>("MajorRef"));
        }

        #endregion
    }
}
