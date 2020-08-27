<%@ Page Language="vb" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="_ASPxTreeList.Default" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v13.2, Version=13.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v13.2, Version=13.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
	<form id="form1" runat="server">
		<div>
			<dx:ASPxTreeList ID="TreeList" runat="server" ClientInstanceName="treeList" AutoGenerateColumns="False"
				DataSourceID="DataSource" KeyFieldName="EmployeeID" ParentFieldName="ReportsTo">
				<Columns>
					<dx:TreeListCommandColumn VisibleIndex="0">
						<EditButton Visible="True">
						</EditButton>
						<NewButton Visible="True">
						</NewButton>
						<DeleteButton Visible="True">
						</DeleteButton>
					</dx:TreeListCommandColumn>
					<dx:TreeListTextColumn FieldName="FirstName" VisibleIndex="1">
					</dx:TreeListTextColumn>
					<dx:TreeListTextColumn FieldName="LastName" VisibleIndex="2">
					</dx:TreeListTextColumn>
					<dx:TreeListDateTimeColumn FieldName="HireDate" VisibleIndex="3">
					</dx:TreeListDateTimeColumn>
					<dx:TreeListTextColumn FieldName="ReportsTo" VisibleIndex="4">
					</dx:TreeListTextColumn>
				</Columns>
				<SettingsBehavior AutoExpandAllNodes="true" />
			</dx:ASPxTreeList>
			<dx:ASPxTreeListExporter ID="Exporter" runat="server"></dx:ASPxTreeListExporter>

			<dx:ASPxButton ID="Button" runat="server" OnClick="Button_Click" Text="Export"></dx:ASPxButton>

			<asp:AccessDataSource ID="DataSource" runat="server"
				DataFile="~/App_Data/nwind.mdb"
				DeleteCommand="DELETE FROM [Employees] WHERE [EmployeeID] = ?"
				InsertCommand="INSERT INTO [Employees] ([FirstName], [LastName], [HireDate], [ReportsTo]) VALUES (?, ?, ?, ?)"
				SelectCommand="SELECT [EmployeeID], [FirstName], [LastName], [HireDate], [ReportsTo] FROM [Employees]"
				UpdateCommand="UPDATE [Employees] SET [FirstName] = ?, [LastName] = ?, [HireDate] = ?, [ReportsTo] = ? WHERE [EmployeeID] = ?">
				<DeleteParameters>
					<asp:Parameter Name="EmployeeID" Type="Int32" />
				</DeleteParameters>
				<InsertParameters>
					<asp:Parameter Name="FirstName" Type="String" />
					<asp:Parameter Name="LastName" Type="String" />
					<asp:Parameter Name="HireDate" Type="DateTime" />
					<asp:Parameter Name="ReportsTo" Type="Int32" />
				</InsertParameters>
				<UpdateParameters>
					<asp:Parameter Name="FirstName" Type="String" />
					<asp:Parameter Name="LastName" Type="String" />
					<asp:Parameter Name="HireDate" Type="DateTime" />
					<asp:Parameter Name="ReportsTo" Type="Int32" />
					<asp:Parameter Name="EmployeeID" Type="Int32" />
				</UpdateParameters>
			</asp:AccessDataSource>
		</div>
	</form>
</body>
</html>