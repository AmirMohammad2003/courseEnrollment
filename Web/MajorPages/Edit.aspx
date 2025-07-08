<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" 
    Inherits="SystemGroup.General.CourseEnrollment.Web.MajorPages.Edit" 
    Title="Labels_Major"%>

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
                                    <sg:sgfieldlabel runat="server" textkey="Labels_Name" required="true"></sg:sgfieldlabel>
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgtextbox runat="server" id="txtName" width="100px"></sg:sgtextbox>
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgrequiredfieldvalidator runat="server" controltovalidate="txtName" 
                                        errormessagekey="Messages_ChooseMajor"></sg:sgrequiredfieldvalidator>
                                </sg:sgtablecell>
                            </sg:sgtablerow>
                            <sg:sgtablerow>
                                <sg:sgtablecell>
                                    <sg:sgfieldlabel runat="server" textkey="Labels_Units" required="true"></sg:sgfieldlabel>
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgnumerictextbox runat="server" id="txtUnits" value="1" minvalue="0" maxvalue="999" 
                                        width="100px" numberformat-decimaldigits="0" datatype="System.Int" />
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgrequiredfieldvalidator runat="server" controltovalidate="txtUnits" 
                                        errormessagekey="Messages_EnterUnits"></sg:sgrequiredfieldvalidator>
                                </sg:sgtablecell>
                            </sg:sgtablerow>
                        </sg:sgfieldlayout>
                    </sg:sgfieldset>
					<sg:SgGrid runat="server" ID="grdCourses" GridType="ClientSide" AllowScroll="true"
						AllowEdit="true" AllowDelete="true" AllowInsert="true" 
						DataSourceID=".MajorCourses" ValidationGroup="vgGrid" >
                        <Columns>
							<sg:SgSelectorGridColumn PropertyName="CourseName" HeaderText="Labels_Course">
								<EditItemTemplate>
									<sg:SgSelector runat="server" ID="sltCourse"
										ComponentName="SystemGroup.General.CourseEnrollment"
										EntityName="Course" ViewName="AllCourse" 
                                        CbSelectedID="{binding CourseRef}" 
                                        OnClientSelectedIndexChanged="sltCourse_selectedIndexChanged"
                                        OnClientItemsRequesting="sltCourse_itemsRequesting"
                                        OnItemsRequested="slt_ItemsRequested">
                                        <Properties>
                                            <sg:SgSelectorProperty Name="Name" ClientSide="true"/>
                                            <sg:SgSelectorProperty Name="Units" ClientSide="true"/>
                                        </Properties>
									</sg:SgSelector>
                                    <sg:SgRequiredFieldValidator runat="server" ControlToValidate="sltCourse"
                                        errormessagekey="Messages_ChooseCourse" ValidationGroup="vgGrid"/>
								</EditItemTemplate>
							</sg:SgSelectorGridColumn>
                            <sg:SgTextGridColumn PropertyName="CourseUnits" HeaderText="Labels_Units" AllowEdit="false"/>
                        </Columns>
                    </sg:SgGrid>
                </div>
            </ContentTemplate>
        </sg:SgUpdatePanel>
    </form>
</body>
</html>
