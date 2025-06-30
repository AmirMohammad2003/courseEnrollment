namespace SystemGroup.General.CourseEnrollment.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using SystemGroup.Framework;
    
    
    // Do not CHANGE this class name or  its content.
    public partial class ComponentMessageContainer : MessageContainer
    {
        
        #region Fields
        private static ComponentMessageContainer instance = new ComponentMessageContainer();
        #endregion
        
        #region Constructors
        static ComponentMessageContainer()
        {
            ComponentMessageContainer.instance.MessageCollection.Add(new SgMessage("SystemGroup.General.CourseEnrollment", "MajorSemesterCoursePlanUniquenessCheck", "_"));
            ComponentMessageContainer.instance.ExceptionMappingCollection.Add(new ExceptionMapping("MajorSemesterCoursePlanUniquenessCheck", 2147483647, new SgKeyword("UQ_SemesterCoursePlan_SemesterRef_MajorRef", SgKeywordType.Default), new SgKeyword("UNIQUE KEY", SgKeywordType.Default)));
        }
        #endregion
        
        #region Properties
        public static SgMessage MajorSemesterCoursePlanUniquenessCheck
        {
            get
            {
                return ComponentMessageContainer.FindMessage(ComponentMessageContainer.instance.MessageCollection, "MajorSemesterCoursePlanUniquenessCheck");
            }
        }
        #endregion
        
        #region Methods
        public override MessageContainer GetSingleton()
        {
            return ComponentMessageContainer.instance;
        }
        #endregion
    }
}
