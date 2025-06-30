<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" 
    Inherits="SystemGroup.General.CourseEnrollment.Web.CoursePages.Edit" 
    Title="درس" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <sg:sgscriptmanager runat="server" id="scriptManager">
            <Scripts>
                <asp:ScriptReference Path="Edit.js" />
            </Scripts>
        </sg:sgscriptmanager>
        <sg:sgupdatepanel runat="server" id="updMain">
            <contenttemplate>
                <div runat="server" id="dvMain">
                    <sg:sgfieldset id="fldsetMain" runat="server">
                        <sg:sgfieldlayout runat="server">
                            <sg:sgtablerow>
                                <sg:sgtablecell>
                                    <sg:sgfieldlabel runat="server" text="نام درس" required="true"></sg:sgfieldlabel>
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgtextbox runat="server" id="txtName" width="135px" ></sg:sgtextbox>
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgrequiredfieldvalidator runat="server" controltovalidate="txtName" errormessage="نام درس را وارد کنید."></sg:sgrequiredfieldvalidator>
                                </sg:sgtablecell>
                            </sg:sgtablerow>
                            <sg:sgtablerow>
                                <sg:sgtablecell>
                                    <sg:sgfieldlabel runat="server" text="تعداد واحد" required="true"></sg:sgfieldlabel>
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgnumerictextbox runat="server" id="txtUnits" value="1" minvalue="0" maxvalue="99"
                                        numberformat-decimaldigits="0" width="135px" datatype="System.Int" />
                                </sg:sgtablecell>
                                <sg:sgtablecell>
                                    <sg:sgrequiredfieldvalidator runat="server" controltovalidate="txtUnits" errormessage="تعداد واحد ها را وارد کنید."></sg:sgrequiredfieldvalidator>
                                </sg:sgtablecell>
                            </sg:sgtablerow>
                        </sg:sgfieldlayout>
                    </sg:sgfieldset>

					<sg:SgGrid runat="server" ID="grdPrerequisites" GridType="ClientSide" AllowScroll="true"
						AllowEdit="true" AllowDelete="true" AllowInsert="true"
						DataSourceID=".Prerequisites" ValidationGroup="vgGrid" >
                        <Columns>
							<sg:SgSelectorGridColumn PropertyName="CourseName" HeaderText="پیشنیاز">
								<EditItemTemplate>
									<sg:SgSelector runat="server" ID="sltCourse"
										ComponentName="SystemGroup.General.CourseEnrollment"
										EntityName="Course" ViewName="AllCourse" Width="128px"
                                        CbSelectedID="{binding PrerequisiteCourseRef}" 
                                        OnClientSelectedIndexChanged="sltCourse_selectedIndexChanged"
                                        OnClientItemsRequesting="sltCourse_itemsRequesting"
                                        OnItemsRequested="sltCourse_ItemsRequested">
                                        <Properties>
                                            <sg:SgSelectorProperty Name="Name" ClientSide="true"/>
                                        </Properties>
									</sg:SgSelector>
                                    <sg:SgRequiredFieldValidator runat="server" ControlToValidate="sltCourse"
                                        errormessage="پیشنیاز را انتخاب کنید." ValidationGroup="vgGrid"/>
								</EditItemTemplate>
							</sg:SgSelectorGridColumn>
                        </Columns>
                    </sg:SgGrid>
                </div>
            </contenttemplate>
        </sg:sgupdatepanel>
    </form>
</body>
</html>
