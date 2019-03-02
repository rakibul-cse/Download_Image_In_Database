<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="TestImage._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
     <asp:Button ID="databaseToFileButton" runat="server" OnClick="databaseToFileButton_Click" Text="Database To File" />
    <br />
    <br />
    <br />
    <br />
    <asp:Label ID="messageLabel" runat="server"></asp:Label>
</asp:Content>
