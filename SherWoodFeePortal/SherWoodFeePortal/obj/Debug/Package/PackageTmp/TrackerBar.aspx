<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrackerBar.aspx.cs" Inherits="SherWoodFeePortal.ProgressBarTracker.TrackerBar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/ProgressTracker.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:BulletedList CssClass="progtrckr" ID="ProgressBarTracker1" runat="server" BulletStyle="Numbered">
        </asp:BulletedList>
    </div>

     <asp:Button ID ="btnSubmit" runat="server" Text="Submit" 
        onclick="btnSubmit_Click" />

    </form>
</body>
</html>
