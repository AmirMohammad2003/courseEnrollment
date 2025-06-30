<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs"
    Inherits="SystemGroup.General.CourseEnrollment.Web.SemesterCoursePlanPages.Edit" 
    Title="برنامه تحصیلی"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <sg:SgScriptManager runat="server" ID="scriptManager">
            <Scripts>
                <asp:ScriptReference Path="Edit.js" />
            </Scripts>
        </sg:SgScriptManager>
        <sg:SgUpdatePanel runat="server" ID="updMain">
            <ContentTemplate>
                <div runat="server" id="dvMain">
                    <sg:sgfieldset id="fldsetMain" runat="server">
                        <sg:sgfieldlayout runat="server" LabelCellWidth="130px">
                            <sg:sgtablerow>
                                <sg:sgtablecell>
                                    <sg:sgfieldlabel runat="server" text="ترم" required="true"></sg:sgfieldlabel>
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgselector runat="server" id="sltSemester"
                                        componentname="SystemGroup.General.CourseEnrollment"
                                        entityname="Semester" viewname="AllSemester" >
                                    </sg:sgselector>
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgrequiredfieldvalidator runat="server" controltovalidate="sltSemester" 
                                        errormessage="تعداد واحد ها را وارد کنید."></sg:sgrequiredfieldvalidator>
                                </sg:sgtablecell>
                            </sg:sgtablerow>
                            <sg:sgtablerow>
                                <sg:sgtablecell>
                                    <sg:sgfieldlabel runat="server" text="رشته" required="true"></sg:sgfieldlabel>
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgselector runat="server" id="sltMajor"
                                        componentname="SystemGroup.General.CourseEnrollment"
                                        entityname="Major" viewname="AllMajor" >
                                    </sg:sgselector>
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgrequiredfieldvalidator runat="server" controltovalidate="sltMajor" 
                                        errormessage="رشته را انتخاب کنید."></sg:sgrequiredfieldvalidator>
                                </sg:sgtablecell>
                            </sg:sgtablerow>
                        </sg:sgfieldlayout>
                    </sg:sgfieldset>
                    <sg:sggrid runat="server" id="grdCourses" gridtype="ClientSide" allowscroll="true"
                        allowedit="true" allowdelete="true" allowinsert="true"
                        datasourceid=".SemesterCoursePlanItems" validationgroup="vgGrid">
                        <Columns>
                            <sg:SgSelectorGridColumn PropertyName="CourseName" HeaderText="درس">
                                <EditItemTemplate>
                                    <sg:SgSelector runat="server" ID="sltCourse"
                                        ComponentName="SystemGroup.General.CourseEnrollment"
                                        EntityName="Course" ViewName="AllCourse" 
                                        CbSelectedID="{binding CourseRef}" 
                                        OnClientSelectedIndexChanged="sltCourse_selectedIndexChanged">
                                        <Properties>
                                            <sg:SgSelectorProperty Name="Name" ClientSide="true"/>
                                        </Properties>
                                    </sg:SgSelector>
                                    <sg:SgRequiredFieldValidator runat="server" ControlToValidate="sltCourse"
                                        errormessage="درس را انتخاب کنید." ValidationGroup="vgGrid"/>
                                </EditItemTemplate>
                            </sg:SgSelectorGridColumn>
                            <sg:SgSelectorGridColumn PropertyName="PartyName" HeaderText="استاد">
                                <EditItemTemplate>
                                    <sg:SgSelector runat="server" ID="sltParty"
                                        ComponentName="SystemGroup.General.PartyManagement"
                                        EntityName="Party" ViewName="AllParties" 
                                        CbSelectedID="{binding PartyRef}"
                                        OnClientSelectedIndexChanged="sltParty_selectedIndexChanged">
                                        <Properties>
                                            <sg:SgSelectorProperty Name="FullName" ClientSide="true"/>
                                        </Properties>
                                    </sg:SgSelector>
                                    <sg:SgRequiredFieldValidator runat="server" ControlToValidate="sltCourse"
                                        errormessage="درس را انتخاب کنید." ValidationGroup="vgGrid"/>
                                </EditItemTemplate>
                            </sg:SgSelectorGridColumn>
                            <sg:SgNumericGridColumn PropertyName="Capacity" HeaderText="ظرفیت">
                                <EditItemTemplate>
                                    <sg:sgnumerictextbox runat="server" id="txtCapacity" value="1" minvalue="0" maxvalue="9999999"
                                        numberformat-decimaldigits="0" datatype="System.Int" CbValue="{binding Capacity}" />
                                </EditItemTemplate>
                            </sg:SgNumericGridColumn>
                            <sg:SgTextGridColumn PropertyName="Taken" HeaderText="اخذ شده" AllowEdit="false" />
                        </Columns>
                    </sg:SgGrid>
                </div>
            </ContentTemplate>
        </sg:SgUpdatePanel>
    </form>
</body>
</html>
